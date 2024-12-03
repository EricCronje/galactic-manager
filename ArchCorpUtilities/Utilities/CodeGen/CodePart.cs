
using System;
using System.Text;
using static ArchCorpUtilities.Utilities.CodeGenHelper;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;
using ArchCorpUtilities.Models;

namespace ArchCorpUtilities.Utilities.CodeGen
{
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

    public abstract class CodePart
    {
        public string? BaseFolder { get; set; }
        public string TargetFile { get; }
        public string Entity { get; }
        public string SearchString { get; }
        public static string? WorkingFolder { get; set; }
        public string Heading { get; }
        public string Tabs { get; }
        public static string? SessionID { get; set; }
        public string? LhLink { get; set; }
        public string? RhLink { get; set; }

        public CodePart(string? baseFolder, string targetFile, string entity, string searchString, string workingFolder, string heading, string searchStringPostPart, string sessionID, string? lHLink = null, string? rHLink = null)
        {
            BaseFolder = baseFolder;
            TargetFile = targetFile;
            Entity = entity;
            SearchString = searchString;
            WorkingFolder = workingFolder;
            Heading = heading;
            Tabs = searchStringPostPart;
            SessionID = sessionID;
            LhLink = lHLink;
            RhLink = rHLink;
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
            catch (Exception err)
            {
                if (SessionID != null) { L.Log($"Error in AlterCode - {err.Message} -- {err.InnerException?.Message} -- {SearchString}", SessionID, 9); }                
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
            U.RemoveFirstLineFeed(SelectedContext[2], stringBuilder);
            var AlteredFile = stringBuilder.ToString();
            stringBuilder.Clear();
            return AlteredFile;
        }

        public static bool CreateCode(M.CodeTemplateEnum codeTemplate, string entity, string baseFolder, string? lhLink, string? rhLink)
        {
            var TemplateCode = GenCode(codeTemplate, entity, lhLink, rhLink);
            if (TemplateCode != "<NoData>")
            {
                var EntityFile = GetEntityPath(codeTemplate, entity, baseFolder);

                if (File.Exists(EntityFile))
                    File.Delete(EntityFile);

                TemplateCode = U.GetGeneratedCodeHeader() + TemplateCode;

                File.WriteAllText(EntityFile, TemplateCode);
                return true;
            }
            else
                return false;
        }

        private static string? GetTemplateCode(string entity, M.CodeTemplateEnum name, string? baseFolder,string? lHLink, string? rHLink)
        {
            if (baseFolder == null) { return null; }

            string? code = null;
            switch (name)
            {
                case M.CodeTemplateEnum.Helper:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\Default\\EntityHelper.cs");
                    break;
                case M.CodeTemplateEnum.HelperLink:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\DefaultLink\\EntityHelper.cs");
                    break;
                case M.CodeTemplateEnum.POCO:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\Default\\Entity.cs");
                    break;
                case M.CodeTemplateEnum.POCOLink:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\DefaultLink\\Entity.cs");
                    break;
                case M.CodeTemplateEnum.MockRepository:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\Default\\EntityMockRepository.cs");
                    break;
                case M.CodeTemplateEnum.MockRepositoryLink:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\DefaultLink\\EntityMockRepository.cs");
                    break;
                case M.CodeTemplateEnum.POCOHierarchy:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\Hierarchy\\Entity.cs");
                    break;
                case M.CodeTemplateEnum.HierarchyHelper:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\Hierarchy\\EntityHelper.cs");
                    break;
                case M.CodeTemplateEnum.MockRepositoryHierarchy:
                    code = U.GetTemplate(entity, lHLink, rHLink, $"{CodeGenHelper.WorkingFolder}\\Code\\Hierarchy\\EntityMockRepository.cs");
                    break;
                default:
                    break;
            }

            return code;
        }

        private static string? GetTemplate(M.CodeTemplateEnum name, string entity, string baseFolder, string? lhLink, string? rhLink)
        {
            if (lhLink == "None") { lhLink = null; }
            if (rhLink == "None") { rhLink = null; }
            return GetTemplateCode(entity, name, baseFolder, lhLink, rhLink) ?? "<NoData>";
        }

        private static string? GenCode(M.CodeTemplateEnum name, string entity, string? lhLink, string? rhLink)
        {
            #region Log that the GenCode method is running.
            #endregion
            #region Preparing. Make sure that all the directories exists. If not Create them. Clear out the old code files. Will indicate to continue.
            var BaseFolder = "\\GeneratedModels";
            string[] FolderNames = [$"{entity}Model"];
            bool Continue = PrepareFolders($"{WorkingFolder}{BaseFolder}\\", FolderNames);
            #endregion
            if (Continue)
            {
                #region Looking for the code in the repository.  Will return the code once found.
                var Code = GetTemplate(name, entity, $"{entity}Model", lhLink, rhLink);
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

        private static bool PrepareFolders(string workingFolder, string[] folderNames)
        {
            try
            {
                if (!Directory.Exists($"{workingFolder}"))
                    Directory.CreateDirectory($"{workingFolder}");

                foreach (var folderName in folderNames)
                {
                    if (!Directory.Exists($"{workingFolder}\\{folderName}"))
                        Directory.CreateDirectory($"{workingFolder}\\{folderName}");
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string GetEntityPath(M.CodeTemplateEnum codeTemplate, string entity, string baseFolder)
        {
            var PrePath = $"{WorkingFolder}{baseFolder}\\{entity}Model\\{entity}";
            string? Path = codeTemplate switch
            {
                M.CodeTemplateEnum.HelperLink => $"{PrePath}Helper.cs",
                M.CodeTemplateEnum.Helper => $"{PrePath}Helper.cs",
                M.CodeTemplateEnum.POCO => $"{PrePath}.cs",
                M.CodeTemplateEnum.POCOLink => $"{PrePath}.cs",
                M.CodeTemplateEnum.MockRepository => $"{PrePath}MockRepository.cs",
                M.CodeTemplateEnum.MockRepositoryLink => $"{PrePath}MockRepository.cs",
                M.CodeTemplateEnum.MockRepositoryHierarchy => $"{PrePath}MockRepository.cs",
                M.CodeTemplateEnum.POCOHierarchy => $"{PrePath}.cs",
                M.CodeTemplateEnum.HierarchyHelper => $"{PrePath}Helper.cs",
                _ => $"{PrePath}.cs",
            };
            return Path;
        }

    }
}