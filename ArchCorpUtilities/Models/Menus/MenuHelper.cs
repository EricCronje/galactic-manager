using System.Text;

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
        }

        private static List<MenuItem> MockGetMenuItems()
        {
            string PageHeading = "Building Manager - Main Menu - V1.0.0";
            List<MenuItem> list =
            [
                new MenuItem("List Buildings", 1, 10, 1, PageHeading, 0, false, 5),
                new MenuItem("Add Buildings", 2, 20, 1, PageHeading, 0, false, 2),
                new MenuItem("Remove Buildings", 3, 30, 1, PageHeading, 0, false, 4),
                new MenuItem("Edit Building Names", 4, 40, 1, PageHeading, 0, false, 3),
                new MenuItem("Save Buildings", 5, 50, 1, PageHeading, 0, false, 6),
                new MenuItem("Load Buildings", 6, 60, 1, PageHeading, 0, false, 7),
                new MenuItem("Exit", 7, 70, 1, PageHeading, 0, true),
            ];
            //Add Options
            PageHeading = "Add - Sub Menu";
            list.Add(new MenuItem("Add Building", 1, 10, 2, PageHeading, 0, false, 0, "AddBuilding", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, 2, PageHeading, 1, false, 1, "None", true));

            //View Buildings
            PageHeading = "View - Sub Menu";
            var pageNumber = 5;
            list.Add(new MenuItem("View Building", 1, 10, pageNumber, PageHeading, 0, false, 0, "ViewBuilding", false, true));
            
            list.Add(new MenuItem("Next Page", 2, 20, pageNumber, PageHeading, 0, false, 0, "NextPage", false, true, "LastPage"));
            list.Add(new MenuItem("Previous Page", 3, 30, pageNumber, PageHeading, 0, false, 0, "PreviousPage", false, true, "FirstPage"));
            list.Add(new MenuItem("First Page", 4, 40, pageNumber, PageHeading, 0, false, 0, "FirstPage", false, true, "FirstPage"));
            list.Add(new MenuItem("Last Page", 5, 50, pageNumber, PageHeading, 0, false, 0, "LastPage", false, true, "LastPage"));
            
            list.Add(new MenuItem("Back to Main Menu", 6, 60, pageNumber, PageHeading, 1, false, 1, "None", true));

            //Edit Buildings
            PageHeading = "Edit - Sub Menu";
            pageNumber = 3;
            list.Add(new MenuItem("Edit Building", 1, 10, pageNumber, PageHeading, 0, false, 0, "EditBuilding", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, pageNumber, PageHeading, 1, false, 1, "None", true));

            //Remove Buildings
            PageHeading = "Remove - Sub Menu";
            pageNumber = 4;
            list.Add(new MenuItem("Remove Building", 1, 10, pageNumber, PageHeading, 0, false, 0, "RemoveBuilding", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, pageNumber, PageHeading, 1, false, 1, "None", true));

            //Save to a file
            PageHeading = "Save - Sub Menu";
            pageNumber = 6;
            list.Add(new MenuItem("Save buildings to a file", 1, 10, pageNumber, PageHeading, 0, false, 0, "SaveBuildings", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, pageNumber, PageHeading, 1, false, 1, "None", true));

            //Load from a file
            PageHeading = "Load - Sub Menu";
            pageNumber = 7;
            list.Add(new MenuItem("Load buildings from a file", 1, 10, pageNumber, PageHeading, 0, false, 0, "LoadBuildings", false, true));
            list.Add(new MenuItem("Back to Main Menu", 2, 20, pageNumber, PageHeading, 1, false, 1, "None", true));

            return list;
        }

        public static string? StartingPageHeading { get; }
        public static string? Line { get; }
        public static string? MenuItems { get; }

        public static string ShowHeading<T>(T heading)
        {
            StringBuilder sb = new();
            sb.AppendLine(Line);
            if (heading != null)
                sb.AppendLine(heading.ToString());

            sb.Append(Line);

            return sb.ToString();
        }

        public static string ShowMenu(string? menuHeading, int? page = 0)
        {
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
    }
}
