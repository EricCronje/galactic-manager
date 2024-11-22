namespace TestProJ
{
    using ArchCorpUtilities.Utilities;
    using AL = ArchCorpUtilities.Models.ArchLoader;
    using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

    public class UnitTest1
    {
        [Fact]
        public void ScenarioValidAddItemSearchView()
        {


            string[] Entity = ["Buildings", "Beacons"];
            #region Getting the Building and Beacons menu items. Copying the file current Guid e.g.
            var CurrentGuid = "{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}"; //
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

            foreach (var entity in Entity)
            {
                Assert.Contains(entity, Content);
            }

            #endregion


            for (int i = 0; i < Entity.Length; i++)
            {
                string[] OriginalResults = ["", ""];

                var ManageEntity = i + 1;

                #region Set the variables needed  for the test.

                string[] AddValues = ["TestBuilding"];
                string[] EditValues = ["9", "EditedBuilding"];

                string[] SearchValues = ["TestBuilding"];

                string[] SearchEditedValues = ["EditedBuilding"];
                string[] RemoveValue = ["6"];

                var Add = 2;
                var BackFromAdd = 2;
                var View = 1;
                var Search = 2;
                var BackFromSearch = 3;
                var BackFromView = 5;
                var Save = 5;
                var ExitOnSave = 3;
                var Load = 6;
                var BackFromLoad = 2;
                var Edit = 4;
                var BackFromEdit = 4;
                var Remove = 3;
                var BackFromRemove = 4;
                var Next = 3;

                #endregion
                #region View, Add TestBuilding - Search for it - View it.

                List<Command> Commands =
                [
                    new Command (ManageEntity, ""),
                    new Command (Add, AddValues),
                    new Command (BackFromAdd, ""),
                    new Command (View, ""),
                    new Command (Search, SearchValues),
                    new Command (BackFromSearch, ""),
                    new Command (View, ""),
                    new Command (BackFromView, ""),
                    new Command (Save, ""),
                    new Command (ExitOnSave, "")
                ];
                CH.ClearFeedback();
                AL.RunArch(Commands);

                var Result = CH.GetFeedback();

                #endregion                
                #region Checking if it looks as expected.                

                var ExpectedValue = "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage Buildings\r\n2 Manage Beacons\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nPlease provide the item name: \r\nSimulate: TestBuilding\r\nItem added.\r\n-------------------------\r\nAdd Buildings - <Version>\r\n-------------------------\r\n1 Add Buildings\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n9) TestBuilding\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 5 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 5 --- \r\n<ClearConsole>\r\nSaved the item successfully - <Date>\r\n-------------------------\r\nSave Buildings to a file - <Version>\r\n-------------------------\r\n1 Save Buildings to a file\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nSave Buildings to a file - <Version>\r\n-------------------------\r\n1 Save Buildings to a file\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\n<ProgramExited>\r\n";
                ExpectedValue = ExpectedValue.Replace(Entity[0], "<Entity>").Replace(Entity[1], "<Entity>");
                ExpectedValue = ExpectedValue.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");
                ExpectedValue = ExpectedValue.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");

                OriginalResults[0] = Result;
                Result = Result.Replace(Entity[0], "<Entity>").Replace(Entity[1], "<Entity>");
                Result = Result.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");
                Result = Result.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");

                #endregion
                #region Create a file if the result and expected does not match
                var FailedTestPath = $"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}";

                if (!Directory.Exists($"{CodeGenHelper.WorkingFolder}\\TestResults"))
                    Directory.CreateDirectory($"{CodeGenHelper.WorkingFolder}\\TestResults");
                if (!Directory.Exists($"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}"))
                    Directory.CreateDirectory($"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}");

                if (ExpectedValue != Result)
                {

                    if (File.Exists($"{FailedTestPath}\\Result"))
                        File.Delete($"{FailedTestPath}\\Result");
                    if (File.Exists($"{FailedTestPath}\\Expected"))
                        File.Delete($"{FailedTestPath}\\Expected");
                    File.WriteAllText($"{FailedTestPath}\\Result", Result);
                    File.WriteAllText($"{FailedTestPath}\\Expected", ExpectedValue);
                }
                else
                {
                    if (File.Exists($"{FailedTestPath}\\Result"))
                        File.Delete($"{FailedTestPath}\\Result");
                    if (File.Exists($"{FailedTestPath}\\Expected"))
                        File.Delete($"{FailedTestPath}\\Expected");
                }
                #endregion
                #region Check if expected matches result
                Assert.True(ExpectedValue == Result);
                #endregion

                #region Search for TestBuidling, Load from the files, Search again.

                Commands =
                 [
                     new Command (ManageEntity, ""),
                     new Command (View, ""),
                     new Command (Search, SearchValues),
                     new Command (BackFromSearch, ""),
                     new Command (Load, ""),
                     new Command (BackFromLoad, ""),
                     new Command (View, ""),
                     new Command (Search, SearchValues),
                     new Command (BackFromSearch, ""),
                     new Command (Edit, EditValues),
                     new Command (BackFromEdit, ""),
                     new Command (View, ""),
                     new Command (Search, SearchValues),
                     new Command (Search, SearchEditedValues),
                     new Command (BackFromSearch, ""),
                     new Command (Remove, RemoveValue),
                     new Command (BackFromRemove, ""),
                     new Command (View, ""),
                     new Command (Search, SearchEditedValues),
                     new Command (BackFromSearch, ""),
                     new Command (View, ""),
                     new Command (Next, "")

                 ];
                CH.ClearFeedback();
                AL.RunArch(Commands);
                Result = CH.GetFeedback();

                #endregion
                #region Check if the result matches the expected value
                ExpectedValue = "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage Buildings\r\n2 Manage Beacons\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\nNo Items Was Found\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 6 --- \r\n<ClearConsole>\r\nItems Loaded Successfully Buildings - <Date>\r\nItem Exists - No action - Old item: Alpha-Buildings - <GUID> - New Item: Alpha-Buildings - <GUID>\r\nItem Exists - No action - Old item: Beta-Buildings - <GUID> - New Item: Beta-Buildings - <GUID>\r\nItem Exists - No action - Old item: Charlie-Buildings - <GUID> - New Item: Charlie-Buildings - <GUID>\r\nItem Exists - No action - Old item: Delta-Buildings - <GUID> - New Item: Delta-Buildings - <GUID>\r\nItem Exists - No action - Old item: Echo-Buildings - <GUID> - New Item: Echo-Buildings - <GUID>\r\nItem Exists - No action - Old item: Foxtrot-Buildings - <GUID> - New Item: Foxtrot-Buildings - <GUID>\r\nItem Exists - No action - Old item: Golf-Buildings - <GUID> - New Item: Golf-Buildings - <GUID>\r\nItem Exists - No action - Old item: Hotel-Buildings - <GUID> - New Item: Hotel-Buildings - <GUID>\r\nItem Added - New Item: TestBuilding - <GUID>\r\n-------------------------\r\nLoad Buildings from a file - <Version>\r\n-------------------------\r\n1 Load Buildings from a file\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n9) TestBuilding\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nSelect an item to edit - <Version>\r\n-------------------------\r\n9) TestBuilding\r\n-------------------------\r\nPages : 1/1\r\nSelect an item to edit\r\nSimulate: 9\r\nPlease enter a new name\r\nSimulate: EditedBuilding\r\nItem was modified\r\n-------------------------\r\nEdit Buildings - <Version>\r\n-------------------------\r\n1 Edit Buildings\r\n2 Next Page\r\n3 Last Page\r\n4 Back to Main Menu\r\n5 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestBuilding\r\nNo Items Was Found\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: EditedBuilding\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n6) EditedBuilding\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nSelect the item to remove - <Version>\r\n-------------------------\r\n6) EditedBuilding\r\n-------------------------\r\nPages : 1/1\r\nSelect the item to remove\r\nSimulate: 6\r\nItem removed.\r\n-------------------------\r\nRemove Buildings - <Version>\r\n-------------------------\r\n1 Remove Buildings\r\n2 Next Page\r\n3 Last Page\r\n4 Back to Main Menu\r\n5 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: EditedBuilding\r\nNo Items Was Found\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n6) Foxtrot-Buildings\r\n7) Golf-Buildings\r\n8) Hotel-Buildings\r\n-------------------------\r\nPages : 2/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 First Page\r\n4 Previous Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\n<ProgramExited>\r\n";

                ExpectedValue = ExpectedValue.Replace(Entity[0], "<Entity>").Replace(Entity[1], "<Entity>");
                ExpectedValue = ExpectedValue.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");
                ExpectedValue = ExpectedValue.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");

                OriginalResults[1] = Result;

                Result = Result.Replace(Entity[0], "<Entity>").Replace(Entity[1], "<Entity>");
                Result = Result.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");
                Result = Result.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");

                #endregion
                #region Create a file if the result and expected does not match
                if (ExpectedValue != Result)
                {

                    if (File.Exists($"{FailedTestPath}\\Result"))
                        File.Delete($"{FailedTestPath}\\Result");
                    if (File.Exists($"{FailedTestPath}\\Expected"))
                        File.Delete($"{FailedTestPath}\\Expected");
                    File.WriteAllText($"{FailedTestPath}\\Result", Result);
                    File.WriteAllText($"{FailedTestPath}\\Expected", ExpectedValue);
                }
                else
                {
                    if (File.Exists($"{FailedTestPath}\\Result"))
                        File.Delete($"{FailedTestPath}\\Result");
                    if (File.Exists($"{FailedTestPath}\\Expected"))
                        File.Delete($"{FailedTestPath}\\Expected");
                }
                #endregion
                #region Check if expected matches result
                Assert.True(ExpectedValue == Result);
                #endregion
                #region Save test results

                if (!Directory.Exists($"{CodeGenHelper.WorkingFolder}\\TestResults"))
                    Directory.CreateDirectory($"{CodeGenHelper.WorkingFolder}\\TestResults");

                if (!Directory.Exists($"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}"))
                    Directory.CreateDirectory($"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}");
                #endregion

                #region Test if you can add duplicate items?
                string Scenario = "Scenario_2_";
                AddValues = ["TestDuplicate"];
                EditValues = ["8", "TestDuplicate"];
                SearchValues = ["TestDuplicate"];
                SearchEditedValues = ["TestDuplicate"];
                string[] EditSecondTimeValues = ["5", "TestDuplicate"];


                Commands =
                 [
                     new Command (ManageEntity, ""),
                     new Command (Add, AddValues),
                     new Command (Add, AddValues),
                     new Command (Add, AddValues),
                     new Command (BackFromAdd, ""),
                     new Command (Edit, EditValues),
                     new Command (Edit, EditSecondTimeValues),
                     new Command (Edit, EditSecondTimeValues),
                     new Command (BackFromEdit, ""),
                     new Command (View, ""),
                     new Command (Search, SearchValues),
                     new Command (Search, SearchEditedValues),
                     new Command (BackFromSearch, ""),

                 ];
                CH.ClearFeedback();
                AL.RunArch(Commands);
                Result = CH.GetFeedback();

                #endregion
                #region Checking if it looks as expected.                

                ExpectedValue = "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage Buildings\r\n2 Manage Beacons\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nPlease provide the item name: \r\nSimulate: TestDuplicate\r\nItem added.\r\n-------------------------\r\nAdd Buildings - <Version>\r\n-------------------------\r\n1 Add Buildings\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nPlease provide the item name: \r\nSimulate: TestDuplicate\r\nDuplicate entry found - operation aborted.\r\n-------------------------\r\nAdd Buildings - <Version>\r\n-------------------------\r\n1 Add Buildings\r\n2 Back to Main Menu\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nSelect an item to edit - <Version>\r\n-------------------------\r\n6) Foxtrot-Buildings\r\n7) Golf-Buildings\r\n8) Hotel-Buildings\r\n-------------------------\r\nPages : 1/1\r\nSelect an item to edit\r\nSimulate: 8\r\nPlease enter a new name\r\nSimulate: TestDuplicate\r\nDuplicate entry found - operation aborted.\r\n-------------------------\r\nEdit Buildings - <Version>\r\n-------------------------\r\n1 Edit Buildings\r\n2 Next Page\r\n3 Last Page\r\n4 Back to Main Menu\r\n5 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- System.String[]\r\n<ClearConsole>\r\n-------------------------\r\nSelect an item to edit - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\nSelect an item to edit\r\nSimulate: 5\r\nPlease enter a new name\r\nSimulate: TestDuplicate\r\nDuplicate entry found - operation aborted.\r\n-------------------------\r\nEdit Buildings - <Version>\r\n-------------------------\r\n1 Edit Buildings\r\n2 Next Page\r\n3 Last Page\r\n4 Back to Main Menu\r\n5 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 4 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n<ClearConsole>\r\n-------------------------\r\nBuildings - <Version>\r\n-------------------------\r\n1) Alpha-Buildings\r\n2) Beta-Buildings\r\n3) Charlie-Buildings\r\n4) Delta-Buildings\r\n5) Echo-Buildings\r\n-------------------------\r\nPages : 1/2\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Next Page\r\n4 Last Page\r\n5 Back to Main Menu\r\n6 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestDuplicate\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n9) TestDuplicate\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- System.String[]\r\n<ClearConsole>\r\nEnter an item name to search for\r\nSimulate: TestDuplicate\r\n-------------------------\r\nSearch Items - <Version>\r\n-------------------------\r\n9) TestDuplicate\r\n-------------------------\r\nPages : 1/1\r\n-------------------------\r\nRefresh - <Version>\r\n-------------------------\r\n1 Refresh\r\n2 Search\r\n3 Back to Main Menu\r\n4 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 3 --- \r\n<ClearConsole>\r\n-------------------------\r\nView Buildings - Sub Menu - <Version>\r\n-------------------------\r\n1 View Buildings\r\n2 Add Buildings\r\n3 Remove Buildings\r\n4 Edit Buildings\r\n5 Save Buildings\r\n6 Load Buildings\r\n7 Back to Main Menu\r\n8 Exit\r\n-------------------------\r\nChoice:\r\n<ProgramExited>\r\n";

                ExpectedValue = ExpectedValue.Replace(Entity[0], "<Entity>").Replace(Entity[1], "<Entity>");
                ExpectedValue = ExpectedValue.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");
                ExpectedValue = ExpectedValue.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");

                OriginalResults[0] = Result;
                Result = Result.Replace(Entity[0], "<Entity>").Replace(Entity[1], "<Entity>");
                Result = Result.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 2 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");
                Result = Result.Replace("Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- 1 --- \r\n", "Loaded the menus successfully - <Date>\r\n-------------------------\r\nGalaxy Manager - Main Menu - <Version>\r\n-------------------------\r\n1 Manage <Entity>\r\n2 Manage <Entity>\r\n3 Exit\r\n-------------------------\r\nChoice:\r\nSimulate:  -- <Manage> --- \r\n");

                #endregion
                #region Create a file if the result and expected does not match
                if (ExpectedValue != Result)
                {
                    if (File.Exists("Result"))
                        File.Delete("Result");
                    if (File.Exists("Expected"))
                        File.Delete("Expected");
                    File.WriteAllText("Result", Result);
                    File.WriteAllText("Expected", ExpectedValue);
                }
                else
                {
                    if (File.Exists("Result"))
                        File.Delete("Result");
                    if (File.Exists("Expected"))
                        File.Delete("Expected");

                }
                #endregion
                #region Check if expected matches result
                Assert.True(ExpectedValue == Result);
                #endregion
                #region Save test results
                var ResultsPath = $"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}\\{Scenario}{CodeGenHelper.CurrentGuid}_{Entity[i]}";

                if (!Directory.Exists($"{CodeGenHelper.WorkingFolder}\\TestResults"))
                    Directory.CreateDirectory($"{CodeGenHelper.WorkingFolder}\\TestResults");

                if (!Directory.Exists($"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}"))
                    Directory.CreateDirectory($"{CodeGenHelper.WorkingFolder}\\TestResults\\{CodeGenHelper.CurrentGuid}");

                try
                {
                    for (int j = 0; j < OriginalResults.Length; j++)
                    {
                        var ResultsPathInternal = $"{ResultsPath}_TestResult{j + 1}";

                        if (File.Exists(ResultsPathInternal))
                            File.Delete(ResultsPathInternal);

                        File.WriteAllText(ResultsPathInternal, OriginalResults[j]);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Logger.Log($"Unit Test Failed to create output files. - {ex.Message} -- {ex.InnerException?.Message}");
                }
                #endregion
            }

            #region Cleaning up. Removing the Building and Beacons menu items file.

            if (File.Exists(TargetPath))
                File.Delete(TargetPath);

            #endregion
            #region Asserting if the file {542fcda8-12dd-4fd1-9701-80d61171d215} was removed.

            Assert.False(File.Exists(TargetPath), "The saved menu file exist. Cleanup failed.");

            #endregion        
        }

    }

}