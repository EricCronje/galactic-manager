using System.Text;
using AM = ArchCorpUtilities.Models.Menus.MenuHelper;
using AR = ArchCorpUtilities.Models.MockBuildingsRepository;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

namespace ArchCorpUtilities.Models.Buildings
{
    public static class BuildingHelper
    {
        public static List<Building>? Buildings { get; set; }
        private readonly static AR aR = new();

        public static int? GetMaxBuildingId()
        {
            return aR.GetMaxId();
        }
        public static int? GetNextBuildingId()
        {
            return aR.GetMaxId() + 1;
        }

        public static bool IsDuplicateEntry(string? buildingName)
        {
            return Buildings?.FirstOrDefault(p => p.Name?.ToUpper() == buildingName?.ToUpper()) != null;
        }

        static BuildingHelper()
        {
            Buildings = [];
            Buildings = aR.AllBuildings()?.ToList();
        }

        public static void ReIndexDisplayId()
        {
            int counter = 1;
            if (Buildings != null)
                foreach (var item in Buildings.OrderBy(c => c.Name))
                {
                    item.DisplayId = counter++;
                }
        }

        public static void ViewBuildings()
        {
            if (Buildings?.Count > 0)
            {
                CH.Feedback(AM.ShowHeading(Resource.BuildingListPrompt));
                foreach (var building in Buildings.OrderBy(c => c.Name))
                {
                    CH.Feedback($"{building.DisplayId}) {building.Name}");
                }
            }
            else
                CH.Feedback($"{Resource.NoBuildingsToList}\n");
        }

        public static void AddBuilding(string? bName)
        {
            if (bName != null)
            {
                int? Id = GetNextBuildingId();
                if (Id != null && Id.HasValue)
                {
                    if (!bName.Equals("x") && !string.IsNullOrWhiteSpace(bName) && !IsDuplicateEntry(bName))
                    {
                        Buildings?.Add(new Building(bName, Id.Value));
                        CH.Feedback(Resource.BuildingAdded);
                        ReIndexDisplayId();
                    }
                    else
                        CH.Feedback(Resource.NoBuildingsAdded);
                }
                else
                {
                    CH.Feedback(Resource.FailedToAddBuilding);
                }
            }
            else
                CH.Feedback(Resource.FailedToAddBuildingNameInvalid);
        }

        public static void EditBuilding(Building? building, string newBuildingName)
        {
            if (Buildings?.Count > 0)
            {
                if (building != null)
                {
                    if (newBuildingName != null)
                    {
                        if (!IsDuplicateEntry(newBuildingName))
                        {
                            if (building != null)
                            {
                                Buildings?.Remove(building);
                                int? Id = GetNextBuildingId();
                                if (Id != null && Id.HasValue)
                                {
                                    Buildings?.Add(new Building(newBuildingName, Id.Value));
                                    ReIndexDisplayId();
                                }
                                CH.Feedback($"{Resource.BuildingUpdatedFromPrompt} {building.Name} to {newBuildingName}");
                            }
                        }
                        else
                        {
                            CH.Feedback(Resource.DuplicateEntryMessage);
                        }
                    }
                    else
                    {
                        CH.Feedback(Resource.NoBuildingNameEntered);
                    }
                }
                else
                {
                    CH.Feedback(Resource.NoBuildingNameSelected);
                }
            }
        }

        public static Building? GetCurrentBuilding(string? simChoice = null)
        {
            _ = int.TryParse(CH.GetInput(simChoice), out int BuildingChoiceId);
            var building = Buildings?.FirstOrDefault(c => c.DisplayId.Equals(BuildingChoiceId));
            return building;
        }

        public static void RemoveBuilding(Building? building)
        {
            if (Buildings?.Count > 0)
            {
                if (building != null)
                {
                    Buildings?.Remove(building);
                    CH.Feedback($"{Resource.BuildingRemoved} {building.Name}");
                    ReIndexDisplayId();
                }
                else
                {
                    CH.Feedback(Resource.NoBuildingRemovedPrompt);
                }
            }

        }

        public static void SaveBuildings(string path = "Buildings.txt")
        {
            if (Buildings != null && Buildings.Count > 0)
            {
                StringBuilder sb = new();
                try
                {
                    sb.AppendLine($"BuidlingID|BuidldingDisplayId|BuidlingName");
                    foreach (var item in Buildings.OrderBy(c => c.Name))
                    {
                        sb.AppendLine($"{item.Id}|{item.DisplayId}|{item.Name}");
                    }

                    if (File.Exists(path))
                        File.Delete(path);

                    File.AppendAllText(path, sb.ToString());
                    CH.Feedback($"{Resource.SaveBuidlingSuccess} {path}");
                }
                catch (Exception ex)
                {
                    CH.Feedback($"{Resource.SaveBuidlingsError} {ex.Message}");
                }
                finally
                {
                    sb?.Clear();
                }

            }
            else
            {
                CH.Feedback(Resource.NoDataSaved);
            }
        }

        public static void LoadBuildings(string path = "Buildings.txt")
        {
            try
            {

                CH.Feedback($"{Resource.BuildingFileImportCompletionSuccessMessage} {path}");

                string FileInput = File.ReadAllText(path);
                bool SkipFirstLine = true;
                foreach (string line in FileInput.Split("\r\n"))
                {
                    if (!SkipFirstLine)
                    {
                        string[] LineItem = line.Split("|");
                        if (LineItem.Length > 0 && LineItem.Length > 2)
                        {
                            _ = int.TryParse(LineItem[0].Trim(), out int id);
                            _ = int.TryParse(LineItem[1].Trim(), out int displayId);
                            string BuildingName = LineItem[2].Trim();
                            var building = Buildings?.FirstOrDefault(c => c.Id == id);
                            if (building == null)
                            {
                                Buildings?.Add(new Building(BuildingName, id));
                                CH.Feedback($"{Resource.BuildingFileImportProgressSuccessMessage} - {BuildingName} - {id} - {displayId}");
                            }
                            else
                                CH.Feedback($"{Resource.BuildingFileImportProgressFailedMessage} {building.Name} - {building.Id} - {building.DisplayId} -->{BuildingName} - {id} - {displayId}");
                        }
                    }
                    SkipFirstLine = false;
                }

                ReIndexDisplayId();

            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void PerformDefaultTasks(int page, int prevPage, int choice, int? simChoice = null, string? simInput = null)
        {
            // if simulation use the simChoice
            if (simChoice != null) { choice = simChoice.Value; }

            int? defaultChoice = AM.CurrentMenuPage?.FirstOrDefault(c => c.IsDefaultChoice && c.Page == page)?.DisplayNumber;
            bool isSamePage = page.Equals(prevPage);

            if (isSamePage) { defaultChoice = choice; }
            // For simulation only

            var TargetTask = AM.CurrentMenuPage?.FirstOrDefault(c => c.Page == page && c.DisplayNumber == defaultChoice)?.TargetTask;

            switch (TargetTask)
            {
                case "AddBuilding":
                    CH.Feedback(Resource.AddBuildingHint);
                    CH.Feedback(Resource.EnterBuildingNamePrompt);
                    string? BuildingName = CH.GetInput(simInput);

                    AddBuilding(BuildingName);
                    break;

                case "ViewBuilding":
                    ViewBuildings();
                    break;

                case "EditBuilding":
                    ViewBuildings();
                    CH.Feedback(Resource.HorizontalLine);
                    CH.Feedback(Resource.SelectABuildingToEditPrompt);
                    Building? building = GetCurrentBuilding(Convert.ToString(simChoice));
                    if (building != null)
                    {
                        CH.Feedback(Resource.EnterANewBuildingNamePrompt);
                        string? NewBuidlingName = CH.GetInput(simInput);
                        EditBuilding(building, NewBuidlingName);
                    }
                    else
                        CH.Feedback(Resource.FailedToAddBuilding);
                    break;

                case "RemoveBuilding":
                    ViewBuildings();
                    CH.Feedback(Resource.HorizontalLine);
                    CH.Feedback(Resource.SelectABuildingToRemove);
                    CH.Feedback(Resource.HorizontalLine);
                    building = GetCurrentBuilding(Convert.ToString(simChoice));
                    RemoveBuilding(building);
                    break;

                case "SaveBuildings":
                    SaveBuildings();
                    break;

                case "LoadBuildings":
                    LoadBuildings();
                    break;

                default:
                    break;
            }
        }

    }

}
