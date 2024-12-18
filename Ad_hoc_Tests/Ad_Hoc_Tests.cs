using System.ComponentModel.DataAnnotations;
using AL = ArchCorpUtilities.Models.ArchLoader;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;

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
            Assert.Equal(1, Result);
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

        [Fact]
        public void Valid_Get_Manage_MenuOptions()
        {
            //{205C93CD-7D5C-40EF-B93E-576465ADA007}
            var menu = MH.ImportMenu("{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}");
            Assert.NotNull(menu);
        }
    }
}