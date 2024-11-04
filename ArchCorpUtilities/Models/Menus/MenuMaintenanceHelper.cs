using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using Page = Patina.Patina;
using L = Logger.Logger;

using static ArchCorpUtilities.Models.Buildings.BuildingHelper;
using System.Net.NetworkInformation;
using System.Text;
using System;

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

        internal static void View(Navigation navigate = Navigation.FirstPage)
        {
            var orderedMenus = MH.Menu?.OrderBy(p => p.Index).ToList();
            MenuItemsOnThePage = BH.ViewWithPagination("Current menu structure", MenuPage, orderedMenus, navigate);
        }

        internal static bool IsMenusOnThePage()
        {
            return !(MenuItemsOnThePage == null || (MenuItemsOnThePage != null && MenuItemsOnThePage.Count == 0));
        }

        internal static void InitialView()
        {
            PrepareMenuWrapper(MH.Menu);
            View(Navigation.FirstPage);
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
            MenuItemsOnThePage = BH.ViewWithPagination("Current menu structure", MenuPage ,orderedMenus, Navigation.FirstPage);
        }

        internal static void Add(int? simChoice = null, string ? simInput = null)
        {
            //List the items
            if (MenuItemsOnThePage != null)
            {
                var orderedMenus = MenuItemsOnThePage?.OrderBy(p => p.Index).ToList();
                MenuItemsOnThePage = BH.ViewWithPagination("Current menu structure",MenuPage, orderedMenus, Navigation.FirstPage);

            }
            CH.Feedback(Resource.SelectMenuItem);
            //Ask to select one
            CH.Feedback(Resource.ChoicePrompt);
            string MenuItemInput = CH.GetInput(Convert.ToString(simChoice));
            //If none selected - do nothing
            if (!string.IsNullOrWhiteSpace(MenuItemInput))
            {
                //If item selected - ask for the details
                //If all details are filled in - create the item.
                var CurrentMenu = MH.Menu.FirstOrDefault(p => p.DisplayMenuItem == MenuItemInput);
                if (CurrentMenu != null)
                {
                    CH.Feedback(Resource.DisplayNamePrompt);
                    string DisplayName = CH.GetInput(simInput);
                    bool IsBack = false;
                    var Page = CurrentMenu.Page;
                    var PageHeading = CurrentMenu.PageHeading;
                    var ParentPage = CurrentMenu.ParentPage;
                    var IsExitOption = false;
                    var NewMenuTargetPage = MH.Menu.Max(p => p.TargetPage) + 1;

                    var AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, ParentPage, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);

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

                        string ItemName = CH.GetInput(simInput);
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
                        CreateSubMenuLevel2(NewViewMenu, DisplayNamesListView);

                        //Find the add menu item - that was created
                        var newAddMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Add {ItemName}");
                        DisplayNamesListView = [$"Add {ItemName}"];
                        CreateSubMenuLevel2(newAddMenu, DisplayNamesListView);

                        //Find the Remove menu item - that was created
                        var newRemoveMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Remove {ItemName}");
                        DisplayNamesListView = [$"Remove {ItemName}", "Next Page", "Last Page", "First Page", "Previous Page"];
                        CreateSubMenuLevel2(newRemoveMenu, DisplayNamesListView);

                        //Find the Edit menu item - that was created
                        var newEditMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Edit {ItemName}");
                        DisplayNamesListView = [$"Edit {ItemName}", "Next Page", "Last Page", "First Page", "Previous Page"];
                        CreateSubMenuLevel2(newEditMenu, DisplayNamesListView);

                        //Find the save menu item - that was created
                        var newSaveMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Save {ItemName}");
                        DisplayNamesListView = [$"Save {ItemName} to a file"];
                        CreateSubMenuLevel2(newSaveMenu, DisplayNamesListView);

                        //Find the load menu item - that was created
                        var newLoadMenu = MH.Menu.FirstOrDefault(p => p.DisplayName == $"Load {ItemName}");
                        DisplayNamesListView = [$"Load {ItemName} from a file"];
                        CreateSubMenuLevel2(newLoadMenu, DisplayNamesListView);

                        //Create a folder in the models
                        var WorkingDir = @"C:\_FLAP03\GBZZBEBJ\Working\dotnet\galactic-manager\ArchCorpUtilities\Models";
                        var ItemCSFile = $"{WorkingDir}\\{ItemName}\\{ItemName}.cs";
                        if (!File.Exists(ItemCSFile))
                        {
                            if (!Directory.Exists($"{WorkingDir}\\{ItemName}"))
                            {
                                Directory.CreateDirectory($"{WorkingDir}\\{ItemName}");
                            }
                            
                            StringBuilder stringBuilder = new();
                            stringBuilder.AppendLine($"namespace ArchCorpUtilities.Models.{ItemName}.{ItemName};");
                            stringBuilder.AppendLine($"public class {ItemName}");
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("public string? Name { get; }");
                            stringBuilder.AppendLine("public int Id { get; set; }");
                            stringBuilder.AppendLine("public int DisplayId { get; internal set; }");
                            stringBuilder.AppendLine("public string? " + ItemName + "Guid {get; internal set; }");
                            stringBuilder.AppendLine($"    public {ItemName}(string? name, int id, string guid = \"\")");
                            stringBuilder.AppendLine("    {");
                            stringBuilder.AppendLine("        Name = name;");
                            stringBuilder.AppendLine("        Id = id;");
                            stringBuilder.AppendLine("        DisplayId = Id;");
                            stringBuilder.AppendLine("        if (guid != null && guid.Length > 0)");
                            stringBuilder.AppendLine("        {");
                            stringBuilder.AppendLine($"            {ItemName}Guid = guid;");
                            stringBuilder.AppendLine("        }");
                            stringBuilder.AppendLine("        else");
                            stringBuilder.AppendLine("        {");
                            stringBuilder.AppendLine($"            {ItemName}Guid = Guid.NewGuid().ToString();");
                            stringBuilder.AppendLine("        }");
                            stringBuilder.AppendLine("    }");
                            stringBuilder.AppendLine("    public void SetDisplayId(int displayId) { DisplayId = displayId; }");
                            stringBuilder.AppendLine("}");

                            var BaseFile = stringBuilder .ToString();


                            File.WriteAllText(ItemCSFile, BaseFile);
                        }

                    }

                }

            }
            else
                CH.Feedback(Resource.NoMenuItemSelected);
        }

        private static void CreateSubMenuLevel2( MenuItem? NewViewMenu, string[] DisplayNamesListView)
        {
            string DisplayName;
            bool IsBack;
            int Page;
            string? PageHeading;
            int ParentPage;
            bool IsExitOption;
            int NewMenuTargetPage;

            if (NewViewMenu != null)
            {
                foreach (var item in DisplayNamesListView)
                {
                    DisplayName = item;
                    IsBack = false;
                    ParentPage = NewViewMenu.Page;
                    Page = NewViewMenu.TargetPage;
                    PageHeading = $"{DisplayName}";
                    IsExitOption = false;
                    NewMenuTargetPage = 0;
                    AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, ParentPage, IsExitOption, NewViewMenu.Domain, NewMenuTargetPage);
                }
                Page = NewViewMenu.TargetPage;
                ParentPage = NewViewMenu.Page;
                CreateBackAndExitMenuOptions(NewViewMenu, Page, ParentPage);
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

                        AddedMenu = AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, ParentPage, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);
                    }
                }
                
                CreateBackAndExitMenuOptions(CurrentMenu, Page, ParentPage);
                return true;
            }
            else
                return false; 
        }

        private static void CreateBackAndExitMenuOptions(MenuItem? CurrentMenu, int Page, int ParentPage)
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
                AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, ParentPage, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);

                DisplayName = "Exit";
                PageHeading = DisplayName;
                NewMenuTargetPage = 0;
                IsExitOption = true;
                AddNewMenuItem(DisplayName, IsBack, Page, PageHeading, ParentPage, IsExitOption, CurrentMenu.Domain, NewMenuTargetPage);
            }
        }

        private static bool AddNewMenuItem(string displayName, bool isBack, int page, string pageHeading, int parentPage, bool isExitOption, MenuItem.MenuDomain domain, int newMenuTargetPage, string hideRule = "None", string targetTask = "None")
        {
            if (!string.IsNullOrWhiteSpace(displayName))
            {
                bool IsBack = isBack;
                var Page = page;
                var PageHeading = pageHeading;
                var ParentPage = parentPage;
                var IsExitOption = isExitOption;
                var NewMenuTargetPage = newMenuTargetPage;

                MenuItem NewMenuItem = new
                (displayName, 0, 0,
                Page,
                PageHeading,
                ParentPage,
                IsExitOption,
                NewMenuTargetPage,
                targetTask,
                IsBack,
                false,
                hideRule,
                false,
                false,
                "UserAdded",
                null,
                false,
                domain);
                // Add the item to the menu structure.
                MH.Menu.Add(NewMenuItem);
                return true;
            }
            else
                return false;
        }

        internal static void Edit(int? simChoice, string? simInput)
        {
            throw new NotImplementedException();
        }

        internal static void Search()
        {
            throw new NotImplementedException();
        }

        internal static void Load()
        {
            throw new NotImplementedException();
        }

        internal static void Save()
        {
            throw new NotImplementedException();
        }

        internal static void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
