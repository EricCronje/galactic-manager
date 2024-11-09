using System.Text;

using L = Logger.Logger;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

using static ArchCorpUtilities.Models.Buildings.BuildingHelper;
using System.Runtime.InteropServices;
using ArchCorpUtilities.Utilities;
using System.Runtime.Versioning;

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

        public static bool ExportMenus (string path = "Menus.txt")
        {
            try
            {
                if (File.Exists(path)) { File.Delete(path); }

                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
                StringBuilder sb = new();
                sb.Append(
                    $"IDGUIDMenu" + "|" +
                    $"IsDefaultChoice" + "|" +
                    $"TargetPage" + "|" +
                    $"TargetTask" + "|" +
                    $"DisplayName" + "|" +
                    $"HideRule" + "|" +
                    $"IsExitOption" + "|" +
                    $"IsHidden" + "|" +
                    $"Page" + "|" +
                    $"PageHeading" + "|" +
                    $"IsBack" + "|" +
                    $"Domain" + "|" +
                    $"IsStartPage" + "|" +
                    Environment.NewLine
                    );

                foreach (MenuItem item in Menu)
                {
                    sb.Append(
                        $"{item.IDGUIDMenu}" + "|" +
                        $"{item.IsDefaultChoice}" + "|" +
                        $"{item.TargetPage}" + "|" +
                        $"{item.TargetTask}" + "|" +
                        $"{item.DisplayName}" + "|" +
                        $"{item.HideRule}" + "|" +
                        $"{item.IsExitOption}" + "|" +
                        $"{item.IsHidden}" + "|" +
                        $"{item.Page}" + "|" +
                        $"{item.PageHeading}" + "|" +
                        $"{item.IsBack}" + "|" +
                        $"{item.Domain}" + "|" +
                        $"{item.IsStartPage}" +
                        Environment.NewLine
                        );
                }
                File.AppendAllText(path, sb.ToString());
                return true;
            }
            catch (Exception ex)
            {
                CH.Feedback($"{Resource.FailedToSave} - Error {ex.Message} - {ex.InnerException?.Message}");
                return false;
                throw;
            }
        }

        private static List<MenuItem> MockGetMenuItems()
        {
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            string PageHeading = "Galaxy Manager - Main Menu - V1.0.0";
            int PageNumber = 9;

            List<MenuItem> list =
            [
                new MenuItem("Manage Buildings", PageNumber, PageHeading, false, 1, "None", false,false, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building, true),
                new MenuItem("Manage Menus", PageNumber, PageHeading, false, 10, "None", false, false, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu),
                new MenuItem("Exit", PageNumber, PageHeading, true),
            ];

            PageHeading = "Manage Buildings";
            PageNumber = 1;

            list.Add(new MenuItem("View Buildings",PageNumber, PageHeading, false, 5));
            list.Add(new MenuItem("Add Buildings", PageNumber, PageHeading, false, 2));
            list.Add(new MenuItem("Remove Buildings", PageNumber, PageHeading, false, 4));
            list.Add(new MenuItem("Edit Building Names", PageNumber, PageHeading, false, 3));
            list.Add(new MenuItem("Save Buildings", PageNumber, PageHeading, false, 6));
            list.Add(new MenuItem("Load Buildings", PageNumber, PageHeading, false, 7));
            list.Add(new MenuItem("Back to Main Menu", PageNumber, PageHeading, false, 9, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Add Options
            PageHeading = "Add Buildings - Sub Menu";
            PageNumber = 2;

            list.Add(new MenuItem("Add Building", PageNumber, PageHeading, false, 0, "Add", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            var TargetPage = 1;
            list.Add(new MenuItem("Back to Manage Buildings", PageNumber, PageHeading, false, TargetPage, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //View Buildings
            PageHeading = "View Buildings - Sub Menu";
            PageNumber = 5;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "View", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            TargetPage = 8;
            list.Add(new MenuItem("Search", PageNumber, PageHeading, false, TargetPage, "Search", false, false, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true,  "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", PageNumber, PageHeading, false, 1, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Edit Buildings
            PageHeading = "Edit Building Names- Sub Menu";
            PageNumber = 3;
            list.Add(new MenuItem("Edit Building", PageNumber, PageHeading, false, 0, "Edit", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", PageNumber, PageHeading, false, 1, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Remove Buildings
            PageHeading = "Remove Buildings - Sub Menu";
            PageNumber = 4;
            list.Add(new MenuItem("Remove Building", PageNumber, PageHeading, false, 0, "Remove", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null,  UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", PageNumber, PageHeading, false, 1, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Save to a file
            PageHeading = "Save Buildings - Sub Menu";
            PageNumber = 6;
            list.Add(new MenuItem("Save buildings to a file", PageNumber, PageHeading, false, 0, "Save", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", PageNumber, PageHeading, false, 1, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Load from a file
            PageHeading = "Load Buildings - Sub Menu";
            PageNumber = 7;
            list.Add(new MenuItem("Load buildings from a file",PageNumber, PageHeading, false, 0, "Load", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Back to Manage Buildings", PageNumber, PageHeading, false, 1, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Search
            PageHeading = "View Buildings - Search - Sub Menu";
            PageNumber = 8;
            list.Add(new MenuItem("Search Again", PageNumber, PageHeading, false, 0, "Search", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Remove Building", PageNumber, PageHeading, false, 0, "Remove", false, true,"NoData", false, "None", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Edit Building", PageNumber, PageHeading, false, 0, "Edit", false, true,"NoData", false, "None", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Back to the Building List menu", PageNumber, PageHeading, false, 5, "None", true, false, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Building));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));


            PageHeading = "Manage Menus";
            PageNumber = 10;

            list.Add(new MenuItem("View Menus", PageNumber, PageHeading,  false, 11));
            list.Add(new MenuItem("Add Menus", PageNumber, PageHeading,  false, 12));
            list.Add(new MenuItem("Remove Menus", PageNumber, PageHeading,  false, 13));
            list.Add(new MenuItem("Edit Menus",  PageNumber, PageHeading,  false, 14));
            list.Add(new MenuItem("Save Menu",  PageNumber, PageHeading,  false, 15));
            list.Add(new MenuItem("Load Menu",  PageNumber, PageHeading,  false, 16));
            list.Add(new MenuItem("Back to Main Menu",  PageNumber, PageHeading,  false, 9, "None", true));
            list.Add(new MenuItem("Exit",  PageNumber, PageHeading,  true));

            //View Menus
            PageHeading = "View Menus - Sub Menu";
            PageNumber = 11;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "View", false, true,"None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus",PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Add Menus
            PageHeading = "Add Menu - Sub Menu";
            PageNumber = 12;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "Refresh", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Add a menu item", PageNumber, PageHeading, false, 0, "Add", false, true, "None", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Remove Menus
            PageHeading = "Remove Menu - Sub Menu";
            PageNumber = 13;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading,false, 0, "Refresh", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Remove a menu item", PageNumber, PageHeading, false, 0, "Remove", false, true, "None", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Edit Menus
            PageHeading = "Edit Menu - Sub Menu";
            PageNumber = 14;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "Refresh", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Edit a menu item", PageNumber, PageHeading, false, 0, "Edit", false, true, "None", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Save Menus to a file
            PageHeading = "Save Menus - Sub Menu";
            PageNumber = 15;
            list.Add(new MenuItem("Save menus to a file", PageNumber, PageHeading, false, 0, "Save", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Load Menus from a file
            PageHeading = "Load Menus - Sub Menu";
            PageNumber = 16;
            list.Add(new MenuItem("Load menus from a file", PageNumber, PageHeading, false, 0, "Load", false, true, "None", false, "Internal", null, UniversalUtilities.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

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

        public static List<MenuItem>? ImportMenu(string path = "Menus.txt")
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
                        if (LineParts.Length == 13 || LineParts.Length == 12)
                        {
                            string IDGUIDMenu = LineParts[0];
                            if (IDGUIDMenu != "IDGUIDMenu")
                            {
                                _ = bool.TryParse(LineParts[1], out bool IsDefaultChoice);
                                _ = Int32.TryParse(LineParts[2], out int TargetPage);
                                string TargetTask = LineParts[3];
                                string DisplayName = LineParts[4];
                                string HideRule = LineParts[5];
                                _ = bool.TryParse(LineParts[6], out bool IsExitOption);
                                _ = bool.TryParse(LineParts[7], out bool IsHidden);
                                _ = Int32.TryParse(LineParts[8], out int Page);
                                string PageHeading = LineParts[9];
                                _ = bool.TryParse(LineParts[10], out bool IsBack);
                                _ = Enum.TryParse(LineParts[11], out UniversalUtilities.MenuDomain Domain);
                                bool IsStartPage = false;
                                
                                if (LineParts.Length > 12)
                                    _ = bool.TryParse(LineParts[12], out IsStartPage);

                                string Source = "Import";
                                menuItem = new(DisplayName, Page, PageHeading, IsExitOption, TargetPage, TargetTask, IsBack, IsDefaultChoice, HideRule, IsHidden, Source, IDGUIDMenu, Domain, IsStartPage);
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

        public static void LoadDefaults()
        {
            CH.Feedback(Resource.LoadMenuDefaults);
            Menu.Clear();
            Menu = MockGetMenuItems();
        }
    }
}
