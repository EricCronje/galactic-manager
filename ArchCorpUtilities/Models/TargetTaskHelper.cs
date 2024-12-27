// Generated Code - Version: 23.11.25 - 2024/12/25 13:29:24 - {bf9f5b57-365e-45ad-b5ab-ad7a7ee3c33e}

using ArchCorpUtilities.Utilities;

using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;

using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;
using E = EnumLib.EnumLib;

//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}
using A = ArchCorpUtilities.Models.ArchLoader;
//{EA6AE6CA-7E51-43DE-95F9-FF66E27AE130}

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
								case E.MenuDomain.Planets:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.PlanetsHelper != null && !A.PlanetsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.Sites:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.SitesHelper != null && !A.SitesHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.Sections:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.SectionsHelper != null && !A.SectionsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.Buildings:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BuildingsHelper != null && !A.BuildingsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.LinkSiteToPlanet:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkSiteToPlanetHelper != null && !A.LinkSiteToPlanetHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.LinkSectionToSite:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkSectionToSiteHelper != null && !A.LinkSectionToSiteHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.LinkBuildingToSection:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkBuildingToSectionHelper != null && !A.LinkBuildingToSectionHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.BuildingTypes:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BuildingTypesHelper != null && !A.BuildingTypesHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.LinkBuildingToBuildingsTypes:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkBuildingToBuildingsTypesHelper != null && !A.LinkBuildingToBuildingsTypesHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.Ships:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.ShipsHelper != null && !A.ShipsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.ShipTypes:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.ShipTypesHelper != null && !A.ShipTypesHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.LinkShipToShipType:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkShipToShipTypeHelper != null && !A.LinkShipToShipTypeHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.Fleets:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.FleetsHelper != null && !A.FleetsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.LinkShipToFleet:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkShipToFleetHelper != null && !A.LinkShipToFleetHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.Beacons:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BeaconsHelper != null && !A.BeaconsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case E.MenuDomain.BuildingHierarchy:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BuildingHierarchyHelper != null && !A.BuildingHierarchyHelper.IsItemsOnThePage())
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
                            case E.MenuDomain.Menu: L.Log("Menu-Add", SessionID, 1); MMH.Add(simInputValues); break;
                            //{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
							case E.MenuDomain.Planets: L.Log("Planets-Add", SessionID, 1); A.PlanetsHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.Sites: L.Log("Sites-Add", SessionID, 1); A.SitesHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.Sections: L.Log("Sections-Add", SessionID, 1); A.SectionsHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.Buildings: L.Log("Buildings-Add", SessionID, 1); A.BuildingsHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.LinkSiteToPlanet: L.Log("LinkSiteToPlanet-Add", SessionID, 1); A.LinkSiteToPlanetHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.LinkSectionToSite: L.Log("LinkSectionToSite-Add", SessionID, 1); A.LinkSectionToSiteHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.LinkBuildingToSection: L.Log("LinkBuildingToSection-Add", SessionID, 1); A.LinkBuildingToSectionHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.BuildingTypes: L.Log("BuildingTypes-Add", SessionID, 1); A.BuildingTypesHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes: L.Log("LinkBuildingToBuildingsTypes-Add", SessionID, 1); A.LinkBuildingToBuildingsTypesHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.Ships: L.Log("Ships-Add", SessionID, 1); A.ShipsHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.ShipTypes: L.Log("ShipTypes-Add", SessionID, 1); A.ShipTypesHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.LinkShipToShipType: L.Log("LinkShipToShipType-Add", SessionID, 1); A.LinkShipToShipTypeHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.Fleets: L.Log("Fleets-Add", SessionID, 1); A.FleetsHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.LinkShipToFleet: L.Log("LinkShipToFleet-Add", SessionID, 1); A.LinkShipToFleetHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.Beacons: L.Log("Beacons-Add", SessionID, 1); A.BeaconsHelper?.Add(simChoice, simInputValues); break;
							case E.MenuDomain.BuildingHierarchy: L.Log("BuildingHierarchy-Add", SessionID, 1); A.BuildingHierarchyHelper?.Add(simChoice, simInputValues); break;
							//{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
                            default: break;
                        }
                        break;

                    case "View":
                        switch (MenuItem?.Domain)
                        {                            
                            case E.MenuDomain.Menu: L.Log("Menu-View", SessionID, 1); MMH.InitialView(); break;
                            //{B03F74F5-9862-4916-9EF1-82DD253A5BC3}
							case E.MenuDomain.Planets: L.Log("Planets-View", SessionID, 1); A.PlanetsHelper?.Refresh(); break;
							case E.MenuDomain.Sites: L.Log("Sites-View", SessionID, 1); A.SitesHelper?.Refresh(); break;
							case E.MenuDomain.Sections: L.Log("Sections-View", SessionID, 1); A.SectionsHelper?.Refresh(); break;
							case E.MenuDomain.Buildings: L.Log("Buildings-View", SessionID, 1); A.BuildingsHelper?.Refresh(); break;
							case E.MenuDomain.LinkSiteToPlanet: L.Log("LinkSiteToPlanet-View", SessionID, 1); A.LinkSiteToPlanetHelper?.Refresh(); break;
							case E.MenuDomain.LinkSectionToSite: L.Log("LinkSectionToSite-View", SessionID, 1); A.LinkSectionToSiteHelper?.Refresh(); break;
							case E.MenuDomain.LinkBuildingToSection: L.Log("LinkBuildingToSection-View", SessionID, 1); A.LinkBuildingToSectionHelper?.Refresh(); break;
							case E.MenuDomain.BuildingTypes: L.Log("BuildingTypes-View", SessionID, 1); A.BuildingTypesHelper?.Refresh(); break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes: L.Log("LinkBuildingToBuildingsTypes-View", SessionID, 1); A.LinkBuildingToBuildingsTypesHelper?.Refresh(); break;
							case E.MenuDomain.Ships: L.Log("Ships-View", SessionID, 1); A.ShipsHelper?.Refresh(); break;
							case E.MenuDomain.ShipTypes: L.Log("ShipTypes-View", SessionID, 1); A.ShipTypesHelper?.Refresh(); break;
							case E.MenuDomain.LinkShipToShipType: L.Log("LinkShipToShipType-View", SessionID, 1); A.LinkShipToShipTypeHelper?.Refresh(); break;
							case E.MenuDomain.Fleets: L.Log("Fleets-View", SessionID, 1); A.FleetsHelper?.Refresh(); break;
							case E.MenuDomain.LinkShipToFleet: L.Log("LinkShipToFleet-View", SessionID, 1); A.LinkShipToFleetHelper?.Refresh(); break;
							case E.MenuDomain.Beacons: L.Log("Beacons-View", SessionID, 1); A.BeaconsHelper?.Refresh(); break;
							case E.MenuDomain.BuildingHierarchy: L.Log("BuildingHierarchy-View", SessionID, 1); A.BuildingHierarchyHelper?.Refresh(); break;
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
							case E.MenuDomain.Planets:
								L.Log("Planets- FirstPage", SessionID, 1);
								A.PlanetsHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites- FirstPage", SessionID, 1);
								A.SitesHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections- FirstPage", SessionID, 1);
								A.SectionsHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings- FirstPage", SessionID, 1);
								A.BuildingsHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet- FirstPage", SessionID, 1);
								A.LinkSiteToPlanetHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite- FirstPage", SessionID, 1);
								A.LinkSectionToSiteHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection- FirstPage", SessionID, 1);
								A.LinkBuildingToSectionHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes- FirstPage", SessionID, 1);
								A.BuildingTypesHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes- FirstPage", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships- FirstPage", SessionID, 1);
								A.ShipsHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes- FirstPage", SessionID, 1);
								A.ShipTypesHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType- FirstPage", SessionID, 1);
								A.LinkShipToShipTypeHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets- FirstPage", SessionID, 1);
								A.FleetsHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet- FirstPage", SessionID, 1);
								A.LinkShipToFleetHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons- FirstPage", SessionID, 1);
								A.BeaconsHelper?.View(E.Navigation.FirstPage);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy- FirstPage", SessionID, 1);
								A.BuildingHierarchyHelper?.View(E.Navigation.FirstPage);
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
                            case E.MenuDomain.Menu:
                                L.Log("Menu-LastPage", SessionID, 1);
                                MMH.View(E.Navigation.LastPage);
                                break;
                            //{39C53717-4163-4B33-B652-4AA3B4D28C5B}
							case E.MenuDomain.Planets:
								L.Log("Planets- LastPage", SessionID, 1);
								A.PlanetsHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites- LastPage", SessionID, 1);
								A.SitesHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections- LastPage", SessionID, 1);
								A.SectionsHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings- LastPage", SessionID, 1);
								A.BuildingsHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet- LastPage", SessionID, 1);
								A.LinkSiteToPlanetHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite- LastPage", SessionID, 1);
								A.LinkSectionToSiteHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection- LastPage", SessionID, 1);
								A.LinkBuildingToSectionHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes- LastPage", SessionID, 1);
								A.BuildingTypesHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes- LastPage", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships- LastPage", SessionID, 1);
								A.ShipsHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes- LastPage", SessionID, 1);
								A.ShipTypesHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType- LastPage", SessionID, 1);
								A.LinkShipToShipTypeHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets- LastPage", SessionID, 1);
								A.FleetsHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet- LastPage", SessionID, 1);
								A.LinkShipToFleetHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons- LastPage", SessionID, 1);
								A.BeaconsHelper?.View(E.Navigation.LastPage);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy- LastPage", SessionID, 1);
								A.BuildingHierarchyHelper?.View(E.Navigation.LastPage);
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
                            case E.MenuDomain.Menu:
                                L.Log("Menu-NextPage", SessionID, 1);
                                MMH.View(E.Navigation.NextPage);
                                break;
                            //{BBDE47EF-8937-4545-A019-652A8A306B6E}
							case E.MenuDomain.Planets:
								L.Log("Planets-NextPage", SessionID, 1);
								A.PlanetsHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-NextPage", SessionID, 1);
								A.SitesHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-NextPage", SessionID, 1);
								A.SectionsHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-NextPage", SessionID, 1);
								A.BuildingsHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-NextPage", SessionID, 1);
								A.LinkSiteToPlanetHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-NextPage", SessionID, 1);
								A.LinkSectionToSiteHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-NextPage", SessionID, 1);
								A.LinkBuildingToSectionHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-NextPage", SessionID, 1);
								A.BuildingTypesHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-NextPage", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-NextPage", SessionID, 1);
								A.ShipsHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-NextPage", SessionID, 1);
								A.ShipTypesHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-NextPage", SessionID, 1);
								A.LinkShipToShipTypeHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-NextPage", SessionID, 1);
								A.FleetsHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-NextPage", SessionID, 1);
								A.LinkShipToFleetHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-NextPage", SessionID, 1);
								A.BeaconsHelper?.View(E.Navigation.NextPage);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-NextPage", SessionID, 1);
								A.BuildingHierarchyHelper?.View(E.Navigation.NextPage);
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
                            case E.MenuDomain.Menu:
                                L.Log("Menu-PreviousPage", SessionID, 1);
                                MMH.View(E.Navigation.PreviousPage);
                                break;
                            //{90D2D480-6963-441E-B2C4-E0EADC878A83}
							case E.MenuDomain.Planets:
								L.Log("Planets-PreviousPage", SessionID, 1);
								A.PlanetsHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-PreviousPage", SessionID, 1);
								A.SitesHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-PreviousPage", SessionID, 1);
								A.SectionsHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-PreviousPage", SessionID, 1);
								A.BuildingsHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-PreviousPage", SessionID, 1);
								A.LinkSiteToPlanetHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-PreviousPage", SessionID, 1);
								A.LinkSectionToSiteHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-PreviousPage", SessionID, 1);
								A.LinkBuildingToSectionHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-PreviousPage", SessionID, 1);
								A.BuildingTypesHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-PreviousPage", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-PreviousPage", SessionID, 1);
								A.ShipsHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-PreviousPage", SessionID, 1);
								A.ShipTypesHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-PreviousPage", SessionID, 1);
								A.LinkShipToShipTypeHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-PreviousPage", SessionID, 1);
								A.FleetsHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-PreviousPage", SessionID, 1);
								A.LinkShipToFleetHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-PreviousPage", SessionID, 1);
								A.BeaconsHelper?.View(E.Navigation.PreviousPage);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-PreviousPage", SessionID, 1);
								A.BuildingHierarchyHelper?.View(E.Navigation.PreviousPage);
								break;
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
							case E.MenuDomain.Planets:
								L.Log("Planets-Edit", SessionID, 1);
								A.PlanetsHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-Edit", SessionID, 1);
								A.SitesHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-Edit", SessionID, 1);
								A.SectionsHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-Edit", SessionID, 1);
								A.BuildingsHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-Edit", SessionID, 1);
								A.LinkSiteToPlanetHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-Edit", SessionID, 1);
								A.LinkSectionToSiteHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-Edit", SessionID, 1);
								A.LinkBuildingToSectionHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-Edit", SessionID, 1);
								A.BuildingTypesHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-Edit", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-Edit", SessionID, 1);
								A.ShipsHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-Edit", SessionID, 1);
								A.ShipTypesHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-Edit", SessionID, 1);
								A.LinkShipToShipTypeHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-Edit", SessionID, 1);
								A.FleetsHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-Edit", SessionID, 1);
								A.LinkShipToFleetHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-Edit", SessionID, 1);
								A.BeaconsHelper?.Edit(simChoice, simInputValues);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-Edit", SessionID, 1);
								A.BuildingHierarchyHelper?.Edit(simChoice, simInputValues);
								break;
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
							case E.MenuDomain.Planets:
								L.Log("Planets-Remove", SessionID, 1);
								A.PlanetsHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-Remove", SessionID, 1);
								A.SitesHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-Remove", SessionID, 1);
								A.SectionsHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-Remove", SessionID, 1);
								A.BuildingsHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-Remove", SessionID, 1);
								A.LinkSiteToPlanetHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-Remove", SessionID, 1);
								A.LinkSectionToSiteHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-Remove", SessionID, 1);
								A.LinkBuildingToSectionHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-Remove", SessionID, 1);
								A.BuildingTypesHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-Remove", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-Remove", SessionID, 1);
								A.ShipsHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-Remove", SessionID, 1);
								A.ShipTypesHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-Remove", SessionID, 1);
								A.LinkShipToShipTypeHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-Remove", SessionID, 1);
								A.FleetsHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-Remove", SessionID, 1);
								A.LinkShipToFleetHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-Remove", SessionID, 1);
								A.BeaconsHelper?.Remove(simChoice, simInputValues);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-Remove", SessionID, 1);
								A.BuildingHierarchyHelper?.Remove(simChoice, simInputValues);
								break;
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
							case E.MenuDomain.Planets:
								L.Log("Planets-Save", SessionID, 1);
								A.PlanetsHelper?.Save();
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-Save", SessionID, 1);
								A.SitesHelper?.Save();
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-Save", SessionID, 1);
								A.SectionsHelper?.Save();
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-Save", SessionID, 1);
								A.BuildingsHelper?.Save();
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-Save", SessionID, 1);
								A.LinkSiteToPlanetHelper?.Save();
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-Save", SessionID, 1);
								A.LinkSectionToSiteHelper?.Save();
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-Save", SessionID, 1);
								A.LinkBuildingToSectionHelper?.Save();
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-Save", SessionID, 1);
								A.BuildingTypesHelper?.Save();
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-Save", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.Save();
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-Save", SessionID, 1);
								A.ShipsHelper?.Save();
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-Save", SessionID, 1);
								A.ShipTypesHelper?.Save();
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-Save", SessionID, 1);
								A.LinkShipToShipTypeHelper?.Save();
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-Save", SessionID, 1);
								A.FleetsHelper?.Save();
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-Save", SessionID, 1);
								A.LinkShipToFleetHelper?.Save();
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-Save", SessionID, 1);
								A.BeaconsHelper?.Save();
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-Save", SessionID, 1);
								A.BuildingHierarchyHelper?.Save();
								break;
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
							case E.MenuDomain.Planets:
								L.Log("Planets-Load", SessionID, 1);
								A.PlanetsHelper?.Load();
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-Load", SessionID, 1);
								A.SitesHelper?.Load();
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-Load", SessionID, 1);
								A.SectionsHelper?.Load();
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-Load", SessionID, 1);
								A.BuildingsHelper?.Load();
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-Load", SessionID, 1);
								A.LinkSiteToPlanetHelper?.Load();
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-Load", SessionID, 1);
								A.LinkSectionToSiteHelper?.Load();
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-Load", SessionID, 1);
								A.LinkBuildingToSectionHelper?.Load();
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-Load", SessionID, 1);
								A.BuildingTypesHelper?.Load();
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-Load", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.Load();
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-Load", SessionID, 1);
								A.ShipsHelper?.Load();
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-Load", SessionID, 1);
								A.ShipTypesHelper?.Load();
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-Load", SessionID, 1);
								A.LinkShipToShipTypeHelper?.Load();
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-Load", SessionID, 1);
								A.FleetsHelper?.Load();
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-Load", SessionID, 1);
								A.LinkShipToFleetHelper?.Load();
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-Load", SessionID, 1);
								A.BeaconsHelper?.Load();
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-Load", SessionID, 1);
								A.BuildingHierarchyHelper?.Load();
								break;
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
							case E.MenuDomain.Planets:
								L.Log("Planets-Search", SessionID, 1);
								A.PlanetsHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.Sites:
								L.Log("Sites-Search", SessionID, 1);
								A.SitesHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.Sections:
								L.Log("Sections-Search", SessionID, 1);
								A.SectionsHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.Buildings:
								L.Log("Buildings-Search", SessionID, 1);
								A.BuildingsHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkSiteToPlanet:
								L.Log("LinkSiteToPlanet-Search", SessionID, 1);
								A.LinkSiteToPlanetHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkSectionToSite:
								L.Log("LinkSectionToSite-Search", SessionID, 1);
								A.LinkSectionToSiteHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkBuildingToSection:
								L.Log("LinkBuildingToSection-Search", SessionID, 1);
								A.LinkBuildingToSectionHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.BuildingTypes:
								L.Log("BuildingTypes-Search", SessionID, 1);
								A.BuildingTypesHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes:
								L.Log("LinkBuildingToBuildingsTypes-Search", SessionID, 1);
								A.LinkBuildingToBuildingsTypesHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.Ships:
								L.Log("Ships-Search", SessionID, 1);
								A.ShipsHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.ShipTypes:
								L.Log("ShipTypes-Search", SessionID, 1);
								A.ShipTypesHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkShipToShipType:
								L.Log("LinkShipToShipType-Search", SessionID, 1);
								A.LinkShipToShipTypeHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.Fleets:
								L.Log("Fleets-Search", SessionID, 1);
								A.FleetsHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.LinkShipToFleet:
								L.Log("LinkShipToFleet-Search", SessionID, 1);
								A.LinkShipToFleetHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.Beacons:
								L.Log("Beacons-Search", SessionID, 1);
								A.BeaconsHelper?.Search(simChoice, simInputValues);
								break;
							case E.MenuDomain.BuildingHierarchy:
								L.Log("BuildingHierarchy-Search", SessionID, 1);
								A.BuildingHierarchyHelper?.Search(simChoice, simInputValues);
								break;
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
							case E.MenuDomain.Planets: L.Log("Planets-View", SessionID, 1); A.PlanetsHelper?.Refresh(); break;
							case E.MenuDomain.Sites: L.Log("Sites-View", SessionID, 1); A.SitesHelper?.Refresh(); break;
							case E.MenuDomain.Sections: L.Log("Sections-View", SessionID, 1); A.SectionsHelper?.Refresh(); break;
							case E.MenuDomain.Buildings: L.Log("Buildings-View", SessionID, 1); A.BuildingsHelper?.Refresh(); break;
							case E.MenuDomain.LinkSiteToPlanet: L.Log("LinkSiteToPlanet-View", SessionID, 1); A.LinkSiteToPlanetHelper?.Refresh(); break;
							case E.MenuDomain.LinkSectionToSite: L.Log("LinkSectionToSite-View", SessionID, 1); A.LinkSectionToSiteHelper?.Refresh(); break;
							case E.MenuDomain.LinkBuildingToSection: L.Log("LinkBuildingToSection-View", SessionID, 1); A.LinkBuildingToSectionHelper?.Refresh(); break;
							case E.MenuDomain.BuildingTypes: L.Log("BuildingTypes-View", SessionID, 1); A.BuildingTypesHelper?.Refresh(); break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes: L.Log("LinkBuildingToBuildingsTypes-View", SessionID, 1); A.LinkBuildingToBuildingsTypesHelper?.Refresh(); break;
							case E.MenuDomain.Ships: L.Log("Ships-View", SessionID, 1); A.ShipsHelper?.Refresh(); break;
							case E.MenuDomain.ShipTypes: L.Log("ShipTypes-View", SessionID, 1); A.ShipTypesHelper?.Refresh(); break;
							case E.MenuDomain.LinkShipToShipType: L.Log("LinkShipToShipType-View", SessionID, 1); A.LinkShipToShipTypeHelper?.Refresh(); break;
							case E.MenuDomain.Fleets: L.Log("Fleets-View", SessionID, 1); A.FleetsHelper?.Refresh(); break;
							case E.MenuDomain.LinkShipToFleet: L.Log("LinkShipToFleet-View", SessionID, 1); A.LinkShipToFleetHelper?.Refresh(); break;
							case E.MenuDomain.Beacons: L.Log("Beacons-View", SessionID, 1); A.BeaconsHelper?.Refresh(); break;
							case E.MenuDomain.BuildingHierarchy: L.Log("BuildingHierarchy-View", SessionID, 1); A.BuildingHierarchyHelper?.Refresh(); break;
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
                            case E.MenuDomain.Planets: L.Log("Planets-Clear", SessionID, 1); A.PlanetsHelper?.Clear(); break;
							case E.MenuDomain.Sites: L.Log("Sites-Clear", SessionID, 1); A.SitesHelper?.Clear(); break;
							case E.MenuDomain.Sections: L.Log("Sections-Clear", SessionID, 1); A.SectionsHelper?.Clear(); break;
							case E.MenuDomain.Buildings: L.Log("Buildings-Clear", SessionID, 1); A.BuildingsHelper?.Clear(); break;
							case E.MenuDomain.LinkSiteToPlanet: L.Log("LinkSiteToPlanet-Clear", SessionID, 1); A.LinkSiteToPlanetHelper?.Clear(); break;
							case E.MenuDomain.LinkSectionToSite: L.Log("LinkSectionToSite-Clear", SessionID, 1); A.LinkSectionToSiteHelper?.Clear(); break;
							case E.MenuDomain.LinkBuildingToSection: L.Log("LinkBuildingToSection-Clear", SessionID, 1); A.LinkBuildingToSectionHelper?.Clear(); break;
							case E.MenuDomain.BuildingTypes: L.Log("BuildingTypes-Clear", SessionID, 1); A.BuildingTypesHelper?.Clear(); break;
							case E.MenuDomain.LinkBuildingToBuildingsTypes: L.Log("LinkBuildingToBuildingsTypes-Clear", SessionID, 1); A.LinkBuildingToBuildingsTypesHelper?.Clear(); break;
							case E.MenuDomain.Ships: L.Log("Ships-Clear", SessionID, 1); A.ShipsHelper?.Clear(); break;
							case E.MenuDomain.ShipTypes: L.Log("ShipTypes-Clear", SessionID, 1); A.ShipTypesHelper?.Clear(); break;
							case E.MenuDomain.LinkShipToShipType: L.Log("LinkShipToShipType-Clear", SessionID, 1); A.LinkShipToShipTypeHelper?.Clear(); break;
							case E.MenuDomain.Fleets: L.Log("Fleets-Clear", SessionID, 1); A.FleetsHelper?.Clear(); break;
							case E.MenuDomain.LinkShipToFleet: L.Log("LinkShipToFleet-Clear", SessionID, 1); A.LinkShipToFleetHelper?.Clear(); break;
							case E.MenuDomain.Beacons: L.Log("Beacons-Clear", SessionID, 1); A.BeaconsHelper?.Clear(); break;
							case E.MenuDomain.BuildingHierarchy: L.Log("BuildingHierarchy-Clear", SessionID, 1); A.BuildingHierarchyHelper?.Clear(); break;
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
				case E.MenuDomain.Planets:
					if (A.PlanetsHelper != null && A.PlanetsHelper.Page != null && ApplyHiddenRules(A.PlanetsHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.PlanetsHelper.Repository);}
					break;
				case E.MenuDomain.Sites:
					if (A.SitesHelper != null && A.SitesHelper.Page != null && ApplyHiddenRules(A.SitesHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.SitesHelper.Repository);}
					break;
				case E.MenuDomain.Sections:
					if (A.SectionsHelper != null && A.SectionsHelper.Page != null && ApplyHiddenRules(A.SectionsHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.SectionsHelper.Repository);}
					break;
				case E.MenuDomain.Buildings:
					if (A.BuildingsHelper != null && A.BuildingsHelper.Page != null && ApplyHiddenRules(A.BuildingsHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.BuildingsHelper.Repository);}
					break;
				case E.MenuDomain.LinkSiteToPlanet:
					if (A.LinkSiteToPlanetHelper != null && A.LinkSiteToPlanetHelper.Page != null && ApplyHiddenRules(A.LinkSiteToPlanetHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.LinkSiteToPlanetHelper.Repository);}
					break;
				case E.MenuDomain.LinkSectionToSite:
					if (A.LinkSectionToSiteHelper != null && A.LinkSectionToSiteHelper.Page != null && ApplyHiddenRules(A.LinkSectionToSiteHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.LinkSectionToSiteHelper.Repository);}
					break;
				case E.MenuDomain.LinkBuildingToSection:
					if (A.LinkBuildingToSectionHelper != null && A.LinkBuildingToSectionHelper.Page != null && ApplyHiddenRules(A.LinkBuildingToSectionHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.LinkBuildingToSectionHelper.Repository);}
					break;
				case E.MenuDomain.BuildingTypes:
					if (A.BuildingTypesHelper != null && A.BuildingTypesHelper.Page != null && ApplyHiddenRules(A.BuildingTypesHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.BuildingTypesHelper.Repository);}
					break;
				case E.MenuDomain.LinkBuildingToBuildingsTypes:
					if (A.LinkBuildingToBuildingsTypesHelper != null && A.LinkBuildingToBuildingsTypesHelper.Page != null && ApplyHiddenRules(A.LinkBuildingToBuildingsTypesHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.LinkBuildingToBuildingsTypesHelper.Repository);}
					break;
				case E.MenuDomain.Ships:
					if (A.ShipsHelper != null && A.ShipsHelper.Page != null && ApplyHiddenRules(A.ShipsHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.ShipsHelper.Repository);}
					break;
				case E.MenuDomain.ShipTypes:
					if (A.ShipTypesHelper != null && A.ShipTypesHelper.Page != null && ApplyHiddenRules(A.ShipTypesHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.ShipTypesHelper.Repository);}
					break;
				case E.MenuDomain.LinkShipToShipType:
					if (A.LinkShipToShipTypeHelper != null && A.LinkShipToShipTypeHelper.Page != null && ApplyHiddenRules(A.LinkShipToShipTypeHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.LinkShipToShipTypeHelper.Repository);}
					break;
				case E.MenuDomain.Fleets:
					if (A.FleetsHelper != null && A.FleetsHelper.Page != null && ApplyHiddenRules(A.FleetsHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.FleetsHelper.Repository);}
					break;
				case E.MenuDomain.LinkShipToFleet:
					if (A.LinkShipToFleetHelper != null && A.LinkShipToFleetHelper.Page != null && ApplyHiddenRules(A.LinkShipToFleetHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.LinkShipToFleetHelper.Repository);}
					break;
				case E.MenuDomain.Beacons:
					if (A.BeaconsHelper != null && A.BeaconsHelper.Page != null && ApplyHiddenRules(A.BeaconsHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.BeaconsHelper.Repository);}
					break;
				case E.MenuDomain.BuildingHierarchy:
					if (A.BuildingHierarchyHelper != null && A.BuildingHierarchyHelper.Page != null && ApplyHiddenRules(A.BuildingHierarchyHelper.Page) && doReIndex)
					{U.ReIndexDisplayId(SessionID, A.BuildingHierarchyHelper.Repository);}
					break;
				//{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}
                default:
                    break;
            }
        }
    }
}
