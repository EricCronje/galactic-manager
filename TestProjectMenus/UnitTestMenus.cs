using ArchCorpUtilities.Models.Buildings;

namespace TestProjectMenus
{
    using MBR = MockBuildingsRepository;
    using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
    using AC = ArchCorpUtilities.Utilities.Command;
    using AL = ArchCorpUtilities.Models.ArchLoader;
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
    using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
    using U = ArchCorpUtilities.Utilities.UniversalUtilities;
    using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

    using ArchCorpUtilities.Models.Menus;
    using System.Text;
    using System;

    public class UnitTestMenus
    {
        [Fact]
        public void ValidTestMapMenus()
        {
            var result = U.ViewWithPagination("Menu List", BH.Page, MH.Menu, U.Navigation.FirstPage );
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

            MenuPageTwo(Result);

        }

        private static void LastPage_Menu(string Result)
        {
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("2.4.8) Exit", Result);
            Assert.Contains("2.5) Save Menu", Result);
            Assert.Contains("2.5.1) Save menus to a file", Result);
            Assert.Contains("2.5.2) Back to Manage Menus", Result);
            Assert.Contains("2.5.3) Exit", Result);
            Assert.Contains("2.6) Load Menu", Result);
            Assert.Contains("2.6.1) Load menus from a file", Result);
            Assert.Contains("2.6.2) Back to Manage Menus", Result);
            Assert.Contains("2.6.3) Exit", Result);
            Assert.Contains("2.7) Back to Main Menu", Result);
            Assert.Contains("2.8) Exit", Result);
            Assert.Contains("3) Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Pages : 3/3", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("View Menus - Sub Menu", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("1 Refresh", Result);
            Assert.Contains("2 Previous Page", Result);
            Assert.Contains("3 First Page", Result);
            Assert.Contains("4 Back to Manage Menus", Result);
            Assert.Contains("5 Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Choice:", Result);
        }
        

        private static void MenuPageTwo(string Result)
        {
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("-------------------------", Result);
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
            Assert.Contains("2.3.1) Refresh", Result);
            Assert.Contains("2.3.2) Remove a menu item", Result);
            Assert.Contains("2.3.3) Next Page", Result);
            Assert.Contains("2.3.4) Previous Page", Result);
            Assert.Contains("2.3.5) First Page", Result);
            Assert.Contains("2.3.6) Last Page", Result);
            Assert.Contains("2.3.7) Back to Manage Menus", Result);
            Assert.Contains("2.3.8) Exit", Result);
            Assert.Contains("2.4) Edit Menus", Result);
            Assert.Contains("2.4.1) Refresh", Result);
            Assert.Contains("2.4.2) Edit a menu item", Result);
            Assert.Contains("2.4.3) Next Page", Result);
            Assert.Contains("2.4.4) Previous Page", Result);
            Assert.Contains("2.4.5) First Page", Result);
            Assert.Contains("2.4.6) Last Page", Result);
            Assert.Contains("2.4.7) Back to Manage Menus", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Pages : 2/3", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("View Menus - Sub Menu", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("1 Refresh", Result);
            Assert.Contains("2 Next Page", Result);
            Assert.Contains("3 Previous Page", Result);
            Assert.Contains("4 First Page", Result);
            Assert.Contains("5 Last Page", Result);
            Assert.Contains("6 Back to Manage Menus", Result);
            Assert.Contains("7 Exit", Result);
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

        [Fact]
        public void ValidMainMenu()
        {
            //Version 1.0.0
            List<AC> commands =
              [
              new AC(0, "")
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();

            //Check the menu options on the screen?
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Galaxy Manager - Main Menu - V1.0.0", Result);
            Assert.Contains("1 Manage Buildings", Result);
            Assert.Contains("2 Manage Menus", Result);
            //Is the Exit on the bottom?
            Assert.Contains("3 Exit", Result);
            Assert.Contains("Choice:", Result);
        }

        [Fact]
        public void ValidVersion()
        {
            //Version 1.0.0
            List<AC> commands =
              [
              new AC(0, "")
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();

            //Latest version?
            Assert.Contains("V1.0.0", Result);
        }

        [Fact]
        public void ValidIsTheExitOnTeBottomMainMenu()
        {
            //Version 1.0.0
            List<AC> commands =
              [
              new AC(0, "")
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();

            Assert.Contains("3 Exit", Result);
        }

        [Fact]
        public void ValidDoesExitExitMainMenu()
        {
            //Version 1.0.0
            List<AC> commands =
              [
              new AC(3, "")
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);
            string Result = CH.GetFeedback();

            Assert.Contains("<ProgramExited>", Result);
        }

        [Fact]
        public void ValidManageMenus()
        {
            List<AC> commands =
              [
              new AC(2, "")
              ];
            CH.ClearFeedback();
            AL.RunArch(commands);

            string Result = CH.GetFeedback();
            Valid_ManageMenusScreen(Result);

        }

        private static void Valid_ManageMenusScreen(string Result)
        {
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Manage Menus", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("1 View Menus", Result);
            Assert.Contains("2 Add Menus", Result);
            Assert.Contains("3 Remove Menus", Result);
            Assert.Contains("4 Edit Menus", Result);
            Assert.Contains("5 Save Menu", Result);
            Assert.Contains("6 Load Menu", Result);
            Assert.Contains("7 Back to Main Menu", Result);
            Assert.Contains("8 Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Choice:", Result);
        }

        [Fact]
        public void ValidAddMenuMaintenance()
        {
            List<AC> commands =
            [
            new AC(2, ""),
            new AC(2, ""), // Add Menu
            ];
            CH.ClearFeedback();
            AL.RunArch(commands);

            string Result = CH.GetFeedback();

            ValidAddMenuMaintenanceAssert(Result);

        }

        private static void ValidAddMenuMaintenanceAssert(string Result)
        {
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("1) Manage Buildings", Result);
            Assert.Contains("2) Manage Menus", Result);
            Assert.Contains("3) Exit", Result);
            Assert.Contains("Pages : 1/1", Result);
            Assert.Contains("Add Menu - Sub Menu", Result);
            Assert.Contains("1 Refresh", Result);
            Assert.Contains("2 Add a menu item", Result);
            Assert.Contains("3 Back to Manage Menus", Result);
            Assert.Contains("4 Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Choice:", Result);
        }

        [Fact]
        public void ValidAddMenuMaintenanceAddOperation()
        {
            List<AC> commands =
            [
            new AC(2, ""),
            new AC(2, ""), // Add Menu
            new AC(2, ""), // Add a new menu
            ];
            CH.ClearFeedback();
            AL.RunArch(commands);

            string Result = CH.GetFeedback();

            ValidAddMenuMaintenanceAddOperationAssert(Result);

        }

        private static void ValidAddMenuMaintenanceAddOperationAssert(string Result)
        {
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("1) Manage Buildings", Result);
            Assert.Contains("2) Manage Menus", Result);
            Assert.Contains("3) Exit", Result);
            Assert.Contains("Pages : 1/1", Result);
            Assert.Contains("Select a menu item.", Result);
            Assert.Contains("Choice:", Result);
        }

        [Fact]
        public void ValidAddMenuMaintenanceAddOperationPartTwo()
        {
            string[] Values = ["1", "", ""];
            List<AC> commands =
            [
            new AC(2, ""),
            new AC(2, ""), // Add Menu
            new AC(2, Values) // Add a new menu

            ];
            CH.ClearFeedback();
            AL.RunArch(commands);

            string Result = CH.GetFeedback();
            
            ValidAddMenuMaintenanceAddOperationPartTwoAssert(Result);

        }

        private static void ValidAddMenuMaintenanceAddOperationPartTwoAssert(string Result)
        {
            Assert.Contains("Current menu structure", Result);
            Assert.Contains("1) Manage Buildings", Result);
            Assert.Contains("2) Manage Menus", Result);
            Assert.Contains("3) Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Pages : 1/1", Result);
            Assert.Contains("Select a menu item.", Result);
            Assert.Contains("Choice:", Result);
            Assert.Contains("1", Result);
            Assert.Contains("Please provide a display name for the new menu option", Result);
        }

        [Fact]
        public void ValidAddMenuMaintenanceAddOperationPartThree()
        {
            string[] Values = ["1", "Manage Sites", "Sites" ];
            List<AC> commands =
            [
            new AC(2, ""),
            new AC(2, ""), // Add Menu
            new AC(2, Values)
            ];
            CH.ClearFeedback();
            AL.RunArch(commands);

            string Result = CH.GetFeedback();

            ValidAddMenuMaintenanceAddOperationPartThreeAssert(Result);

            Assert.Contains("<ProgramExited>", Result);
            //Then remove it.
            MH.LoadDefaults();
            RemoveCreatedModelFiles("Sites");
            RestoreOriginalModels();
        }

        private static void RemoveCreatedModelFiles(string Entity)
        {
            var CSFile = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}.cs";
            var CSFileHelper = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}Helper.cs";
            var GeneratedFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model";

            if (File.Exists(CSFile))
                File.Delete(CSFile);
            if (File.Exists(CSFileHelper))
                File.Delete(CSFileHelper);
            if (Directory.Exists(GeneratedFolder))
                Directory.Delete(GeneratedFolder);

        }

        private void RestoreOriginalModels()
        {
            var UniversalUtilitiesFile = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.cs";
            var UniversalUtilitiesFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Utilities\\UniversalUtilities.cs";

            var ArchLoaderFile = $"{CodeGen.WorkingFolder}\\Models\\ArchLoader.cs";
            var ArchLoaderFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\ArchLoader.cs";

            var TargetTaskHelperFile = $"{CodeGen.WorkingFolder}\\Models\\TargetTaskHelper.cs";
            var TargetTaskHelperFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\TargetTaskHelper.cs";

            if (File.Exists(UniversalUtilitiesFile))
                File.Delete(UniversalUtilitiesFile);

            if (File.Exists(TargetTaskHelperFile))
                File.Delete(TargetTaskHelperFile);

            if (File.Exists(ArchLoaderFile))
                File.Delete(ArchLoaderFile);

            if (File.Exists(UniversalUtilitiesFileBackup))
                File.Copy(UniversalUtilitiesFileBackup, UniversalUtilitiesFile);

            if (File.Exists(ArchLoaderFileBackup))
                File.Copy(ArchLoaderFileBackup, ArchLoaderFile);

            if (File.Exists(TargetTaskHelperFileBackup))
                File.Copy(TargetTaskHelperFileBackup, TargetTaskHelperFile);
        }

        private static void ValidAddMenuMaintenanceAddOperationPartThreeAssert(string result)
        {
            Assert.Contains("-------------------------", result);
            Assert.Contains("Current menu structure", result);
            Assert.Contains("-------------------------", result);
            Assert.Contains("1) Manage Buildings", result);
            Assert.Contains("2) Manage Menus", result);
            Assert.Contains("3) Exit", result);
            Assert.Contains("-------------------------", result);
            Assert.Contains("Pages : 1/1", result);
            Assert.Contains("Select a menu item.", result);
            Assert.Contains("Choice:", result);
            Assert.Contains("1", result);
            Assert.Contains("Please provide a display name for the new menu option", result);
            Assert.Contains("Manage Sites", result);
            Assert.Contains("Enter the item name only - which will be a post fix to View ..., Add ... etc.", result);
            Assert.Contains("Sites", result);
            Assert.Contains("Menu Items Created successfully", result);
            Assert.Contains("Model file created successfully.", result);
            Assert.Contains("-------------------------", result);
            Assert.Contains("Add Menu - Sub Menu", result);
            Assert.Contains("-------------------------", result);
            Assert.Contains("1 Refresh", result);
            Assert.Contains("2 Add a menu item", result);
            Assert.Contains("3 Back to Manage Menus", result);
            Assert.Contains("4 Exit", result);
            Assert.Contains("-------------------------", result);
            Assert.Contains("Choice:", result);
        }
    }
}