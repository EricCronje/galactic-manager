using ArchCorpUtilities.Models.Buildings;

namespace TestProjectMenus
{
    using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
    using AC = ArchCorpUtilities.Utilities.Command;
    using AL = ArchCorpUtilities.Models.ArchLoader;
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
    using U = ArchCorpUtilities.Utilities.UniversalUtilities;
    using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

    using ArchCorpUtilities.Models.Menus;
    using System.Text;
    using System;

    public class UnitTestMenus
    {
        //[Fact]
        //public void ValidTestStartMenu()
        //{
        //    List<AC> commands =
        //          [
        //          new AC(null, "")
        //          ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);
        //    string Result = CH.GetFeedback();
        //    Assert.Contains("Galaxy Manager", Result);
        //    Assert.Contains("1 Manage Menus", Result);
        //    Assert.Contains("2 Exit", Result);

        //    Assert.NotNull(Result);
        //}

        //[Fact]
        //public void ValidTestMenuMaintenance()
        //{
        //    List<AC> commands =
        //          [
        //          new AC(1, "")
        //          ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);
        //    string Result = CH.GetFeedback();
        //    Assert.Contains("1 View Menus", Result);
        //    Assert.Contains("2 Add Menus", Result);
        //    Assert.Contains("3 Remove Menus", Result);
        //    Assert.Contains("4 Edit Menus", Result);
        //    Assert.Contains("5 Save Menu", Result);
        //    Assert.Contains("6 Load Menu", Result);
        //    Assert.Contains("7 Back to Main Menu", Result);
        //    Assert.Contains("8 Exit", Result);

        //    Assert.NotNull(Result);
        //}

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

        //[Fact]
        //public void ValidMainMenu()
        //{
        //    //Version 1.0.0
        //    List<AC> commands =
        //      [
        //      new AC(0, "")
        //      ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);
        //    string Result = CH.GetFeedback();

        //    //Check the menu options on the screen?
        //    Assert.Contains("-------------------------", Result);
        //    Assert.Contains("Galaxy Manager - Main Menu", Result);
        //    Assert.Contains("1 Manage Menus", Result);
        //    //Is the Exit on the bottom?
        //    Assert.Contains("2 Exit", Result);
        //    Assert.Contains("Choice:", Result);
        //}

        //[Fact]
        //public void ValidIsTheExitOnTeBottomMainMenu()
        //{
        //    //Version 1.0.0
        //    List<AC> commands =
        //      [
        //      new AC(0, "")
        //      ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);
        //    string Result = CH.GetFeedback();

        //    Assert.Contains("2 Exit", Result);
        //}

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
              new AC(1, "")
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

        //[Fact]
        //public void ValidAddMenuMaintenance()
        //{
        //    List<AC> commands =
        //    [
        //    new AC(2, ""),
        //    new AC(2, ""), // Add Menu
        //    ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);

        //    string Result = CH.GetFeedback();

        //    ValidAddMenuMaintenanceAssert(Result);

        //}

        private static void ValidAddMenuMaintenanceAssert(string Result)
        {
            Assert.Contains("1 Manage Menus", Result);
            Assert.Contains("2 Exit", Result);
            Assert.Contains("-------------------------", Result);
            Assert.Contains("Choice:", Result);
        }

        [Fact]
        public void ValidAddMenuMaintenanceAddOperation()
        {
            List<AC> commands =
            [
            new AC(1, ""),
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
            Assert.Contains("1) Manage Menus", Result);
            Assert.Contains("2) Exit", Result);
            Assert.Contains("Pages : 1/1", Result);
            Assert.Contains("Select a menu item.", Result);
            Assert.Contains("Choice:", Result);
        }

        //[Fact]
        //public void ValidAddMenuMaintenanceAddOperationPartTwo()
        //{
        //    string[] Values = ["1", "", ""];
        //    List<AC> commands =
        //    [
        //    new AC(1, ""),
        //    new AC(2, ""), // Add Menu
        //    new AC(2, Values) // Add a new menu

        //    ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);

        //    string Result = CH.GetFeedback();
            
        //    ValidAddMenuMaintenanceAddOperationPartTwoAssert(Result);

        //}

        //private static void ValidAddMenuMaintenanceAddOperationPartTwoAssert(string Result)
        //{
        //    Assert.Contains("Current menu structure", Result);
        //    Assert.Contains("1) Manage Menus", Result);
        //    Assert.Contains("2) Exit", Result);
        //    Assert.Contains("-------------------------", Result);
        //    Assert.Contains("Pages : 1/1", Result);
        //    Assert.Contains("Select a menu item.", Result);
        //    Assert.Contains("Choice:", Result);
        //    Assert.Contains("1", Result);
        //    Assert.Contains("Please provide a display name for the new menu option", Result);
        //}

        //[Fact]
        //public void ValidAddMenuMaintenanceAddOperationPartThree()
        //{
        //    string[] Values = ["1", "Manage Sites", "Sites" ];
        //    List<AC> commands =
        //    [
        //    new AC(1, ""),
        //    new AC(2, ""), // Add Menu
        //    new AC(2, Values)
        //    ];
        //    CH.ClearFeedback();
        //    AL.RunArch(commands);

        //    string Result = CH.GetFeedback();

        //    ValidAddMenuMaintenanceAddOperationPartThreeAssert(Result);

        //    Assert.Contains("<ProgramExited>", Result);
        //    //Then remove it.
        //    MH.LoadDefaults();
        //    RemoveCreatedModelFiles("Sites");
        //    RestoreOriginalModels();
        //}

        //private static void RemoveCreatedModelFiles(string Entity)
        //{
        //    var CSFile = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}.cs";
        //    var CSFileHelper = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model\\{Entity}Helper.cs";
        //    var GeneratedFolder = $"{CodeGen.WorkingFolder}\\GeneratedModels\\{Entity}Model";

        //    if (File.Exists(CSFile))
        //        File.Delete(CSFile);
        //    if (File.Exists(CSFileHelper))
        //        File.Delete(CSFileHelper);
        //    if (Directory.Exists(GeneratedFolder))
        //        Directory.Delete(GeneratedFolder);

        //}

        //private static void RestoreOriginalModels()
        //{
        //    var UniversalUtilitiesFile = $"{CodeGen.WorkingFolder}\\Utilities\\UniversalUtilities.cs";
        //    var UniversalUtilitiesFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Utilities\\UniversalUtilities.cs";

        //    var ArchLoaderFile = $"{CodeGen.WorkingFolder}\\Models\\ArchLoader.cs";
        //    var ArchLoaderFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\ArchLoader.cs";

        //    var TargetTaskHelperFile = $"{CodeGen.WorkingFolder}\\Models\\TargetTaskHelper.cs";
        //    var TargetTaskHelperFileBackup = $"{CodeGen.WorkingFolder}\\Backup\\Models\\TargetTaskHelper.cs";

        //    if (File.Exists(UniversalUtilitiesFile))
        //        File.Delete(UniversalUtilitiesFile);

        //    if (File.Exists(TargetTaskHelperFile))
        //        File.Delete(TargetTaskHelperFile);

        //    if (File.Exists(ArchLoaderFile))
        //        File.Delete(ArchLoaderFile);

        //    if (File.Exists(UniversalUtilitiesFileBackup))
        //        File.Copy(UniversalUtilitiesFileBackup, UniversalUtilitiesFile);

        //    if (File.Exists(ArchLoaderFileBackup))
        //        File.Copy(ArchLoaderFileBackup, ArchLoaderFile);

        //    if (File.Exists(TargetTaskHelperFileBackup))
        //        File.Copy(TargetTaskHelperFileBackup, TargetTaskHelperFile);
        //}

        //private static void ValidAddMenuMaintenanceAddOperationPartThreeAssert(string result)
        //{
        //    Assert.Contains("-------------------------", result);
        //    Assert.Contains("Current menu structure", result);
        //    Assert.Contains("-------------------------", result);
        //    Assert.Contains("1) Manage Menus", result);
        //    Assert.Contains("2) Exit", result);
        //    Assert.Contains("-------------------------", result);
        //    Assert.Contains("Pages : 1/1", result);
        //    Assert.Contains("Select a menu item.", result);
        //    Assert.Contains("Choice:", result);
        //    Assert.Contains("1", result);
        //    Assert.Contains("Please provide a display name for the new menu option", result);
        //    Assert.Contains("Manage Sites", result);
        //    Assert.Contains("Enter the item name only - which will be a post fix to View ..., Add ... etc.", result);
        //    Assert.Contains("Sites", result);
        //    Assert.Contains("Menu Items Created successfully", result);
        //    Assert.Contains("Model file created successfully.", result);
        //    Assert.Contains("-------------------------", result);
        //    Assert.Contains("Add Menu - Sub Menu", result);
        //    Assert.Contains("-------------------------", result);
        //    Assert.Contains("1 Refresh", result);
        //    Assert.Contains("2 Add a menu item", result);
        //    Assert.Contains("3 Back to Manage Menus", result);
        //    Assert.Contains("4 Exit", result);
        //    Assert.Contains("-------------------------", result);
        //    Assert.Contains("Choice:", result);
        //}
    }
}