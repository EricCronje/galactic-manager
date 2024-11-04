namespace TestProjectMenus
{
    using MBR = ArchCorpUtilities.Models.MockBuildingsRepository;
    using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
    using AC = ArchCorpUtilities.Utilities.Command;
    using AL = ArchCorpUtilities.Models.ArchLoader;
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
    using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
    using ArchCorpUtilities.Models.Menus;

    public class UnitTestMenus
    {
        [Fact]
        public void ValidTestMapMenus()
        {
            var result = BH.ViewWithPagination("Menu List", BH.Page, MH.Menu, BH.Navigation.FirstPage );
            Assert.NotNull(result);
        }

        [Fact]
        public void ValidTestStartMenu()
        {
            List<AC> commands =
                  [
                  new AC(null, "")
                  ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            Assert.Contains("Galaxy Manager", Result);
            Assert.Contains("1 Manage Buildings", Result);
            Assert.Contains("2 Manage Menus", Result);
            Assert.Contains("3 Exit", Result);

            Assert.NotNull(Result);
        }

        [Fact]
        public void ValidTestMenuMaintenance()
        {
            List<AC> commands =
                  [
                  new AC(2, "")
                  ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            Assert.Contains("1 View Menus", Result);
            Assert.Contains("2 Add Menus", Result);
            Assert.Contains("3 Remove Menus", Result);
            Assert.Contains("4 Edit Menus", Result);
            Assert.Contains("5 Save Menu", Result);
            Assert.Contains("6 Load Menu", Result);
            Assert.Contains("7 Back to Main Menu", Result);
            Assert.Contains("8 Exit", Result);

            Assert.NotNull(Result);
        }

        [Fact]
        public void ValidTestMenuMaintenance_ViewPart()
        {
            List<AC> commands =
                  [
                  new AC(2, ""),
                  new AC(1, "")
                  ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            FirstPage_Menu(Result);

            Assert.NotNull(Result);
        }

        private static void FirstPage_Menu(string Result)
        {
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("1) Manage Buildings", Result);
            Assert.Contains("1.1) View Buildings", Result);
            Assert.Contains("1.1.1) Refresh", Result);
            Assert.Contains("1.1.2) Search", Result);
            Assert.Contains("1.1.2.1) Search Again", Result);
            Assert.Contains("1.1.2.2) Remove Building", Result);
            Assert.Contains("1.1.2.3) Edit Building", Result);
            Assert.Contains("1.1.2.4) Back to the Building List menu", Result);
            Assert.Contains("1.1.2.5) Exit", Result);
            Assert.Contains("1.1.3) Next Page", Result);
            Assert.Contains("1.1.4) Previous Page", Result);
            Assert.Contains("1.1.5) First Page", Result);
            Assert.Contains("1.1.6) Last Page", Result);
            Assert.Contains("1.1.7) Back to Manage Buildings", Result);
            Assert.Contains("1.1.8) Exit", Result);
            Assert.Contains("1.2) Add Buildings", Result);
            Assert.Contains("1.2.1) Add Building", Result);
            Assert.Contains("1.2.2) Back to Manage Buildings", Result);
            Assert.Contains("1.2.3) Exit", Result);
            Assert.Contains("1.3) Remove Buildings", Result);
            Assert.Contains("1.3.1) Remove Building", Result);
            Assert.Contains("1.3.2) Next Page", Result);
            Assert.Contains("1.3.3) Previous Page", Result);
            Assert.Contains("1.3.4) First Page", Result);
            Assert.Contains("1.3.5) Last Page", Result);
            Assert.Contains("1.3.6) Back to Manage Buildings", Result);
            Assert.Contains("1.3.7) Exit", Result);
            Assert.Contains("1.4) Edit Building Names", Result);
            Assert.Contains("1.4.1) Edit Building", Result);
            Assert.Contains("1.4.2) Next Page", Result);
            Assert.Contains("1.4.3) Previous Page", Result);
            Assert.Contains("1.4.4) First Page", Result);
            Assert.Contains("1.4.5) Last Page", Result);
            Assert.Contains("1.4.6) Back to Manage Buildings", Result);
            Assert.Contains("1.4.7) Exit", Result);
            Assert.Contains("1.5) Save Buildings", Result);
            Assert.Contains("1.5.1) Save buildings to a file", Result);
            Assert.Contains("1.5.2) Back to Manage Buildings", Result);
            Assert.Contains("1.5.3) Exit", Result);
            Assert.Contains("1.6) Load Buildings", Result);
            Assert.Contains("View Menus - Sub Menu", Result);
            Assert.Contains("1 Refresh", Result);
            Assert.Contains("2 Next Page", Result);
            Assert.Contains("3 Last Page", Result);
            Assert.Contains("4 Back to Manage Menus", Result);
            Assert.Contains("5 Exit", Result);
        }

        [Fact]
        public void ValidTestMenuMaintenance_ViewPart_NextPage()
        {
            List<AC> commands =
              [
              new AC(2, ""),
              new AC(1, ""),
              new AC(2, "")
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();

            LastPage_Menu(Result);

        }

        private static void LastPage_Menu(string Result)
        {
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("1.6.1) Load buildings from a file", Result);
            Assert.Contains("1.6.2) Back to Manage Buildings", Result);
            Assert.Contains("1.6.3) Exit", Result);
            Assert.Contains("1.7) Back to Main Menu", Result);
            Assert.Contains("1.8) Exit", Result);
            Assert.Contains("2) Manage Menus", Result);
            Assert.Contains("2.1) View Menus", Result);
            Assert.Contains("2.1.1) Refresh", Result);
            Assert.Contains("2.1.2) Next Page", Result);
            Assert.Contains("2.1.3) Previous Page", Result);
            Assert.Contains("2.1.4) First Page", Result);
            Assert.Contains("2.1.5) Last Page", Result);
            Assert.Contains("2.1.6) Back to Manage Menus", Result);
            Assert.Contains("2.1.7) Exit", Result);
            Assert.Contains("2.2) Add Menus", Result);
            Assert.Contains("2.2.1) Refresh", Result);
            Assert.Contains("2.2.2) Add a menu item", Result);
            Assert.Contains("2.2.3) Next Page", Result);
            Assert.Contains("2.2.4) Previous Page", Result);
            Assert.Contains("2.2.5) First Page", Result);
            Assert.Contains("2.2.6) Last Page", Result);
            Assert.Contains("2.2.7) Back to Manage Menus", Result);
            Assert.Contains("2.2.8) Exit", Result);
            Assert.Contains("2.3) Remove Menus", Result);
            Assert.Contains("2.4) Edit Menus", Result);
            Assert.Contains("2.5) Save Menu", Result);
            Assert.Contains("2.6) Load Menu", Result);
            Assert.Contains("2.7) Back to Main Menu", Result);
            Assert.Contains("3) Exit", Result);
            Assert.Contains("2.8) Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Choice:", Result);
        }

        [Fact]
        public void ValidTestMenuMaintenance_ViewPart_FirstPage()
        {
            List<AC> commands =
              [
              new AC(2, ""), // Menu maintenance
              new AC(1, ""), // View
              new AC(3, ""), // Last Page
              new AC(3, "")  // First Page
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            FirstPage_Menu(Result);
        }

        [Fact]
        public void ValidTestMenuMaintenance_ViewPart_LastPage()
        {
            List<AC> commands =
              [
              new AC(2, ""), // Menu maintenance
              new AC(1, ""), // View
              new AC(3, "")  // Last Page
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();
            LastPage_Menu(Result);
        }

        [Fact]
        public void ValidMapMenus_DoesTheCounterReset()
        {
            var StartPage = MH.Menu.FirstOrDefault(p => p.IsStartPage)?.Page;
            Assert.True(StartPage != null);
            MH.MapMenus(MH.Menu, StartPage);
            MH.MapMenus(MH.Menu, StartPage);
            var Menu = MH.Menu.FirstOrDefault(p => p.Index == 1);
            Assert.True(Menu != null, "Could not find the index of 1 - means - after running it twice - it overwrote the index - it does not reset the counter.");
            var DistinctCountOfMenus = MH.Menu.DistinctBy(p => p.Index).ToList<MenuItem>().Count;
            Assert.True(MH.Menu.Count == DistinctCountOfMenus, "If the menu count is not equal to the max menu count then the index is not correct. must be unique.");
            Menu = MH.Menu.FirstOrDefault(p => p.Index == MH.Menu.Count);
            Assert.True(Menu != null, "Could not find the index of 65(Max) so far - means - index is not sequential.");
        }

    }
}