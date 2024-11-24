namespace GenMenuHelperLib
{
    using L = Logger.Logger;
    using F = Feedback.Feedback;
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
    using C = CLILib.CLIP;
    using G = ArchCorpUtilities.Utilities.CodeGenHelper;
    using U = ArchCorpUtilities.Utilities.UniversalUtilities;
    using M = MenuEnumLib.MenuEnumLib;
    using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;

    public class GenMenuHelperLib : IDisposable
    {
        static string? SessionId { get; set; }
        private static F? Feedback { get; set; }


        public GenMenuHelperLib()
        {
            Feedback = new();
            
            L.CurrentLevel = 1;
        }        

        public static void ProcessArgs(string[] args, string[]? simulate)
        {
            SessionId = Guid.NewGuid().ToString();
            CH.IsSimulate = (simulate != null);
            F.AddArgsReceivedToFeedback(simulate ?? args);

            var Result = C.ProcessArgs(args);

            var SplitResult = Result.Split('|');
            string? GuidPath = SplitResult[2];
            string Action = SplitResult[1];
            string? Content = string.Empty;

            if (Action == "GenerateMenu")
            { 
                if(MMH.GenerateDefaultMenus(GuidPath, simulate))
                {
                    U.ExecuteCMD("C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\ArchCorpUtilities\\DeployMenu.bat");
                }
            }
                

        }

        public void Dispose()
        {
            Feedback?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}
