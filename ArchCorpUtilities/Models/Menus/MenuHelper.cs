using System.Text;
using L = Logger.Logger;

namespace ArchCorpUtilities.Models.Menus
{
    public static class MenuHelper
    {
        public static List<MenuItem> CurrentMenuPage { get; set; }

        static MenuHelper()
        {
            Line = Resource.HorizontalLine;
            StartingPageHeading = "Building Manager - Main Menu - V1.0.0";
            CurrentMenuPage = MockGetMenuItems();
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

            foreach (MenuItem item in CurrentMenuPage)
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
            string PageHeading = "Building Manager - Main Menu - V1.0.0";
            int BuildingMenuParent = 0;
            int DisplayNumber = 1;
            int PageNumber = 1;
            List<MenuItem> list =
            [
                new MenuItem("View Buildings", DisplayNumber, 10, PageNumber, PageHeading, BuildingMenuParent, false, 5),
                new MenuItem("Add Buildings", DisplayNumber++, 20, PageNumber, PageHeading, BuildingMenuParent, false, 2),
                new MenuItem("Remove Buildings", DisplayNumber++, 30, PageNumber, PageHeading, BuildingMenuParent, false, 4),
                new MenuItem("Edit Building Names", DisplayNumber++, 40, PageNumber, PageHeading, BuildingMenuParent, false, 3),
                new MenuItem("Save Buildings", DisplayNumber++, 50, PageNumber, PageHeading, BuildingMenuParent, false, 6),
                new MenuItem("Load Buildings", DisplayNumber++, 60, PageNumber, PageHeading, BuildingMenuParent, false, 7),
                new MenuItem("Exit", DisplayNumber++, 70, PageNumber, PageHeading, BuildingMenuParent, true),
            ];
            //Add Options
            PageHeading = "Add Buildings - Sub Menu";
            PageNumber = 2;
            list.Add(new MenuItem("Add Building", 1, 10, PageNumber, PageHeading, 0, false, 0, "AddBuilding", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, PageNumber, PageHeading, 1, false, 1, "None", true));

            //View Buildings
            PageHeading = "View Buildings - Sub Menu";
            PageNumber = 5;
            list.Add(new MenuItem("Refresh", 1, 10, PageNumber, PageHeading, 0, false, 0, "ViewBuilding", false, true));
            var TargetPage = 8;
            list.Add(new MenuItem("Search", 2, 20, PageNumber, PageHeading, 0, false, TargetPage, "SearchBuildings", false, true));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true));
            list.Add(new MenuItem("Back to Main Menu", 7, 70, PageNumber, PageHeading, 1, false, 1, "None", true));

            //Edit Buildings
            PageHeading = "Edit Building Names- Sub Menu";
            PageNumber = 3;
            list.Add(new MenuItem("Edit Building", 1, 10, PageNumber, PageHeading, 0, false, 0, "EditBuilding", false, true));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true));
            list.Add(new MenuItem("Back to Main Menu", 8, 80, PageNumber, PageHeading, 1, false, 1, "None", true));

            //Remove Buildings
            PageHeading = "Remove Buildings - Sub Menu";
            PageNumber = 4;
            list.Add(new MenuItem("Remove Building", 1, 10, PageNumber, PageHeading, 0, false, 0, "RemoveBuilding", false, true));
            list.Add(new MenuItem("Next Page", 3, 30, PageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage", true));
            list.Add(new MenuItem("Previous Page", 4, 40, PageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage", true));
            list.Add(new MenuItem("First Page", 5, 50, PageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage", true));
            list.Add(new MenuItem("Last Page", 6, 60, PageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage", true));
            list.Add(new MenuItem("Back to Main Menu", 8, 80, PageNumber, PageHeading, 1, false, 1, "None", true));

            //Save to a file
            PageHeading = "Save Buildings - Sub Menu";
            PageNumber = 6;
            list.Add(new MenuItem("Save buildings to a file", 1, 10, PageNumber, PageHeading, 0, false, 0, "SaveBuildings", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, PageNumber, PageHeading, 1, false, 1, "None", true));

            //Load from a file
            PageHeading = "Load Buildings - Sub Menu";
            PageNumber = 7;
            list.Add(new MenuItem("Load buildings from a file", 1, 10, PageNumber, PageHeading, 0, false, 0, "LoadBuildings", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, PageNumber, PageHeading, 1, false, 1, "None", true));

            //Search
            PageHeading = "View Buildings - Search - Sub Menu";
            PageNumber = 8;
            list.Add(new MenuItem("Search Again", 1, 10, PageNumber, PageHeading, 0, false, 0, "SearchBuildings", false, true));
            list.Add(new MenuItem("Remove Building", 2, 20, PageNumber, PageHeading, 0, false, 0, "RemoveBuilding", false, true,"NoBuildings"));
            list.Add(new MenuItem("Edit Building", 3, 30, PageNumber, PageHeading, 0, false, 0, "EditBuilding", false, true,"NoBuildings"));
            list.Add(new MenuItem("Back to the Building List menu", 4, 40, PageNumber, PageHeading, 5, false, 5, "None", true));
            return list;
        }

        public static string? StartingPageHeading { get; }
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
                foreach (MenuItem item in CurrentMenuPage.OrderBy(c => c.Index).Where(p => p.Page.Equals(page.Value) && p.IsHidden == false).ToList())
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
    }
}
