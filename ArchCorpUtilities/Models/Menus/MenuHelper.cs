using System.Text;
using static System.Net.Mime.MediaTypeNames;
using L = Logger.Logger;
using BH = ArchCorpUtilities.Models.Buildings.BuildingHelper;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;

using static ArchCorpUtilities.Models.Buildings.BuildingHelper;
using System.Runtime.InteropServices;

namespace ArchCorpUtilities.Models.Menus
{
    public static class MenuHelper
    {

        public static List<MenuItem> Menu { get; set; }

        static MenuHelper()
        {
            Line = Resource.HorizontalLine;
            Menu = MockGetMenuItems();
            SessionID = "TBS";
        }

        public static void ExportMenus (string path = "Menus")
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            StringBuilder sb = new();
            sb.Append(
                $"IDGUIDMenu" + "|" +
                $"IsDefaultChoice" + "|" +
                $"TargetPage" + "|" +
                $"TargetTask" + "|" +
                $"DisplayMenuItem" + "|" +
                $"DisplayName" + "|" +
                $"DisplayNumber" + "|" +
                $"HideRule" + "|" +
                $"Index" + "|" +
                $"IsExitOption" + "|" +
                $"IsHidden" + "|" +
                $"IsPagination" + "|" +
                $"Page" + "|" +
                $"PageHeading" + "|" +
                $"ParentPage" + "|" +
                $"IsBack" +
                Environment.NewLine
                );

            foreach (MenuItem item in Menu)
            {
                sb.Append(
                    $"{item.IDGUIDMenu}" + "|" +
                    $"{item.IsDefaultChoice}" + "|" +
                    $"{item.TargetPage}" + "|" +
                    $"{item.TargetTask}" + "|" +
                    $"{item.DisplayMenuItem}" + "|" +
                    $"{item.DisplayName}" + "|" +
                    $"{item.DisplayNumber}" + "|" +
                    $"{item.HideRule}" + "|" +
                    $"{item.Index}" + "|" +
                    $"{item.IsExitOption}" + "|" +
                    $"{item.IsHidden}" + "|" +
                    $"{item.IsPagination}" + "|" +
                    $"{item.Page}" + "|" +
                    $"{item.PageHeading}" + "|" +
                    $"{item.ParentPage}" + "|" +
                    $"{item.IsBack}" +
                    Environment.NewLine
                    );
            }
            File.AppendAllText(path, sb.ToString());
        }

        private static List<MenuItem> MockGetMenuItems()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            string PageHeading = "Galaxy Manager - Main Menu - V1.0.0";
            int BuildingMenuParent = -1;
            int DisplayNumber = 1;
            int PageNumber = 9;

            List<MenuItem> list =
            [
                new MenuItem("Manage Buildings", DisplayNumber, 10, PageNumber, PageHeading, BuildingMenuParent, false, 1, "None", false,false, "None", false, false, "Internal", null, true,MenuItem.MenuDomain.Building),
                new MenuItem("Manage Menus", DisplayNumber, 20, PageNumber, PageHeading, BuildingMenuParent, false, 10, "None", false, false, "None", false, false, "Internal", null, false,MenuItem.MenuDomain.Menu),
                new MenuItem("Exit", DisplayNumber++, 30, PageNumber, PageHeading, BuildingMenuParent, true),
            ];

            PageHeading = "Manage Buildings";
            BuildingMenuParent = 0;
            DisplayNumber = 1;
            PageNumber = 1;

            list.Add(new MenuItem("View Buildings", DisplayNumber, 10, PageNumber, PageHeading, BuildingMenuParent, false, 5));
            list.Add(new MenuItem("Add Buildings", DisplayNumber++, 20, PageNumber, PageHeading, BuildingMenuParent, false, 2));
            list.Add(new MenuItem("Remove Buildings", DisplayNumber++, 30, PageNumber, PageHeading, BuildingMenuParent, false, 4));
            list.Add(new MenuItem("Edit Building Names", DisplayNumber++, 40, PageNumber, PageHeading, BuildingMenuParent, false, 3));
            list.Add(new MenuItem("Save Buildings", DisplayNumber++, 50, PageNumber, PageHeading, BuildingMenuParent, false, 6));
            list.Add(new MenuItem("Load Buildings", DisplayNumber++, 60, PageNumber, PageHeading, BuildingMenuParent, false, 7));
            list.Add(new MenuItem("Back to Main Menu", DisplayNumber++, 70, PageNumber, PageHeading, BuildingMenuParent, false, 9, "None", true));
            list.Add(new MenuItem("Exit", DisplayNumber++, 80, PageNumber, PageHeading, BuildingMenuParent, true));

            //Add Options
            PageHeading = "Add Buildings - Sub Menu";
            PageNumber = 2;

            list.Add(new MenuItem("Add Building", 1, 10, PageNumber, PageHeading, 0, false, 0, "Add", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            BuildingMenuParent = 1;
            var TargetPage = 1;
            list.Add(new MenuItem("Back to Manage Buildings", 2, 20, PageNumber, PageHeading, BuildingMenuParent, false, TargetPage, "None", true));
            list.Add(new MenuItem("Exit", 3, 30, PageNumber, PageHeading, BuildingMenuParent, true));

            //View Buildings
            PageHeading = "View Buildings - Sub Menu";
            PageNumber = 5;
            list.Add(new MenuItem("Refresh", 1, 10, PageNumber, PageHeading, 0, false, 0, "View", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            TargetPage = 8;
            list.Add(new MenuItem("Search", 2, 20, PageNumber, PageHeading, 0, false, TargetPage, "Search", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", 7, 70, PageNumber, PageHeading, 1, false, 1, "None", true));
            list.Add(new MenuItem("Exit", 8, 80, PageNumber, PageHeading, BuildingMenuParent, true));

            //Edit Buildings
            PageHeading = "Edit Building Names- Sub Menu";
            PageNumber = 3;
            list.Add(new MenuItem("Edit Building", 1, 10, PageNumber, PageHeading, 0, false, 0, "Edit", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", 8, 80, PageNumber, PageHeading, 1, false, 1, "None", true));
            list.Add(new MenuItem("Exit", 9, 90, PageNumber, PageHeading, BuildingMenuParent, true));

            //Remove Buildings
            PageHeading = "Remove Buildings - Sub Menu";
            PageNumber = 4;
            list.Add(new MenuItem("Remove Building", 1, 10, PageNumber, PageHeading, 0, false, 0, "Remove", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", 8, 80, PageNumber, PageHeading, 1, false, 1, "None", true));
            list.Add(new MenuItem("Exit", 9, 90, PageNumber, PageHeading, BuildingMenuParent, true));

            //Save to a file
            PageHeading = "Save Buildings - Sub Menu";
            PageNumber = 6;
            list.Add(new MenuItem("Save buildings to a file", 1, 10, PageNumber, PageHeading, 0, false, 0, "Save", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", 2, 20, PageNumber, PageHeading, 1, false, 1, "None", true));
            list.Add(new MenuItem("Exit", 3, 80, PageNumber, PageHeading, BuildingMenuParent, true));

            //Load from a file
            PageHeading = "Load Buildings - Sub Menu";
            PageNumber = 7;
            list.Add(new MenuItem("Load buildings from a file", 1, 10, PageNumber, PageHeading, 0, false, 0, "Load", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", 2, 20, PageNumber, PageHeading, 1, false, 1, "None", true));
            list.Add(new MenuItem("Exit", 3, 80, PageNumber, PageHeading, BuildingMenuParent, true));

            //Search
            PageHeading = "View Buildings - Search - Sub Menu";
            PageNumber = 8;
            list.Add(new MenuItem("Search Again", 1, 10, PageNumber, PageHeading, 0, false, 0, "Search", false, true, "None", false, false, "None", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Remove Building", 2, 20, PageNumber, PageHeading, 0, false, 0, "Remove", false, true,"NoData", false, false, "None", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Edit Building", 3, 30, PageNumber, PageHeading, 0, false, 0, "Edit", false, true,"NoData", false, false, "None", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Back to the Building List menu", 4, 40, PageNumber, PageHeading, 5, false, 5, "None", true, false, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Building));
            list.Add(new MenuItem("Exit", 3, 80, PageNumber, PageHeading, BuildingMenuParent, true));


            PageHeading = "Manage Menus";
            BuildingMenuParent = 0;
            DisplayNumber = 1;
            PageNumber = 10;

            list.Add(new MenuItem("View Menus", DisplayNumber, 10, PageNumber, PageHeading, BuildingMenuParent, false, 11));
            list.Add(new MenuItem("Add Menus", DisplayNumber++, 20, PageNumber, PageHeading, BuildingMenuParent, false, 12));
            list.Add(new MenuItem("Remove Menus", DisplayNumber++, 30, PageNumber, PageHeading, BuildingMenuParent, false));
            list.Add(new MenuItem("Edit Menus", DisplayNumber++, 40, PageNumber, PageHeading, BuildingMenuParent, false));
            list.Add(new MenuItem("Save Menu", DisplayNumber++, 50, PageNumber, PageHeading, BuildingMenuParent, false));
            list.Add(new MenuItem("Load Menu", DisplayNumber++, 60, PageNumber, PageHeading, BuildingMenuParent, false));
            list.Add(new MenuItem("Back to Main Menu", DisplayNumber++, 70, PageNumber, PageHeading, BuildingMenuParent, false, 9, "None", true));
            list.Add(new MenuItem("Exit", DisplayNumber++, 80, PageNumber, PageHeading, BuildingMenuParent, true));

            //View Menus
            PageHeading = "View Menus - Sub Menu";
            PageNumber = 11;
            BuildingMenuParent = 10;
            list.Add(new MenuItem("Refresh", 1, 10, PageNumber, PageHeading, 0, false, 0, "View", false, true,"None", false, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", 7, 70, PageNumber, PageHeading, 1, false, 10, "None", true));
            list.Add(new MenuItem("Exit", 8, 80, PageNumber, PageHeading, BuildingMenuParent, true));

            //View Menus
            PageHeading = "Add Menu - Sub Menu";
            PageNumber = 12;
            BuildingMenuParent = 10;
            list.Add(new MenuItem("Refresh", 1, 10, PageNumber, PageHeading, 0, false, 0, "Refresh", false, true, "None", false, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Add a menu item", 3, 30, PageNumber, PageHeading, 0, false, 0, "Add", false, true, "None", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", 7, 70, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true, false, "Internal", null, false, MenuItem.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", 8, 80, PageNumber, PageHeading, 1, false, 10, "None", true));
            list.Add(new MenuItem("Exit", 9, 90, PageNumber, PageHeading, BuildingMenuParent, true));



            return list;
        }

        public static string? Line { get; }
        public static string? MenuItems { get; }
        public static string SessionID { get; internal set; }

        public static string ShowHeading<T>(T heading)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            StringBuilder sb = new();
            sb.AppendLine(Line);
            if (heading != null)
                sb.AppendLine(heading.ToString());

            sb.Append(Line);

            return sb.ToString();
        }

        public static string ShowMenu(string? menuHeading, int? page = 0)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            StringBuilder _menuItemsSB = new();
            _menuItemsSB.AppendLine(ShowHeading(menuHeading));
            if (page != null)
            {
                int Index = 1;
                foreach (MenuItem item in Menu.OrderBy(c => c.Index).Where(p => p.Page.Equals(page.Value) && p.IsHidden == false).ToList())
                {
                    item.DisplayNumber = Index;
                    _menuItemsSB.AppendLine($"{item.DisplayNumber} {item.DisplayName}");
                    Index++;
                }
                _menuItemsSB.AppendLine(Line);
                _menuItemsSB.Append("Choice:");
            }

            return _menuItemsSB.ToString();
        }

        public static List<MenuItem>? ImportMenu(string path)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            MenuItem? menuItem;
            List<MenuItem>? menuItems;

            if (File.Exists(path))
            {
                using StreamReader read = new(path);
                menuItems = [];
                string? linesb;
                while ((linesb = read.ReadLine()) != null)
                {
                    var Line = linesb.Trim();
                    var LineParts = Line.Split('|');
                    if (LineParts.Length > 0)
                    {
                        if (LineParts.Length == 16)
                        {
                            string IDGUIDMenu = LineParts[0];
                            if (IDGUIDMenu != "IDGUIDMenu")
                            {
                                _ = bool.TryParse(LineParts[1], out bool IsDefaultChoice);
                                _ = Int32.TryParse(LineParts[2], out int TargetPage);
                                string TargetTask = LineParts[3];
                                string DisplayMenuItem = LineParts[4];
                                string DisplayName = LineParts[5];
                                _ = Int32.TryParse(LineParts[6], out int DisplayNumber);
                                string HideRule = LineParts[7];
                                _ = Int32.TryParse(LineParts[8], out int Index);
                                _ = bool.TryParse(LineParts[9], out bool IsExitOption);
                                _ = bool.TryParse(LineParts[10], out bool IsHidden);
                                _ = bool.TryParse(LineParts[11], out bool IsPagination);
                                _ = Int32.TryParse(LineParts[12], out int Page);
                                string PageHeading = LineParts[13];
                                _ = Int32.TryParse(LineParts[14], out int ParentPage);
                                _ = bool.TryParse(LineParts[15], out bool IsBack);
                                string Source = "Import";
                                menuItem = new(DisplayName, DisplayNumber, Index, Page, PageHeading, ParentPage, IsExitOption, TargetPage, TargetTask, IsBack, IsDefaultChoice, HideRule, IsHidden, IsPagination, Source, IDGUIDMenu);
                                menuItems?.Add(menuItem);
                            }
                        }
                    }
                    else 
                    {
                        throw new Exception("Empty lines");
                    }
                }
                return menuItems;

            }
            return null;   
        }

        static int _Counter = 0;

        /// <summary>
        /// Updates the DisplayMenuItem with the indentation numbers. e.g 1.1.1 2.5
        /// Updates the index of the menu
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="page"></param>
        /// <param name="level"></param>
        /// <param name="prefix"></param>
        public static void MapMenus(List<MenuItem> menus, int? page, int level = 0, string prefix = "")
        {
            var MenuItems = menus.Where(p => p.Page == page);
            var Index = 1;
            
            foreach (var item in MenuItems)
            {
                
                //This adds the numbering into DisplayMenuItem e.g. 1.1.1 2.5 etc. depending on the indention level.
                //Updates the menu index.
                string TempPrefix = $"{prefix.Trim()}{Index}".Trim();
                //Updates the number in the menu structure - for retrieval later.
                MenuItem? Menu = menus.FirstOrDefault(p => p.IDGUIDMenu == item.IDGUIDMenu);
                if (Menu != null)
                {
                    //_Counter++;
                    _Counter++;
                    Menu.Index = _Counter;
                    Menu.DisplayMenuItem = TempPrefix;
                    Menu.Level = level;
                }
                
                if (item.TargetPage > 0 && item.IsBack == false)
                {
                    level++;
                    prefix = $"{prefix}{Index}.";
                    MapMenus(menus, item.TargetPage, level, prefix);
                    level--;
                    if (level == 0)
                    {
                        prefix = "";
                    }
                    else
                    {
                        var sPrefix = prefix.Trim().Split(".");
                        prefix = "";
                        for (var i = 0; i < level; i++)
                        {
                            prefix = prefix + sPrefix[i] + ".";
                        }
                    }
                }
                Index++;
            }
            
            if (prefix.Length == 0) //The prefix is not passed in the initial call ...
                _Counter = 0; // meaning this is the last instance - and the counter can be reset.
        }

        public static bool ApplyHiddenRules(Patina.Patina page)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            bool ResetPage = false;
            if (Menu != null)
            {
                foreach (var item in Menu.Where(p => p.HideRule != "None"))
                {
                    switch (item.HideRule)
                    {
                        case "FirstPage":
                            if (page.IsFirstPage())
                            {
                                item.IsHidden = true;
                                ResetPage = true;
                            }
                            break;
                        case "LastPage":
                            if (page.IsLastPage())
                            {
                                item.IsHidden = true;
                                ResetPage = true;
                            }
                            break;
                        case "NoData":
                            switch (item?.Domain)
                            {
                                case MenuItem.MenuDomain.Building: 
                                    L.Log("Building-Add", SessionID, 1);
                                    if (!IsBuildingsOnThePage())
                                    {
                                        item.IsHidden = true; 
                                        ResetPage = true;
                                    }
                                    break;
                                case MenuItem.MenuDomain.Menu: 
                                    L.Log("Menu-Add", SessionID, 1);
                                    if (!MMH.IsMenusOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
                                default: break;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
            return ResetPage;
        }

        public static void ResetIsHidden()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            if (Menu != null)
            {
                foreach (var item in Menu.Where(p => p.IsHidden))
                {
                    item.IsHidden = false;
                }
            }
        }

        public static void DefaultTasks(int page, int? simChoice, string? simInput, int? defaultChoice)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            var MenuItem = Menu?.FirstOrDefault(c => c.Page == page && c.DisplayNumber == defaultChoice && c.IsHidden == false);

            var TargetTask = MenuItem?.TargetTask;

            switch (TargetTask)
            {
                case "Add":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building: L.Log("Building-Add", SessionID, 1); Add(simInput); break;
                        case MenuItem.MenuDomain.Menu: L.Log("Menu-Add", SessionID, 1); MMH.Add(simChoice, simInput); break;
                        default: break;
                    }
                    break;

                case "View":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building: L.Log("Building-View", SessionID, 1); BH.InitialView(); break;
                        case MenuItem.MenuDomain.Menu: L.Log("Menu-View", SessionID, 1); MMH.InitialView(); break;
                        default: break;
                    }
                    break;

                case "FirstPage":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-FirstPage", SessionID, 1);
                            BH.View(Navigation.FirstPage);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-FirstPage", SessionID, 1);
                            MMH.View(Navigation.FirstPage);
                            break;
                        default:
                            break;
                    }
                    break;

                case "LastPage":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-LastPage", SessionID, 1);
                            BH.View(Navigation.LastPage);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-LastPage", SessionID, 1);
                            MMH.View(Navigation.LastPage);
                            break;
                        default:
                            break;
                    }
                    break;

                case "NextPage":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-NextPage", SessionID, 1);
                            BH.View(Navigation.NextPage);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-NextPage", SessionID, 1);
                            MMH.View(Navigation.NextPage);
                            break;
                        default:
                            break;
                    }
                    break;

                case "PreviousPage":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-PreviousPage", SessionID, 1);
                            BH.View(Navigation.PreviousPage);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-PreviousPage", SessionID, 1);
                            MMH.View(Navigation.PreviousPage);
                            break;
                        default:
                            break;
                    }
                    break;

                case "Edit":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-Edit", SessionID, 1);
                            BH.Edit(simChoice, simInput);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-Edit", SessionID, 1);
                            MMH.Edit(simChoice, simInput);
                            break;
                        default:
                            break;
                    }
                    break;

                case "Remove":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-Remove", SessionID, 1);
                            BH.Remove(simChoice);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-Remove", SessionID, 1);
                            MMH.Remove();
                            break;
                        default:
                            break;
                    }                    
                    break;

                case "Save":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-Save", SessionID, 1);
                            BH.Save();
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-Save", SessionID, 1);
                            MMH.Save();
                            break;
                        default:
                            break;
                    }
                    break;

                case "Load":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-Load", SessionID, 1);
                            BH.Load();
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-Load", SessionID, 1);
                            MMH.Load();
                            break;
                        default:
                            break;
                    }                    
                    break;

                case "Search":                    
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-Search", SessionID, 1);
                            BH.Search();
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-Search", SessionID, 1);
                            MMH.Search();
                            break;
                        default:
                            break;
                    }
                    break;

                case "Refresh":
                    switch (MenuItem?.Domain)
                    {
                        case MenuItem.MenuDomain.Building:
                            L.Log("Building-Refresh", SessionID, 1);
                            break;
                        case MenuItem.MenuDomain.Menu:
                            L.Log("Menu-Refresh", SessionID, 1);
                            MMH.Refresh();
                            break;
                        default:
                            break;
                    }                    
                    break;

                default:
                    break;
            }
        }

        public static void PerformDefaultTasks(int page, int prevPage, int choice , int? simChoice = null, string? simInput = null)
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            // if simulation use the simChoice
            if (simChoice != null) { choice = simChoice.Value; }

            int? defaultChoice = Menu?.FirstOrDefault(c => c.IsDefaultChoice && c.Page == page)?.DisplayNumber;
            bool isSamePage = page.Equals(prevPage);

            if (isSamePage) { defaultChoice = choice; }
            // For simulation only

            DefaultTasks(page, simChoice, simInput, defaultChoice);
            ResetIsHidden();
            ApplyHiddenRules(page);

        }

        private static void ApplyHiddenRules(int page)
        {
            var CurrentMenu = Menu?.FirstOrDefault(c => c.Page == page)?.Domain;
            // Apply hiding rules per menu domain - one domain per page ...
            switch (CurrentMenu)
            {
                case MenuItem.MenuDomain.Building:
                    if (ApplyHiddenRules(BH.Page))
                        BH.ReIndexDisplayId(BH.Buildings);
                    break;

                case MenuItem.MenuDomain.Menu:
                    ApplyHiddenRules(MMH.MenuPage);
                    break;

                default:
                    break;
            }
        }
    }
}
