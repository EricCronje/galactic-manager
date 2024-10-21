using System.Text;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using MBR = ArchCorpUtilities.Models.MockBuildingsRepository;
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
        public void Valid_Sim_AddBuilding_2_Testone_GoBack()
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
            string Expected = "-------------------------\r\nBuilding Manager - Main Menu - V1.0.0\r\n-------------------------\r\n1) List Buildings\r\n2) Add Buildings\r\n3) Remove Buildings\r\n4) Edit Building Names\r\n5) Save Buildings\r\n6) Load Buildings\r\n7) Exit\r\n-------------------------\r\nChoice:\r\nSimulate: TestOne -- 2\r\n<ClearConsole>\r\n(Hint: Press x if you do not want to enter a building name.)\r\nEnter the building name:\r\nSimulate: TestOne\r\nBuilding added successfully\r\n-------------------------\r\nAdd - Sub Menu\r\n-------------------------\r\n1) Add Building\r\n2) Back to Main Menu\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2\r\n<ClearConsole>\r\n-------------------------\r\nBuilding Manager - Main Menu - V1.0.0\r\n-------------------------\r\n1) List Buildings\r\n2) Add Buildings\r\n3) Remove Buildings\r\n4) Edit Building Names\r\n5) Save Buildings\r\n6) Load Buildings\r\n7) Exit\r\n-------------------------\r\nChoice:\r\n";
            Assert.Equal(Expected, Result);
            Assert.Contains("Building added successfully", Result);
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
            string Expected =
            "-------------------------\r\nBuilding Manager - Main Menu - V1.0.0\r\n-------------------------\r\n1) List Buildings\r\n2) Add Buildings\r\n3) Remove Buildings\r\n4) Edit Building Names\r\n5) Save Buildings\r\n6) Load Buildings\r\n7) Exit\r\n-------------------------\r\nChoice:\r\nSimulate: TestOne -- 2\r\n<ClearConsole>\r\n(Hint: Press x if you do not want to enter a building name.)\r\nEnter the building name:\r\nSimulate: TestOne\r\nNo building added.\r\n-------------------------\r\nAdd - Sub Menu\r\n-------------------------\r\n1) Add Building\r\n2) Back to Main Menu\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2\r\n<ClearConsole>\r\n-------------------------\r\nBuilding Manager - Main Menu - V1.0.0\r\n-------------------------\r\n1) List Buildings\r\n2) Add Buildings\r\n3) Remove Buildings\r\n4) Edit Building Names\r\n5) Save Buildings\r\n6) Load Buildings\r\n7) Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1\r\n<ClearConsole>\r\n-------------------------\r\nBuildings list:\r\n-------------------------\r\n1) Alpha\r\n2) Beta\r\n3) Dragon One\r\n4) TestOne\r\n-------------------------\r\nView - Sub Menu\r\n-------------------------\r\n1) View Building\r\n2) Back to Main Menu\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2\r\n<ClearConsole>\r\n-------------------------\r\nBuilding Manager - Main Menu - V1.0.0\r\n-------------------------\r\n1) List Buildings\r\n2) Add Buildings\r\n3) Remove Buildings\r\n4) Edit Building Names\r\n5) Save Buildings\r\n6) Load Buildings\r\n7) Exit\r\n-------------------------\r\nChoice:\r\n";
            Assert.Equal(Expected, Result);
            // Is TestOne there?
            Assert.Contains("TestOne", Result);
        }



    }
}