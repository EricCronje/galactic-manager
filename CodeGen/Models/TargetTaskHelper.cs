// Generated Code - Version: 23.11.25 - 2024/11/18 21:08:58 - {3a6d9981-e6ad-4c67-848b-61b518dfa8f8}

using ArchCorpUtilities.Utilities;

using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;
using E = EnumLib.EnumLib;

//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}
//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}
//{24D86755-6962-4074-BD9F-73E8FE0A5F68}
//{24D86755-6962-4074-BD9F-73E8FE0A5F68}

namespace ArchCorpUtilities.Models
{
    public static class TargetTaskHelper
    {

        public static string? SessionID { get; internal set; }

        private static bool ApplyHiddenRules(Patina.Patina page)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            bool ResetPage = false;
            if (MH.Menu != null)
            {
                foreach (var item in MH.Menu.Where(p => p.HideRule != "None"))
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
                                case E.MenuDomain.Menu:
                                    if (SessionID != null)
                                        L.Log("Menu-NoData", SessionID, 1);
                                    if (!MMH.IsMenusOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
                                //{2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0}
								//{2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0}
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

        private static void ResetIsHidden()
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            if (MH.Menu != null)
            {
                foreach (var item in MH.Menu.Where(p => p.IsHidden))
                {
                    item.IsHidden = false;
                }
            }
        }

        private static void DefaultTasks(int page, int? simChoice, string? simInput, int? defaultChoice, string[]? simInputValues)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            var MenuItem = MH.Menu?.FirstOrDefault(c => c.Page == page && c.DisplayNumber == defaultChoice && c.IsHidden == false);

            var TargetTask = MenuItem?.TargetTask;
            if (SessionID != null)
            {
                switch (TargetTask)
                {
                    case "Add":
                        switch (MenuItem?.Domain)
                        {                            
                            case E.MenuDomain.Menu: L.Log("Menu-Add", SessionID, 1); MMH.Add(simInputValues); break;
                            //{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
							//{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
                            default: break;
                        }
                        break;

                    case "View":
                        switch (MenuItem?.Domain)
                        {                            
                            case E.MenuDomain.Menu: L.Log("Menu-View", SessionID, 1); MMH.InitialView(); break;
                            //{B03F74F5-9862-4916-9EF1-82DD253A5BC3}
							//{B03F74F5-9862-4916-9EF1-82DD253A5BC3}
                            default: break;
                        }
                        break;

                    case "FirstPage":
                    case "First":
                        switch (MenuItem?.Domain)
                        {                           
                            case E.MenuDomain.Menu:
                                L.Log("Menu-FirstPage", SessionID, 1);
                                MMH.View(E.Navigation.FirstPage);
                                break;
                            //{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}
							//{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}
                              
                            default:
                                break;

                        }
                        break;

                    case "LastPage":
                    case "Last":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-LastPage", SessionID, 1);
                                MMH.View(E.Navigation.LastPage);
                                break;
                            //{39C53717-4163-4B33-B652-4AA3B4D28C5B}
							//{39C53717-4163-4B33-B652-4AA3B4D28C5B}
                            default:
                                break;
                        }
                        break;

                    case "NextPage":
                    case "Next":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-NextPage", SessionID, 1);
                                MMH.View(E.Navigation.NextPage);
                                break;
                            //{BBDE47EF-8937-4545-A019-652A8A306B6E}
							//{BBDE47EF-8937-4545-A019-652A8A306B6E}
                            default:
                                break;
                        }
                        break;

                    case "PreviousPage":
                    case "Previous":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-PreviousPage", SessionID, 1);
                                MMH.View(E.Navigation.PreviousPage);
                                break;
                            //{90D2D480-6963-441E-B2C4-E0EADC878A83}
							//{90D2D480-6963-441E-B2C4-E0EADC878A83}
                            default:
                                break;
                        }
                        break;

                    case "Edit":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-Edit", SessionID, 1);
                                MMH.Edit(simChoice.ToString(), simInput);
                                break;
                            //{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}
							//{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}
                            default:
                                break;
                        }
                        break;

                    case "Remove":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-Remove", SessionID, 1);
                                MMH.Remove(simChoice.ToString());
                                break;
                            //{11002DF2-E6AB-485E-B896-C3ED92706E30}
							//{11002DF2-E6AB-485E-B896-C3ED92706E30}
                            default:
                                break;
                        }
                        break;

                    case "Save":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-Save", SessionID, 1);
                                MMH.Save();
                                break;
                            //{36DE75D7-A730-4F6B-A7C9-4660245BD895}
							//{36DE75D7-A730-4F6B-A7C9-4660245BD895}
                            default:
                                break;
                        }
                        break;

                    case "Load":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-Load", SessionID, 1);
                                MMH.Load();
                                break;
                            //{BD92B12F-6AB8-420C-9A4B-654233721FB7}
							//{BD92B12F-6AB8-420C-9A4B-654233721FB7}
                            default:
                                break;
                        }
                        break;

                    case "Search":
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu:
                                L.Log("Menu-Search", SessionID, 1);
                                MMH.Search(simChoice, simInputValues);
                                break;
                            //{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}
							//{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}
                            default:
                                break;
                        }
                        break;

                    case "Refresh":
                        
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu: L.Log("Menu-Refresh", SessionID, 1); MMH.Refresh(); break;
                            //{B2FED166-7FCF-4163-8507-EB1CC28B6435}
							//{B2FED166-7FCF-4163-8507-EB1CC28B6435}
                            default:
                                break;
                        }
                        break;
                        
					case "Clear":
                        
                        switch (MenuItem?.Domain)
                        {
                            case E.MenuDomain.Menu: L.Log("Menu-Refresh", SessionID, 1); MMH.Refresh(); break;
                            //{F85FF648-A0B3-45FA-9784-8E3F1528B870}
                            //{F85FF648-A0B3-45FA-9784-8E3F1528B870}
                            default:
                                break;
                        }
                        break;                        

                    default:
                        break;
                }

            }
        }

        public static void PerformDefaultTasks(int page, int prevPage, int choice, int? simChoice = null, string? simInput = null, string[]? simInputValues = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            // if simulation use the simChoice
            if (simChoice != null) { choice = simChoice.Value; }

            int? defaultChoice = MH.Menu?.FirstOrDefault(c => c.IsDefaultChoice && c.Page == page)?.DisplayNumber;
            bool isSamePage = page.Equals(prevPage);

            if (isSamePage) { defaultChoice = choice; }
            // For simulation only

            DefaultTasks(page, simChoice, simInput, defaultChoice, simInputValues);
            ResetIsHidden();
            ApplyHiddenRules(page, page != prevPage);

        }

        private static void ApplyHiddenRules(int page, bool doReIndex)
        {
            var CurrentMenu = MH.Menu?.FirstOrDefault(c => c.Page == page)?.Domain;
            // Apply hiding rules per menu domain - one domain per page ...
            switch (CurrentMenu)
            {
                case E.MenuDomain.Menu:
                    ApplyHiddenRules(MMH.MenuPage);
                    break;
                //{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}
				//{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}
                default:
                    break;
            }
        }
    }
}
