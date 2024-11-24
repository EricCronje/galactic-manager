// Generated Code - Version: 23.11.25 - 2024/11/21 18:51:08 - {fdf41348-cd51-48d1-bcf1-1c828e7f032c}

using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using IC = InvalidCharacters.InvalidCharacters;
using System.Text;
using System.Diagnostics;
using E = EnumLib.EnumLib;
using ArchCorpUtilities.Models.Helper;

namespace ArchCorpUtilities.Utilities
{

    public class UniversalUtilities
    {
        public static string? SessionID { get; set; }

        public static List<T>? ViewWithPagination<T>(string heading, Patina.Patina page, List<T>? modelList, E.Navigation navigation = E.Navigation.FirstPage, int GoToPageNumber = -1)
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

        public static List<T>? GetPaginatedList<T>(List<T>? items, E.Navigation navigation, Patina.Patina page, int GoToPageNumber = 0)
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
                        case E.Navigation.FirstPage:
                            page.GoToFirstPage();
                            break;
                        case E.Navigation.LastPage:
                            page.GoToLastPage();
                            break;
                        case E.Navigation.NextPage:
                            page.GetNextPage();
                            break;
                        case E.Navigation.PreviousPage:
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
        
        internal static object GetCurrentDate()
        {
            return CH.IsSimulate ? "<Date>" : DateTime.Now;
        }

        internal static object GetVersion()
        {
            return CH.IsSimulate ? "<Version>" : CodeGenHelper.Version;
        }

        //{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}
        public static string? ValidateInput(string? input)
        {
            if (input == null)
                return null;
            
            IC? InvalidCharacters = null;

            try
            {                
                InvalidCharacters = new IC();
                return InvalidCharacters.Result(input);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                InvalidCharacters?.Dispose();
            }
        }

        //{CAA55BEC-8E9F-42F8-8B7B-F52B625D9708}

        internal static bool RemoveFirstLineFeed(string? context, StringBuilder stringBuilder)
        {
            try
            {
                if (context == null) { return false; }                
                if (context != null && context.Length > 0 && context.StartsWith(Environment.NewLine)) { context = context.AsSpan(2).ToString(); }
                if (context != null && context.Length > 0) { stringBuilder.Append(context); return true; }
                return false;
            }
            catch (Exception err)
            {
                if (SessionID != null)
                    L.Log($"Error in RemoveLineFeed - {err.Message} -- {err.InnerException?.Message}", SessionID, 9);
                return false;
            }            
        }

        public static string ProcessingCodeToAlter(string newCode, string tabs, string codeToAlter, bool addComma = true)
        {
            StringBuilder StringBuilder = new();
            try
            {
                tabs ??= string.Empty;
                List<string> Existing = [];
                codeToAlter = codeToAlter.Replace("\t", "");
                var SplitContext = codeToAlter.Split("\r\n");
                foreach (var item in SplitContext)
                    if (item.Length > 3)
                            Existing.Add(item);

                Existing.Add(newCode);
                if (Existing != null && Existing.Count > 0)
                    for (int i = 0; i < Existing.Count; i++)
                    {
                        if (Existing[i].Trim().Length > 0)
                        {
                            StringBuilder.Append($"{tabs}{Existing[i]}");
                            if (addComma && i != (Existing.Count - 1) && !Existing[i].EndsWith(',')) { StringBuilder.Append(','); }
                            StringBuilder.Append(Environment.NewLine);
                        }
                    }
                var Result = StringBuilder.ToString();
                return Result;
            }
            catch (Exception err)
            {
                L.Log($"Error in ProcessingCodeToAlter - {err.Message} -- {err.InnerException?.Message}");
                return "<Error>";
            }
            finally
            {
                StringBuilder.Clear();
            }
        }

        public static string GetGeneratedCodeHeader()
        {
            return $"// Generated Code - Version: {U.GetVersion()} - {U.GetCurrentDate()} - {{{SessionID}}}\n\r";
        }

        public static bool ClearGeneratedHeaders()
        {

            List<string> HeaderSources = [];

            try
            {
                HeaderSources.Add("\\Models\\TargetTaskHelper.cs");
                HeaderSources.Add("\\Models\\ArchLoader.cs");
                HeaderSources.Add("\\Utilities\\UniversalUtilities.cs");
                HeaderSources.Add("EnumLib.cs");

                foreach (var item in HeaderSources)
                {
                    var Path = $"{CodeGenHelper.WorkingFolder}\\{item}";
                    if (File.Exists(Path))
                    {
                        var Content = File.ReadAllText(Path);
                        var SplitContent = Content.Split("\n\r");
                        StringBuilder stringBuilder = new();
                        var Counter = 0;
                        foreach (string v in SplitContent)
                        {

                            if (v.Length > 0)
                            {
                                if (v.Contains("Generated"))
                                    Counter++;
                                else
                                    break;
                            }
                        }
                        stringBuilder.Append(GetGeneratedCodeHeader());
                        stringBuilder.AppendJoin("\n\r", SplitContent.ToList().GetRange(Counter, SplitContent.Length - Counter)).ToString();
                        var Output = stringBuilder.ToString();
                        stringBuilder.Clear();
                        File.Delete(Path);
                        File.WriteAllText(Path, Output);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }


        }

        public static void ExecuteCMD(string CMDPath, string success = "Deployed the changes to the code.", string fail = "Could not deploy the changes to the code.")
        {
            try
            {
                Process process = new()
                {
                    StartInfo = new ProcessStartInfo(CMDPath)
                };
                process.Start();
                process.WaitForExit();
                process.Dispose();
                CH.Feedback(success);
            }
            catch (Exception)
            {
                CH.Feedback(fail);
            }
        }

        static public T? SelectEntityFromTheList<T>(string[]? simInput, ref string input, string heading, string selectionHeading, IHelper<T>? entityHelper)
        {
            if (entityHelper == null) { return default; }
            T? Entity = default;
            E.Navigation navigation = E.Navigation.FirstPage;
            
            while (Entity == null || !input.Equals("A", StringComparison.CurrentCultureIgnoreCase))
            {
                CH.ClearScreen();
                CH.Feedback(heading);
                entityHelper.ResetEntitiesOnThePage();
                if (entityHelper != null)
                {
                    Entity = entityHelper.ViewAndSelectItem(simInput?[0], selectionHeading, navigation);
                    if (Entity != null) { return Entity; }

                    CH.Feedback("N (Next), P (Previous), L (Last), A (Abort)");
                    input = CH.GetInput(simInput?[0]);
                    switch (input.ToUpper())
                    {
                        case "N":
                            navigation = E.Navigation.NextPage;
                            break;
                        case "P":
                            navigation = E.Navigation.PreviousPage;
                            break;
                        case "F":
                            navigation = E.Navigation.FirstPage;
                            break;
                        case "L":
                            navigation = E.Navigation.LastPage;
                            break;
                        default:
                            break;
                    }

                }                
                else
                    break;
            }

            return default;
        }
        static public bool IsValidGuid(string? guid)
        {
            return !string.IsNullOrWhiteSpace(guid);
        }

    }
}




