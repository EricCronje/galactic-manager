// Generated Code - Version: 20.11.25 - 2024/11/14 03:19:10 - {a0d47dc6-67e7-402a-af37-33d83ed34429}

using ArchCorpUtilities.Utilities;

using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;

using ArchCorpUtilities.Models.Buildings;
//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}
using A = ArchCorpUtilities.Models.ArchLoader;
//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}
//{24D86755-6962-4074-BD9F-73E8FE0A5F68}
using ArchCorpUtilities.GeneratedModels.BeaconsModel;
using ArchCorpUtilities.GeneratedModels.BuildingsModel;
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
                                case U.MenuDomain.Menu:
                                    if (SessionID != null)
                                        L.Log("Menu-NoData", SessionID, 1);
                                    if (!MMH.IsMenusOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
                                //{2D8B5F74-6CC5-4C0F-AB99-8E596C463DA0}
                                case U.MenuDomain.Beacons:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BeaconsHelper != null && !A.BeaconsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Buildings:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BuildingsHelper != null && !A.BuildingsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
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
                            case U.MenuDomain.Menu: L.Log("Menu-Add", SessionID, 1); MMH.Add(simInputValues); break;
                            //{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
                            case U.MenuDomain.Beacons: L.Log("Beacons-Add", SessionID, 1); A.BeaconsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Buildings: L.Log("Buildings-Add", SessionID, 1); A.BuildingsHelper?.Add(simChoice, simInputValues); break;
							//{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
                            default: break;
                        }
                        break;

                    case "View":
                        switch (MenuItem?.Domain)
                        {                            
                            case U.MenuDomain.Menu: L.Log("Menu-View", SessionID, 1); MMH.InitialView(); break;
                            //{B03F74F5-9862-4916-9EF1-82DD253A5BC3}
                            case U.MenuDomain.Beacons: L.Log("Beacons-View", SessionID, 1);
								if (A.BeaconsHelper != null && A.BeaconsHelper.Items != null) { A.BeaconsHelper?.Refresh(A.BeaconsHelper.Items); }
								break;
							case U.MenuDomain.Buildings: L.Log("Buildings-View", SessionID, 1);
								if (A.BuildingsHelper != null && A.BuildingsHelper.Items != null) { A.BuildingsHelper?.Refresh(A.BuildingsHelper.Items); }
								break;
							//{B03F74F5-9862-4916-9EF1-82DD253A5BC3}
                            default: break;
                        }
                        break;

                    case "FirstPage":
                    case "First":
                        switch (MenuItem?.Domain)
                        {                           
                            case U.MenuDomain.Menu:
                                L.Log("Menu-FirstPage", SessionID, 1);
                                MMH.View(U.Navigation.FirstPage);
                                break;
                            //{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons- FirstPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings- FirstPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.FirstPage);
								break;
							//{ADECB8B3-1779-4107-9DF5-9E250E31AFDD}
                              
                            default:
                                break;

                        }
                        break;

                    case "LastPage":
                    case "Last":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-LastPage", SessionID, 1);
                                MMH.View(U.Navigation.LastPage);
                                break;
                            //{39C53717-4163-4B33-B652-4AA3B4D28C5B}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons- LastPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings- LastPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.LastPage);
								break;
							//{39C53717-4163-4B33-B652-4AA3B4D28C5B}
                            default:
                                break;
                        }
                        break;

                    case "NextPage":
                    case "Next":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-NextPage", SessionID, 1);
                                MMH.View(U.Navigation.NextPage);
                                break;
                            //{BBDE47EF-8937-4545-A019-652A8A306B6E}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-NextPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-NextPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.NextPage);
								break;
							//{BBDE47EF-8937-4545-A019-652A8A306B6E}
                            default:
                                break;
                        }
                        break;

                    case "PreviousPage":
                    case "Previous":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-PreviousPage", SessionID, 1);
                                MMH.View(U.Navigation.PreviousPage);
                                break;
                            //{90D2D480-6963-441E-B2C4-E0EADC878A83}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-PreviousPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-PreviousPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.PreviousPage);
								break;
							//{90D2D480-6963-441E-B2C4-E0EADC878A83}
                            default:
                                break;
                        }
                        break;

                    case "Edit":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-Edit", SessionID, 1);
                                MMH.Edit(simChoice.ToString(), simInput);
                                break;
                            //{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-Edit", SessionID, 1);
								A.BeaconsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Edit", SessionID, 1);
								A.BuildingsHelper?.Edit(simChoice, simInputValues);
								break;
							//{74A75AB1-1AB2-46C7-B63F-39F52AF0049A}
                            default:
                                break;
                        }
                        break;

                    case "Remove":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-Remove", SessionID, 1);
                                MMH.Remove(simChoice.ToString());
                                break;
                            //{11002DF2-E6AB-485E-B896-C3ED92706E30}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-Remove", SessionID, 1);
								A.BeaconsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Remove", SessionID, 1);
								A.BuildingsHelper?.Remove(simChoice, simInputValues);
								break;
							//{11002DF2-E6AB-485E-B896-C3ED92706E30}
                            default:
                                break;
                        }
                        break;

                    case "Save":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-Save", SessionID, 1);
                                MMH.Save();
                                break;
                            //{36DE75D7-A730-4F6B-A7C9-4660245BD895}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-Save", SessionID, 1);
								A.BeaconsHelper?.Save();
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Save", SessionID, 1);
								A.BuildingsHelper?.Save();
								break;
							//{36DE75D7-A730-4F6B-A7C9-4660245BD895}
                            default:
                                break;
                        }
                        break;

                    case "Load":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-Load", SessionID, 1);
                                MMH.Load();
                                break;
                            //{BD92B12F-6AB8-420C-9A4B-654233721FB7}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-Load", SessionID, 1);
								A.BeaconsHelper?.Load();
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Load", SessionID, 1);
								A.BuildingsHelper?.Load();
								break;
							//{BD92B12F-6AB8-420C-9A4B-654233721FB7}
                            default:
                                break;
                        }
                        break;

                    case "Search":
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu:
                                L.Log("Menu-Search", SessionID, 1);
                                MMH.Search(simChoice, simInputValues);
                                break;
                            //{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}
                            case U.MenuDomain.Beacons:
								L.Log("Beacons-Search", SessionID, 1);
                                A.BeaconsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Search", SessionID, 1);
								A.BuildingsHelper?.Search(simChoice, simInputValues);
                                break;
							//{F745E72F-A908-4AB1-AF8B-E3FDE13BF46E}
                            default:
                                break;
                        }
                        break;

                    case "Refresh":
                        
                        switch (MenuItem?.Domain)
                        {
                            case U.MenuDomain.Menu: L.Log("Menu-Refresh", SessionID, 1); MMH.Refresh(); break;
                            //{B2FED166-7FCF-4163-8507-EB1CC28B6435}
                            case U.MenuDomain.Beacons: L.Log("Beacons-View", SessionID, 1);
								if (A.BeaconsHelper != null && A.BeaconsHelper.Items != null && A.BeaconsHelper.Items.Count > 0)
								{A.BeaconsHelper.Refresh(A.BeaconsHelper.Items);}
							break;
							case U.MenuDomain.Buildings: L.Log("Buildings-View", SessionID, 1);
								if (A.BuildingsHelper != null && A.BuildingsHelper.Items != null && A.BuildingsHelper.Items.Count > 0)
								{A.BuildingsHelper.Refresh(A.BuildingsHelper.Items);}
							break;
							//{B2FED166-7FCF-4163-8507-EB1CC28B6435}
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
                case U.MenuDomain.Menu:
                    ApplyHiddenRules(MMH.MenuPage);
                    break;
                //{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}
                case U.MenuDomain.Beacons:
					if (A.BeaconsHelper != null && ApplyHiddenRules(A.BeaconsHelper.Page) && doReIndex)
					{A.BeaconsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Buildings:
					if (A.BuildingsHelper != null && ApplyHiddenRules(A.BuildingsHelper.Page) && doReIndex)
					{A.BuildingsHelper.ReIndexDisplayId();}
					break;
				//{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}
                default:
                    break;
            }
        }
    }
}
