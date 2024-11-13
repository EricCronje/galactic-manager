using ArchCorpUtilities.Utilities.CodeGen;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using System.Text;
using L = Logger.Logger;

namespace ArchCorpUtilities.Utilities;

public static class CodeGenHelper
{
    public static string? SessionID { get; set; }
    public static string WorkingFolder { get; set; }
    public static string TargetWorkingFolder { get; set; }
    public static string Version { get; set; }
    public static string BackupFolder{ get; set; }
    public static string CurrentGuid { get; set; }

    static CodeGenHelper()
    {
        WorkingFolder = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen";
        TargetWorkingFolder = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities";
        Version = "21.11.25";
        CurrentGuid = "{744852ea-d309-4f87-bbd2-03fe76ba877b}";
        BackupFolder = $"\\Backup\\{CurrentGuid}";
    }

    private static readonly List<CodeTemplate> Repository = GetMockData();

    private static string GetTemplate(CodeTemplateEnum name, string entity)
    {
        string? Template = Repository.FirstOrDefault(p => p.Name.ToUpper().Equals(name.ToString().ToUpper()))?.Code;
        if (Template != null)
        {
            switch (name)
            {
                case CodeTemplateEnum.POCO:
                    Template = PocoCodeTemplate($"{entity}Model", entity);
                    break;
                case CodeTemplateEnum.Helper:
                    Template = HelperCodeTemplate($"{entity}Model", $"{entity}");
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
        POCO
    }

    private static string? GenCode(CodeTemplateEnum name, string entity)
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
            var Code = GetTemplate(name, entity);
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

    #region Items for the mock data - Repository

    private static List<CodeTemplate> GetMockData()
    {
        List<CodeTemplate> TemplateList =
        [
            new CodeTemplate("Helper", HelperCodeTemplate("PlanetsModel", "Planets")),
            new CodeTemplate("POCO", PocoCodeTemplate("Poco", "")),
        ];
        return TemplateList;
    }

    private static string HelperCodeTemplate(string folderName, string entity)
    {
        if (SessionID != null)
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

        StringBuilder stringBuilder = new();

        try
        {
            EmptyLineLogic(stringBuilder);
            DefaultUsingLogic(stringBuilder, entity);
            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine($"namespace ArchCorpUtilities.GeneratedModels.{folderName}");
            ScopeStartLogic(stringBuilder, "");
            stringBuilder.AppendLine($"    public class {entity}Helper : IHelper<{entity}>, IDisposable");
            ScopeStartLogic(stringBuilder, "\t");
            stringBuilder.AppendLine("        public string? SessionID { get; set; }");
            stringBuilder.AppendLine("        public List<" + entity + ">? Items { get; set; }");
            stringBuilder.AppendLine("        public List<" + entity + ">? EntitiesOnThePage { get; set; }");
            stringBuilder.AppendLine("        public Patina.Patina Page { get; set; }");
            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine($"       public {entity}Helper(string? sessionID)");
            ScopeStartLogic(stringBuilder, "\t\t");
            stringBuilder.AppendLine("            SessionID = sessionID;");
            stringBuilder.AppendLine("            Items = MockData();");
            stringBuilder.AppendLine("            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Items?.Count));");
            ScopeEndLogic(stringBuilder, "\t\t");
            stringBuilder.AppendLine($"        private List<{entity}>? MockData()");
            ScopeStartLogic(stringBuilder, "\t\t\t");
            stringBuilder.AppendLine("            if (Items == null || Items.Count == 0)");
            stringBuilder.AppendLine("                Items = [];");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Alpha-{entity}\", 1));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Beta-{entity}\", 2));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Charlie-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Delta-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Echo-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Foxtrot-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Golf-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Hotel-{entity}\", 3));");
            stringBuilder.AppendLine("            return Items;");
            ScopeEndLogic(stringBuilder, "\t\t\t");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public bool View(U.Navigation navigate = U.Navigation.FirstPage)");
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            ViewLogic(stringBuilder, entity);
            ScopeEndLogic(stringBuilder, "\t\t\t");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public bool Add(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            AddLogic(stringBuilder);
            stringBuilder.AppendLine("        }");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public void Dispose()");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            stringBuilder.AppendLine("            GC.SuppressFinalize(this);");
            stringBuilder.AppendLine("        }");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public bool Edit(int? simChoice, string[]? simInput)");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            EditLogic(stringBuilder, entity);
            stringBuilder.AppendLine("        }");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public bool IsItemsOnThePage()");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            stringBuilder.AppendLine("            return !(EntitiesOnThePage == null || (EntitiesOnThePage != null && EntitiesOnThePage.Count == 0));");
            stringBuilder.AppendLine("        }");

            EmptyLineLogic(stringBuilder);
            MethodDeclarationLogic("Search", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            SearchLogic(stringBuilder, entity);            
            ScopeEndLogic(stringBuilder, "\t\t\t");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine($"        public bool Refresh(List<{entity}> modelList, U.Navigation navigate = U.Navigation.FirstPage)");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            RefreshLogic(stringBuilder, entity);
            stringBuilder.AppendLine("        }");

            EmptyLineLogic(stringBuilder);
            MethodDeclarationLogic("Remove", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            RemoveLogic(stringBuilder, entity);
            ScopeEndLogic(stringBuilder, "\t\t\t");

            EmptyLineLogic(stringBuilder);
            MethodDeclarationLogic("Save", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            SaveLogic(stringBuilder, entity);
            ScopeEndLogic(stringBuilder, "\t\t\t");

            EmptyLineLogic(stringBuilder);
            MethodDeclarationLogic("Load", stringBuilder);
            ScopeStartLogic(stringBuilder, "\t\t\t");
            LoggingLogic(stringBuilder);
            LoadLogic(stringBuilder, entity);
            ScopeEndLogic(stringBuilder, "\t\t\t");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public void ReIndexDisplayId()");
            stringBuilder.AppendLine("        {");
            LoggingLogic(stringBuilder);
            ReIndexLogic(stringBuilder);
            stringBuilder.AppendLine("        }");

            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine("        public void ResetPageMaxCount()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            Page = new Patina.Patina(5, Convert.ToUInt32(Items?.Count));");
            stringBuilder.AppendLine("        }");

            stringBuilder.AppendLine($"        private {entity}? ViewAndSelectItem(string? simInput, string heading)");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            var orderedEntities = EntitiesOnThePage ?? Items?.OrderBy(p => p.Index).ToList();");
            stringBuilder.AppendLine($"            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));");
            stringBuilder.AppendLine($"            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, U.Navigation.FirstPage);");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            if (EntitiesOnThePage == null)");
            stringBuilder.AppendLine($"                return null;");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            CH.Feedback(heading);");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            string ItemInput = CH.GetInput(simInput);");
            stringBuilder.AppendLine($"            //If none selected - do nothing");
            stringBuilder.AppendLine($"            if (!string.IsNullOrWhiteSpace(ItemInput))");
            stringBuilder.AppendLine($"            {{");
            stringBuilder.AppendLine($"                _ = Int32.TryParse(ItemInput, out int Choice);");
            stringBuilder.AppendLine($"                //If item selected");
            stringBuilder.AppendLine($"                {entity}? SelectedEntity = EntitiesOnThePage?.FirstOrDefault(p => p.DisplayId == Choice);");
            stringBuilder.AppendLine($"                if (SelectedEntity != null)");
            stringBuilder.AppendLine($"                {{");
            stringBuilder.AppendLine($"                    return SelectedEntity;");
            stringBuilder.AppendLine($"                }}");
            stringBuilder.AppendLine($"            }}");
            stringBuilder.AppendLine($"            return null;");
            stringBuilder.AppendLine($"        }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"        private void ResetEntitiesOnThePage()");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            if (SessionID != null)");
            stringBuilder.AppendLine($"                L.Log(\"Entities was reset.\", SessionID, 4);");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            EntitiesOnThePage = null;");
            stringBuilder.AppendLine($"        }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"        public bool LoadDefaults()");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            Items?.Clear();");
            stringBuilder.AppendLine($"            Items = MockData();");
            stringBuilder.AppendLine($"            return true;");
            stringBuilder.AppendLine($"        }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"        private bool DuplicateFound(string Input)");
            stringBuilder.AppendLine($"        {{");
            stringBuilder.AppendLine($"            var DuplicateFound = Items?.FirstOrDefault(p => p.Name != null && p.Name.Length > 0 && p.Name.Equals(Input));");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            if (DuplicateFound != null)");
            stringBuilder.AppendLine($"                return true;");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine($"            return false;");
            stringBuilder.AppendLine($"        }}");
            stringBuilder.AppendLine($"");
            stringBuilder.AppendLine("    }");
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

    private static void SearchLogic(StringBuilder stringBuilder, string entity)
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
        stringBuilder.AppendLine($"                    EntitiesOnThePage = [new {entity}(\"None\", 1)];");
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

    private static void LoadLogic(StringBuilder stringBuilder, string entity)
    {
        stringBuilder.AppendLine($"            if (SessionID != null)");
        stringBuilder.AppendLine($"                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);");
        stringBuilder.AppendLine($"");
        stringBuilder.AppendLine($"	        var path = \"{entity}\";");
        stringBuilder.AppendLine($"	        try");
        stringBuilder.AppendLine($"	        {{");
        stringBuilder.AppendLine($"	            if (File.Exists(path))");
        stringBuilder.AppendLine($"	            {{");
        stringBuilder.AppendLine($"	                CH.Feedback($\"Items Loaded Successfully {{path}} - {{U.GetCurrentDate()}}\");");
        stringBuilder.AppendLine($"	");
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
        stringBuilder.AppendLine($"	                            string GUID = CH.IsSimulate ? \"<GUID>\" : LineItem[1].Trim();");
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
        stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tItems?.Add(new {entity}(Name, 0, GUID));");
        stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tCH.Feedback($\"Item Added - New Item: {{Name}} - {{GUID}}\");");
        stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tReIndexDisplayId();");
        stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tResetPageMaxCount();");
        stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\tResetEntitiesOnThePage();");
        stringBuilder.AppendLine($"\t\t\t\t\t\t\t\t\treturn true;");
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

    private static void ReIndexLogic(StringBuilder stringBuilder)
    {
        EmptyLineLogic(stringBuilder);
        stringBuilder.AppendLine("            var OrderedModels = Items?.OrderBy(c => c.Name).ToList();");
        EmptyLineLogic(stringBuilder);
        stringBuilder.AppendLine("            int counter = 1;");
        stringBuilder.AppendLine("            if (OrderedModels != null)");
        stringBuilder.AppendLine("            {");
        stringBuilder.AppendLine("                foreach (var item in OrderedModels)");
        stringBuilder.AppendLine("                {");
        stringBuilder.AppendLine("                    item.DisplayId = counter++;");
        stringBuilder.AppendLine("                    item.Id = item.DisplayId;");
        stringBuilder.AppendLine("                    item.Index = item.DisplayId;");
        stringBuilder.AppendLine("                }");
        stringBuilder.AppendLine("            }");
    }

    private static void SaveLogic(StringBuilder stringBuilder, string entity)
    {
        stringBuilder.AppendLine($"");
        stringBuilder.AppendLine($"            var Path = \"{entity}\";");
        stringBuilder.AppendLine($"");
        stringBuilder.AppendLine($"            if (Items != null && Items.Count > 0)");
        stringBuilder.AppendLine($"            {{");
        stringBuilder.AppendLine($"                StringBuilder sb = new();");
        stringBuilder.AppendLine($"                try");
        stringBuilder.AppendLine($"                {{");
        stringBuilder.AppendLine($"                    sb.AppendLine($\"{entity}Name|{entity}Guid\");");
        stringBuilder.AppendLine($"                    foreach (var item in Items.OrderBy(c => c.Name))");
        stringBuilder.AppendLine($"                    {{");
        stringBuilder.AppendLine($"                        sb.AppendLine($\"{{ item.Name}}|{{ item.{entity}Guid}}\");");
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

    private static void EditLogic(StringBuilder stringBuilder, string entity)
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
        stringBuilder.AppendLine($"\t                        Items?.Add(new(Input, 0));");
        stringBuilder.AppendLine($"\t                        Items?.Remove(Entity);");
        stringBuilder.AppendLine($"\t                        Page = new Patina.Patina(1, 1);");
        stringBuilder.AppendLine($"\t                        EntitiesOnThePage = [new(Input, 0)];");
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

    private static void AddLogic(StringBuilder stringBuilder)
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
        stringBuilder.AppendLine("                Items?.Add(new(Input, 0));");
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

    private static void DefaultUsingLogic(StringBuilder stringBuilder, string entity)
    {
        stringBuilder.AppendLine("using L = Logger.Logger;");
        stringBuilder.AppendLine("using U = ArchCorpUtilities.Utilities.UniversalUtilities;");
        stringBuilder.AppendLine("using CH = ArchCorpUtilities.Utilities.ConsoleHelper;");
        stringBuilder.AppendLine($"using ArchCorpUtilities.Models.{entity}Model.{entity};");
        stringBuilder.AppendLine("using ArchCorpUtilities.Models.Helper;");
        stringBuilder.AppendLine("using System.Text;");
    }

    private static void LoggingLogic(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("            if (SessionID != null)");
        stringBuilder.AppendLine("                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);");
        EmptyLineLogic(stringBuilder);
    }

    private static string PocoCodeTemplate(string folderName, string entity)
    {
        if (SessionID != null)
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

        StringBuilder stringBuilder = new();

        try
        {
            EmptyLineLogic(stringBuilder);
            stringBuilder.AppendLine($"namespace ArchCorpUtilities.Models.{folderName}.{entity};");
            stringBuilder.AppendLine($"public class {entity}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    public string? Name { get; }");
            stringBuilder.AppendLine("    public int Id { get; set; }");
            stringBuilder.AppendLine("    public int Index { get; set; }");
            stringBuilder.AppendLine("    public int DisplayId { get; internal set; }");
            stringBuilder.AppendLine("    public string? " + entity + "Guid {get; internal set; }");
            stringBuilder.AppendLine($"    public {entity}(string? name, int id, string guid = \"\")");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        Name = name;");
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
            stringBuilder.AppendLine("    public override string ToString() {return $\"{DisplayId}) {Name}\";}");
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

    private static string GetEntityPath(CodeTemplateEnum codeTemplate, string entity, string baseFolder)
    {
        var PrePath = $"{WorkingFolder}{baseFolder}\\{entity}Model\\{entity}";
        string? Path = codeTemplate switch
        {
            CodeTemplateEnum.Helper => $"{PrePath}Helper.cs",
            CodeTemplateEnum.POCO => $"{PrePath}.cs",
            _ => $"{PrePath}.cs",
        };
        return Path;
    }

    public static bool CreateDefaultCode(string entity)
    {
        if (string.IsNullOrWhiteSpace(entity))
            return false;

        var BaseFolder = "\\GeneratedModels";
        bool IsCreated = CreateCode(CodeTemplateEnum.POCO, entity, BaseFolder);
        if (IsCreated)
            IsCreated = CreateCode(CodeTemplateEnum.Helper, entity, BaseFolder);

        if (IsCreated)
        {
            List<CodePart> codeVault = [];
            var Header = GetGeneratedCodeHeader();

            CodePartMenuEnum codePartMenuEnum = new("\\Utilities", "UniversalUtilities.cs", entity, "{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePartMenuEnum);

            CodePartGenUsing codePart = new("\\Models", "ArchLoader.cs", entity, "{0ACDC688-3120-452F-94AE-2DD1771A9991}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePart);

            CodePartHelperInstance codePartHelperInstance = new("\\Models", "ArchLoader.cs", entity, "{048A4DD6-2F1B-4178-A732-E3B50D3F0791}", WorkingFolder, "", "\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartHelperInstance);

            CodePartISetInConstructorArch codePartISetInConstructorArch = new("\\Models", "ArchLoader.cs", entity, "{9ED7AF33-DE0E-45C3-821F-4669558AD744}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA");            
            codeVault.Add(codePartISetInConstructorArch);
            
            CodePartTargetTaskHelperUsing codePartTargetTaskHelperUsing = new("\\Models", "TargetTaskHelper.cs", entity, "{24D86755-6962-4074-BD9F-73E8FE0A5F68}", WorkingFolder, Header, "", SessionID ?? "TBA");
            codeVault.Add(codePartTargetTaskHelperUsing);

            CodePartNodal codePartNodal = new("\\Models", "TargetTaskHelper.cs", entity, "{2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0}", WorkingFolder, "", "\t\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartNodal);
            
            CodePartArchLoaderUsing codePartArchLoaderUsing = new("\\Models", "TargetTaskHelper.cs", entity, "{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}", WorkingFolder, "", "", SessionID ?? "TBA");
            codeVault.Add(codePartArchLoaderUsing);

            CodePartAdd codePartAdd = new("\\Models", "TargetTaskHelper.cs", entity, "{A6E3C6F1-D649-45CE-8C05-3A87466618A9}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartAdd);

            CodePartView codePartView = new("\\Models", "TargetTaskHelper.cs", entity, "{B03F74F5-9862-4916-9EF1-82DD253A5BC3}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartView);

            CodePartFirst codePartFirst = new("\\Models", "TargetTaskHelper.cs", entity, "{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartFirst);

            CodePartLast codePartLast = new("\\Models", "TargetTaskHelper.cs", entity, "{39C53717-4163-4B33-B652-4AA3B4D28C5B}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLast);

            CodePartNext codePartNext = new("\\Models", "TargetTaskHelper.cs", entity, "{BBDE47EF-8937-4545-A019-652A8A306B6E}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartNext);

            CodePartPrevious codePartPrevious = new("\\Models", "TargetTaskHelper.cs", entity, "{90D2D480-6963-441E-B2C4-E0EADC878A83}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartPrevious);

            CodePartEdit codePartEdit = new("\\Models", "TargetTaskHelper.cs", entity, "{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartEdit);

            CodePartRemove codePartRemove = new("\\Models", "TargetTaskHelper.cs", entity, "{11002DF2-E6AB-485E-B896-C3ED92706E30}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartRemove);

            CodePartSave codePartSave = new("\\Models", "TargetTaskHelper.cs", entity, "{36DE75D7-A730-4F6B-A7C9-4660245BD895}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartSave);

            CodePartLoad codePartLoad = new("\\Models", "TargetTaskHelper.cs", entity, "{BD92B12F-6AB8-420C-9A4B-654233721FB7}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLoad);

            CodePartSearch codePartSearch = new("\\Models", "TargetTaskHelper.cs", entity, "{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartSearch);

            CodePartRefresh codePartRefresh = new("\\Models", "TargetTaskHelper.cs", entity, "{B2FED166-7FCF-4163-8507-EB1CC28B6435}", WorkingFolder, "", "\t\t\t\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartRefresh);

            CodePartHiddenRules codePartHiddenRules = new("\\Models", "TargetTaskHelper.cs", entity, "{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}", WorkingFolder, "", "\t\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartHiddenRules);

            CodePartLoadDefaults codePartLoadDefaults = new("\\Models", "ArchLoader.cs", entity, "{E4C217C0-AC0D-4571-95E4-16CE056F35A5}", WorkingFolder, "", "\t\t\t", SessionID ?? "TBA");
            codeVault.Add(codePartLoadDefaults);

            int Counter = 0;
            foreach (var item in codeVault)
            {
                Counter++;
                if (!item.AlterCode())
                    break;
            }

            IsCreated = (Counter == codeVault.Count);
        }

        if (IsCreated)
            IsCreated = ClearGeneratedHeaders();

        return IsCreated;

    }

    private static bool CreateCode(CodeTemplateEnum codeTemplate, string entity, string basePath)
    {
        var TemplateCode = GenCode(codeTemplate, entity);
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

    private static string GetGeneratedCodeHeader()
    {
        return $"// Generated Code - Version: {U.GetVersion()} - {U.GetCurrentDate()} - {{{SessionID}}}\n\r";
    }

    public static bool ClearGeneratedHeaders()
    {

        List<string> HeaderSources = [];

        try
        {
            HeaderSources.Add("\\Models\\TargetTaskHelper.cs");
            HeaderSources.Add("\\Models\\ArchLoader.cs");
            HeaderSources.Add("\\Utilities\\UniversalUtilities.cs");

            foreach (var item in HeaderSources)
            {
                var Path = $"{WorkingFolder}\\{item}";
                if (File.Exists(Path))
                {
                    var Content = File.ReadAllText(Path);
                    var SplitContent = Content.Split("\n\r");
                    StringBuilder stringBuilder = new();
                    var Counter = 0;
                    foreach (string v in SplitContent)
                    {

                        if (v.Length > 0)
                        {
                            if (v.Contains("Generated"))
                                Counter++;
                            else
                                break;
                        }
                    }
                    stringBuilder.Append(GetGeneratedCodeHeader());
                    stringBuilder.AppendJoin("\n\r", SplitContent.ToList().GetRange(Counter, SplitContent.Length - Counter)).ToString();
                    var Output = stringBuilder.ToString();
                    stringBuilder.Clear();
                    File.Delete(Path);
                    File.WriteAllText(Path, Output);
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
            throw;
        }


    }

    #endregion
}
