using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;

namespace ArchCorpUtilities.Utilities
{

    public class UniversalUtilities
    {
        public static string? SessionID { get; internal set; }

        //{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}
        public enum MenuDomain
        {
            None,
            Menu,
            Building
            //{E401C6FC-99B7-41B0-A612-8DABFE8734C3}
        }
        //{F8FE36D7-3F08-48BA-9CAB-FBAA102C8149}

        public enum Navigation
        {
            FirstPage,
            LastPage,
            NextPage,
            PreviousPage,
        }

        public static List<T>? ViewWithPagination<T>(string heading, Patina.Patina page, List<T>? modelList, U.Navigation navigation = U.Navigation.FirstPage, int GoToPageNumber = -1)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            List<T>? paginated = GetPaginatedList(modelList, navigation, page, GoToPageNumber);

            if (paginated != null)
                ViewGen(paginated, page, heading);

            return paginated;
        }

        public static void ViewGen<T>(IEnumerable<T> modelList, Patina.Patina page, string heading)
        {
            if (SessionID != null)
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

        public static List<T>? GetPaginatedList<T>(List<T>? items, U.Navigation navigation, Patina.Patina page, int GoToPageNumber = 0)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            List<T>? paginatedList = null;
            if (items?.Count > 0)
            {
                if (GoToPageNumber < 0)
                {
                    switch (navigation)
                    {
                        case U.Navigation.FirstPage:
                            page.GoToFirstPage();
                            break;
                        case U.Navigation.LastPage:
                            page.GoToLastPage();
                            break;
                        case U.Navigation.NextPage:
                            page.GetNextPage();
                            break;
                        case U.Navigation.PreviousPage:
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

    }
}
