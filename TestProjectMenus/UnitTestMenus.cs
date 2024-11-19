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


        [Fact]
        public void ValidAddMenuMaintenanceAddOperation()
        {
            string[] values = ["Y"];
            List<AC> commands =
            [
            new AC(1, ""),
            new AC(2, ""), // Add Menu
            new AC(2, values), // Add a new menu
            ];
            CH.ClearFeedback();
            AL.RunArch(commands);

            string Result = CH.GetFeedback();
            string Expected = "-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage Menus\r\n2 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nManage Menus - <Version>\r\n-------------------------\r\n1 View Menus\r\n2 Add Menus\r\n3 Remove Menus\r\n4 Edit Menus\r\n5 Save Menu\r\n6 Load Menu\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nCurrent menu structure - <Version>\r\n-------------------------\r\n1) Manage Menus\r\n2) Exit\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nAdd Menu - Sub Menu - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Add a menu item\r\n3 Back to Manage Menus\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nDo you want to generate the code from {744852ea-d309-4f87-bbd2-03fe76ba877b}? (Y/N)\r\nSimulate: Y\r\nTarget file not altered.         - Buildings - {F8FE36D7-3F08-48BA-9CAB-FBAA102C8149} - UniversalUtilities.cs.\r\nTarget file not altered.         - Buildings - {0ACDC688-3120-452F-94AE-2DD1771A9991} - ArchLoader.cs.\r\nTarget file not altered.         - Buildings - {048A4DD6-2F1B-4178-A732-E3B50D3F0791} - ArchLoader.cs.\r\nTarget file not altered.         - Buildings - {9ED7AF33-DE0E-45C3-821F-4669558AD744} - ArchLoader.cs.\r\nTarget file not altered.         - Buildings - {24D86755-6962-4074-BD9F-73E8FE0A5F68} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {EA6AE6CA-7E51-43DE-95F9-FF66E27AE130} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {A6E3C6F1-D649-45CE-8C05-3A87466618A9} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {B03F74F5-9862-4916-9EF1-82DD253A5BC3} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {ADECB8B3-1779-4107-9DF5-9E250E31AFDD} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {39C53717-4163-4B33-B652-4AA3B4D28C5B} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {BBDE47EF-8937-4545-A019-652A8A306B6E} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {90D2D480-6963-441E-B2C4-E0EADC878A83} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {74A75AB1-1AB2-46C7-B63F-39F52AF0049A} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {11002DF2-E6AB-485E-B896-C3ED92706E30} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {36DE75D7-A730-4F6B-A7C9-4660245BD895} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {BD92B12F-6AB8-420C-9A4B-654233721FB7} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {F745E72F-A908-4AB1-AF8B-E3FDE13BF46E} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {B2FED166-7FCF-4163-8507-EB1CC28B6435} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {5ED05F9F-E960-4964-AD0F-89E21CCCD9F5} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Buildings - {E4C217C0-AC0D-4571-95E4-16CE056F35A5} - ArchLoader.cs.\r\nModel file created successfully. - Buildings.\r\nMenu Items Created successfully\r\nMenus (Add, Remove, Edit, View, Search, import, save etc.) created for - Buildings\r\nMenus exported for - Buildings - C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\CodeGen\\Generate\\{744852ea-d309-4f87-bbd2-03fe76ba877b}\\Menus{744852ea-d309-4f87-bbd2-03fe76ba877b}\r\nTarget file not altered.         - Beacons - {F8FE36D7-3F08-48BA-9CAB-FBAA102C8149} - UniversalUtilities.cs.\r\nTarget file not altered.         - Beacons - {0ACDC688-3120-452F-94AE-2DD1771A9991} - ArchLoader.cs.\r\nTarget file not altered.         - Beacons - {048A4DD6-2F1B-4178-A732-E3B50D3F0791} - ArchLoader.cs.\r\nTarget file not altered.         - Beacons - {9ED7AF33-DE0E-45C3-821F-4669558AD744} - ArchLoader.cs.\r\nTarget file not altered.         - Beacons - {24D86755-6962-4074-BD9F-73E8FE0A5F68} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {EA6AE6CA-7E51-43DE-95F9-FF66E27AE130} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {A6E3C6F1-D649-45CE-8C05-3A87466618A9} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {B03F74F5-9862-4916-9EF1-82DD253A5BC3} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {ADECB8B3-1779-4107-9DF5-9E250E31AFDD} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {39C53717-4163-4B33-B652-4AA3B4D28C5B} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {BBDE47EF-8937-4545-A019-652A8A306B6E} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {90D2D480-6963-441E-B2C4-E0EADC878A83} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {74A75AB1-1AB2-46C7-B63F-39F52AF0049A} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {11002DF2-E6AB-485E-B896-C3ED92706E30} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {36DE75D7-A730-4F6B-A7C9-4660245BD895} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {BD92B12F-6AB8-420C-9A4B-654233721FB7} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {F745E72F-A908-4AB1-AF8B-E3FDE13BF46E} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {B2FED166-7FCF-4163-8507-EB1CC28B6435} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {5ED05F9F-E960-4964-AD0F-89E21CCCD9F5} - TargetTaskHelper.cs.\r\nTarget file not altered.         - Beacons - {E4C217C0-AC0D-4571-95E4-16CE056F35A5} - ArchLoader.cs.\r\nModel file created successfully. - Beacons.\r\nMenu Items Created successfully\r\nMenus (Add, Remove, Edit, View, Search, import, save etc.) created for - Beacons\r\nMenus exported for - Beacons - C:\\_FLAP03\\GBZZBEBJ\\Working\\dotnet\\galactic-manager\\CodeGen\\Generate\\{744852ea-d309-4f87-bbd2-03fe76ba877b}\\Menus{744852ea-d309-4f87-bbd2-03fe76ba877b}\r\nGenerated - {744852ea-d309-4f87-bbd2-03fe76ba877b}\r\nIn Order for the changes to take affect:\r\nDeploy the code manually.\r\nThen create the menu structure.\r\nPress any key to exit the program.\r\nSimulate: Y\r\n<Exit System>\r\n-------------------------\r\nAdd Menu - Sub Menu - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Add a menu item\r\n3 Back to Manage Menus\r\n4 Exit\r\n-------------------------\r\nChoice:\r\n<ProgramExited>\r\n";
            Assert.Equal(Result, Expected);
        }

    }
}