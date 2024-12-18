using ArchCorpUtilities.Utilities;
using System.Text;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using E = EnumLib.EnumLib;
using M = MenuEnumLib.MenuEnumLib;

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

        public static bool ExportMenus(string path = "Menus.txt", bool DoNotExportMenuItself = true)
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
                    $"Action" + "|" +
                    $"IsVisible" + "|" +
                    $"Tag" + "|" +
                    $"Level" +
                    Environment.NewLine
                    );

                List<MenuItem> MenuEntities = Menu;

                if (DoNotExportMenuItself)
                    MenuEntities = Menu.Where(p => p.Source != null && p.Source.Equals("UserAdded")).ToList();

                foreach (MenuItem item in MenuEntities)
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
                        $"{item.IsStartPage}|" +
                        $"{item.Action}|" +
                        $"{item.IsVisible}|" +
                        $"{item.Tag}|" +
                        $"{item.Level}" +
                        Environment.NewLine
                        );
                }
                File.WriteAllText(path, sb.ToString());
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
            string PageHeading = "Galaxy Manager - Main Menu";
            int PageNumber = 9;

            List<MenuItem> list =
            [
                new MenuItem("Manage Menus", PageNumber, PageHeading, false, 10, "None", false, false, "None", false, "Internal", null, E.MenuDomain.Menu, true),
                new MenuItem("Exit", PageNumber, PageHeading, true),
            ];

            PageHeading = "Manage Menus";
            PageNumber = 10;

            list.Add(new MenuItem("View Menus", PageNumber, PageHeading, false, 11));
            list.Add(new MenuItem("Add Menus", PageNumber, PageHeading, false, 12, "Add",false, true));
            list.Add(new MenuItem("Remove Menus", PageNumber, PageHeading, false, 13));
            list.Add(new MenuItem("Edit Menus", PageNumber, PageHeading, false, 14));
            list.Add(new MenuItem("Save Menu", PageNumber, PageHeading, false, 15));
            list.Add(new MenuItem("Load Menu", PageNumber, PageHeading, false, 16));
            list.Add(new MenuItem("Back to Main Menu", PageNumber, PageHeading, false, 9, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //View Menus
            PageHeading = "View Menus - Sub Menu";
            PageNumber = 11;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "View", false, true, "None", false, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Add Menus
            PageHeading = "Add Menu - Sub Menu";
            PageNumber = 12;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "Refresh", false, false, "None", false, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Add a menu item", PageNumber, PageHeading, false, 0, "Add", false, true, "None", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, false, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, false, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, false, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Remove Menus
            PageHeading = "Remove Menu - Sub Menu";
            PageNumber = 13;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "Refresh", false, true, "None", false, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Remove a menu item", PageNumber, PageHeading, false, 0, "Remove", false, true, "None", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Edit Menus
            PageHeading = "Edit Menu - Sub Menu";
            PageNumber = 14;
            list.Add(new MenuItem("Refresh", PageNumber, PageHeading, false, 0, "Refresh", false, true, "None", false, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Edit a menu item", PageNumber, PageHeading, false, 0, "Edit", false, true, "None", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Next Page", PageNumber, PageHeading, false, 0, "NextPage", false, true, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Previous Page", PageNumber, PageHeading, false, 0, "PreviousPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("First Page", PageNumber, PageHeading, false, 0, "FirstPage", false, true, "FirstPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Last Page", PageNumber, PageHeading, false, 0, "LastPage", false, true, "LastPage", true, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Save Menus to a file
            PageHeading = "Save Menus - Sub Menu";
            PageNumber = 15;
            list.Add(new MenuItem("Save menus to a file", PageNumber, PageHeading, false, 0, "Save", false, true, "None", false, "Internal", null, E.MenuDomain.Menu));
            list.Add(new MenuItem("Back to Manage Menus", PageNumber, PageHeading, false, 10, "None", true));
            list.Add(new MenuItem("Exit", PageNumber, PageHeading, true));

            //Load Menus from a file
            PageHeading = "Load Menus - Sub Menu";
            PageNumber = 16;
            list.Add(new MenuItem("Load menus from a file", PageNumber, PageHeading, false, 0, "Load", false, true, "None", false, "Internal", null, E.MenuDomain.Menu));
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
                sb.Append(heading.ToString());
            sb.AppendLine($" - {U.GetVersion()}");

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
                bool IsStartPage = false;
                M.MenuActionEnum Action_ = M.MenuActionEnum.None;
                while ((linesb = read.ReadLine()) != null)
                {
                    var Line = linesb.Trim();
                    var LineParts = Line.Split('|');
                    if (LineParts.Length > 0)
                    {
                        if (LineParts.Length == 17 || LineParts.Length == 12)
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
                                _ = Enum.TryParse(LineParts[11], out E.MenuDomain Domain);
                                
                                
                                bool IsVisible = false;
                                string Tag = "";
                                if (LineParts.Length > 12)
                                {
                                    _ = bool.TryParse(LineParts[12], out IsStartPage);
                                    _ = Enum.TryParse(LineParts[13], out Action_);
                                    Tag = LineParts[15];
                                }
                                string Source = "Import";
                                menuItem = new(DisplayName, Page, PageHeading, IsExitOption, TargetPage, TargetTask, IsBack, IsDefaultChoice, HideRule, IsHidden, Source, IDGUIDMenu, Domain, IsStartPage, 0 , Action_, IsVisible, Tag);
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
            int InternalCounter = 0;
            foreach (var item in MenuItems)
            {

                //This adds the numbering into DisplayMenuItem e.g. 1.1.1 2.5 etc. depending on the indention level.
                //Updates the menu index.
                string TempPrefix = $"{prefix.Trim()}{Index}".Trim();
                //Updates the number in the menu structure - for retrieval later.
                MenuItem? Menu = menus.FirstOrDefault(p => p.IDGUIDMenu == item.IDGUIDMenu);
                if (Menu != null)
                {
                    InternalCounter++;
                    //_Counter++;
                    _Counter++;
                    Menu.Index = _Counter;
                    Menu.DisplayMenuItem = TempPrefix;
                    Menu.Level = level;
                    Menu.DisplayNumber = InternalCounter;
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
