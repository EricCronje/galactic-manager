using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using Page = Patina.Patina;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;
using ArchCorpUtilities.Utilities;
using static ArchCorpUtilities.Utilities.UniversalUtilities;

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
            if(GenerateUsingGUIDMassFile(simInputValues))
            {
                CH.Feedback($"Generated - {CodeGenHelper.CurrentGuid}\r\nIn Order for the changes to take affect:\r\nDeploy the code manually.\r\nThen create the menu structure.\r\nPress any key to exit the program.");
                var input = CH.GetInput(simInputValues?[0]);
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

        private static bool GenerateMenuItems(MenuItem? CurrentMenu, ref string DisplayName, string ItemName, ref bool IsBack, ref int Page, ref string PageHeading, ref int ParentPage, ref bool IsExitOption, ref int NewMenuTargetPage)
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

                var AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, Domain, NewMenuTargetPage);
                //Re-Add it...
                if (IsExitMenuItem != null)
                    MH.Menu.Add(IsExitMenuItem);
                if (!AddedMenu)
                    CH.Feedback(Resource.NoDisplayNameProvided);

                if (AddedMenu)
                {
                    string[] DisplayNamesList = ["View", "Add", "Remove", "Edit", "Save", "Load"];
                    //Create "View", "Add", "Remove", "Edit", "Save", "Load", Back to Main, Exit                    
                    var result = CreateSubMenusLevel1(CurrentMenu, ref DisplayName, ref IsBack, ref Page, ref PageHeading, ref ParentPage, ref IsExitOption, ref NewMenuTargetPage, ref AddedMenu, DisplayNamesList, ItemName);

                    if (result == false)
                        CH.Feedback(Resource.NoItemNameProvided);

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
                    return true;
                }
                else
                    if (SessionID != null)
                        L.Log("No Entity name provided.", SessionID, 8);

                return false;
            }
            catch (Exception)
            {
                if (SessionID != null)
                    L.Log("Error - in generate menus.", SessionID, 9);
                return false;
            }

        }

        private static void GenerateFileQuestion(string[]? simInputValues, ref string DisplayName, ref string ItemName, ref string Input)
        {
            if (DisplayName.Length == 0 && ItemName.Length == 0)
            {
                CH.Feedback(Resource.DisplayNamePrompt);
                DisplayName = CH.GetInput(simInputValues?[1]);

                CH.Feedback(Resource.EnterItemPrompt);
                ItemName = CH.GetInput(simInputValues?[2]);

                CH.Feedback("Do you want to generate the code? (Y/N)");
                Input = CH.GetInput(simInputValues?[0]);

                if (File.Exists("Last"))
                    File.Delete("Last");

                File.WriteAllText("Last", $"{DisplayName}|{ItemName}|{Input}");
            }
        }

        private static void GenerateLastEntityQuestion(string[]? simInputValues, ref string DisplayName, ref string ItemName, ref string Input)
        {
            if (File.Exists("Last"))
            {
                var Context = File.ReadAllText("Last");
                var ContextSplit = Context.Split("|");
                if (ContextSplit.Length == 3)
                {
                    DisplayName = ContextSplit[0];
                    ItemName = ContextSplit[1];

                    CH.Feedback($"(Last Item Processed) Do you want to continue with DisplayName: {DisplayName} - ItemName: {ItemName} - CreateCode: {ContextSplit[2]}  (Y/N)");
                    var Continue = CH.GetInput(simInputValues?[0]);
                    if (!string.IsNullOrWhiteSpace(Continue) && Continue.Equals("N"))
                    {
                        DisplayName = "";
                        ItemName = "";
                    }
                    else
                    {
                        CH.Feedback("(Last Item Processed) Do you want to generate the code? (Y/N)");
                        Input = CH.GetInput(simInputValues?[0]);
                    }
                }
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
                        foreach (var SplitCommand in SplitCommands)
                        {
                            if (SessionID != null)
                                L.Log($"Found line count of {Content.Length}", SessionID, 1);

                            if (SplitCommand.Length > 0)
                            {
                                if (SessionID != null)
                                    L.Log($"Found command of length {SplitCommand.Length}", SessionID, 1);

                                var SplitLine = SplitCommand.Split("|");
                                if (SplitLine.Length == 2)
                                {
                                    if (SessionID != null)
                                        L.Log($"Found command of length {SplitCommand.Length}", SessionID, 1);
                                    var MenuName = SplitLine[0];
                                    var EntityName = SplitLine[1];

                                    if (CodeGen.CreateDefaultCode(EntityName))
                                    {
                                        CH.Feedback($"{Resource.ModelFileCreated} - {EntityName}.");
                                    }
                                    else
                                        CH.Feedback($"{Resource.ModelFileNotCreated} - {EntityName}");

                                    if (CurrentMenu != null)
                                    {
                                        var Page = CurrentMenu.Page;
                                        var PageHeading = CurrentMenu.PageHeading;
                                        var ParentPage = CurrentMenu.ParentPage;
                                        var NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;

                                        if (MenuName.Length > 0 && EntityName.Length > 0)
                                            if (GenerateMenuItems(CurrentMenu, ref MenuName, EntityName, ref IsBack, ref Page, ref PageHeading, ref ParentPage, ref IsExitOption, ref NewMenuTargetPage))
                                            {
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

        private static void CreateSubMenuLevel2( MenuItem? NewViewMenu, string[] DisplayNamesListView, string itemName)
        {
            string DisplayName;
            bool IsBack;
            int Page;
            string? PageHeading;
            bool IsExitOption;
            int NewMenuTargetPage;
            bool IsDefault = false;
            U.MenuDomain menuDomain = MenuDomain.None;

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

                    _ = Enum.TryParse(itemName, out menuDomain);
                    var TargetTaskSplit = item.Split(' ');

                    AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomain, NewMenuTargetPage, HideRule, TargetTaskSplit[0], IsDefault);
                }
                Page = NewViewMenu.TargetPage;                
                CreateBackAndExitMenuOptions(menuDomain , NewViewMenu, Page);
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

                U.MenuDomain MenuDomainOption = U.MenuDomain.None;

                foreach (var subDisplayName in DisplayNamesList)
                {
                    if (CurrentMenu != null)
                    {
                        
                        DisplayName = $"{subDisplayName} {itemName}";
                        PageHeading = $"{subDisplayName} {itemName} - Sub Menu";
                        NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;
                        _ = Enum.TryParse(itemName, out MenuDomainOption);

                        AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, MenuDomainOption, NewMenuTargetPage);
                    }
                }
                
                CreateBackAndExitMenuOptions(MenuDomainOption, CurrentMenu, Page);
                return true;
            }
            else
                return false; 
        }

        private static void CreateBackAndExitMenuOptions(MenuDomain menuDomainOption, MenuItem? CurrentMenu, int Page)
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
                AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomainOption, NewMenuTargetPage);

                DisplayName = "Exit";
                PageHeading = DisplayName;
                NewMenuTargetPage = 0;
                IsExitOption = true;
                AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, IsExitOption, menuDomainOption, NewMenuTargetPage);
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

        internal static void Load(string? path = null)
        {            
            var Menus  = MH.ImportMenu(path ?? "Menus.txt");
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
            if (MH.ExportMenus())
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
