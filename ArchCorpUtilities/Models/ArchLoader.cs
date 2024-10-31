using ArchCorpUtilities.Utilities;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
namespace ArchCorpUtilities.Models
{
    public static class ArchLoader
    {

        public static string SessionID { get; }

        static ArchLoader()
        {
            SessionID = Guid.NewGuid().ToString();
            MH.SessionID = SessionID;
            BH.SessionID = SessionID;
        }

        public static void RunArch(List<Command>? commands = null, int logLevel = 1)
        {
            L.CurrentLevel = logLevel;
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            int? SimChoice = null;
            string? SimInput = null;

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
            }
            //Retrieve starting page
            int? TargetPage = MH.Menus?.FirstOrDefault(c => c.IsStartPage == true)?.Page;
            
            if (TargetPage != null)
            {
                var PrevPagenumber = TargetPage;
                var PageHeading = MH.Menus?.FirstOrDefault(c => c.Page == TargetPage)?.PageHeading;


                int Choice = ShowMenu(TargetPage, PageHeading, SimChoice);


                var ExitOption = MH.Menus?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;

                L.Log($"Before loop - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 0);

                int Counter = 0;
                while (Choice != ExitOption)
                {

                    if (CH.IsSimulate && commands != null && Counter < commands?.Count)
                    {
                        SimInput = commands[Counter].Value;
                        SimChoice = commands[Counter].Choice;
                        if (SimChoice != null)
                            Choice = SimChoice.Value;
                        CH.Feedback($"Simulate: {SimInput} -- {SimChoice}");
                        Counter++;
                        L.Log($"Inside loop - Simulation - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);
                    }



                    if (!CH.IsSimulate) { Console.Clear(); }
                    if (CH.IsSimulate) { CH.Feedback(Resource.ClearConsoleMessage); }

                    var menuItem = MH.Menus?.FirstOrDefault(c => c.DisplayNumber == Choice && c.Page == TargetPage && c.IsHidden == false);

                    if (menuItem != null && menuItem.TargetPage != 0) { TargetPage = menuItem.TargetPage; }

                    PageHeading = MH.Menus?.FirstOrDefault(c => c.Page == TargetPage.Value)?.PageHeading;

                    ExitOption = MH.Menus?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage.Value)?.DisplayNumber;

                    L.Log($"Inside loop - Before PerformDefaultTasks - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);


                    BH.PerformDefaultTasks(TargetPage.Value, PrevPagenumber.Value, Choice, SimChoice, SimInput);

                    Choice = ShowMenu(TargetPage.Value, PageHeading, SimChoice);

                    PrevPagenumber = TargetPage.Value;

                    if (Counter == commands?.Count)
                    {
                        ExitOption = Choice;
                    }
                    else
                    {
                        ExitOption = MH.Menus?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;
                    }
                }
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
