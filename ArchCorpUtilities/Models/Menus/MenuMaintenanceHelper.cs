using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using Page = Patina.Patina;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

using static ArchCorpUtilities.Models.Buildings.BuildingHelper;
using System.Net.NetworkInformation;
using System.Text;
using System;
using ArchCorpUtilities.Utilities;
using System.Xml.Linq;

namespace ArchCorpUtilities.Models.Menus
{
    
    public static class MenuMaintenanceHelper
    {
        public static string? SessionID { get; internal set; }

        static List<MenuItem>? MenuItemsOnThePage = null;

        public static Patina.Patina MenuPage { get; internal set; }

        static MenuMaintenanceHelper()
        {
            MenuPage = new(40, Convert.ToUInt16(MH.Menu.Count));
        }

        internal static void View(U.Navigation navigate = U.Navigation.FirstPage)
        {
            var orderedMenus = MH.Menu?.OrderBy(p => p.Index).ToList();
            MenuItemsOnThePage = U.ViewWithPagination("Current menu structure", MenuPage, orderedMenus, navigate);
        }

        internal static bool IsMenusOnThePage()
        {
            return !(MenuItemsOnThePage == null || (MenuItemsOnThePage != null && MenuItemsOnThePage.Count == 0));
        }

        internal static void InitialView()
        {
            PrepareMenuWrapper(MH.Menu);
            View(U.Navigation.FirstPage);
        }

        private static void PrepareMenuWrapper(List<MenuItem> menu)
        {
            MenuPage = new Page(40, Convert.ToUInt16(menu?.Count));

            MenuItem? StartingMenu = menu?.FirstOrDefault(p => p.IsStartPage == true);

            if (menu != null)
                MH.MapMenus(menu, StartingMenu?.Page);

            if (SessionID != null)
                L.Log("ViewMenu", SessionID, 1);
        }

        internal static void Refresh()
        {
            ViewMenuTopLevelWrapper();            
        }

        private static void ViewMenuTopLevelWrapper()
        {
            PrepareMenuWrapper(MH.Menu);
            var orderedMenus = MH.Menu?.Where(p => p.Level == 0).OrderBy(p => p.Index).ToList();
            MenuPage = new Page(40, Convert.ToUInt16(orderedMenus?.Count));
            MenuItemsOnThePage = U.ViewWithPagination("Current menu structure", MenuPage ,orderedMenus, U.Navigation.FirstPage);
        }

        internal static void Add(string[]? simInputValues = null)
        {

            var CurrentMenu = ViewAndSelectItem(simInputValues?[0]);

            if (CurrentMenu != null)
            {
                CH.Feedback(Resource.DisplayNamePrompt);
                string DisplayName = CH.GetInput(simInputValues?[1]);
                bool IsBack = false;
                var Page = CurrentMenu.Page;
                var PageHeading = CurrentMenu.PageHeading;
                var ParentPage = CurrentMenu.ParentPage;
                var IsExitOption = false;
                var NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;

                //Get the IsExit Option for the page on this current menu item
                var IsExitMenuItem = MH.Menu.FirstOrDefault(p => p.Page == CurrentMenu.Page && p.IsExitOption);
                if (IsExitMenuItem != null)
                    MH.Menu.Remove(IsExitMenuItem);
                //Remove it
                var AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);
                //Re-Add it...
                if (IsExitMenuItem != null)
                    MH.Menu.Add(IsExitMenuItem);
                if (!AddedMenu)
                    CH.Feedback(Resource.NoDisplayNameProvided);

                if (AddedMenu)
                {
                    CH.Feedback(Resource.EnterItemPrompt);

                    /*                        
                    -------------------------
                    Manage Buildings
                    -------------------------
                    1 View Buildings
                    2 Add Buildings
                    3 Remove Buildings
                    4 Edit Building Names
                    5 Save Buildings
                    6 Load Buildings
                    7 Back to Main Menu
                    8 Exit
                    -------------------------
                    Choice:
                    */

                    string ItemName = CH.GetInput(simInputValues?[2]);
                    string[] DisplayNamesList = ["View", "Add", "Remove", "Edit", "Save", "Load"];
                    //Create "View", "Add", "Remove", "Edit", "Save", "Load", Back to Main, Exit                    
                    var result = CreateSubMenusLevel1(CurrentMenu, ref DisplayName, ref IsBack, ref Page, ref PageHeading, ref ParentPage, ref IsExitOption, ref NewMenuTargetPage, ref AddedMenu, DisplayNamesList, ItemName);

                    if (result == false)
                        CH.Feedback(Resource.NoItemNameProvided);


                    /*
                     //Add view part ...
                    -------------------------
                    Buildings list:
                    -------------------------
                    1) Alpha
                    2) Beta
                    3) Dragon One
                    -------------------------
                    Pages : 1/1
                    -------------------------
                    View Buildings - Sub Menu
                    -------------------------
                    1 Refresh
                    2 Search
                    3 Back to Manage Buildings
                    4 Exit
                    -------------------------
                    Choice:

                    */


                    //Find the view menu - that was created!
                    var NewViewMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"View {ItemName}");
                    string[] DisplayNamesListView = ["Refresh", "Search", "Next Page", "Last Page", "First Page", "Previous Page"];
                    CreateSubMenuLevel2(NewViewMenu, DisplayNamesListView, ItemName);

                    //Find the add menu item - that was created
                    var newAddMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Add {ItemName}");
                    DisplayNamesListView = [$"Add {ItemName}"];
                    CreateSubMenuLevel2(newAddMenu, DisplayNamesListView, ItemName);

                    //Find the Remove menu item - that was created
                    var newRemoveMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Remove {ItemName}");
                    DisplayNamesListView = [$"Remove {ItemName}", "Next Page", "Last Page", "First Page", "Previous Page"];
                    CreateSubMenuLevel2(newRemoveMenu, DisplayNamesListView, ItemName);

                    //Find the Edit menu item - that was created
                    var newEditMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Edit {ItemName}");
                    DisplayNamesListView = [$"Edit {ItemName}", "Next Page", "Last Page", "First Page", "Previous Page"];
                    CreateSubMenuLevel2(newEditMenu, DisplayNamesListView, ItemName);

                    //Find the save menu item - that was created
                    var newSaveMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Save {ItemName}");
                    DisplayNamesListView = [$"Save {ItemName} to a file"];
                    CreateSubMenuLevel2(newSaveMenu, DisplayNamesListView, ItemName);

                    //Find the load menu item - that was created
                    var newLoadMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Load {ItemName}");
                    DisplayNamesListView = [$"Load {ItemName} from a file"];
                    CreateSubMenuLevel2(newLoadMenu, DisplayNamesListView, ItemName);
                    CH.Feedback(Resource.MenuItemsCreatedSuccess);

                    ReIndexDisplayId();

                    #region Generate default code
                    if (CodeGen.CreateDefaultCode(ItemName))
                        CH.Feedback(Resource.ModelFileCreated);                          
                    else
                        CH.Feedback(Resource.ModelFileNotCreated);
                    #endregion 
                }
            }
            else
                CH.Feedback(Resource.NoMenuItemSelected);

            
        }

        private static void CreateSubMenuLevel2( MenuItem? NewViewMenu, string[] DisplayNamesListView, string itemName)
        {
            string DisplayName;
            bool IsBack;
            int Page;
            string? PageHeading;
            bool IsExitOption;
            int NewMenuTargetPage;
            bool IsDefault = false;

            if (NewViewMenu != null)
            {
                var Counter = 0;
                foreach (var item in DisplayNamesListView)
                {
                    Counter++;
                    DisplayName = item;
                    IsBack = false;
                    Page = NewViewMenu.TargetPage;
                    PageHeading = $"{DisplayName}";
                    IsExitOption = false;
                    NewMenuTargetPage = 0;
                    
                    if (Counter == 1)
                        IsDefault = true;

                    string HideRule = "None";

                    switch (item)
                    {
                        case "Next Page":
                        case "Last Page":                                            
                            HideRule = "LastPage";
                            break;                        
                        case "Previous Page":
                        case "First Page":
                            HideRule = "FirstPage";
                            break;

                        default:
                            break;
                    }

                    _ = Enum.TryParse(itemName, out U.MenuDomain menuDomain);
                    var TargetTaskSplit = item.Split(' ');

                    AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomain, NewMenuTargetPage, HideRule, TargetTaskSplit[0], IsDefault);
                }
                Page = NewViewMenu.TargetPage;                
                CreateBackAndExitMenuOptions(NewViewMenu, Page);
            }
        }

        private static bool CreateSubMenusLevel1(MenuItem? CurrentMenu, ref string DisplayName, ref bool IsBack, ref int Page, ref string PageHeading, ref int ParentPage, ref bool IsExitOption, ref int NewMenuTargetPage, ref bool AddedMenu, string[] DisplayNamesList, string itemName)
        {
            if (itemName != null)
            {
                IsBack = false;
                Page = NewMenuTargetPage;
                ParentPage = Page;
                IsExitOption = false;

                foreach (var subDisplayName in DisplayNamesList)
                {
                    if (CurrentMenu != null)
                    {
                        
                        DisplayName = $"{subDisplayName} {itemName}";
                        PageHeading = $"{subDisplayName} {itemName} - Sub Menu";
                        NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;
                        _ = Enum.TryParse(itemName, out U.MenuDomain MenuDomain);

                        AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, MenuDomain, NewMenuTargetPage);
                    }
                }
                
                CreateBackAndExitMenuOptions(CurrentMenu, Page);
                return true;
            }
            else
                return false; 
        }

        private static void CreateBackAndExitMenuOptions(MenuItem? CurrentMenu, int Page)
        {

            string DisplayName;
            bool IsBack;
            string PageHeading;

            bool IsExitOption = false;
            int NewMenuTargetPage;

            DisplayName = "Back to Main Menu";
            PageHeading = DisplayName;
            if (CurrentMenu != null)
            {
                NewMenuTargetPage = CurrentMenu.Page;
                IsBack = true;
                AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);

                DisplayName = "Exit";
                PageHeading = DisplayName;
                NewMenuTargetPage = 0;
                IsExitOption = true;
                AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);
            }
        }

        private static bool AddNewMenuItem(string displayName, bool isBack, int page, string pageHeading, bool isExitOption, UniversalUtilities.MenuDomain domain, int newMenuTargetPage, string hideRule = "None", string targetTask = "None", bool isDefault = false)
        {
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                bool IsBack = isBack;
                var Page = page;
                var PageHeading = pageHeading;
                var IsExitOption = isExitOption;
                var NewMenuTargetPage = newMenuTargetPage;
                var IsDefault = isDefault;

                MenuItem NewMenuItem = new
                (displayName,
                Page,
                PageHeading,
                IsExitOption,
                NewMenuTargetPage,
                targetTask,
                IsBack,
                IsDefault,
                hideRule,
                false,
                "UserAdded",
                null,
                domain);
                // Add the item to the menu structure.
                MH.Menu.Add(NewMenuItem);
                return true;
            }
            else
                return false;
        }

        internal static void Edit(string? simChoice, string? simInput)
        {
            var CurrentMenu = ViewAndSelectItem(simChoice);
            if (CurrentMenu != null)
            {
                var ActualMenu = MH.Menu.FirstOrDefault(p => p.IDGUIDMenu == CurrentMenu.IDGUIDMenu);
                if (ActualMenu != null)
                {
                    CH.Feedback(Resource.EnterANewMenuNamePrompt);
                    var NewMenuName = CH.GetInput(simInput);
                    if (!string.IsNullOrWhiteSpace(NewMenuName))
                    {
                        var FoundMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == NewMenuName);

                        if (FoundMenu != null)
                            CH.Feedback(Resource.DuplicateEntryMessage);
                        else
                        {
                            ActualMenu.DisplayName = NewMenuName;
                            CH.Feedback(Resource.MenuEditedSuccess);
                        }
                    }
                    else
                        CH.Feedback(Resource.NoMenuNameEntered);
                }
                else
                    CH.Feedback(Resource.NoData);
            }
            else
                CH.Feedback(Resource.NoMenuItemSelected);
        }

        internal static void Search(int? simChoice = null, string[]? simInput = null)
        {
            throw new NotImplementedException();
        }

        internal static void Load()
        {
            var Menus  = MH.ImportMenu();
            if (Menus != null)
            {
                var Start = Menus.FirstOrDefault(p => p.IsStartPage)?.Page;
                MH.MapMenus(Menus, Start);
                MH.Menu.Clear();
                MH.Menu = Menus;
                CH.Feedback($"{Resource.LoadMenusSuccess} - {DateTime.Now}");

            }
            else
                CH.Feedback($"{Resource.LoadMenusFailed} - {DateTime.Now}");

        }

        internal static void Save()
        {
            if (MH.ExportMenus())
                CH.Feedback($"{Resource.SaveMenusSuccess} - {DateTime.Now}");
        }

        internal static void Remove(string? simChoice)
        {
            //List the items
            var CurrentMenu = ViewAndSelectItem(simChoice);

            if (CurrentMenu != null) 
                if (MH.Menu.Remove(CurrentMenu))
                    CH.Feedback(Resource.MenuItemRemovedSuccess);
                else
                    CH.Feedback(Resource.MenuItemRemovedFail);

        }

        private static MenuItem? ViewAndSelectItem(string? simInput)
        {
            if (MenuItemsOnThePage != null)
            {
                var orderedMenus = MenuItemsOnThePage?.OrderBy(p => p.Index).ToList();
                MenuItemsOnThePage = U.ViewWithPagination("Current menu structure", MenuPage, orderedMenus, U.Navigation.FirstPage);

            }
            CH.Feedback(Resource.SelectMenuItem);
            //Ask to select one
            CH.Feedback(Resource.ChoicePrompt);
            string MenuItemInput = CH.GetInput(simInput);
            //If none selected - do nothing
            if (!string.IsNullOrWhiteSpace(MenuItemInput))
            {
                //If item selected - Remove
                var CurrentMenu = MH.Menu.FirstOrDefault(p => p.DisplayMenuItem == MenuItemInput);
                if (CurrentMenu != null)
                {
                    return CurrentMenu;
                }
            }
            return null;
        }
    }
}
