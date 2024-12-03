using System.Text;
using L = Logger.Logger;
using F = Feedback.Feedback;
using C = CLILib.CLIP;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using G = ArchCorpUtilities.Utilities.CodeGenHelper;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using M = MenuEnumLib.MenuEnumLib;

namespace GenCodeLib
{
    public class GenCodeLib : IDisposable
    {
        string? SessionId;
        private static F? Feedback { get; set; }
        public GenCodeLib()
        {
            Feedback = new();
            L.CurrentLevel = 1;
        }

        public void Dispose()
        {
            Feedback?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void ProcessCode(string[] args, string[]? simulate = null)
        {
            CH.IsSimulate = (simulate != null);
            F.AddArgsReceivedToFeedback(simulate ?? args);

            var Result = C.ProcessArgsCode(args);

            var SplitResult = Result.Split('|');
            string? GuidPath = SplitResult[2];
            string Action = SplitResult[1];
            string? Content = string.Empty;
            string[]? SplitItems = null;

            GenerateCode(GuidPath, Action, ref Content, ref SplitItems);

            if (Action == "RollBack")
                RollBackCode("Roll back successful.", "Roll back failed.");

            if (Action == "Deploy")
                DeployCode();

        }

        private static void RollBackCode(string success, string failed)
        {
            CH.Feedback("Rolling back the code ...");
            U.ExecuteCMD("C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\ArchCorpUtilities\\RollbackCode.bat", success, failed);
        }


        private void GenerateCode(string GuidPath, string Action, ref string Content, ref string[]? SplitItems)
        {
            bool DeployCode = false;
            RollBackCode("Roll back successful", "Roll back failed.");
            StringBuilder Summary = new();

            if (IsGenerateCode(GuidPath, Action, ref Content, ref SplitItems) && SplitItems != null)
            {

                
                G.CurrentGuid = "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}";                
                G.SessionID = Guid.NewGuid().ToString();
                G.WorkingFolder = "C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\CodeGen\\";
                G.BackupFolder = $"{G.WorkingFolder}\\Backup";
                SessionId = G.SessionID;
                CH.SessionID = SessionId;
                U.SessionID = SessionId;

                CH.Feedback("Setting configuration ...");
                CH.Feedback($"GUID: {G.CurrentGuid}");
                CH.Feedback($"SessionID: {G.SessionID}");
                CH.Feedback($"WorkingFolder: {G.WorkingFolder}");
                CH.Feedback($"BackupFolder: {G.BackupFolder}");

                Summary.AppendLine($"----------------------------------------------------------------------------");
                Summary.AppendLine("Setting configuration ...");
                Summary.AppendLine($"GUID: {G.CurrentGuid}");
                Summary.AppendLine($"SessionID: {G.SessionID}");
                Summary.AppendLine($"WorkingFolder: {G.WorkingFolder}");
                Summary.AppendLine($"BackupFolder: {G.BackupFolder}");
                Summary.AppendLine($"----------------------------------------------------------------------------");

                foreach (var Item in SplitItems)
                {
                    var SplitLines = Item.Split('|');
                    CH.Feedback($"Checking if parameters match ...");
                    if (SplitLines.Length > 0 && SplitLines.Length == 5)
                    {
                        //var MenuName        = SplitLines[0];
                        var Entity = SplitLines[1];
                        var EntityType = SplitLines[2];
                        var LhLink = SplitLines[3];
                        var RhLink = SplitLines[4];

                        _ = Enum.TryParse(EntityType, out M.MenuTypeEnum MenuType);

                        CH.Feedback($"Processing {Entity} -- {EntityType} --- {LhLink} - {RhLink}");

                        if (G.CreateDefaultCode(Entity, MenuType, LhLink, RhLink))
                        {
                            if (SessionId != null)
                            {
                                L.Log($"Generated code {Entity}", SessionId, 5);
                                CH.Feedback($"Generated code {Entity}");
                                Summary.AppendLine($"Generated code {Entity}");
                                DeployCode = true;
                            }
                        }
                        else
                        {
                            if (SessionId != null)
                            {
                                L.Log($"Did NOT generate code {Entity}", SessionId, 5);
                                CH.Feedback($"did NOT generate code {Entity}");
                                Summary.AppendLine($"did NOT generate code {Entity}");
                            }
                        }
                    }
                }

                if (U.ClearGeneratedHeaders())
                    CH.Feedback($"Header information created.");
                else
                {
                    CH.Feedback($"Header information was not created.");
                }

                if (DeployCode)
                {
                    GenCodeLib.DeployCode();
                    CH.Feedback(Summary.ToString());
                }

            }
        }

        private static void DeployCode()
        {
            CH.Feedback("Deploying code ...");
            U.ExecuteCMD("C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\ArchCorpUtilities\\DeployCode.bat");
            U.ExecuteCMD("C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\ArchCorpUtilities\\DeployGenMenu.bat");            
        }

        private static bool IsGenerateCode(string GuidPath, string Action, ref string Content, ref string[]? SplitItems)
        {
            bool Continue = false;
            if (Action == "GenerateCode" && GuidPath?.Length > 0 && File.Exists(GuidPath))
                Content = File.ReadAllText(GuidPath);
            if (Content != null && Content.Length > 0)
                SplitItems = Content.Split(Environment.NewLine);
            if (SplitItems != null && SplitItems?.Length > 0)
                Continue = true;
            return Continue;
        }
    }
}
