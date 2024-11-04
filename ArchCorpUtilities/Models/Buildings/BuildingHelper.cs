using System.Text;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using AR = ArchCorpUtilities.Models.MockBuildingsRepository;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using Page = Patina.Patina;
using L = Logger.Logger;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;

using ArchCorpUtilities.Models.Menus;
using System;


namespace ArchCorpUtilities.Models.Buildings
{
    public static class BuildingHelper
    {
        public static List<Building>? Buildings { get; set; }
        public static string SessionID { get; internal set; }

        private readonly static AR aR = new();

        static readonly UInt32 _PageSize;
        static UInt32 _MaxItems = Convert.ToUInt32(Buildings?.Count);
        public static Page Page { get; internal set; }

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
            _PageSize = 5;
            _MaxItems = Convert.ToUInt32(Buildings?.Count);
            Page = new(_PageSize, _MaxItems);
        }

        public static void ReIndexDisplayId(List<Building>? buildings = null)
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
            }
        }

        public static List<T>? ViewWithPagination<T>(string heading, Patina.Patina page, List<T>? modelList ,Navigation navigation = Navigation.FirstPage, int GoToPageNumber = -1)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            List<T>? paginated = GetPaginatedList(modelList, navigation, page, GoToPageNumber);

            if (paginated != null)
                ViewGen(paginated, page, heading);

            return paginated;
        }

        public static void ViewGen<T>(IEnumerable<T> modelList, Patina.Patina page, string heading)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (modelList != null && modelList.Any())
            {
                CH.Feedback(MH.ShowHeading(heading));
                foreach (var item in modelList)
                {
                    CH.Feedback(item?.ToString());
                }
                CH.Feedback(Resource.HorizontalLine);
                CH.Feedback($"Pages : {page.GetPageNumberCaption()}");
            }
            else
                CH.Feedback($"{Resource.NoData}\n");
        }

        public enum Navigation
        {
            FirstPage,
            LastPage,
            NextPage,
            PreviousPage,
        }

        public static List<T>? GetPaginatedList <T>(List<T>? items, Navigation navigation, Patina.Patina page , int GoToPageNumber = 0)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            List<T>? paginatedList = null;
            if (items?.Count > 0)
            {
                if (GoToPageNumber < 0)
                {
                    switch (navigation)
                    {
                        case Navigation.FirstPage:
                            page.GoToFirstPage();
                            break;
                        case Navigation.LastPage:
                            page.GoToLastPage();
                            break;
                        case Navigation.NextPage:
                            page.GetNextPage();
                            break;
                        case Navigation.PreviousPage:
                            page.GetPreviousPage();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    page.GoToPage(Convert.ToUInt32(GoToPageNumber));
                }
                paginatedList = items?.GetRange(Convert.ToInt32(page.GetFirstItemNumberOnPage0Based()), Convert.ToInt32(page.GetItemCountOnPage()));

            }
            else
                CH.Feedback($"{Resource.NoItemsToList}\n");
            return paginatedList;
        }

        public static bool AddBuilding(string? bName)
        {
            if (bName != null && !bName.Equals("x") && !string.IsNullOrWhiteSpace(bName) && !IsDuplicateEntry(bName))
            {
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
                int? Id = GetNextBuildingId();
                if (Id != null && Id.HasValue)
                {

                    Buildings?.Add(new Building(bName, Id.Value));
                    CH.Feedback(Resource.BuildingAdded);
                    ReIndexDisplayId(Buildings);
                    ResetBuidlingPagination(Buildings);
                    return true;
                }
                else
                {
                    CH.Feedback(Resource.FailedToAddBuilding);
                    L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error - Could not get Id for the building.", SessionID, 9);
                    return false;
                }
            }
            else
            {
                CH.Feedback(Resource.NoBuildingNameEntered);
                L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - No Building name selected or entered.", SessionID, 7);
                return false;
            }                
        }

        public static void EditBuilding(Building? building, string newBuildingName)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);            
            if(AddBuilding(newBuildingName))
            {
                if (building != null)
                    RemoveBuilding(building);
                else
                    CH.Feedback(Resource.NoBuildingRemovedPrompt);
            }
                
        }

        public static Building? GetCurrentBuilding(List<Building>? buildings,string? simChoice = null)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            _ = int.TryParse(CH.GetInput(simChoice), out int BuildingChoiceId);
            var building = buildings?.FirstOrDefault(c => c.DisplayId.Equals(BuildingChoiceId));
            return building;
        }

        public static void RemoveBuilding (Building building)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (Buildings?.Count > 0)
            {
                if (building != null)
                {
                    Buildings?.Remove(building);
                    CH.Feedback($"{Resource.BuildingRemoved} {building.Name}");
                    ReIndexDisplayId(Buildings);
                    ResetBuildingOnPage();
                    ResetBuidlingPagination(Buildings);
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
                    sb.AppendLine($"BuidlingID|BuidldingDisplayId|BuidlingName|BuildingGuid");
                    foreach (var item in Buildings.OrderBy(c => c.Name))
                    {
                        sb.AppendLine($"{item.Id}|{item.DisplayId}|{item.Name}|{item.BuildingGuid}");
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
                if (File.Exists (path))
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
                                _ = int.TryParse(LineItem[0].Trim(), out int Id);
                                _ = int.TryParse(LineItem[1].Trim(), out int DisplayId);
                                string BuildingName = LineItem[2].Trim();
                                string BuildingGuid = LineItem[3].Trim();
                                var Building = Buildings?.FirstOrDefault(c => c.BuildingGuid == BuildingGuid);
                                if (Building == null)
                                {
                                    var BuidlingNameItem = Buildings?.FirstOrDefault(c => c.Name == BuildingName);
                                    if (BuidlingNameItem != null)
                                    {
                                        CH.Feedback($"{Resource.BuildingAlreadyExistInSystemNoActionPerformed} - No action performed - Old building item: {BuidlingNameItem.Name} - {BuidlingNameItem.BuildingGuid} - New Item: {BuildingName} - {BuildingGuid}");
                                    }
                                    else
                                    {
                                        Buildings?.Add(new Building(BuildingName, Id, BuildingGuid));
                                        CH.Feedback($"{Resource.BuildingFileImportProgressSuccessMessage} - New Item : {BuildingName} - {BuildingGuid}");
                                    }
                                }
                                else
                                    CH.Feedback($"{Resource.BuildingFileImportProgressFailedMessage} - No action performed - Old building item: {Building.Name} - {Building.BuildingGuid} - New Item: {BuildingName} - {BuildingGuid}");
                            }
                        }
                        SkipFirstLine = false;
                    }

                    ResetBuildingOnPage();
                    ReIndexDisplayId(Buildings);
                }
                else
                {
                    L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error - Import file not found", SessionID, 8);
                    CH.Feedback($"{Resource.BuildingImportFileNotFound} {path}");
                }
            }
            catch (Exception ex)
            {
                L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error message - {ex.Message}", SessionID, 9);
                CH.Feedback($"{Resource.UnexpectedImportFail} {path}");
            }

        }
               
        static List<Building>? BuildingsOnThePage = null;        
        
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

        private static void ResetBuidlingPagination(List<Building>? buildings)
        {
            _MaxItems = Convert.ToUInt32(buildings?.Count);
            Page = new(_PageSize, _MaxItems);
        }

        internal static void Search()
        {
            L.Log("SearchBuilding", SessionID, 1);
            CH.Feedback(Resource.HorizontalLine);
            CH.Feedback(Resource.BuildingNameSearch);

            string InputBuildingName = CH.GetInput();
            CH.Feedback(Resource.HorizontalLine);
            CH.Feedback(Resource.SearchResults);
            CH.Feedback(Resource.HorizontalLine);
            BuildingsOnThePage = SearchBuildings(InputBuildingName);
            var buildings = BuildingsOnThePage;

            ReIndexDisplayId(buildings);

            if (buildings != null && buildings.Count > 0)
            {
                foreach (var item in buildings)
                {
                    CH.Feedback($"{item.DisplayId} {item.Name}");
                }
            }
            else
                CH.Feedback($"Could not find a building(s) by the name {InputBuildingName}");

            ReIndexDisplayId(Buildings);
            ResetBuidlingPagination(buildings);
        }

        internal static void Load()
        {
            L.Log("LoadBuilding", SessionID, 1);
            LoadBuildings();
        }

        internal static void Save()
        {
            L.Log("SaveBuilding", SessionID, 1);
            SaveBuildings();
        }

        internal static void Remove(int? simChoice)
        {
            L.Log("RemoveBuilding", SessionID, 1);
            Building? building = ViewAndSelectBuilding(simChoice);
            if (building != null)
                RemoveBuilding(building);
        }

        internal static void Edit(int? simChoice, string? simInput)
        {
            L.Log("EditBuilding", SessionID, 1);
            Building? building = ViewAndSelectBuilding(simChoice);
            if (building != null)
            {
                CH.Feedback(Resource.EnterANewBuildingNamePrompt);
                string? NewBuildingName = CH.GetInput(simInput);
                EditBuilding(building, NewBuildingName);
            }
            else
                CH.Feedback(Resource.FailedToEditBuilding);
        }

        private static Building? ViewAndSelectBuilding(int? simChoice)
        {
            var OrderedBuildings = BuildingsOnThePage ?? Buildings?.OrderBy(p => p.Name).ToList();
            BuildingsOnThePage = ViewWithPagination(Resource.BuildingListPrompt, Page, OrderedBuildings);

            CH.Feedback(Resource.HorizontalLine);
            CH.Feedback(Resource.SelectABuildingToEditPrompt);
            Building? building = GetCurrentBuilding(BuildingsOnThePage, Convert.ToString(simChoice));
            return building;
        }

        internal static void View(Navigation navigation = Navigation.FirstPage)
        {
            L.Log("ViewBuilding-FirstPage", SessionID, 1);
            var OrderedBuildings = Buildings?.OrderBy(p => p.Name).ToList();
            BuildingsOnThePage = ViewWithPagination(Resource.BuildingListPrompt, Page,OrderedBuildings, navigation);
        }

        internal static void InitialView()
        {
            Page = new Page(5, Convert.ToUInt16(Buildings?.Count));
            L.Log("ViewBuilding", SessionID, 1);
            var OrderedBuildings = Buildings?.OrderBy(p => p.Name).ToList();
            BuildingsOnThePage = ViewWithPagination(Resource.BuildingListPrompt, Page,OrderedBuildings, Navigation.FirstPage);
        }

        internal static void Add(string? simInput)
        {
            L.Log("AddBuilding", SessionID, 1);
            CH.Feedback(Resource.AddBuildingHint);
            CH.Feedback(Resource.EnterBuildingNamePrompt);
            string? BuildingName = CH.GetInput(simInput);

            AddBuilding(BuildingName);
        }

        internal static bool IsBuildingsOnThePage()
        {
            return !(BuildingsOnThePage == null || (BuildingsOnThePage != null && BuildingsOnThePage.Count == 0));
        }

     
    }
}
