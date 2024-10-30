using System.Text;
using AM = ArchCorpUtilities.Models.Menus.MenuHelper;
using AR = ArchCorpUtilities.Models.MockBuildingsRepository;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using Page = Patina.Patina;
using L = Logger.Logger;


namespace ArchCorpUtilities.Models.Buildings
{
    public static class BuildingHelper
    {
        public static List<Building>? Buildings { get; set; }
        public static string SessionID { get; internal set; }

        private readonly static AR aR = new();

        static readonly UInt32 _PageSize;
        static UInt32 _MaxItems = Convert.ToUInt32(Buildings?.Count);
        static Page _Page;

        //public static int? GetMaxBuildingId()
        //{
        //    L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
        //    return aR.GetMaxId();
        //}
        public static int? GetNextBuildingId()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            return aR.GetMaxId() + 1;
        }

        public static bool IsDuplicateEntry(string? buildingName)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            return Buildings?.FirstOrDefault(p => p.Name?.ToUpper() == buildingName?.ToUpper()) != null;
        }

        static BuildingHelper()
        {
            SessionID = "None";
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            Buildings = [];
            Buildings = aR.AllBuildings()?.ToList();
            _PageSize = 2;
            _MaxItems = Convert.ToUInt32(Buildings?.Count);
            _Page = new(_PageSize, _MaxItems);
        }

        public static void ReIndexDisplayId(bool renewPage = true, List<Building>? buildings = null)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            int counter = 1;
            if (buildings != null)
            {
                foreach (var item in buildings.OrderBy(c => c.Name))
                {
                    item.DisplayId = counter++;
                    item.Id = item.DisplayId;
                }

                if (renewPage)
                {
                    _MaxItems = Convert.ToUInt32(buildings?.Count);
                    _Page = new(_PageSize, _MaxItems);
                }
            }
        }

        public static List<Building>? ViewBuildings(List<Building>? buildings ,string navigation = "FirstPage", int GoToPageNumber = -1)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            List<Building>? paginatedBuildings = GetPagination(buildings, navigation, GoToPageNumber);

            ShowBuildings(paginatedBuildings);

            return paginatedBuildings;
        }

        private static void ShowBuildings(List<Building>? paginatedBuildings)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (paginatedBuildings != null && paginatedBuildings.Count > 0)
            {
                CH.Feedback(AM.ShowHeading(Resource.BuildingListPrompt));
                foreach (var building in paginatedBuildings.OrderBy(c => c.Name))
                {
                    CH.Feedback($"{building.DisplayId}) {building.Name}");
                }
                CH.Feedback(Resource.HorizontalLine);
                CH.Feedback($"Pages : {_Page.GetPageNumberCaption()}");
            }
            else
                CH.Feedback($"{Resource.NoBuildingsToList}\n");
        }

        private static List<Building>? GetPagination(List<Building>? buildings, string navigation, int GoToPageNumber)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            List<Building>? paginatedBuildings = null;
            var OrderdBuildings = buildings?.OrderBy(p => p.Name).ToList();
            if (OrderdBuildings?.Count > 0)
            {
                if (GoToPageNumber < 0)
                {
                    switch (navigation)
                    {
                        case "FirstPage":
                            _Page.GoToFirstPage();
                            break;
                        case "LastPage":
                            _Page.GoToLastPage();
                            break;
                        case "NextPage":
                            _Page.GetNextPage();
                            break;
                        case "PreviousPage":
                            _Page.GetPreviousPage();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    _Page.GoToPage(Convert.ToUInt32(GoToPageNumber));
                }
                paginatedBuildings = OrderdBuildings?.GetRange(Convert.ToInt32(_Page.GetFirstItemNumberOnPage0Based()), Convert.ToInt32(_Page.GetItemCountOnPage()));

            }
            else
                CH.Feedback($"{Resource.NoBuildingsToList}\n");
            return paginatedBuildings;
        }

        public static void AddBuilding(string? bName)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (bName != null)
            {
                int? Id = GetNextBuildingId();
                if (Id != null && Id.HasValue)
                {
                    if (!bName.Equals("x") && !string.IsNullOrWhiteSpace(bName) && !IsDuplicateEntry(bName))
                    {
                        Buildings?.Add(new Building(bName, Id.Value));
                        CH.Feedback(Resource.BuildingAdded);
                        ReIndexDisplayId(true, Buildings);
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
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (Buildings?.Count > 0)
            {
                if (building != null)
                {
                    if (newBuildingName != null && newBuildingName.Length > 0)
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
                                    ReIndexDisplayId(true, Buildings); ;
                                }
                                CH.Feedback($"{Resource.BuildingUpdatedFromPrompt} {building.Name} to {newBuildingName}");
                                ResetBuildingOnPage();
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

        public static Building? GetCurrentBuilding(List<Building>? buildings,string? simChoice = null)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            _ = int.TryParse(CH.GetInput(simChoice), out int BuildingChoiceId);
            var building = buildings?.FirstOrDefault(c => c.DisplayId.Equals(BuildingChoiceId));
            return building;
        }

        public static void RemoveBuilding(Building? building)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (Buildings?.Count > 0)
            {
                if (building != null)
                {
                    Buildings?.Remove(building);
                    CH.Feedback($"{Resource.BuildingRemoved} {building.Name}");
                    ReIndexDisplayId(true, Buildings);
                    ResetBuildingOnPage();
                }
                else
                {
                    CH.Feedback(Resource.NoBuildingRemovedPrompt);
                }
            }

        }

        private static void ResetBuildingOnPage()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            BuildingsOnThePage = null;
        }

        public static void SaveBuildings(string path = "Buildings.txt")
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
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
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
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

                ReIndexDisplayId(true, Buildings); ;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void PerformDefaultTasks(int page, int prevPage, int choice, int? simChoice = null, string? simInput = null)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            // if simulation use the simChoice
            if (simChoice != null) { choice = simChoice.Value; }

            int? defaultChoice = AM.CurrentMenuPage?.FirstOrDefault(c => c.IsDefaultChoice && c.Page == page)?.DisplayNumber;
            bool isSamePage = page.Equals(prevPage);

            if (isSamePage) { defaultChoice = choice; }
            // For simulation only

            DefaultTasks(page, simChoice, simInput, defaultChoice);

            ResetIsHidden();
            ApplyHiddenRules();

        }

        static List<Building>? BuildingsOnThePage = null;

        public static void DefaultTasks(int page, int? simChoice, string? simInput, int? defaultChoice)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            var MenuItem = AM.CurrentMenuPage?.FirstOrDefault(c => c.Page == page && c.DisplayNumber == defaultChoice && c.IsHidden == false);

            

            var TargetTask = MenuItem?.TargetTask;

            switch (TargetTask)
            {
                case "AddBuilding":
                    L.Log("AddBuilding", SessionID, 1);
                    CH.Feedback(Resource.AddBuildingHint);
                    CH.Feedback(Resource.EnterBuildingNamePrompt);
                    string? BuildingName = CH.GetInput(simInput);

                    AddBuilding(BuildingName);
                    break;

                case "ViewBuilding":
                    L.Log("ViewBuilding", SessionID, 1);
                    BuildingsOnThePage = ViewBuildings(Buildings, "FirstPage");
                    break;

                case "FirstPage":
                    L.Log("ViewBuilding-FirstPage", SessionID, 1);
                    BuildingsOnThePage = ViewBuildings(Buildings, "FirstPage");
                    break;

                case "LastPage":
                    L.Log("ViewBuilding-LastPage", SessionID, 1);
                    BuildingsOnThePage = ViewBuildings(Buildings, "LastPage");
                    break;

                case "NextPage":
                    L.Log("ViewBuilding-NextPage", SessionID, 1);
                    BuildingsOnThePage = ViewBuildings(Buildings, "NextPage");
                    break;

                case "PreviousPage":
                    L.Log("ViewBuilding-PreviousPage", SessionID, 1);
                    BuildingsOnThePage = ViewBuildings(Buildings, "PreviousPage");
                    break;

                case "EditBuilding":
                    L.Log("EditBuilding", SessionID, 1);
                    List<Building>? buildingsOnThePage = null;
                    if (BuildingsOnThePage != null) 
                    {
                        buildingsOnThePage = BuildingsOnThePage;
                        ShowBuildings(buildingsOnThePage);
                    }
                    else
                    {
                        buildingsOnThePage = ViewBuildings(Buildings);
                    }
                    
                    CH.Feedback(Resource.HorizontalLine);
                    CH.Feedback(Resource.SelectABuildingToEditPrompt);
                    Building? building = GetCurrentBuilding(buildingsOnThePage, Convert.ToString(simChoice));
                    if (building != null)
                    {
                        CH.Feedback(Resource.EnterANewBuildingNamePrompt);
                        string? NewBuidlingName = CH.GetInput(simInput);
                        EditBuilding(building, NewBuidlingName);
                    }
                    else
                        CH.Feedback(Resource.FailedToEditBuilding);
                    break;

                case "RemoveBuilding":
                    L.Log("RemoveBuilding", SessionID, 1);
                    if (BuildingsOnThePage != null)
                    {
                        buildingsOnThePage = BuildingsOnThePage;
                        ShowBuildings(buildingsOnThePage);
                    }
                    else
                    {
                        buildingsOnThePage = ViewBuildings(Buildings);
                    }                    
                    CH.Feedback(Resource.HorizontalLine);
                    CH.Feedback(Resource.SelectABuildingToRemove);
                    CH.Feedback(Resource.HorizontalLine);
                    building = GetCurrentBuilding(buildingsOnThePage, Convert.ToString(simChoice));
                    RemoveBuilding(building);
                    break;

                case "SaveBuildings":
                    L.Log("SaveBuilding", SessionID, 1);
                    SaveBuildings();
                    break;

                case "LoadBuildings":
                    L.Log("LoadBuilding", SessionID, 1);
                    LoadBuildings();
                    break;

                case "SearchBuildings":
                    L.Log("SearchBuilding", SessionID, 1);
                    CH.Feedback(Resource.HorizontalLine);
                    CH.Feedback(Resource.BuildingNameSearch);

                    string InputBuildingName = CH.GetInput();
                    CH.Feedback(Resource.HorizontalLine);
                    CH.Feedback(Resource.SearchResults);
                    CH.Feedback(Resource.HorizontalLine);
                    BuildingsOnThePage = SearchBuildings(InputBuildingName);
                    var buildings = BuildingsOnThePage;

                    ReIndexDisplayId(true, buildings);

                    if (buildings != null && buildings.Count > 0)
                    {
                        foreach (var item in buildings)
                        {
                            CH.Feedback($"{item.DisplayId} {item.Name}");
                        }
                    }
                    else
                        CH.Feedback($"Could not find a building(s) by the name {InputBuildingName}");

                    ReIndexDisplayId(true, Buildings);

                    break;

                default:
                    break;
            }
        }

        private static List<Building>? SearchBuildings(string inputBuildingName)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            List<Building>? buildings = [];
            if (!string.IsNullOrWhiteSpace(inputBuildingName))
            {
                buildings = Buildings?.Where(c => c.Name != null && c.Name.ToUpper().Contains(inputBuildingName.ToUpper(), StringComparison.CurrentCultureIgnoreCase)).ToList<Building>();
            }
            else
                CH.Feedback(Resource.InvalidBuildingNameEntered);

            return buildings;
        }

        public static void ApplyHiddenRules()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (AM.CurrentMenuPage != null)
            {
                bool ResetPage = false;
                foreach (var item in AM.CurrentMenuPage.Where(p => p.HideRule != "None"))
                {
                    switch (item.HideRule)
                    {
                        case "FirstPage":
                            if (_Page.IsFirstPage())
                            {
                                item.IsHidden = true;
                                ResetPage = true;
                            }
                            break;
                        case "LastPage":
                            if (_Page.IsLastPage())
                            {
                                item.IsHidden = true;
                                ResetPage = true;
                            }
                            break;
                        case "NoBuildings":
                            if (BuildingsOnThePage == null || (BuildingsOnThePage != null && BuildingsOnThePage.Count == 0))
                            {
                                item.IsHidden = true;
                                ResetPage = true;
                            }
                            break;

                        default:
                            break;
                    }
                }
                if (ResetPage)
                {
                    ReIndexDisplayId(false, Buildings);
                }
            }
        }

        public static void ResetIsHidden()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (AM.CurrentMenuPage != null)
            {
                foreach (var item in AM.CurrentMenuPage.Where(p => p.IsHidden == true))
                {
                    item.IsHidden = false;
                }
            }
        }
    }

}
