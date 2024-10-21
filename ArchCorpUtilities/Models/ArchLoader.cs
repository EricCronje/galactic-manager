using ArchCorpUtilities.Utilities;
using AM = ArchCorpUtilities.Models.Menus.MenuHelper;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

namespace ArchCorpUtilities.Models
{
    public static class ArchLoader
    {


        public static void RunArch(List<Command>? commands = null)
        {
            int? SimChoice = null;
            string? SimInput = null;

            if (commands == null || commands.Count == 0)
            {
                CH.IsSimulate = false;
                CH.Feedback(Resource.SimulationNotSet);
            }
            if (commands != null)
            {
                CH.IsSimulate = true;
                //Simulate
                SimChoice = 1; // just to get in the loop
                SimInput = "";
            }

            int? TargetPage = 1;
            var PrevPagenumber = TargetPage;
            var PageHeading = AM.CurrentMenuPage?.FirstOrDefault(c => c.Page == TargetPage)?.PageHeading;


            int Choice = ShowMenu(TargetPage, PageHeading, SimChoice);


            var ExitOption = AM.CurrentMenuPage?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;


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
                }

                if (!CH.IsSimulate) { Console.Clear(); }
                if (CH.IsSimulate) { CH.Feedback(Resource.ClearConsoleMessage); }

                var menuItem = AM.CurrentMenuPage?.FirstOrDefault(c => c.DisplayNumber == Choice && c.Page == TargetPage);

                if (menuItem != null && menuItem.TargetPage != 0) { TargetPage = menuItem.TargetPage; }

                PageHeading = AM.CurrentMenuPage?.FirstOrDefault(c => c.Page == TargetPage.Value)?.PageHeading;
                ExitOption = AM.CurrentMenuPage?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage.Value)?.DisplayNumber;

                BH.PerformDefaultTasks(TargetPage.Value, PrevPagenumber.Value, Choice, SimChoice, SimInput);

                Choice = ShowMenu(TargetPage.Value, PageHeading, SimChoice);

                PrevPagenumber = TargetPage.Value;

                if (Counter == commands?.Count)
                    ExitOption = Choice;
            }
        }



        static int ShowMenu(int? page, string? menuHeading, int? simChoice = null)
        {
            CH.Feedback(AM.ShowMenu(menuHeading, page));

            if (simChoice == null)
            {
                _ = int.TryParse(CH.GetInput(), out int Choice);
                simChoice = Choice;
            }

            return simChoice.Value;
        }

    }
}
