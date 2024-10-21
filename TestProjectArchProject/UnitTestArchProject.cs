using System.Text;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using MBR = ArchCorpUtilities.Models.MockBuildingsRepository;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using AL = ArchCorpUtilities.Models.ArchLoader;
using AC = ArchCorpUtilities.Utilities.Command;
using System.ComponentModel.Design;

namespace TestProjectArchProject
{
    public class UnitTestArchProject
    {
        [Fact]
        public void ValidCheckIntro()
        {
            Assert.Equal("Building Manager - Main Menu - V1.0.0", MH.StartingPageHeading);
            Assert.Equal("-------------------------", MH.Line);
        }
        [Fact]
        public void ValidCheckLine()
        {
            Assert.Equal("-------------------------", MH.Line);
        }

        [Fact]
        public void ValidCheckMenuItems()
        {
            Assert.False(MH.CurrentMenuPage == null);
            StringBuilder sb = new();
            if (MH.CurrentMenuPage != null)
            {
                foreach (var item in MH.CurrentMenuPage)
                {
                    sb.AppendLine(item.DisplayName);
                }

                string result = sb.ToString();
                string expected = "List Buildings\r\nAdd Buildings\r\nRemove Buildings\r\nEdit Building Names\r\nSave Buildings\r\nLoad Buildings\r\nExit\r\nAdd Building\r\nBack to Main Menu\r\nView Building\r\nBack to Main Menu\r\nEdit Building\r\nBack to Main Menu\r\nRemove Building\r\nBack to Main Menu\r\nSave buildings to a file\r\nBack to Main Menu\r\nLoad buildings from a file\r\nBack to Main Menu\r\n";
                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void ValidListBuildings()
        {
            MBR mockBuildingsRepository = new();
            BH.Buildings = mockBuildingsRepository.AllBuildings()?.ToList();
            CH.ClearFeedback();
            BH.ViewBuildings();
            string Result = CH.GetFeedback();
            string Expected = "-------------------------\r\nBuildings list:\r\n-------------------------\r\n1) Alpha\r\n2) Beta\r\n3) Dragon One\r\n";
            Assert.Equal(Result, Expected);
        }

        [Fact]
        public void ValidAddAndRemoveBuildings()
        {

            MBR mockBuildingsRepository = new();
            BH.Buildings = mockBuildingsRepository.AllBuildings()?.ToList();
            string? Name = "Ultra Building";
            CH.ClearFeedback();
            BH.AddBuilding(Name);
            string Result = CH.GetFeedback();
            Assert.Contains("Building added successfully", Result);

            CH.ClearFeedback();
            BH.ViewBuildings();
            Result = CH.GetFeedback();
            Assert.Contains(Name, Result);

            var Building = BH.Buildings?.FirstOrDefault(c => c.Name == Name);
            Assert.NotNull(Building);
            BH.RemoveBuilding(Building);
            Result = CH.GetFeedback();
            Assert.Contains("Building removed Ultra Building", Result);

            CH.ClearFeedback();
            BH.ViewBuildings();
            Result = CH.GetFeedback();
            Assert.DoesNotContain(Name, Result);
        }

        [Fact]
        public void ValidEditBuildings()
        {
            MBR mockBuildingsRepository = new();
            BH.Buildings = mockBuildingsRepository.AllBuildings()?.ToList();
            string? bName = "Dragon One";
            CH.ClearFeedback();
            var building = BH.Buildings?.FirstOrDefault(c => c.Name == bName);
            Assert.NotNull(building);
            string NewBuildingName = "Bob One";
            BH.EditBuilding(building, NewBuildingName);
            var Result = CH.GetFeedback();
            Assert.Contains(bName, Result);
            building = BH.Buildings?.FirstOrDefault(c => c.Name == NewBuildingName);
            Assert.NotNull(building);
            BH.EditBuilding(building, bName);
            Assert.Contains(bName, Result);
        }

        [Fact]
        public void ValidSaveBuildings()
        {
            // Setup the mock data
            MBR mockBuildingsRepository = new();
            //Load the mock data.
            BH.Buildings = mockBuildingsRepository.AllBuildings()?.ToList();

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
            Assert.Contains("Dragon One", content);
            Assert.Contains("Alpha", content);
            Assert.Contains("Beta", content);

            //Make sure that there are no duplicates - if the length is bigger than this...
            Assert.True(content.Length == 81);

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
            BH.Buildings = mockBuildingsRepository.AllBuildings()?.ToList();

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
    }
}