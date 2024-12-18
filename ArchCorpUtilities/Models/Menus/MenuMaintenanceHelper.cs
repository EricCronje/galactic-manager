using ArchCorpUtilities.Utilities;
using System.Diagnostics;
using static ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;
using L = Logger.Logger;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using Page = Patina.Patina;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Models.Menus
{

    public static class MenuMaintenanceHelper
    {
        public static string? SessionID { get; internal set; }

        static List<MenuItem>? MenuItemsOnThePage = null;

        public static Patina.Patina MenuPage { get; internal set; }

        static MenuMaintenanceHelper()
        {
            SessionID = Guid.NewGuid().ToString();
            MenuPage = new(40, Convert.ToUInt16(MH.Menu.Count));
        }

        internal static void View(E.Navigation navigate = E.Navigation.FirstPage)
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
            View(E.Navigation.FirstPage);
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
            MenuItemsOnThePage = U.ViewWithPagination("Current menu structure", MenuPage, orderedMenus, E.Navigation.FirstPage);
        }

        internal static void Add(string[]? simInputValues = null)
        {
            if (GenerateUsingGUIDMassFile(simInputValues))
            {
                CH.Feedback($"Generated - {CodeGenHelper.CurrentGuid}\r\nIn Order for the changes to take affect:\r\nDeploy the code manually.\r\nThen create the menu structure.\r\nPress any key to exit the program.");
                _ = CH.GetInput(simInputValues?[0]);
                if (simInputValues == null)
                    Environment.Exit(0);
                else
                    CH.Feedback("<Exit System>");
            }
            else
            {
                CH.Feedback($"No code or menus created. for {CodeGenHelper.CurrentGuid}");
                if (SessionID != null)
                    L.Log($"No code or menus created. for {CodeGenHelper.CurrentGuid}", SessionID, 7);
            }
        }

        private static bool GenerateMenuItems(MenuItem? CurrentMenu, ref string DisplayName, string ItemName, ref bool IsBack, ref int Page, ref string PageHeading, ref int ParentPage, ref bool IsExitOption, ref int NewMenuTargetPage, bool isStartPage)
        {
            if (CurrentMenu == null)
                return false;
            try
            {
                if (SessionID != null)
                    L.Log($"Generate menus. {ItemName}", SessionID, 9);
                //Get the IsExit Option for the page on this current menu item
                var IsExitMenuItem = MH.Menu.FirstOrDefault(p => p.Page == CurrentMenu.Page && p.IsExitOption);
                if (IsExitMenuItem != null)
                    MH.Menu.Remove(IsExitMenuItem);
                //Remove it
                var Domain = CurrentMenu.Domain;
                _ = Enum.TryParse(ItemName, out Domain);

                var AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, Domain, NewMenuTargetPage, "None", "None", false, isStartPage,M.MenuActionEnum.Manage);
                //Re-Add it...
                if (IsExitMenuItem != null)
                {
                    IsExitMenuItem.Source = "UserAdded";
                    IsExitMenuItem.Action = M.MenuActionEnum.Exit;
                    MH.Menu.Add(IsExitMenuItem);
                }
                if (!AddedMenu)
                    CH.Feedback(Resource.NoDisplayNameProvided);

                if (AddedMenu)
                {
                    string[] DisplayNamesList = ["View", "Add", "Remove", "Edit", "Save", "Load"];
                    M.MenuActionEnum[] MenuActionList = 
                        [M.MenuActionEnum.View, 
                        M.MenuActionEnum.Add, 
                        M.MenuActionEnum.Remove,
                        M.MenuActionEnum.Edit,
                        M.MenuActionEnum.Save,
                        M.MenuActionEnum.Load
                        ];

                    //Create "View", "Add", "Remove", "Edit", "Save", "Load", Back to Main, Exit                    
                    var result = CreateSubMenusLevel1(CurrentMenu, ref DisplayName, ref IsBack, ref Page, ref PageHeading, ref ParentPage, ref IsExitOption, ref NewMenuTargetPage, ref AddedMenu, DisplayNamesList, ItemName, MenuActionList);

                    if (result == false)
                        CH.Feedback(Resource.NoItemNameProvided);

                    //Find the view menu - that was created!
                    var NewViewMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"View {ItemName}");
                    string[] DisplayNamesListView = ["Refresh", "Search", "Next Page", "Last Page", "First Page", "Previous Page"];
                    MenuActionList =
                        [
                        M.MenuActionEnum.Refresh,
                        M.MenuActionEnum.Search,
                        M.MenuActionEnum.Next,
                        M.MenuActionEnum.Last,
                        M.MenuActionEnum.First,
                        M.MenuActionEnum.Previous
                        ];
                    result = CreateSubMenuLevel2(NewViewMenu, DisplayNamesListView, ItemName, "View", MenuActionList);

                    //Find the add menu item - that was created
                    var newAddMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Add {ItemName}");
                    DisplayNamesListView = [$"Add {ItemName}"];
                    MenuActionList =
                        [
                        M.MenuActionEnum.Add
                        ];
                    result = CreateSubMenuLevel2(newAddMenu, DisplayNamesListView, ItemName, "Add", MenuActionList);

                    //Find the Remove menu item - that was created
                    var newRemoveMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Remove {ItemName}");
                    DisplayNamesListView = [$"Remove {ItemName}", $"Clear All {ItemName}", "Next Page", "Last Page", "First Page", "Previous Page"];
                    MenuActionList =
                        [
                        M.MenuActionEnum.Remove,
                        M.MenuActionEnum.ClearAll,
                        M.MenuActionEnum.Next,
                        M.MenuActionEnum.Last,
                        M.MenuActionEnum.First,
                        M.MenuActionEnum.Previous
                        ];
                    result = CreateSubMenuLevel2(newRemoveMenu, DisplayNamesListView, ItemName, "Remove", MenuActionList);

                    //Find the Edit menu item - that was created
                    var newEditMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Edit {ItemName}");
                    DisplayNamesListView = [$"Edit {ItemName}", "Next Page", "Last Page", "First Page", "Previous Page"];
                    MenuActionList =
                        [
                        M.MenuActionEnum.Edit,
                        M.MenuActionEnum.Next,
                        M.MenuActionEnum.Last,
                        M.MenuActionEnum.First,
                        M.MenuActionEnum.Previous
                        ];

                    result = CreateSubMenuLevel2(newEditMenu, DisplayNamesListView, ItemName, "Edit", MenuActionList);

                    //Find the save menu item - that was created
                    var newSaveMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Save {ItemName}");
                    DisplayNamesListView = [$"Save {ItemName} to a file"];
                    MenuActionList =
                        [
                        M.MenuActionEnum.Save
                        ];
                    result = CreateSubMenuLevel2(newSaveMenu, DisplayNamesListView, ItemName, "Save", MenuActionList);

                    //Find the load menu item - that was created
                    var newLoadMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Load {ItemName}");
                    DisplayNamesListView = [$"Load {ItemName} from a file"];
                    MenuActionList =
                       [
                       M.MenuActionEnum.Load
                       ];
                    result = CreateSubMenuLevel2(newLoadMenu, DisplayNamesListView, ItemName, "Load", MenuActionList);
                    CH.Feedback(Resource.MenuItemsCreatedSuccess);
                    return result;
                }
                else
                    if (SessionID != null)
                    L.Log("No Entity name provided.", SessionID, 8);

                return false;
            }
            catch (Exception ex)
            {
                if (SessionID != null)
                    L.Log($"Error - in generate menus. {ex.Message}", SessionID, 9);
                return false;
            }

        }
        private static bool GenerateUsingGUIDMassFile(string[]? simInputValues)
        {

            var CurrentMenu = MH.Menu.FirstOrDefault(p => p.IsStartPage);

            bool IsBack = false;
            var IsExitOption = false;

            var GeneratePath = $"{CodeGenHelper.WorkingFolder}\\Generate\\{CodeGenHelper.CurrentGuid}";
            if (GeneratePath.Length > 0 && File.Exists(GeneratePath))
            {
                if (SessionID != null)
                    L.Log($"Found a generate file {GeneratePath}", SessionID, 1);

                CH.Feedback($"Do you want to generate the code from {CodeGenHelper.CurrentGuid}? (Y/N)");
                var InputGen = CH.GetInput(simInputValues?[0]);

                if (!string.IsNullOrWhiteSpace(InputGen) && InputGen.Equals("Y"))
                {
                    var Content = File.ReadAllText(GeneratePath);
                    if (Content != null && Content.Length > 0)
                    {

                        if (SessionID != null)
                            L.Log($"Length of {Content.Length}", SessionID, 1);

                        var SplitCommands = Content.Split("\r\n");


                        bool IsStartPage = true;
                        foreach (var SplitCommand in SplitCommands)
                        {
                            if (SessionID != null)
                                L.Log($"Found line count of {Content.Length}", SessionID, 1);

                            if (SplitCommand.Length > 0)
                            {
                                if (SessionID != null)
                                    L.Log($"Found command of length {SplitCommand.Length}", SessionID, 1);

                                var SplitLine = SplitCommand.Split("|");
                                if (SplitLine.Length == 5)
                                {
                                    if (SessionID != null)
                                        L.Log($"Found command of length {SplitCommand.Length}", SessionID, 1);
                                    var MenuName = SplitLine[0];
                                    var EntityName = SplitLine[1];
                                    var MenuType = SplitLine[2];
                                    var LinkGuidOnTheLeft = SplitLine[3];
                                    var LinkGuidOnTheRight = SplitLine[4];

                                    _ = Enum.TryParse(MenuType, out M.MenuTypeEnum MenuTypeEnum);

                                    if (CodeGen.CreateDefaultCode(EntityName, MenuTypeEnum, LinkGuidOnTheLeft == "None" ? null : LinkGuidOnTheLeft, LinkGuidOnTheRight == "None" ? null : LinkGuidOnTheRight))
                                    {
                                        CH.Feedback($"{Resource.ModelFileCreated} - {EntityName}.");
                                    }
                                    else
                                        CH.Feedback($"{Resource.ModelFileNotCreated} - {EntityName}");

                                    if (U.ClearGeneratedHeaders())
                                        CH.Feedback($"Header information created.");
                                    else
                                    {
                                        CH.Feedback($"Header information was not created.");
                                        if (SessionID != null) { L.Log($"Error -- Headers not created", SessionID, 9); }
                                    }


                                    if (CurrentMenu != null)
                                    {
                                        var Page = CurrentMenu.Page;
                                        var PageHeading = CurrentMenu.PageHeading;
                                        var ParentPage = CurrentMenu.ParentPage;
                                        var NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;

                                        if (MenuName.Length > 0 && EntityName.Length > 0)
                                            if (GenerateMenuItems(CurrentMenu, ref MenuName, EntityName, ref IsBack, ref Page, ref PageHeading, ref ParentPage, ref IsExitOption, ref NewMenuTargetPage, IsStartPage))
                                            {
                                                IsStartPage = false;
                                                CH.Feedback($"Menus (Add, Remove, Edit, View, Search, import, save etc.) created for - {EntityName}");
                                                if (MH.ExportMenus($"{GeneratePath}_Menus"))
                                                    CH.Feedback($"Menus exported for - {EntityName} - {GeneratePath}\\Menus{CodeGenHelper.CurrentGuid}");
                                                else
                                                    CH.Feedback("Menus was not saved.");
                                            }
                                    }
                                    else
                                        CH.Feedback($"No start page - {EntityName}");
                                }
                                else
                                {
                                    if (SessionID != null)
                                        L.Log($"Abort - not enough fields.", SessionID, 1);
                                }
                            }
                            else
                            {
                                if (SessionID != null)
                                    L.Log($"Abort - No lines", SessionID, 1);
                            }
                        }

                        return true;
                    }
                    else
                    {
                        if (SessionID != null)
                            L.Log($"Abort - Empty file.", SessionID, 1);
                        return false;
                    }
                }
                else
                {
                    if (SessionID != null)
                        L.Log($"Code creation and menu creation - Aborted by user.", SessionID, 7);
                    return false;
                }

            }

            return false;
        }

        public static bool GenerateDefaultMenus(string generatePath, string[]? simInputValues)
        {
            

            if (simInputValues != null) { generatePath = simInputValues[0]; }

            if (generatePath == null )
            {
                if (SessionID != null) { L.Log($"Found a generate file {generatePath}", SessionID, 9); }
                return false;
            }

            var CurrentMenu = MH.Menu.FirstOrDefault(p => p.IsStartPage);

            bool IsBack = false;
            var IsExitOption = false;

            //GeneratePath = $"{CodeGenHelper.WorkingFolder}\\Generate\\{CodeGenHelper.CurrentGuid}";
            if (generatePath.Length > 0 && File.Exists(generatePath))
            {
                if (SessionID != null) { L.Log($"Found a generate file {generatePath}", SessionID, 1); }

                var Content = File.ReadAllText(generatePath);
                if (Content != null && Content.Length > 0)
                {
                    if (SessionID != null) { L.Log($"Length of {Content.Length}", SessionID, 1); }
                    var SplitCommands = Content.Split("\r\n");
                    bool IsStartPage = true;

                    foreach (var SplitCommand in SplitCommands)
                    {
                        if (SessionID != null) { L.Log($"Found line count of {Content.Length}", SessionID, 1); }                            

                        if (SplitCommand.Length > 0)
                        {
                            if (SessionID != null) { L.Log($"Found command of length {SplitCommand.Length}", SessionID, 1); }                                

                            var SplitLine = SplitCommand.Split("|");
                            if (SplitLine.Length == 5)
                            {
                                if (SessionID != null) { L.Log($"Found command of length {SplitCommand.Length}", SessionID, 1); }
                                
                                var MenuName = SplitLine[0];
                                var EntityName = SplitLine[1];
                                var MenuType = SplitLine[2];
                                var LinkGuidOnTheLeft = SplitLine[3];
                                var LinkGuidOnTheRight = SplitLine[4];

                                _ = Enum.TryParse(MenuType, out M.MenuTypeEnum MenuTypeEnum);

                                if (CurrentMenu != null)
                                {
                                    var Page = CurrentMenu.Page;
                                    var PageHeading = CurrentMenu.PageHeading;
                                    var ParentPage = CurrentMenu.ParentPage;
                                    var NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;

                                    if (MenuName.Length > 0 && EntityName.Length > 0)
                                        if (GenerateMenuItems(CurrentMenu, ref MenuName, EntityName, ref IsBack, ref Page, ref PageHeading, ref ParentPage, ref IsExitOption, ref NewMenuTargetPage, IsStartPage))
                                        {
                                            IsStartPage = false;
                                            CH.Feedback($"Menus (Add, Remove, Edit, View, Search, import, save etc.) created for - {EntityName}");
                                            if (MH.ExportMenus($"{generatePath}_Menus"))
                                                CH.Feedback($"Menus exported for - {EntityName} - {generatePath}\\Menus{CodeGenHelper.CurrentGuid}");
                                            else
                                                CH.Feedback("Menus was not saved.");
                                        }
                                }
                                else
                                    CH.Feedback($"No start page - {EntityName}");
                            }
                            else
                                if (SessionID != null) { L.Log($"Abort - not enough fields.", SessionID, 1); }
                        }
                        else
                            if (SessionID != null) { L.Log($"Abort - No lines", SessionID, 1); }                                
                    }

                    return true;
                }
                else
                {
                    if (SessionID != null)
                        L.Log($"Abort - Empty file.", SessionID, 1);
                    return false;
                }

            }

            return false;
        }

        private static bool CreateSubMenuLevel2(MenuItem? NewViewMenu, string[] DisplayNamesListView, string itemName, string heading = "x", M.MenuActionEnum[]? menuActionList = null)
        {
            string DisplayName;
            bool IsBack;
            int Page;
            string? PageHeading;
            bool IsExitOption;
            int NewMenuTargetPage;
            bool IsDefault = false;
            E.MenuDomain menuDomain = E.MenuDomain.None;
            bool result;

            if (NewViewMenu != null)
            {
                var Counter = 0;
                foreach (var item in DisplayNamesListView)
                {                    
                    DisplayName = item;
                    IsBack = false;
                    Page = NewViewMenu.TargetPage;
                    PageHeading = $"{heading}";
                    IsExitOption = false;
                    NewMenuTargetPage = 0;

                    //if (Counter == 1)
                    //    IsDefault = true;

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

                    _ = Enum.TryParse(itemName, out menuDomain);
                    var TargetTaskSplit = item.Split(' ');

                    var MenuEnumAction = menuActionList != null ? menuActionList[Counter] : M.MenuActionEnum.None;
                    result = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomain, NewMenuTargetPage, HideRule, TargetTaskSplit[0], IsDefault, false, MenuEnumAction);
                    if (!result)
                    { 
                        return false; 
                    }
                    Counter++;
                }
                Page = NewViewMenu.TargetPage;
                return CreateBackAndExitMenuOptions(menuDomain, NewViewMenu, Page);                
            }
            return false;
        }

        private static bool CreateSubMenusLevel1(MenuItem? CurrentMenu, ref string DisplayName, ref bool IsBack, ref int Page, ref string PageHeading, ref int ParentPage, ref bool IsExitOption, ref int NewMenuTargetPage, ref bool AddedMenu, string[] DisplayNamesList, string itemName, M.MenuActionEnum[] menuActionList)
        {
            if (itemName != null)
            {
                IsBack = false;
                Page = NewMenuTargetPage;
                ParentPage = Page;
                IsExitOption = false;

                E.MenuDomain MenuDomainOption = E.MenuDomain.None;

                int Index = 0;
                foreach (var subDisplayName in DisplayNamesList)
                {
                    if (CurrentMenu != null)
                    {

                        DisplayName = $"{subDisplayName} {itemName}";
                        PageHeading = $"{itemName} - Sub Menu";
                        NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;
                        _ = Enum.TryParse(itemName, out MenuDomainOption);

                        AddedMenu = AddNewMenuItem(DisplayName, IsBack
                            , Page, PageHeading, IsExitOption
                            , MenuDomainOption, NewMenuTargetPage,"None"
                            , "None", false, false
                            , menuActionList[Index]
                            );
                        Index++;
                    }
                }

                CreateBackAndExitMenuOptions(MenuDomainOption, CurrentMenu, Page);
                return true;
            }
            else
                return false;
        }

        private static bool CreateBackAndExitMenuOptions(E.MenuDomain menuDomainOption, MenuItem? CurrentMenu, int Page)
        {

            string DisplayName;
            bool IsBack;
            string PageHeading;

            bool IsExitOption = false;
            int NewMenuTargetPage;
            bool result = false;

            if (CurrentMenu != null)
            {
                DisplayName = "Back to Main Menu";
                PageHeading = DisplayName;
                NewMenuTargetPage = CurrentMenu.Page;
                IsBack = true;
                result = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomainOption, NewMenuTargetPage, "None", "None", false, false, M.MenuActionEnum.Back);
                if (result)
                {
                    DisplayName = "Exit";
                    PageHeading = DisplayName;
                    NewMenuTargetPage = 0;
                    IsExitOption = true;
                    result = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomainOption, NewMenuTargetPage, "None", "None", false, false, M.MenuActionEnum.Exit);
                }
            }
            return result;
        }

        private static bool AddNewMenuItem(string displayName, bool isBack, int page, string pageHeading, bool isExitOption, E.MenuDomain domain, int newMenuTargetPage, string hideRule = "None", string targetTask = "None", bool isDefault = false, bool isStartPage = false, M.MenuActionEnum menuActionEnum = default)
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
                domain, 
                isStartPage
                , 0
                , menuActionEnum
                );
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

        internal static void Load(string? path = null)
        {
            var Menus = MH.ImportMenu(path ?? "Menus.txt");
            if (Menus != null)
            {
                var Start = Menus.FirstOrDefault(p => p.IsStartPage)?.Page;
                MH.MapMenus(Menus, Start);
                MH.Menu.Clear();
                MH.Menu = Menus;


                CH.Feedback($"{Resource.LoadMenusSuccess} - {U.GetCurrentDate()}");

            }
            else
                CH.Feedback($"{Resource.LoadMenusFailed} - {U.GetCurrentDate()}");

        }

        internal static void Save()
        {
            CH.Feedback("Export the menu maintenance as well? (Y/N)");
            if (MH.ExportMenus("Menus", !CH.GetInput().Contains('Y')))
                CH.Feedback($"{Resource.SaveMenusSuccess} - {U.GetCurrentDate()}");
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
                MenuItemsOnThePage = U.ViewWithPagination("Current menu structure", MenuPage, orderedMenus, E.Navigation.FirstPage);

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
