
using System.Text;
using static ArchCorpUtilities.Utilities.CodeGenHelper;
using L = Logger.Logger;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

    public abstract class CodePart
    {
        public string BaseFolder { get; }
        public string TargetFile { get; }
        public string Entity { get; }
        public string SearchString { get; }
        public static string? WorkingFolder { get; set; }
        public string Heading { get; }
        public string Tabs { get; }
        public static string? SessionID { get; set; }
        public static string? LHLink { get; set; }
        public static string? RHLink { get; set; }

        public CodePart(string baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null)
        {
            BaseFolder = baseFolder;
            TargetFile = targetFile;
            Entity = entity;
            SearchString = searchString;
            WorkingFolder = workingFolder;
            Heading = heading;
            Tabs = searchStringPostPart;
            SessionID = sessionID;
            LHLink = lHLink;
            RHLink = rHLink;
        }
        public virtual bool AlterCode()
        {
            var TargetPath = $"{WorkingFolder}{BaseFolder}\\{TargetFile}";

            try
            {
                if (File.Exists(TargetPath))
                {
                    var Context = File.ReadAllText(TargetPath);
                    if (string.IsNullOrWhiteSpace(Context))
                        return false;
                    else
                    {
                        var SelectedContext = Context.Split($"//{SearchString}");
                        if (SelectedContext.Length is > 0 and 3)
                        {
                            var CodeToAlter = SelectedContext[1];
                            if (!CodeToAlter.Contains(Entity))
                            {
                                string AlteredCode = ModifyCode(CodeToAlter);
                                if (AlteredCode != "<NoAction>")
                                {
                                    string AlteredFile = ConcatinateCode(SelectedContext, AlteredCode);
                                    File.WriteAllText(TargetPath, AlteredFile);
                                }
                                else
                                {
                                    CH.Feedback($"Target file not altered.         - {Entity} - {SearchString} - {TargetFile}.");
                                }
                            }
                            else
                            {
                                CH.Feedback($"Target file not altered.         - {Entity} - {SearchString} - {TargetFile}.");
                            }
                        }
                        else
                            if (SessionID != null)
                            L.Log($"Resource.CouldNotLocateTheSearchKey - {SearchString}", SessionID, 9);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal virtual string ModifyCode(string CodeToAlter)
        {
            throw new NotImplementedException();
        }

        private string ConcatinateCode(string[] SelectedContext, string codeAltered)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Clear();

            if (Heading.Length > 0)
                stringBuilder.Append(Heading);

            stringBuilder.Append(SelectedContext[0]);
            stringBuilder.AppendLine($"//{SearchString}");
            stringBuilder.Append(codeAltered);
            stringBuilder.AppendLine($"{Tabs}//{SearchString}");
            stringBuilder.Append(SelectedContext[2].AsSpan(2)); // remove the linefeed
            var AlteredFile = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredFile;
        }

        public static bool CreateCode(CodeTemplateEnum codeTemplate, string entity, string basePath, string? lHLinkGuid = null, string? rhLinkGuid = null)
        {
            var TemplateCode = GenCode(codeTemplate, entity, lHLinkGuid, rhLinkGuid);
            if (TemplateCode != "<NoData>")
            {
                var EntityFile = GetEntityPath(codeTemplate, entity, basePath);

                if (File.Exists(EntityFile))
                    File.Delete(EntityFile);

                TemplateCode = GetGeneratedCodeHeader() + TemplateCode;

                File.WriteAllText(EntityFile, TemplateCode);
                return true;
            }
            else
                return false;
        }

        private static List<CodeTemplate> GetMockData(string? lhLink, string? rhLink)
        {
            List<CodeTemplate> TemplateList =
            [
                new CodeTemplate("Helper", HelperCodeTemplate("PlanetsModel", "Planets")),
                new CodeTemplate("HelperLink", HelperCodeTemplate("PlanetsModel", "Planets")),
                new CodeTemplate("POCO", PocoCodeTemplate("")),
                new CodeTemplate("POCOLink", PocoCodeTemplate("", false, lhLink, rhLink)),
                new CodeTemplate("MockRepository", MockRepositoryTemplate(""))
        ];
            return TemplateList;
        }

        private static string MockRepositoryTemplate(string entity)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"using ArchCorpUtilities.Models;");
            stringBuilder.AppendLine($"using AL = ArchCorpUtilities.Models.ArchLoader;");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"namespace ArchCorpUtilities.GeneratedModels.{entity}Model");
            stringBuilder.AppendLine($"    {{");
            stringBuilder.AppendLine($"        public class {entity}MockRepository<T> : IRepository<{entity}>, IDisposable");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            private readonly List<{entity}>? Items;");
            stringBuilder.AppendLine($"            string PostFix {{ get; set; }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public {entity}MockRepository(string postFix)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                PostFix = postFix;");
            stringBuilder.AppendLine($"                Items = [];");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public void Add({entity}? entity)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                if (Items != null && entity != null) {{ Items?.Add(entity); }}");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public IEnumerable<{entity}>? All()");
            stringBuilder.AppendLine($"            {{");

            if (RHLink != null && LHLink != null)
            {
                stringBuilder.AppendLine($"            var {RHLink}Items = AL.{RHLink}Helper?.Items;");
                stringBuilder.AppendLine($"            var {LHLink}Items = AL.{LHLink}Helper?.Items;");
                stringBuilder.AppendLine($"            if ({RHLink}Items != null && {LHLink}Items != null)");
                stringBuilder.AppendLine($"                for (int i = 0; i < {RHLink}Items.Count; i++)");
                stringBuilder.AppendLine($"                {{");
                stringBuilder.AppendLine($"                    {RHLink}Model.{RHLink}? {RHLink} = {RHLink}Items[i];");
                stringBuilder.AppendLine($"                    {LHLink}Model.{LHLink}? {LHLink} = {LHLink}Items[i];");
                stringBuilder.AppendLine($"                    Items?.Add(new {entity}($\"{{{LHLink}.Name}}-{{{RHLink}.Name}}\", 0, {LHLink}.{LHLink}Guid, {RHLink}.{RHLink}Guid));");
                stringBuilder.AppendLine($"                }}");
            }

            if (RHLink == null && LHLink == null)
            {
                stringBuilder.AppendLine($"                string[] ItemsToAdd = [\"Alpha\", \"Beta\", \"Charlie\", \"Delta\", \"Echo\", \"Foxtrot\", \"Golf\", \"Hotel\"];");
                stringBuilder.AppendLine($"                foreach (var item in ItemsToAdd)");
                stringBuilder.AppendLine($"                {{");
                stringBuilder.AppendLine($"                    {entity} NewEntity = new($\"{{item}}{{PostFix}}\", 0, Guid.NewGuid().ToString());");
                stringBuilder.AppendLine($"                    if (Items != null && !Items.Contains(NewEntity))");
                stringBuilder.AppendLine($"                        Add(NewEntity);");
                stringBuilder.AppendLine($"                }}");
            }

            stringBuilder.AppendLine($"                return Items;");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public void Dispose()");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                Items?.Clear();");
            stringBuilder.AppendLine($"                GC.SuppressFinalize(this);");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public IEnumerable<{entity}>? GetByGUID(string guid)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                if (Items == null) {{ return null; }}");
            stringBuilder.AppendLine($"                return Items.Where(p => p.{entity}Guid != null && p.{entity}Guid == guid);");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public IEnumerable<{entity}>? GetById(int id)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                if (Items == null) {{ return null; }}");
            stringBuilder.AppendLine($"                return Items.Where(p => p.Id == id);");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public IEnumerable<{entity}>? GetByName(string name)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                if (Items == null) {{ return null; }}");
            stringBuilder.AppendLine($"                return Items.Where(p => p.Name == name);");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public bool Remove({entity}? entity)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                if (Items != null && entity != null) {{ return Items.Remove(entity); }}");
            stringBuilder.AppendLine($"                return false;");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public int Count()");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                return Items == null ? 0 : Items.Count;");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            public List<{entity}>? OrderByIndex()");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                return (List<{entity}>?)Items?.OrderBy(p => p.Index);");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"        }}");
            stringBuilder.AppendLine($"    }}");

            return stringBuilder.ToString();
        }

        private static void SearchLogic(StringBuilder stringBuilder, string entity, string? lHLink, string? rHLink)
        {
            stringBuilder.AppendLine($"            CH.Feedback(\"Enter an item name to search for\");");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            var Input = CH.GetInput(simInput?[0]);");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            if (string.IsNullOrWhiteSpace(Input))");
            stringBuilder.AppendLine($"                CH.Feedback(\"No Items Name Was Entered\");");
            stringBuilder.AppendLine($"            else ");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"                List<{entity}>? Entities = Items?.Where(c => c.Name != null && c.Name.ToUpper().Contains(Input.ToUpper(), StringComparison.CurrentCultureIgnoreCase)).ToList<{entity}>();");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"                if (Entities != null && Entities.Count > 0)");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                    Page = new Patina.Patina(5, Convert.ToUInt32(Entities.Count));");
            stringBuilder.AppendLine($"                    EntitiesOnThePage = U.ViewWithPagination(\"Search Items\", Page, Entities);");
            stringBuilder.AppendLine($"                    return true;");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"                else ");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                    CH.Feedback(\"No Items Was Found\");");

            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine($"                    EntitiesOnThePage = [new {entity}(\"None\", 1)];");
            else
                stringBuilder.AppendLine($"                    EntitiesOnThePage = [new {entity}(\"None\", 1, null, null)];");

            stringBuilder.AppendLine($"                    Page = new Patina.Patina(1, 1);");
            stringBuilder.AppendLine($"                    return false;");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            ReIndexDisplayId();");
            stringBuilder.AppendLine($"            ResetPageMaxCount();");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            return false;");
        }

        private static void LoadLogic(StringBuilder stringBuilder, string entity, string? lHLink, string? rHLink)
        {
            stringBuilder.AppendLine($"	        var path = \"{entity}\";");
            stringBuilder.AppendLine($"	        try");
            stringBuilder.AppendLine($"	        {{");
            stringBuilder.AppendLine($"	            if (File.Exists(path))");
            stringBuilder.AppendLine($"	            {{");
            stringBuilder.AppendLine($"	                CH.Feedback($\"Items Loaded Successfully {{path}} - {{U.GetCurrentDate()}}\");");
            stringBuilder.AppendLine($"	                string FileInput = File.ReadAllText(path);");
            stringBuilder.AppendLine($"	                bool SkipFirstLine = true;");
            stringBuilder.AppendLine($"	                foreach (string line in FileInput.Split(\"\\r\\n\"))");
            stringBuilder.AppendLine($"	                {{");
            stringBuilder.AppendLine($"	                    if (!SkipFirstLine)");
            stringBuilder.AppendLine($"	                    {{");
            stringBuilder.AppendLine($"	                        string[] LineItem = line.Split(\"|\");");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\tif (LineItem.Length > 0 && LineItem.Length > 1)");
            stringBuilder.AppendLine($"	                        {{");
            stringBuilder.AppendLine($"	                            string Name = LineItem[0].Trim();");
            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine($"	                            string GUID = CH.IsSimulate ? \"<GUID>\" : LineItem[1].Trim();");
            else
            {
                stringBuilder.AppendLine($"	                            string GUID = CH.IsSimulate ? \"<GUID>\" : LineItem[5].Trim();");
                stringBuilder.AppendLine($"	                            string LHLinkGuid = LineItem[6].Trim();");
                stringBuilder.AppendLine($"	                            string RHLinkGuid = LineItem[7].Trim();");
            }
            stringBuilder.AppendLine($"	                            var Entity = Items?.FirstOrDefault(c => c.{entity}Guid == GUID);");
            stringBuilder.AppendLine($"	                            var EntityItem = Items?.FirstOrDefault(c => c.Name == Name);");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\tvar OldGUID = CH.IsSimulate ? \"<GUID>\" : EntityItem?.{entity}Guid;");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\tif (Entity == null)");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t{{");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tif (EntityItem != null)");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t{{");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t\tCH.Feedback($\"Item Exists - No action - Old item: {{ EntityItem.Name}} - {{OldGUID}} - New Item: {{Name}} - {{GUID}}\");");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t}}");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\telse");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t{{");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tif (SessionID != null)");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t\tL.Log($\"Item found - {{Name}}\", SessionID);");
            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tItems?.Add(new {entity}(Name, 0, GUID));");
            else
                stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tItems?.Add(new {entity}(Name, 0, LHLinkGuid, RHLinkGuid, GUID));");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tCH.Feedback($\"Item Added - New Item: {{Name}} - {{GUID}}\");");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tReIndexDisplayId();");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tResetPageMaxCount();");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tResetEntitiesOnThePage();");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\t}}");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t}}");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\telse");
            stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tCH.Feedback($\"No action - Old item : {{EntityItem?.Name}} - {{OldGUID}} - New Item: {{Name}} - {{GUID}}\");");
            stringBuilder.AppendLine($"	                        }}");
            stringBuilder.AppendLine($"	                    }}");
            stringBuilder.AppendLine($"	                    SkipFirstLine = false;");
            stringBuilder.AppendLine($"	                }}");
            stringBuilder.AppendLine($"	");
            stringBuilder.AppendLine($"	                ReIndexDisplayId();");
            stringBuilder.AppendLine($"	                ResetPageMaxCount();");
            stringBuilder.AppendLine($"	                ResetEntitiesOnThePage();");
            stringBuilder.AppendLine($"                 return true;");
            stringBuilder.AppendLine($"	            }}");
            stringBuilder.AppendLine($"	            else");
            stringBuilder.AppendLine($"	            {{");
            stringBuilder.AppendLine($"                    if (SessionID != null)");
            stringBuilder.AppendLine($"	                    L.Log($\"{{System.Reflection.MethodBase.GetCurrentMethod()?.Name}} - Error - Import file not found\", SessionID, 8);");
            stringBuilder.AppendLine($"	                CH.Feedback($\"Loading The Items Failed - {{path}}\");");
            stringBuilder.AppendLine($"	            }}");
            stringBuilder.AppendLine($"	        }}");
            stringBuilder.AppendLine($"	        catch (Exception ex)");
            stringBuilder.AppendLine($"	        {{");
            stringBuilder.AppendLine($"                if (SessionID != null)");
            stringBuilder.AppendLine($"	                L.Log($\"{{System.Reflection.MethodBase.GetCurrentMethod()?.Name}} - Error message - {{ex.Message}}\", SessionID, 9);");
            stringBuilder.AppendLine($"	            CH.Feedback($\"Error in the loading of the items {{ex.Message}} {{path}}\");");
            stringBuilder.AppendLine($"	        }}");
            stringBuilder.AppendLine($"	        return false;");
        }

        private static void MethodDeclarationLogic(string methodName, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        public bool {methodName}(int? simChoice = null, string[]? simInput = null)");
        }

        private static void ScopeEndLogic(StringBuilder stringBuilder, string tabs)
        {
            stringBuilder.AppendLine($"{tabs}}}");
        }

        private static void ScopeStartLogic(StringBuilder stringBuilder, string tabs)
        {
            stringBuilder.AppendLine($"{tabs}{{");
        }

        private static void EmptyLineLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"");
        }

        private static void ReIndexLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("            var OrderedModels = Items?.OrderBy(c => c.Name).ToList();");
            stringBuilder.AppendLine("            if (OrderedModels != null)");
            stringBuilder.AppendLine("                for (int i = 0; i < OrderedModels.Count; i++)");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine($"                    {entity}? item = OrderedModels[i];");
            stringBuilder.AppendLine("                    item.DisplayId = i + 1;");
            stringBuilder.AppendLine("                    item.Id = item.DisplayId;");
            stringBuilder.AppendLine("                    item.Index = item.DisplayId;");
            stringBuilder.AppendLine("                }");
        }

        private static void SaveLogic(StringBuilder stringBuilder, string entity, string? lHLink, string? rHLink)
        {
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            var Path = \"{entity}\";");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            if (Items != null && Items.Count > 0)");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                StringBuilder sb = new();");
            stringBuilder.AppendLine($"                try");
            stringBuilder.AppendLine($"                {{");

            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine($"                    sb.AppendLine($\"{entity}Name|{entity}Guid\");");
            else
                stringBuilder.AppendLine($"                    sb.AppendLine($\"{entity}Name|LHLink|RHLink|LHLinkName|RHLinkName|Guid|LHLinkGuid|RHLinkGuid\");");

            stringBuilder.AppendLine($"                    foreach (var item in Items.OrderBy(c => c.Name))");
            stringBuilder.AppendLine($"                    {{");
            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine($"                        sb.AppendLine($\"{{ item.Name}}|{{ item.{entity}Guid}}\");");
            else
                stringBuilder.AppendLine($"                        sb.AppendLine($\"{{ item.Name}}|{lHLink}|{rHLink}|{{AL.{lHLink}Helper?.GetName(item.{lHLink}Guid)}}|{{AL.{rHLink}Helper?.GetName(item.{rHLink}Guid)}}|{{ item.{entity}Guid}}|{{item.{lHLink}Guid}}|{{item.{rHLink}Guid}}\");");
            stringBuilder.AppendLine($"                    }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"                    if (File.Exists(Path))");
            stringBuilder.AppendLine($"                        File.Delete(Path);");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"                    File.AppendAllText(Path, sb.ToString());");
            stringBuilder.AppendLine($"                    CH.Feedback($\"Saved the item successfully - {{U.GetCurrentDate()}}\");");
            stringBuilder.AppendLine($"                    return true;");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"                catch (Exception ex)");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                    CH.Feedback($\"Items was not saved. - Error {{ex.Message}}\");");
            stringBuilder.AppendLine($"                    return false;");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"                finally");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                    sb?.Clear();");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"            else");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                CH.Feedback(\"No data to save\");");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"            return false;");
        }

        private static void EditLogic(StringBuilder stringBuilder, string entity, string? lHLink = null, string? rHLink = null)
        {
            stringBuilder.AppendLine($"\t        try");
            stringBuilder.AppendLine($"\t        {{");
            stringBuilder.AppendLine($"\t            {entity}? Entity = ViewAndSelectItem(simInput?[0], \"Select an item to edit\");");
            stringBuilder.AppendLine($"\t");
            stringBuilder.AppendLine($"\t            if (Entity != null)");
            stringBuilder.AppendLine($"\t            {{");
            stringBuilder.AppendLine($"\t                CH.Feedback(\"Please enter a new name\");");
            stringBuilder.AppendLine($"\t                var Input = CH.GetInput(simInput?[1]);");
            stringBuilder.AppendLine($"\t                if (!string.IsNullOrWhiteSpace(Input))");
            stringBuilder.AppendLine($"\t                {{");
            stringBuilder.AppendLine($"\t                    var NotFound = Items?.FirstOrDefault(i => (i.Name != null && i.Name.Equals(Input, StringComparison.CurrentCultureIgnoreCase)));");
            stringBuilder.AppendLine($"\t                    if (NotFound == null)");
            stringBuilder.AppendLine($"\t                    {{");
            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine($"\t                        EntitiesOnThePage = [new(Input, 0)];");
            else
                stringBuilder.AppendLine($"\t                        EntitiesOnThePage = [new(Input, 0, Entity?.{lHLink}Guid, Entity?.{rHLink}Guid)];");
            stringBuilder.AppendLine($"\t                        Items?.Add(EntitiesOnThePage[0]);");
            stringBuilder.AppendLine($"\t                        if (Entity != null)");
            stringBuilder.AppendLine($"\t\t                      Items?.Remove(Entity);");
            stringBuilder.AppendLine($"\t                        Page = new Patina.Patina(1, 1);");
            stringBuilder.AppendLine($"\t                        ResetPageMaxCount();");
            stringBuilder.AppendLine($"\t                        ReIndexDisplayId();");
            stringBuilder.AppendLine($"\t                        ResetEntitiesOnThePage();");
            stringBuilder.AppendLine($"\t                        CH.Feedback(\"Item was modified\");");
            stringBuilder.AppendLine($"\t                        return true;");
            stringBuilder.AppendLine($"\t                    }}");
            stringBuilder.AppendLine($"\t                    else");
            stringBuilder.AppendLine($"\t                        CH.Feedback(\"Duplicate entry found - operation aborted.\");");
            stringBuilder.AppendLine($"\t                }}");
            stringBuilder.AppendLine($"\t                else");
            stringBuilder.AppendLine($"\t                    CH.Feedback(\"No name entered\");");
            stringBuilder.AppendLine($"\t            }}");
            stringBuilder.AppendLine($"\t            else");
            stringBuilder.AppendLine($"\t                {{");
            stringBuilder.AppendLine($"\t                if (EntitiesOnThePage != null)");
            stringBuilder.AppendLine($"\t                    CH.Feedback(\"No Item selected\");");
            stringBuilder.AppendLine($"\t                }}");
            stringBuilder.AppendLine($"\t");
            stringBuilder.AppendLine($"\t            ResetPageMaxCount();");
            stringBuilder.AppendLine($"\t            ReIndexDisplayId();");
            stringBuilder.AppendLine($"\t            ResetEntitiesOnThePage();");
            stringBuilder.AppendLine($"\t");
            stringBuilder.AppendLine($"\t            return false;");
            stringBuilder.AppendLine($"\t");
            stringBuilder.AppendLine($"\t        }}");
            stringBuilder.AppendLine($"\t        catch (Exception err)");
            stringBuilder.AppendLine($"\t        {{");
            stringBuilder.AppendLine($"\t            L.Log($\"Error - Edit an Item Helper -- {{err.Message}} --- {{err.InnerException?.Message}}\");");
            stringBuilder.AppendLine($"\t            return false;");
            stringBuilder.AppendLine($"\t        }}");
        }

        private static void RemoveLogic(StringBuilder stringBuilder, string entity)
        {
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            //List the items");
            stringBuilder.AppendLine($"            var Current{entity} = ViewAndSelectItem(simInput?[0], \"Select the item to remove\");");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            if (Items != null && Current{entity} != null)");
            stringBuilder.AppendLine($"                if (Items.Remove(Current{entity}))");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                    CH.Feedback(\"Item removed.\");");
            stringBuilder.AppendLine($"                    ResetPageMaxCount();");
            stringBuilder.AppendLine($"                    ReIndexDisplayId();");
            stringBuilder.AppendLine($"                    ResetEntitiesOnThePage();");
            stringBuilder.AppendLine($"                    return true;");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"                else");
            stringBuilder.AppendLine($"                    CH.Feedback(\"Item was not removed.\");");
            stringBuilder.AppendLine($"            else");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                if (EntitiesOnThePage != null)");
            stringBuilder.AppendLine($"                    CH.Feedback(\"Nothing selected.\");");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            ReIndexDisplayId();");
            stringBuilder.AppendLine($"            ResetPageMaxCount();");
            stringBuilder.AppendLine($"            ResetEntitiesOnThePage();");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            return false;");
        }

        private static void AddLogic(StringBuilder stringBuilder, string? lHLink, string? rHLink)
        {
            stringBuilder.AppendLine("            CH.Feedback(\"Please provide the item name: \");");
            stringBuilder.AppendLine("            var Input = CH.GetInput(simInput?[0]);");
            stringBuilder.AppendLine("            if(!string.IsNullOrWhiteSpace(Input))");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("                if (DuplicateFound(Input))");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine("                    CH.Feedback(\"Duplicate entry found - operation aborted.\");");
            stringBuilder.AppendLine("                    return false;");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("");
            if (lHLink == null && rHLink == null)
                stringBuilder.AppendLine("                Items?.Add(new(Input, 0));");
            else
                stringBuilder.AppendLine("                Items?.Add(new(Input, 0, null, null));");
            stringBuilder.AppendLine("                CH.Feedback(\"Item added.\");");
            stringBuilder.AppendLine("                ResetPageMaxCount();");
            stringBuilder.AppendLine("                ReIndexDisplayId();");
            stringBuilder.AppendLine("                return true;");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("            else");
            stringBuilder.AppendLine("                CH.Feedback(\"Invalid Name or empty - No item added.\");");
            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("            return false;");
        }

        private static void ViewLogic(StringBuilder stringBuilder, string entity)
        {
            stringBuilder.AppendLine("            var orderedEntities = Items?.OrderBy(p => p.Index).ToList();");
            stringBuilder.AppendLine($"           EntitiesOnThePage = U.ViewWithPagination(\"{entity}\", Page, orderedEntities, navigate);");
            stringBuilder.AppendLine("            return true;");
        }

        private static void RefreshLogic(StringBuilder stringBuilder, string entity)
        {
            stringBuilder.AppendLine("            ReIndexDisplayId();");
            stringBuilder.AppendLine("            ResetPageMaxCount();");
            stringBuilder.AppendLine("            var orderedEntities = modelList?.OrderBy(p => p.Index).ToList();");
            stringBuilder.AppendLine($"           EntitiesOnThePage = U.ViewWithPagination(\"{entity}\", Page, orderedEntities, navigate);");
            stringBuilder.AppendLine("            return true;");
        }

        private static void DefaultUsingLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("using L = Logger.Logger;");
            stringBuilder.AppendLine("using U = ArchCorpUtilities.Utilities.UniversalUtilities;");
            stringBuilder.AppendLine("using CH = ArchCorpUtilities.Utilities.ConsoleHelper;");
            stringBuilder.AppendLine("using ArchCorpUtilities.Models.Helper;");
            stringBuilder.AppendLine("using System.Text;");
            stringBuilder.AppendLine("using AL = ArchCorpUtilities.Models.ArchLoader;");
        }

        private static void LoggingLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}");
        }

        private static List<CodeTemplate>? Repository;

        private static string GetTemplate(CodeTemplateEnum name, string entity, string? lhLink = null, string? rhLink = null)
        {
            Repository = GetMockData(lhLink, rhLink);
            string? Template = Repository.FirstOrDefault(p => p.Name.ToUpper().Equals(name.ToString().ToUpper()))?.Code;
            if (Template != null)
            {
                switch (name)
                {
                    case CodeTemplateEnum.POCO:
                        Template = PocoCodeTemplate(entity);
                        break;
                    case CodeTemplateEnum.POCOLink:
                        Template = PocoCodeTemplate(entity, true, lhLink, rhLink);
                        break;
                    case CodeTemplateEnum.Helper:
                        Template = HelperCodeTemplate($"{entity}Model", $"{entity}");
                        break;
                    case CodeTemplateEnum.HelperLink:
                        Template = HelperCodeTemplate($"{entity}Model", $"{entity}");
                        break;
                    case CodeTemplateEnum.MockRepository:
                        Template = MockRepositoryTemplate(entity);
                        break;
                    default:
                        break;
                }

                return Template;
            }

            return "<NoData>";
        }

        public enum CodeTemplateEnum
        {
            Helper,
            POCO,
            POCOLink,
            HelperLink,
            MockRepository
        }

        public enum MenuTypeEnum
        {
            None,
            Link,
            Manage
        }
        private static string? GenCode(CodeTemplateEnum name, string entity, string? lhLinkGuid = null, string? rhLinkGuid = null)
        {
            #region Log that the GenCode method is running.
            #endregion
            #region Preparing. Make sure that all the directories exists. If not Create them. Clear out the old code files. Will indicate to continue.
            var BaseFolder = "\\GeneratedModels";
            bool Continue = PrepareFolders($"{WorkingFolder}{BaseFolder}\\", $"{entity}Model");
            #endregion
            if (Continue)
            {
                #region Looking for the code in the repository.  Will return the code once found.
                var Code = GetTemplate(name, entity, lhLinkGuid, rhLinkGuid);
                // The template exists - return it
                if (Code != null && Code != " <NoData>")
                    return Code;
                #endregion
                #region Something went wrong. Log it. Return the string <NoData>.
                // Nothing was done
                return "<NoData>";
                #endregion
            }
            return "<NoData>";
        }

        private static bool PrepareFolders(string workingFolder, string folderName)
        {
            try
            {

                if (!Directory.Exists($"{workingFolder}"))
                {
                    Directory.CreateDirectory($"{workingFolder}");
                }


                if (!Directory.Exists($"{workingFolder}\\{folderName}"))
                {
                    Directory.CreateDirectory($"{workingFolder}\\{folderName}");
                }
                return true;

            }
            catch (Exception)
            {


                return false;
            }


        }

        private static string GetEntityPath(CodeTemplateEnum codeTemplate, string entity, string baseFolder)
        {
            var PrePath = $"{WorkingFolder}{baseFolder}\\{entity}Model\\{entity}";
            string? Path = codeTemplate switch
            {
                CodeTemplateEnum.HelperLink => $"{PrePath}Helper.cs",
                CodeTemplateEnum.Helper => $"{PrePath}Helper.cs",
                CodeTemplateEnum.POCO => $"{PrePath}.cs",
                CodeTemplateEnum.POCOLink => $"{PrePath}.cs",
                CodeTemplateEnum.MockRepository => $"{PrePath}MockRepository.cs",
                _ => $"{PrePath}.cs",
            };
            return Path;
        }

        private static string HelperCodeTemplate(string folderName, string entity)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            StringBuilder stringBuilder = new();

            try
            {
                DefaultUsingLogic(stringBuilder);
                NamespaceLogic(folderName, stringBuilder);
                ScopeStartLogic(stringBuilder, "");
                ClassDeclarationLogic(entity, stringBuilder);
                ScopeStartLogic(stringBuilder, "\t");
                VariablesDeclarationLogic(entity, stringBuilder);
                ConstructorLogic(entity, stringBuilder);

                //MockDataLogic(entity, LHLink, RHLink, stringBuilder);
                
                ViewLogicInternal(entity, stringBuilder);
                AddLogicInternal(stringBuilder);
                DisposeLogic(stringBuilder);
                EditLogicInternal(entity, stringBuilder);
                IsItemsOnThePageLogic(stringBuilder);
                SearchLogicInternal(entity, stringBuilder);
                RefreshLogicInternal(entity, stringBuilder);
                RemoveLogicInternal(entity, stringBuilder);
                LoggingLogicInternal(entity, stringBuilder);
                LoadLogicInternal(entity, stringBuilder);
                ReIndexDisplayIdLogic(entity, stringBuilder);
                ResetPageMaxCountLogic(stringBuilder);
                ViewAndSelectItemLogic(entity, stringBuilder);
                ResetEntitiesOnThePageLogic(stringBuilder);
                LoadDefaultsLogic(stringBuilder);
                DuplicateFoundLogic(stringBuilder);
                GetNameLogic(entity, stringBuilder);
                GetGuidLogic(entity, stringBuilder);
                ScopeEndLogic(stringBuilder, "\t");
                ScopeEndLogic(stringBuilder, "");
                return stringBuilder.ToString();
            }
            catch (Exception err)
            {
                if (SessionID != null)
                    L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error - {err.Message} -- {err.InnerException?.Message}", SessionID);
                return "<NoData>";
            }
            finally
            {
                stringBuilder.Clear();
            }
        }

        private static void VariablesDeclarationLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public string? SessionID { get; set; }");
            stringBuilder.AppendLine("        public List<" + entity + ">? Items { get; set; }");
            stringBuilder.AppendLine("        public List<" + entity + ">? EntitiesOnThePage { get; set; }");
            stringBuilder.AppendLine("        public Patina.Patina Page { get; set; }");
            stringBuilder.AppendLine($"        public {entity}MockRepository<{entity}> Repository {{ get; set; }}");
        }

        private static void ClassDeclarationLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"    public class {entity}Helper : IHelper<{entity}>, IDisposable");
        }

        private static void NamespaceLogic(string folderName, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"namespace ArchCorpUtilities.GeneratedModels.{folderName}");
        }

        private static void ConstructorLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"       public {entity}Helper(string? sessionID)");
            ScopeStartLogic(stringBuilder, "\t\t");
            stringBuilder.AppendLine("            SessionID = sessionID;");
            stringBuilder.AppendLine("            Items = [];");
            stringBuilder.AppendLine("            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Items?.Count));");
            stringBuilder.AppendLine($"            Repository = new(\"-{entity}\");");
            ScopeEndLogic(stringBuilder, "\t\t");
        }

        private static void ViewLogicInternal(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public bool View(U.Navigation navigate = U.Navigation.FirstPage)");
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            ViewLogic(stringBuilder, entity);
            ScopeEndLogic(stringBuilder, "\t\t\t");
        }

        private static void AddLogicInternal(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public bool Add(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            AddLogic(stringBuilder, LHLink, RHLink);
            stringBuilder.AppendLine("        }");
        }

        private static void DisposeLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public void Dispose()");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            stringBuilder.AppendLine("            GC.SuppressFinalize(this);");
            stringBuilder.AppendLine("        }");
        }

        private static void EditLogicInternal(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public bool Edit(int? simChoice, string[]? simInput)");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            EditLogic(stringBuilder, entity, LHLink, RHLink);
            stringBuilder.AppendLine("        }");
        }

        private static void IsItemsOnThePageLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public bool IsItemsOnThePage()");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            stringBuilder.AppendLine("            return !(EntitiesOnThePage == null || (EntitiesOnThePage != null && EntitiesOnThePage.Count == 0));");
            stringBuilder.AppendLine("        }");
        }

        private static void SearchLogicInternal(string entity, StringBuilder stringBuilder)
        {
            MethodDeclarationLogic("Search", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            SearchLogic(stringBuilder, entity, LHLink, RHLink);
            ScopeEndLogic(stringBuilder, "\t\t\t");
        }

        private static void RefreshLogicInternal(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        public bool Refresh(List<{entity}> modelList, U.Navigation navigate = U.Navigation.FirstPage)");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            RefreshLogic(stringBuilder, entity);
            stringBuilder.AppendLine("        }");
        }

        private static void RemoveLogicInternal(string entity, StringBuilder stringBuilder)
        {
            MethodDeclarationLogic("Remove", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            RemoveLogic(stringBuilder, entity);
            ScopeEndLogic(stringBuilder, "\t\t\t");
        }

        private static void LoggingLogicInternal(string entity, StringBuilder stringBuilder)
        {
            MethodDeclarationLogic("Save", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            SaveLogic(stringBuilder, entity, LHLink, RHLink);
            ScopeEndLogic(stringBuilder, "\t\t\t");
        }

        private static void LoadLogicInternal(string entity, StringBuilder stringBuilder)
        {
            MethodDeclarationLogic("Load", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            LoadLogic(stringBuilder, entity, LHLink, RHLink);
            ScopeEndLogic(stringBuilder, "\t\t\t");
        }

        private static void ReIndexDisplayIdLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public void ReIndexDisplayId()");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            ReIndexLogic(entity, stringBuilder);
            stringBuilder.AppendLine("        }");
        }

        private static void ResetPageMaxCountLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("        public void ResetPageMaxCount()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            Page = new Patina.Patina(5, Convert.ToUInt32(Items?.Count));");
            stringBuilder.AppendLine("        }");
        }

        private static void GetGuidLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        internal string? GetGuid(string? name)");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            return Items?.FirstOrDefault(p => p.Name != null && p.Name.Equals(name))?.{entity}Guid;");
            stringBuilder.AppendLine($"        }}");
        }

        private static void GetNameLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        internal string? GetName(string? guid)");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            return Items?.FirstOrDefault(p => p.{entity}Guid != null && p.{entity}Guid.Equals(guid))?.Name;");
            stringBuilder.AppendLine($"        }}");
        }

        private static void DuplicateFoundLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        private bool DuplicateFound(string Input)");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            return Items?.FirstOrDefault(p => p.Name != null && p.Name.Length > 0 && p.Name.Equals(Input)) != null;");
            stringBuilder.AppendLine($"        }}");
        }

        private static void LoadDefaultsLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        public bool LoadDefaults()");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            Items?.Clear(); Items = Repository.All()?.ToList(); return true;");
            stringBuilder.AppendLine($"        }}");
        }

        private static void ResetEntitiesOnThePageLogic(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        private void ResetEntitiesOnThePage()");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine("            if (SessionID != null) { L.Log(\"Entities was reset.\", SessionID, 4);}");
            stringBuilder.AppendLine($"               EntitiesOnThePage = null;");
            stringBuilder.AppendLine($"        }}");
        }

        private static void ViewAndSelectItemLogic(string entity, StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"        private {entity}? ViewAndSelectItem(string? simInput, string heading)");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            var orderedEntities = EntitiesOnThePage ?? Items?.OrderBy(p => p.Index).ToList();");
            stringBuilder.AppendLine($"            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));");
            stringBuilder.AppendLine($"            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, U.Navigation.FirstPage);");
            stringBuilder.AppendLine($"            if (EntitiesOnThePage == null) {{ return null; }}");
            stringBuilder.AppendLine($"            CH.Feedback(heading);");
            stringBuilder.AppendLine($"            _ = Int32.TryParse(CH.GetInput(simInput), out int Choice);");
            stringBuilder.AppendLine($"            return EntitiesOnThePage?.FirstOrDefault(p => p.DisplayId == Choice);");
            stringBuilder.AppendLine($"        }}");
        }

        private static string PocoCodeTemplate(string entity, bool isLink = false, string? lhLinkGuid = null, string? rhLinkGuid = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            StringBuilder stringBuilder = new();

            try
            {
                EmptyLineLogic(stringBuilder);

                stringBuilder.AppendLine($"using ArchCorpUtilities.Models;");
                stringBuilder.AppendLine($"namespace ArchCorpUtilities.GeneratedModels.{entity}Model;");
                stringBuilder.AppendLine($"using AL = ArchLoader;");
                stringBuilder.AppendLine($"public class {entity}");
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("    public string? Name { get; }");
                if (lhLinkGuid != null && rhLinkGuid != null && isLink)
                {
                    stringBuilder.AppendLine($"    public string? {lhLinkGuid}Guid {{ get; }}");
                    stringBuilder.AppendLine($"    public string? {rhLinkGuid}Guid {{ get; }}");
                }
                stringBuilder.AppendLine("    public int Id { get; set; }");
                stringBuilder.AppendLine("    public int Index { get; set; }");
                stringBuilder.AppendLine("    public int DisplayId { get; internal set; }");
                stringBuilder.AppendLine("    public string? " + entity + "Guid {get; internal set; }");
                if (!isLink)
                    stringBuilder.AppendLine($"    public {entity}(string? name, int id, string guid = \"\")");
                else
                {
                    if (lhLinkGuid != null && rhLinkGuid != null)
                    {
                        stringBuilder.AppendLine($"    public {entity}(string? name , int id ,string? {lhLinkGuid.ToLower()}Guid,string? {rhLinkGuid.ToLower()}Guid, string guid = \"\")");
                    }
                }
                stringBuilder.AppendLine("    {");
                stringBuilder.AppendLine("        Name = name;");
                if (lhLinkGuid != null && rhLinkGuid != null && isLink)
                {
                    stringBuilder.AppendLine($"        {rhLinkGuid}Guid = {rhLinkGuid.ToLower()}Guid;");
                    stringBuilder.AppendLine($"        {lhLinkGuid}Guid = {lhLinkGuid.ToLower()}Guid;");
                }
                stringBuilder.AppendLine("        Id = id;");
                stringBuilder.AppendLine("        DisplayId = Id;");
                stringBuilder.AppendLine("        if (guid != null && guid.Length > 0)");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine($"            {entity}Guid = guid;");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("        else");
                stringBuilder.AppendLine("        {");
                stringBuilder.AppendLine($"            {entity}Guid = Guid.NewGuid().ToString();");
                stringBuilder.AppendLine("        }");
                stringBuilder.AppendLine("    }");
                stringBuilder.AppendLine("    public void SetDisplayId(int displayId) { DisplayId = displayId; }");
                if (!isLink)
                    stringBuilder.AppendLine("    public override string ToString() {return $\"{DisplayId}) {Name}\";}");
                else
                {
                    if (lhLinkGuid != null && rhLinkGuid != null)
                    {
                        stringBuilder.AppendLine($"    public override string ToString() {{return $\"{{DisplayId}}) {{Name}} -- {rhLinkGuid}-{lhLinkGuid}\";}}");
                    }
                }
                stringBuilder.AppendLine("}");

                return stringBuilder.ToString();
            }
            catch (Exception err)
            {
                if (SessionID != null)
                    L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error - {err.Message} -- {err.InnerException?.Message}", SessionID);
                return "<NoData>";
            }
            finally
            {
                stringBuilder.Clear();
            }

        }

    }
}