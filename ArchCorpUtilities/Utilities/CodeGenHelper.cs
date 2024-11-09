using ArchCorpUtilities.Models.Helper;
using ArchCorpUtilities.Utilities.CodeGen;

//using ArchCorpUtilities.Models.SitesModel.Sites;
using System.Text;
using L = Logger.Logger;

namespace ArchCorpUtilities.Utilities;

public static class CodeGenHelper
{
    public static string? SessionID { get; set; }
    public static string WorkingFolder { get; set; }
    public static string TargetWorkingFolder { get; set; }
    public static string Version { get; set; }

    static CodeGenHelper()
    {
        WorkingFolder = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\CodeGen";
        TargetWorkingFolder = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities";
        Version = "1.0.0";
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
            AddGenerationComment(stringBuilder);
            stringBuilder.AppendLine("using ArchCorpUtilities.Models.Helper;");
            stringBuilder.AppendLine($"using ArchCorpUtilities.GeneratedModels.{folderName};");
            stringBuilder.AppendLine("using ArchCorpUtilities.Utilities;");
            stringBuilder.AppendLine($"using ArchCorpUtilities.Models.{entity}Model.{entity};");
            AddDefaultUsingStatments(stringBuilder);
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"namespace ArchCorpUtilities.GeneratedModels.{folderName}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    public class {entity}Helper : IHelper<{entity}>, IDisposable");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        public string? SessionID { get; set; }");
            stringBuilder.AppendLine("        public List<" + entity + ">? Items { get; set; }");
            stringBuilder.AppendLine("        public List<" + entity + ">? EntitiesOnThePage { get; set; }");
            stringBuilder.AppendLine("        public Patina.Patina Page { get; set; }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"       public {entity}Helper(string? sessionID)");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            SessionID = sessionID;");
            stringBuilder.AppendLine("            Items = MockData();");
            stringBuilder.AppendLine("            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Items?.Count));");
            stringBuilder.AppendLine("        }");

            stringBuilder.AppendLine($"        private List<{entity}>? MockData()");
            stringBuilder.AppendLine("        {");
            stringBuilder.AppendLine("            if (Items == null || Items.Count == 0)");
            stringBuilder.AppendLine("                Items = [];");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Alpha-{entity}\", 1));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Beta-{entity}\", 2));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Charlie-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Delta-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Echo-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Foxtrot-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Golf-{entity}\", 3));");
            stringBuilder.AppendLine($"            Items.Add(new {entity}(\"Hotel-{entity}\", 3));");
            stringBuilder.AppendLine("            return Items;");
            stringBuilder.AppendLine("        }");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool View(U.Navigation navigate = U.Navigation.FirstPage)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            ViewDefaultCode(stringBuilder, entity);
            stringBuilder.AppendLine("        }");

            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool Add(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("            return true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public void Dispose()");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool Edit(int? simChoice, string[]? simInput)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("            return true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool IsItemsOnThePage()");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            stringBuilder.AppendLine("            return !(EntitiesOnThePage == null || (EntitiesOnThePage != null && EntitiesOnThePage.Count == 0));");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool Load(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("            return true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"        public bool Refresh(List<{entity}> modelList, U.Navigation navigate = U.Navigation.FirstPage)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            RefreshDefaultCode(stringBuilder, entity);
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool Remove(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("            return true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool Save(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("            return true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public bool Search(int? simChoice = null, string[]? simInput = null)");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            UnderConstructionMessage(stringBuilder);
            stringBuilder.AppendLine("            return true;");
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("        public void ReIndexDisplayId()");
            stringBuilder.AppendLine("        {");
            AddLoggingForMethod(stringBuilder);
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("            var sites = Items?.OrderBy(c => c.Name).ToList();");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("            int counter = 1;");
            stringBuilder.AppendLine("            if (sites != null)");
            stringBuilder.AppendLine("            {");
            stringBuilder.AppendLine("                foreach (var item in sites)");
            stringBuilder.AppendLine("                {");
            stringBuilder.AppendLine("                    item.DisplayId = counter++;");
            stringBuilder.AppendLine("                    item.Id = item.DisplayId;");
            stringBuilder.AppendLine("                }");
            stringBuilder.AppendLine("            }");
            stringBuilder.AppendLine("        }");
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

    private static void UnderConstructionMessage(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("            CH.Feedback(\"Under Construction\");");
    }

    private static void ViewDefaultCode(StringBuilder stringBuilder, string entity)
    {
        stringBuilder.AppendLine("            var orderedEntities = Items?.OrderBy(p => p.Index).ToList();");
        stringBuilder.AppendLine($"           EntitiesOnThePage = U.ViewWithPagination(\"{entity}\", Page, orderedEntities, navigate);");
        stringBuilder.AppendLine("            return true;");
    }

    private static void RefreshDefaultCode(StringBuilder stringBuilder, string entity)
    {
        stringBuilder.AppendLine("            var orderedEntities = modelList?.OrderBy(p => p.Index).ToList();");
        stringBuilder.AppendLine($"           EntitiesOnThePage = U.ViewWithPagination(\"{entity}\", Page, orderedEntities, navigate);");
        stringBuilder.AppendLine("            return true;");
    }

    private static void AddDefaultUsingStatments(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("using L = Logger.Logger;");
        stringBuilder.AppendLine("using U = ArchCorpUtilities.Utilities.UniversalUtilities;");
        stringBuilder.AppendLine("using CH = ArchCorpUtilities.Utilities.ConsoleHelper;");
    }

    private static void AddLoggingForMethod(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("            if (SessionID != null)");
        stringBuilder.AppendLine("                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);");
        stringBuilder.AppendLine("");
    }

    private static string PocoCodeTemplate(string folderName, string entity)
    {
        if (SessionID != null)
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

        StringBuilder stringBuilder = new();

        try
        {
            AddGenerationComment(stringBuilder);
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

    private static void AddGenerationComment(StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine($"//Generated code : {DateTime.Now} - Session : {SessionID}");
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

        BaseFolder = "\\Utilities";
        var TargetFile = "UniversalUtilities.cs";
        if (IsCreated)
            IsCreated = AlterCode(entity, BaseFolder, TargetFile, "{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}", "//{E401C6FC-99B7-41B0-A612-8DABFE8734C3}");

        if (IsCreated)
        {

            List<CodePart> codeVault = [];
            var Header = GetGeneratedCodeHeader();
            
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

            int Counter = 0;
            foreach (var item in codeVault)
            {
                Counter++;
                if (!item.AlterCode())
                    break;
            }

            IsCreated = (Counter == codeVault.Count);
        }

        return IsCreated;

    }

    private static bool AlterCode(string entity, string baseFolder, string targetFile, string searchString, string entryPoint)
    {
        var TargetPath = $"{WorkingFolder}{baseFolder}\\{targetFile}";

        try
        {
            if (File.Exists(TargetPath))
            {
                var Context = File.ReadAllText(TargetPath);
                if (string.IsNullOrWhiteSpace(Context))
                    return false;
                else
                {
                    var SelectedContext = Context.Split($"//{searchString}");
                    if (SelectedContext.Length is > 0 and 3)
                    {
                        var CodeToAlter = SelectedContext[1];
                        // Only alter the code if the entity in question does not exist.
                        if (!CodeToAlter.Contains(entity))
                        {
                            StringBuilder stringBuilder = new();
                            var PlaceHolder = $"\r\n            {entryPoint}";
                            var CloseBracketPart = "\r\n        }\r\n        ";

                            stringBuilder.Append($"//{searchString}");
                            stringBuilder.Append(SelectedContext[1].Replace(PlaceHolder, "").Replace(CloseBracketPart, ""));
                            stringBuilder.Append($",\r\n            {entity}{PlaceHolder}{CloseBracketPart}");
                            stringBuilder.Append($"//{searchString}");

                            var CodeAltered = stringBuilder.ToString();

                            stringBuilder.Clear();
                            stringBuilder.Append(GetGeneratedCodeHeader());
                            stringBuilder.Append(SelectedContext[0]);
                            stringBuilder.Append(CodeAltered);
                            stringBuilder.Append(SelectedContext[2]);
                            var AlteredFile = stringBuilder.ToString();
                            stringBuilder.Clear();
                            
                            if (File.Exists(targetFile))
                                File.Delete(TargetPath);

                            File.WriteAllText(TargetPath, AlteredFile);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        ////Check to find two of them.



        //// Add the domain if it does not exist.
        ////Domain GUID : { F8FE36D7 - 3F08 - 48BA - 9CAB - FBAA102C8149}
        //if (SessionID != null)
        //    L.Log("Domain GUID : {F8FE36D7 - 3F08 - 48BA - 9CAB - FBAA102C8149}", SessionID);
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
        return $"// Generated Code - Version: {Version} - {DateTime.Now} - {{{SessionID}}} \n\r";
    }

    //public static bool RollBack(string entity)
    //{
    //    try
    //    {
    //        var EntityFile = GetEntityPath(CodeTemplateEnum.Helper, entity, "\\GeneratedModels");
    //        if (File.Exists(EntityFile))
    //            File.Delete(EntityFile);
    //        EntityFile = GetEntityPath(CodeTemplateEnum.POCO, entity, "\\GeneratedModels");
    //        if (File.Exists(EntityFile))
    //            File.Delete(EntityFile);
    //        var ModelFolder = $"{TargetWorkingFolder}\\GeneratedModels\\{entity}Model";
    //        if (Directory.Exists(ModelFolder))
    //            Directory.Delete(ModelFolder);

    //        var Result = RollBackAlteredCode("\\Utilities", "UniversalUtilities.cs");

    //        return Result;
    //    }
    //    catch (Exception)
    //    {

    //        return false;
    //    }

    //}

    //private static bool RollBackAlteredCode(string baseFolder, string targetFile)
    //{
    //    try
    //    {
    //        if (SessionID != null)
    //        {
    //            var Backup = targetFile.Replace(".cs", $".{SessionID}");
    //            var Source = $"{WorkingFolder}{baseFolder}\\{Backup}";
    //            var Target = $"{WorkingFolder}{baseFolder}\\{targetFile}";
    //            if (File.Exists(Target) && File.Exists(Source))
    //                File.Delete(Target);
    //            if (File.Exists(Source))
    //                File.Copy(Source, Target);
    //            //File.Delete(Source);
    //            return true;
    //        }
    //        else
    //            return false;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //}

    #endregion
}
