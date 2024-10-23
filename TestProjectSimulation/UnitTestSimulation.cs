using System.Text;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using MBR = ArchCorpUtilities.Models.MockBuildingsRepository;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using AL = ArchCorpUtilities.Models.ArchLoader;
using AC = ArchCorpUtilities.Utilities.Command;
using ArchCorpUtilities.Models.Buildings;
using System.Collections.Generic;


namespace TestProjectSimulation
{
    public class UnitTestSimulation
    {
        [Fact]
        public void Valid_Sim_AddBuilding_2_TestOne_GoBack()
        {
            // On the main menu
            // Choice 2 with value TestOne - Add - TestOne
            // Choice 2 - Back to Main Menu
            List<AC> commands =
                [new AC(2, "TestOne"),
                new AC(2, "")
                ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            Assert.Contains("Building added successfully", Result);
            Assert.Contains("TestOne", Result);
        }

        [Fact]
        public void ValidListCheckIfTestOneIsThere()
        {
            //List and go back to main
            List<AC> commands =
            [
             new AC(2, "TestOne"),
             new AC(2, ""),
             new AC(1, ""),
             new AC(2, "")
            ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            Assert.Contains("TestOne", Result);
        }



    }
}