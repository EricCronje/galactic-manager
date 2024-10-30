using AC = ArchCorpUtilities.Utilities.Command;
using AL = ArchCorpUtilities.Models.ArchLoader;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;


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

        [Fact]
        public void ValidCheckSessionIDOnArchRun()
        {
            Assert.True(AL.SessionID.Length > 0);
        }

    }
}