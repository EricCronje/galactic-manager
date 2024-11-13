namespace TestProjectCoreFunctionals
{
    using ArchCorpUtilities.Models.Menus;
    using ArchCorpUtilities.Utilities;
    using System.Buffers;
    using AL = ArchCorpUtilities.Models.ArchLoader;
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
    using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
    using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;

    public class UnitTest1
    {
        [Fact]
        public void ScenarioValidAddItemSearchView()
        {

            #region Getting the Building and Beacons menu items. Copying the file current Guid e.g {542fcda8-12dd-4fd1-9701-80d61171d215}.
            var CurrentGuid = CodeGenHelper.CurrentGuid;
            var SourcePath = $"{CodeGenHelper.WorkingFolder}\\SavedMenus\\{CurrentGuid}";
            var TargetPath = $"{Environment.CurrentDirectory}\\{CurrentGuid}";

            if (File.Exists(TargetPath))
                File.Delete(TargetPath);

            Assert.True(File.Exists(SourcePath), $"Source file does not exist. {CurrentGuid} -- {SourcePath}");

            if (File.Exists(SourcePath) && !File.Exists(TargetPath))
                File.Copy(SourcePath, TargetPath);

            #endregion
            #region Does the file exist after being copied to the target?

            Assert.True(File.Exists(TargetPath), "The saved menu file does not exist. {542fcda8-12dd-4fd1-9701-80d61171d215}. Unable to test Beacons and Buildings.");

            #endregion
            #region Does it contain Beacons and Buildings?

            var Content = File.ReadAllText(TargetPath);
            Assert.Contains("Beacons", Content);
            Assert.Contains("Buildings", Content);

            #endregion
            #region Navigating to Buildings - Option 1, View it - Option 1 Again.
            CH.ClearFeedback();
            List<Command>? Commands = new List<Command>();
            Commands.Add(new Command(1, ""));
            Commands.Add(new Command(1, ""));
            
            AL.RunArch(Commands);
            var Result = CH.GetFeedback();
            #endregion
            #region Add TestBuilding - Search for it - View it.
            int? ManageDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Manage Buildings")?.DisplayNumber;
            int? EntityPage = MH.Menu.FirstOrDefault(p => p.DisplayName == "Manage Buildings")?.TargetPage;
            int? AddDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Add Buildings")?.DisplayNumber;
            int? AddTargetPage = MH.Menu.FirstOrDefault(p => p.DisplayName == "Add Buildings")?.TargetPage;

            int? BacktoMainMenuViewDisplayNumber = 5;//MH.Menu.FirstOrDefault(p => p.DisplayName == "Back to Main Menu" && p.Page == EntityPage)?.DisplayNumber;
            int? BacktoMainMenuFromAddDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Back to Main Menu" && p.Page == AddTargetPage)?.DisplayNumber;
            int? ViewBuildingsDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "View Buildings")?.DisplayNumber;
            int? ViewBuildingsPage = MH.Menu.FirstOrDefault(p => p.DisplayName == "View Buildings")?.Page;
            var SearchDisplayMenuItem = $"{ManageDisplayNumber?.ToString()}.{ViewBuildingsDisplayNumber?.ToString()}";
            int? BacktoMainMenuFromSearchDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Back to Main Menu" && p.Page == EntityPage)?.DisplayNumber;
            int? SaveBuildingsDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Save Buildings")?.DisplayNumber;

            int? SearchDisplayNumber = 0;
            string[] AddValues = ["TestBuilding"];
            string[] SearchValues = ["TestBuilding"];

            if (AddDisplayNumber != null && ManageDisplayNumber != null)
            {
                SearchDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Search" && p.DisplayMenuItem.Contains(SearchDisplayMenuItem))?.DisplayNumber;
                //SearchPage = MH.Menu.FirstOrDefault(p => p.DisplayName == "Search" && p.DisplayMenuItem.Contains(SearchDisplayMenuItem))?.Page;
                BacktoMainMenuFromSearchDisplayNumber = 3;//MH.Menu.FirstOrDefault(p => p.DisplayName == "Back to Main Menu" && p.Page == SearchPage)?.DisplayNumber;
            }

            if (AddDisplayNumber != null && ManageDisplayNumber != null && SearchDisplayNumber != null && SaveBuildingsDisplayNumber != null)
            {
                string? Id = AddDisplayNumber.ToString();

                if (Id != null)
                {
                    Commands = null;
                    Commands =
                    [
                        new Command (ManageDisplayNumber, ""),                        
                        new Command (AddDisplayNumber, AddValues),
                        new Command (BacktoMainMenuFromAddDisplayNumber, ""),
                        new Command (ViewBuildingsDisplayNumber, ""),
                        new Command (SearchDisplayNumber, SearchValues),
                        new Command (BacktoMainMenuFromSearchDisplayNumber, ""),
                        new Command (ViewBuildingsDisplayNumber, ""),
                        new Command (BacktoMainMenuViewDisplayNumber, ""),
                        new Command (SaveBuildingsDisplayNumber, ""),
                        new Command (3, "") // Exit
                    ];
                    CH.ClearFeedback();
                    AL.RunArch(Commands);
                }
                Result = CH.GetFeedback();
            }

            #endregion

            #region Checking if it looks as expected.
            var ExpectedValue = "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage Buildings\r\n2 Manage Beacons\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nPlease provide the item name: \r\nSimulate: TestBuilding\r\nItem added.\r\n-------------------------\r\nAdd Buildings - <Version>\r\n-------------------------\r\n1 Add Buildings\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n9) TestBuilding\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 5 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 5 --- \r\n<ClearConsole>\r\nSaved the item successfully - <Date>\r\n-------------------------\r\nSave Buildings to a file - <Version>\r\n-------------------------\r\n1 Save Buildings to a file\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nSave Buildings to a file - <Version>\r\n-------------------------\r\n1 Save Buildings to a file\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\n<ProgramExited>\r\n";
            Assert.True(Result == ExpectedValue);
            Assert.Contains("Save Buildings to a file", Result.Substring(Result.Length - 350));
            #endregion

            #region Search for TestBuidling, Load from the files, Search again.

            
            ManageDisplayNumber = 1;
            EntityPage = MH.Menu.FirstOrDefault(p => p.DisplayName == "Manage Buildings")?.TargetPage;
            BacktoMainMenuFromSearchDisplayNumber = MH.Menu.FirstOrDefault(p => p.DisplayName == "Back to Main Menu" && p.Page == EntityPage)?.DisplayNumber;
            string[] EditValues = ["9", "EditedBuilding"];
            SearchValues = ["TestBuilding"];
            string[] SearchEditedValues = ["EditedBuilding"];
            string[] RemoveValue = ["6"];
            Commands =
             [
                 new Command (ManageDisplayNumber, ""),
                 new Command (ViewBuildingsDisplayNumber, ""),
                 new Command (SearchDisplayNumber, SearchValues),
                 new Command (3, ""), // back to view etc.
                 new Command (6, ""), // Load
                 new Command (2, ""), // Back
                 new Command (ViewBuildingsDisplayNumber, ""),
                 new Command (SearchDisplayNumber, SearchValues),
                 new Command (3, ""), // Back -- Only one item!
                 new Command (4, EditValues), // Edit
                 new Command (2, ""), // Back
                 new Command (ViewBuildingsDisplayNumber, ""),
                 new Command (SearchDisplayNumber, SearchValues),
                 new Command (SearchDisplayNumber, SearchEditedValues),
                 new Command (3, ""), // Back
                 new Command (3, RemoveValue), // Remove
                 new Command (4, ""), // Back
                 new Command (ViewBuildingsDisplayNumber, ""),
                 new Command (SearchDisplayNumber, SearchEditedValues),
                 new Command (3, ""), // Back
                 new Command (ViewBuildingsDisplayNumber, ""),
                 new Command (3, "") // Next Page

             ];
            CH.ClearFeedback();
            AL.RunArch(Commands);
            Result = CH.GetFeedback();

            #endregion

            #region Check if the result matches the expected value
            ExpectedValue = "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage Buildings\r\n2 Manage Beacons\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\nNo Items Was Found\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 6 --- \r\n<ClearConsole>\r\nItems Loaded successfully Buildings - <Date>\r\nItem Exists - No action - Old item : Alpha-Buildings - de69c863-0f24-45eb-bca5-044a6edba036 - New Item: Alpha-Buildings - 61f207cb-a171-43ed-ae34-8d740ec24e4f\r\nItem Exists - No action - Old item : Beta-Buildings - 0e62ae2b-1563-4188-8ac7-f3af7b1fbad4 - New Item: Beta-Buildings - 4546a511-5844-42e5-b55b-d4de02bb147f\r\nItem Exists - No action - Old item : Charlie-Buildings - 1a96b351-cda7-4604-98a6-ee49efc59256 - New Item: Charlie-Buildings - 71ae8499-9bcf-401a-9c98-c8fedd00d3de\r\nItem Exists - No action - Old item : Delta-Buildings - f73dc02a-a8c5-40a8-b683-0d8354161b18 - New Item: Delta-Buildings - f44821ed-cbd6-48cd-9873-3f07a9adc36e\r\nItem Exists - No action - Old item : Echo-Buildings - 0cf15ca1-cc14-49eb-b37c-925c63780603 - New Item: Echo-Buildings - 9ce067b0-1e19-48d0-ba4b-93498cada0ab\r\nItem Exists - No action - Old item : Foxtrot-Buildings - 39bb76c7-b4f3-4227-b1a6-91489aed2500 - New Item: Foxtrot-Buildings - 00faca02-ce4f-4c37-9b5f-05c83f5d3ee5\r\nItem Exists - No action - Old item : Golf-Buildings - 31732a63-fae0-41ac-a706-00f201e6a4e2 - New Item: Golf-Buildings - 498af0bf-5332-48f3-8b9e-4d10b0e1dbdd\r\nItem Exists - No action - Old item : Hotel-Buildings - 2545e46b-b05d-40d1-ace8-62f86e2e2ca4 - New Item: Hotel-Buildings - f48af09d-fe38-4a75-b625-0db11df376ea\r\nItem Added - New Item: TestBuilding - 34cb9ab1-8007-4459-9db5-773b63b7a27f\r\n-------------------------\r\nLoad Buildings from a file - <Version>\r\n-------------------------\r\n1 Load Buildings from a file\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n9) TestBuilding\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nSelect an item to edit - <Version>\r\n-------------------------\r\n9) TestBuilding\r\n-------------------------\r\nPages : 1/1\r\nSelect an item to edit\r\nSimulate: 9\r\nPlease enter a new name\r\nSimulate: EditedBuilding\r\nItem was modified\r\n-------------------------\r\nEdit Buildings - <Version>\r\n-------------------------\r\n1 Edit Buildings\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\nNo Items Was Found\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: EditedBuilding\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n6) EditedBuilding\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nSelect the item to remove - <Version>\r\n-------------------------\r\n6) EditedBuilding\r\n-------------------------\r\nPages : 1/1\r\nSelect the item to remove\r\nSimulate: 6\r\nItem removed.\r\n-------------------------\r\nRemove Buildings - <Version>\r\n-------------------------\r\n1 Remove Buildings\r\n2 Next Page\r\n3 Last Page\r\n4 Back to Main Menu\r\n5 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back toMain Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: EditedBuilding\r\nNo Items Was Found\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n6) Foxtrot-Buildings\r\n7) Golf-Buildings\r\n8) Hotel-Buildings\r\n-------------------------\r\nPages : 2/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 First Page\r\n4 Previous Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\n<ProgramExited>\r\n";
            Assert.True(ExpectedValue == Result);
            #endregion

            #region Cleaning up. Removing the Building and Beacons menu items file {542fcda8-12dd-4fd1-9701-80d61171d215}.

            if (File.Exists(TargetPath))
                File.Delete(TargetPath);

            #endregion

            #region Asserting if the file {542fcda8-12dd-4fd1-9701-80d61171d215} was removed.

            Assert.False(File.Exists(TargetPath), "The saved menu file exist. {542fcda8-12dd-4fd1-9701-80d61171d215}. Cleanup failed.");

            #endregion

        }
    }
}