using System.ComponentModel.DataAnnotations;
using AL = ArchCorpUtilities.Models.ArchLoader;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;


namespace Ad_hoc_Tests
{
    public class Ad_Hoc_Tests
    {
        [Fact]
        public void ValidShowMenu_Arch_Simulated()
        {
            //{0EFC2DF7-9635-48A9-8A37-ED03992483F6}
            CH.ClearFeedback();
            var Result = AL.ShowMenu(1, "Test", 1);
            Assert.True(Result == 1);
        }

        [Fact]
        public void ValidShowMenu_Heading_Arch_Simulated()
        {
            //{0EFC2DF7-9635-48A9-8A37-ED03992483F6}
            CH.ClearFeedback();
            CH.IsSimulate = true;
            AL.ShowMenu(1, "TestHeading", 1);
            var Feedback = CH.GetFeedback();
            Assert.Contains("TestHeading", Feedback);
            Assert.Contains("<Version>", Feedback);
            Assert.Contains("Choice:", Feedback);
        }
    }
}