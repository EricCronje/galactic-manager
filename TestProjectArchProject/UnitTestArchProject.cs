using ArchCorpUtilities.Models.Buildings;
using ArchCorpUtilities.Models.Menus;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using System.Text;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using MBR = ArchCorpUtilities.Models.Buildings.MockBuildingsRepository;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace TestProjectArchProject
{
    public class UnitTestArchProject
    {
        private const int MaxMenuCountV = 92;

        [Fact]
        public void ValidCheckMenuItems()
        {
            Assert.False(MH.Menu == null);
            StringBuilder sb = new();
            if (MH.Menu != null)
            {
                sb.AppendJoin("|", MH.Menu.AsEnumerable<MenuItem>().Select(p => p.DisplayName));

                string result = sb.ToString();

                Assert.Contains("View Buildings", result);
                Assert.Contains("Remove Buildings", result);
                Assert.Contains("Add Buildings", result);
                Assert.Contains("Edit Building Names", result);
                Assert.Contains("Save Buildings", result);
                Assert.Contains("Load Buildings", result);
            }
        }

        [Fact]
        public void ValidListBuildings()
        {
            MBR mockBuildingsRepository = new();
            BH.Buildings = mockBuildingsRepository.All()?.ToList();
            CH.ClearFeedback();
            U.ViewWithPagination("Building List", BH.Page, BH.Buildings);
            string Result = CH.GetFeedback();
            Assert.Contains("Alpha", Result);
            Assert.Contains("Beta", Result);
        }

        [Fact]
        public void ValidAddAndRemoveBuildings()
        {

            MBR mockBuildingsRepository = new();
            string? Name = "Ultra Building";
            CH.ClearFeedback();
            BH.AddBuilding(Name);
            string Result = CH.GetFeedback();
            Assert.Contains("Building added successfully", Result);

            CH.ClearFeedback();
            U.ViewWithPagination("Building List", BH.Page, BH.Buildings, U.Navigation.NextPage);
            Result = CH.GetFeedback();
            Assert.Contains(Name, Result);

            var Building = BH.Buildings?.FirstOrDefault(c => c.Name == Name);
            Assert.NotNull(Building);
            BH.RemoveBuilding(Building);
            Result = CH.GetFeedback();
            Assert.Contains("Building removed Ultra Building", Result);

            CH.ClearFeedback();
            U.ViewWithPagination("Building List", BH.Page, BH.Buildings);
            Result = CH.GetFeedback();
            Assert.DoesNotContain(Name, Result);
        }

        [Fact]
        public void ValidEditBuildings()
        {
            string? bName = "Dragon One";
            CH.ClearFeedback();
            var building = BH.Repository.GetByName(bName)?.ToList()[0];
            Assert.NotNull(building);
            string NewBuildingName = "Bob One";
            BH.EditBuilding(building, NewBuildingName);
            var Result = CH.GetFeedback();
            Assert.Contains("Building Edited Successfully", Result);
            building = BH.Repository.GetByName(NewBuildingName)?.ToList()[0];            
            Assert.NotNull(building);
            Assert.Contains(NewBuildingName, building.Name);
            BH.EditBuilding(building, bName);
            Assert.Contains("Building Edited Successfully", Result);
            building = BH.Repository.GetByName(bName)?.ToList()[0];
            Assert.NotNull(building);
            Assert.Contains(bName, building.Name);
        }

        [Fact]
        public void ValidSaveBuildings()
        {
            // Setup the mock data
            MBR mockBuildingsRepository = new();
            //Load the mock data.
            BH.Buildings = mockBuildingsRepository.All()?.ToList();

            string Path = "TestBuildingSaveOne.txt";
            // If the file exists for some reason ... Failed attempt ...
            if (File.Exists(Path))
                File.Delete(Path);

            // Make sure that the file does not exist before hand
            Assert.False(File.Exists(Path));

            //Save the mock data
            BH.SaveBuildings(Path);
            Assert.True(File.Exists(Path));

            // Get the content
            string content = File.ReadAllText(Path);

            //Check if the following buildings are present...
            Assert.Contains("Alpha", content);
            Assert.Contains("Beta", content);

            //Make sure that there are no duplicates - if the length is bigger than this...
            Assert.True(content.Length == 205);

            //Make sure there are five lines. (Last line is empty...
            int Lines = content.Split('\n').Length;
            Assert.Equal(5, Lines);

            // Remove the file if it exists (Clean up)
            if (File.Exists(Path))
                File.Delete(Path);

        }

        [Fact]
        public void ValidLoadBuilding()
        {
            // Prepare the mock data
            MBR mockBuildingsRepository = new();
            // Load the buildings with the mock data
            BH.Buildings = mockBuildingsRepository.All()?.ToList();

            string Path = "TestBuildingSaveTwo.txt";
            // Remove the file if it exists
            if (File.Exists(Path))
                File.Delete(Path);

            // Create a new file using save ...
            Assert.False(File.Exists(Path));
            BH.SaveBuildings(Path);
            Assert.True(File.Exists(Path));

            // Make sure the data is still there - is should be.
            Assert.NotNull(BH.Buildings);

            // Clear it - to load the next file...
            BH.Buildings?.Clear();
            Assert.True(BH.Buildings?.Count == 0);

            // Perform the load.
            BH.LoadBuildings(Path);
            // Make sure there are items
            Assert.False(BH.Buildings.Count == 0);

            //Check if the mock data are there - start
            var buidling = BH.Buildings?.FirstOrDefault(c => c.Name != null && c.Name.Equals("Dragon One"));
            Assert.NotNull(buidling);
            buidling = BH.Buildings?.FirstOrDefault(c => c.Name != null && c.Name.Equals("Alpha"));
            Assert.NotNull(buidling);
            buidling = BH.Buildings?.FirstOrDefault(c => c.Name != null && c.Name.Equals("Beta"));
            //Check if the mock data are there - end

            // Make sure there are only three items.
            Assert.NotNull(buidling);
            Assert.True(BH.Buildings?.Count == 3);

            // Clean up.
            File.Delete(Path);
            //Make sure it is gone.
            Assert.False(File.Exists(Path));
        }

        [Fact]
        public void ValidExportMenu()
        {
            string Path = "TestMenus.txt";
            MH.ExportMenus(Path);
            Assert.True(File.Exists("TestMenus.txt"));
            Assert.Contains("|", File.ReadAllText(Path));
            File.Delete(Path);
        }

        [Fact]
        public void ValidImportMenu()
        {
            string Path = "TestMenusDemo.txt";
            MH.ExportMenus(Path);
            Assert.True(File.Exists("TestMenusDemo.txt"));
            Assert.Contains("|", File.ReadAllText(Path));
            List<MenuItem>? MenuItems = MH.ImportMenu(Path);
            File.Delete(Path);
            Assert.NotNull(MenuItems);
            Assert.True(MenuItems.Count == MaxMenuCountV);
        }

        [Fact]
        public void ValidImportMenuExistingGuid()
        {
            string Path = "TestMenusDemo.txt";
            MH.ExportMenus(Path);
            Assert.True(File.Exists("TestMenusDemo.txt"));
            Assert.Contains("|", File.ReadAllText(Path));
            List<MenuItem>? MenuItems = MH.ImportMenu(Path);
            File.Delete(Path);
            Assert.NotNull(MenuItems);
            Assert.True(MenuItems.Count == MaxMenuCountV);
            var MenuMockData = MH.Menu.FirstOrDefault(p => p.DisplayName == "View Buildings");
            var MenuFileData = MenuItems.FirstOrDefault(p => p.DisplayName == "View Buildings");
            Assert.NotNull(MenuFileData);
            Assert.NotNull(MenuMockData);
            Assert.True(MenuMockData.IDGUIDMenu == MenuFileData.IDGUIDMenu);
        }

        [Fact]
        public void InValidImportMenuNotExistingGuid()
        {
            string Path = "TestMenusDemoEmptyGuids.txt";
            Assert.True(File.Exists(Path));
            Assert.Contains("|", File.ReadAllText(Path));
            List<MenuItem>? MenuItems = MH.ImportMenu(Path);
            Assert.NotNull(MenuItems);
            Assert.True(MenuItems.Count == 89);

            var MenuFileData = MenuItems?.FirstOrDefault(p => p.DisplayName == "View Buildings");
            Assert.NotNull(MenuFileData);
            Assert.True(MenuFileData.IDGUIDMenu.Length > 0);
            Assert.True(Guid.TryParse(MenuFileData.IDGUIDMenu, out Guid guid));
            Assert.NotNull(guid.ToString());

            var MenuMockData = MH.Menu.FirstOrDefault(p => p.DisplayName == "View Buildings");
            Assert.NotNull(MenuMockData);
            Assert.False(MenuMockData.IDGUIDMenu == MenuFileData.IDGUIDMenu);
        }

        [Fact]
        public void InValidEditBuildingsGiveBlankName()
        {
            MBR Repository = new();
            string? bName = "Dragon One";
            CH.ClearFeedback();
            var building = Repository.GetByName(bName)?.ToList()[0];
            Assert.NotNull(building);
            string NewBuildingName = "";
            BH.EditBuilding(building, NewBuildingName);
            var Result = CH.GetFeedback();
            building = BH.Buildings?.FirstOrDefault(c => c.Name == NewBuildingName);
            Assert.Null(building);
            Assert.Contains("No building name entered", Result);
        }

        [Fact]
        public void InvalidDuplicateBuildingName()
        {
            string? bName = "Dragon One";
            CH.ClearFeedback();
            BH.AddBuilding(bName);
            var Result = CH.GetFeedback();
            Assert.Contains("No building name entered or duplicate building name", Result);

        }
    }
}
