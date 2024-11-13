// Generated Code - Version: 14.11.22 - 2024/11/12 15:23:07 - {b8a2c482-4992-45c4-84df-80204f82066f}

using ArchCorpUtilities.Utilities;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using TTH = ArchCorpUtilities.Models.TargetTaskHelper;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

//{0ACDC688-3120-452F-94AE-2DD1771A9991}
using BeaconsHelper = ArchCorpUtilities.GeneratedModels.BeaconsModel.BeaconsHelper;
using BuildingsHelper = ArchCorpUtilities.GeneratedModels.BuildingsModel.BuildingsHelper;
//{0ACDC688-3120-452F-94AE-2DD1771A9991}

using ArchCorpUtilities.Models.Menus;

namespace ArchCorpUtilities.Models
{
    public static class ArchLoader
    {
        
        public static string SessionID { get; }

        //{048A4DD6-2F1B-4178-A732-E3B50D3F0791}
        internal static BeaconsHelper BeaconsHelper;
		internal static BuildingsHelper? BuildingsHelper = null;
		//{048A4DD6-2F1B-4178-A732-E3B50D3F0791}

        static ArchLoader()
        {
            SessionID = Guid.NewGuid().ToString();
            MH.SessionID = SessionID;
            MMH.SessionID = SessionID;
            U.SessionID = SessionID;
            TTH.SessionID = SessionID;
            CodeGen.SessionID = SessionID;

            //{9ED7AF33-DE0E-45C3-821F-4669558AD744}
            BeaconsHelper = new(SessionID);
			BuildingsHelper = new(SessionID);
			//{9ED7AF33-DE0E-45C3-821F-4669558AD744}

        }

        public static void RunArch(List<Command>? commands = null, int logLevel = 1)
        {
            //{E4C217C0-AC0D-4571-95E4-16CE056F35A5}
            BuildingsHelper?.LoadDefaults();
            BeaconsHelper?.LoadDefaults();
            //{E4C217C0-AC0D-4571-95E4-16CE056F35A5}

            if (File.Exists(CodeGenHelper.CurrentGuid))
                MMH.Load(CodeGenHelper.CurrentGuid);
			
            L.CurrentLevel = logLevel;
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            int? SimChoice = null;
            string? SimInput = null;
            string[]? SimInputValues = null;

            if (commands == null || commands.Count == 0)
            {
                CH.IsSimulate = false;
            }
            if (commands != null)
            {
                CH.IsSimulate = true;
                //Simulate
                SimChoice = 1; // just to get in the loop
                SimInput = "";
                SimInputValues = null;
            }
            //Retrieve starting page
            int? TargetPage = MH.Menu?.FirstOrDefault(c => c.IsStartPage == true)?.Page;
            
            if (TargetPage != null)
            {
                var PrevPagenumber = TargetPage;
                var PageHeading = MH.Menu?.FirstOrDefault(c => c.Page == TargetPage)?.PageHeading;


                int Choice = ShowMenu(TargetPage, PageHeading, SimChoice);


                var ExitOption = MH.Menu?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;

                L.Log($"Before loop - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 0);

                int Counter = 0;
                while (Choice != ExitOption)
                {

                    if (CH.IsSimulate && commands != null && Counter < commands?.Count)
                    {
                        SimInput = commands[Counter].Value;
                        SimChoice = commands[Counter].Choice;
                        SimInputValues = commands[Counter].Values;
                        if (SimChoice != null)
                            Choice = SimChoice.Value;
                        CH.Feedback($"Simulate: {SimInput} -- {SimChoice} --- {SimInputValues?.ToString()}");
                        Counter++;
                        L.Log($"Inside loop - Simulation - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);
                    }



                    if (!CH.IsSimulate) { Console.Clear(); }
                    if (CH.IsSimulate) { CH.Feedback(Resource.ClearConsoleMessage); }

                    var menuItem = MH.Menu?.FirstOrDefault(c => c.DisplayNumber == Choice && c.Page == TargetPage && c.IsHidden == false);

                    if (menuItem != null && menuItem.TargetPage != 0) { TargetPage = menuItem.TargetPage; }

                    PageHeading = MH.Menu?.FirstOrDefault(c => c.Page == TargetPage.Value)?.PageHeading;

                    ExitOption = MH.Menu?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage.Value)?.DisplayNumber;

                    L.Log($"Inside loop - Before PerformDefaultTasks - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);


                    TTH.PerformDefaultTasks(TargetPage.Value, PrevPagenumber.Value, Choice, SimChoice, SimInput, SimInputValues);
                                      
                    Choice = ShowMenu(TargetPage.Value, PageHeading, SimChoice);

                    PrevPagenumber = TargetPage.Value;

                    if (Counter == commands?.Count)
                        ExitOption = Choice;
                    else
                        ExitOption = MH.Menu?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;
                }
                if (CH.IsSimulate) { CH.Feedback(Resource.ExitIndicator); }
                L.Log($"After loop - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);
            }
            else
            {
                CH.Feedback(Resource.TargetPageNotSet);
                L.Log($"TargetPage is not marked - fix the menu structure", SessionID, 9);
            }
        }

        static int ShowMenu(int? page, string? menuHeading, int? simChoice = null)
        {
            CH.Feedback(MH.ShowMenu(menuHeading, page));

            if (simChoice == null)
            {
                _ = int.TryParse(CH.GetInput(), out int Choice);
                simChoice = Choice;
            }

            return simChoice.Value;
        }

    }
}
