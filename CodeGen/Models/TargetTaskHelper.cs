// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:35 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:34 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:34 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:34 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:33 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:33 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:33 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:32 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:32 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:32 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/19 18:43:32 - {ce1afc7d-bcd9-49eb-9275-a7dc8279e07e}
// Generated Code - Version: 23.11.25 - 2024/11/18 21:08:58 - {3a6d9981-e6ad-4c67-848b-61b518dfa8f8}

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
using ArchCorpUtilities.GeneratedModels.PlanetsModel;
using ArchCorpUtilities.GeneratedModels.SitesModel;
using ArchCorpUtilities.GeneratedModels.BuildingsModel;
using ArchCorpUtilities.GeneratedModels.FleetsModel;
using ArchCorpUtilities.GeneratedModels.ShipsModel;
using ArchCorpUtilities.GeneratedModels.PersonnelModel;
using ArchCorpUtilities.GeneratedModels.BeaconsModel;
using ArchCorpUtilities.GeneratedModels.RoleGroupsModel;
using ArchCorpUtilities.GeneratedModels.RolesModel;
using ArchCorpUtilities.GeneratedModels.UsersModel;
using ArchCorpUtilities.GeneratedModels.LinkSitesToPlanetsModel;
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
								case U.MenuDomain.Planets:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.PlanetsHelper != null && !A.PlanetsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Sites:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.SitesHelper != null && !A.SitesHelper.IsItemsOnThePage())
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
								case U.MenuDomain.Fleets:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.FleetsHelper != null && !A.FleetsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Ships:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.ShipsHelper != null && !A.ShipsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Personnel:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.PersonnelHelper != null && !A.PersonnelHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Beacons:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.BeaconsHelper != null && !A.BeaconsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.RoleGroups:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.RoleGroupsHelper != null && !A.RoleGroupsHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Roles:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.RolesHelper != null && !A.RolesHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.Users:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.UsersHelper != null && !A.UsersHelper.IsItemsOnThePage())
                                    {
                                        item.IsHidden = true;
                                        ResetPage = true;
                                    }
                                    break;
								case U.MenuDomain.LinkSitesToPlanets:
                                    if (SessionID != null)
                                        L.Log("Menu - NoData", SessionID, 1);
                                    if (A.LinkSitesToPlanetsHelper != null && !A.LinkSitesToPlanetsHelper.IsItemsOnThePage())
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
							case U.MenuDomain.Planets: L.Log("Planets-Add", SessionID, 1); A.PlanetsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Sites: L.Log("Sites-Add", SessionID, 1); A.SitesHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Buildings: L.Log("Buildings-Add", SessionID, 1); A.BuildingsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Fleets: L.Log("Fleets-Add", SessionID, 1); A.FleetsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Ships: L.Log("Ships-Add", SessionID, 1); A.ShipsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Personnel: L.Log("Personnel-Add", SessionID, 1); A.PersonnelHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Beacons: L.Log("Beacons-Add", SessionID, 1); A.BeaconsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.RoleGroups: L.Log("RoleGroups-Add", SessionID, 1); A.RoleGroupsHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Roles: L.Log("Roles-Add", SessionID, 1); A.RolesHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.Users: L.Log("Users-Add", SessionID, 1); A.UsersHelper?.Add(simChoice, simInputValues); break;
							case U.MenuDomain.LinkSitesToPlanets: L.Log("LinkSitesToPlanets-Add", SessionID, 1); A.LinkSitesToPlanetsHelper?.Add(simChoice, simInputValues); break;
							//{A6E3C6F1-D649-45CE-8C05-3A87466618A9}
                            default: break;
                        }
                        break;

                    case "View":
                        switch (MenuItem?.Domain)
                        {                            
                            case U.MenuDomain.Menu: L.Log("Menu-View", SessionID, 1); MMH.InitialView(); break;
                            //{B03F74F5-9862-4916-9EF1-82DD253A5BC3}
							case U.MenuDomain.Planets: L.Log("Planets-View", SessionID, 1);
								if (A.PlanetsHelper != null && A.PlanetsHelper.Items != null) { A.PlanetsHelper?.Refresh(A.PlanetsHelper.Items); }
								break;
							case U.MenuDomain.Sites: L.Log("Sites-View", SessionID, 1);
								if (A.SitesHelper != null && A.SitesHelper.Items != null) { A.SitesHelper?.Refresh(A.SitesHelper.Items); }
								break;
							case U.MenuDomain.Buildings: L.Log("Buildings-View", SessionID, 1);
								if (A.BuildingsHelper != null && A.BuildingsHelper.Items != null) { A.BuildingsHelper?.Refresh(A.BuildingsHelper.Items); }
								break;
							case U.MenuDomain.Fleets: L.Log("Fleets-View", SessionID, 1);
								if (A.FleetsHelper != null && A.FleetsHelper.Items != null) { A.FleetsHelper?.Refresh(A.FleetsHelper.Items); }
								break;
							case U.MenuDomain.Ships: L.Log("Ships-View", SessionID, 1);
								if (A.ShipsHelper != null && A.ShipsHelper.Items != null) { A.ShipsHelper?.Refresh(A.ShipsHelper.Items); }
								break;
							case U.MenuDomain.Personnel: L.Log("Personnel-View", SessionID, 1);
								if (A.PersonnelHelper != null && A.PersonnelHelper.Items != null) { A.PersonnelHelper?.Refresh(A.PersonnelHelper.Items); }
								break;
							case U.MenuDomain.Beacons: L.Log("Beacons-View", SessionID, 1);
								if (A.BeaconsHelper != null && A.BeaconsHelper.Items != null) { A.BeaconsHelper?.Refresh(A.BeaconsHelper.Items); }
								break;
							case U.MenuDomain.RoleGroups: L.Log("RoleGroups-View", SessionID, 1);
								if (A.RoleGroupsHelper != null && A.RoleGroupsHelper.Items != null) { A.RoleGroupsHelper?.Refresh(A.RoleGroupsHelper.Items); }
								break;
							case U.MenuDomain.Roles: L.Log("Roles-View", SessionID, 1);
								if (A.RolesHelper != null && A.RolesHelper.Items != null) { A.RolesHelper?.Refresh(A.RolesHelper.Items); }
								break;
							case U.MenuDomain.Users: L.Log("Users-View", SessionID, 1);
								if (A.UsersHelper != null && A.UsersHelper.Items != null) { A.UsersHelper?.Refresh(A.UsersHelper.Items); }
								break;
							case U.MenuDomain.LinkSitesToPlanets: L.Log("LinkSitesToPlanets-View", SessionID, 1);
								if (A.LinkSitesToPlanetsHelper != null && A.LinkSitesToPlanetsHelper.Items != null) { A.LinkSitesToPlanetsHelper?.Refresh(A.LinkSitesToPlanetsHelper.Items); }
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
							case U.MenuDomain.Planets:
								L.Log("Planets- FirstPage", SessionID, 1);
								A.PlanetsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites- FirstPage", SessionID, 1);
								A.SitesHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings- FirstPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets- FirstPage", SessionID, 1);
								A.FleetsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships- FirstPage", SessionID, 1);
								A.ShipsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel- FirstPage", SessionID, 1);
								A.PersonnelHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons- FirstPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups- FirstPage", SessionID, 1);
								A.RoleGroupsHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles- FirstPage", SessionID, 1);
								A.RolesHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.Users:
								L.Log("Users- FirstPage", SessionID, 1);
								A.UsersHelper?.View(U.Navigation.FirstPage);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets- FirstPage", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.View(U.Navigation.FirstPage);
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
							case U.MenuDomain.Planets:
								L.Log("Planets- LastPage", SessionID, 1);
								A.PlanetsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites- LastPage", SessionID, 1);
								A.SitesHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings- LastPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets- LastPage", SessionID, 1);
								A.FleetsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships- LastPage", SessionID, 1);
								A.ShipsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel- LastPage", SessionID, 1);
								A.PersonnelHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons- LastPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups- LastPage", SessionID, 1);
								A.RoleGroupsHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles- LastPage", SessionID, 1);
								A.RolesHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.Users:
								L.Log("Users- LastPage", SessionID, 1);
								A.UsersHelper?.View(U.Navigation.LastPage);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets- LastPage", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.View(U.Navigation.LastPage);
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
							case U.MenuDomain.Planets:
								L.Log("Planets-NextPage", SessionID, 1);
								A.PlanetsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-NextPage", SessionID, 1);
								A.SitesHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-NextPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-NextPage", SessionID, 1);
								A.FleetsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-NextPage", SessionID, 1);
								A.ShipsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-NextPage", SessionID, 1);
								A.PersonnelHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-NextPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-NextPage", SessionID, 1);
								A.RoleGroupsHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-NextPage", SessionID, 1);
								A.RolesHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.Users:
								L.Log("Users-NextPage", SessionID, 1);
								A.UsersHelper?.View(U.Navigation.NextPage);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-NextPage", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.View(U.Navigation.NextPage);
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
							case U.MenuDomain.Planets:
								L.Log("Planets-PreviousPage", SessionID, 1);
								A.PlanetsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-PreviousPage", SessionID, 1);
								A.SitesHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-PreviousPage", SessionID, 1);
								A.BuildingsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-PreviousPage", SessionID, 1);
								A.FleetsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-PreviousPage", SessionID, 1);
								A.ShipsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-PreviousPage", SessionID, 1);
								A.PersonnelHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-PreviousPage", SessionID, 1);
								A.BeaconsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-PreviousPage", SessionID, 1);
								A.RoleGroupsHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-PreviousPage", SessionID, 1);
								A.RolesHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.Users:
								L.Log("Users-PreviousPage", SessionID, 1);
								A.UsersHelper?.View(U.Navigation.PreviousPage);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-PreviousPage", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.View(U.Navigation.PreviousPage);
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
							case U.MenuDomain.Planets:
								L.Log("Planets-Edit", SessionID, 1);
								A.PlanetsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-Edit", SessionID, 1);
								A.SitesHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Edit", SessionID, 1);
								A.BuildingsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-Edit", SessionID, 1);
								A.FleetsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-Edit", SessionID, 1);
								A.ShipsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-Edit", SessionID, 1);
								A.PersonnelHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-Edit", SessionID, 1);
								A.BeaconsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-Edit", SessionID, 1);
								A.RoleGroupsHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-Edit", SessionID, 1);
								A.RolesHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.Users:
								L.Log("Users-Edit", SessionID, 1);
								A.UsersHelper?.Edit(simChoice, simInputValues);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-Edit", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.Edit(simChoice, simInputValues);
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
							case U.MenuDomain.Planets:
								L.Log("Planets-Remove", SessionID, 1);
								A.PlanetsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-Remove", SessionID, 1);
								A.SitesHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Remove", SessionID, 1);
								A.BuildingsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-Remove", SessionID, 1);
								A.FleetsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-Remove", SessionID, 1);
								A.ShipsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-Remove", SessionID, 1);
								A.PersonnelHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-Remove", SessionID, 1);
								A.BeaconsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-Remove", SessionID, 1);
								A.RoleGroupsHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-Remove", SessionID, 1);
								A.RolesHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.Users:
								L.Log("Users-Remove", SessionID, 1);
								A.UsersHelper?.Remove(simChoice, simInputValues);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-Remove", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.Remove(simChoice, simInputValues);
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
							case U.MenuDomain.Planets:
								L.Log("Planets-Save", SessionID, 1);
								A.PlanetsHelper?.Save();
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-Save", SessionID, 1);
								A.SitesHelper?.Save();
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Save", SessionID, 1);
								A.BuildingsHelper?.Save();
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-Save", SessionID, 1);
								A.FleetsHelper?.Save();
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-Save", SessionID, 1);
								A.ShipsHelper?.Save();
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-Save", SessionID, 1);
								A.PersonnelHelper?.Save();
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-Save", SessionID, 1);
								A.BeaconsHelper?.Save();
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-Save", SessionID, 1);
								A.RoleGroupsHelper?.Save();
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-Save", SessionID, 1);
								A.RolesHelper?.Save();
								break;
							case U.MenuDomain.Users:
								L.Log("Users-Save", SessionID, 1);
								A.UsersHelper?.Save();
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-Save", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.Save();
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
							case U.MenuDomain.Planets:
								L.Log("Planets-Load", SessionID, 1);
								A.PlanetsHelper?.Load();
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-Load", SessionID, 1);
								A.SitesHelper?.Load();
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Load", SessionID, 1);
								A.BuildingsHelper?.Load();
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-Load", SessionID, 1);
								A.FleetsHelper?.Load();
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-Load", SessionID, 1);
								A.ShipsHelper?.Load();
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-Load", SessionID, 1);
								A.PersonnelHelper?.Load();
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-Load", SessionID, 1);
								A.BeaconsHelper?.Load();
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-Load", SessionID, 1);
								A.RoleGroupsHelper?.Load();
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-Load", SessionID, 1);
								A.RolesHelper?.Load();
								break;
							case U.MenuDomain.Users:
								L.Log("Users-Load", SessionID, 1);
								A.UsersHelper?.Load();
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-Load", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.Load();
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
							case U.MenuDomain.Planets:
								L.Log("Planets-Search", SessionID, 1);
								A.PlanetsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Sites:
								L.Log("Sites-Search", SessionID, 1);
								A.SitesHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Buildings:
								L.Log("Buildings-Search", SessionID, 1);
								A.BuildingsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Fleets:
								L.Log("Fleets-Search", SessionID, 1);
								A.FleetsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Ships:
								L.Log("Ships-Search", SessionID, 1);
								A.ShipsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Personnel:
								L.Log("Personnel-Search", SessionID, 1);
								A.PersonnelHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Beacons:
								L.Log("Beacons-Search", SessionID, 1);
								A.BeaconsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.RoleGroups:
								L.Log("RoleGroups-Search", SessionID, 1);
								A.RoleGroupsHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Roles:
								L.Log("Roles-Search", SessionID, 1);
								A.RolesHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.Users:
								L.Log("Users-Search", SessionID, 1);
								A.UsersHelper?.Search(simChoice, simInputValues);
								break;
							case U.MenuDomain.LinkSitesToPlanets:
								L.Log("LinkSitesToPlanets-Search", SessionID, 1);
								A.LinkSitesToPlanetsHelper?.Search(simChoice, simInputValues);
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
							case U.MenuDomain.Planets: L.Log("Planets-View", SessionID, 1);
								if (A.PlanetsHelper != null && A.PlanetsHelper.Items != null && A.PlanetsHelper.Items.Count > 0)
								{A.PlanetsHelper.Refresh(A.PlanetsHelper.Items);}
							break;
							case U.MenuDomain.Sites: L.Log("Sites-View", SessionID, 1);
								if (A.SitesHelper != null && A.SitesHelper.Items != null && A.SitesHelper.Items.Count > 0)
								{A.SitesHelper.Refresh(A.SitesHelper.Items);}
							break;
							case U.MenuDomain.Buildings: L.Log("Buildings-View", SessionID, 1);
								if (A.BuildingsHelper != null && A.BuildingsHelper.Items != null && A.BuildingsHelper.Items.Count > 0)
								{A.BuildingsHelper.Refresh(A.BuildingsHelper.Items);}
							break;
							case U.MenuDomain.Fleets: L.Log("Fleets-View", SessionID, 1);
								if (A.FleetsHelper != null && A.FleetsHelper.Items != null && A.FleetsHelper.Items.Count > 0)
								{A.FleetsHelper.Refresh(A.FleetsHelper.Items);}
							break;
							case U.MenuDomain.Ships: L.Log("Ships-View", SessionID, 1);
								if (A.ShipsHelper != null && A.ShipsHelper.Items != null && A.ShipsHelper.Items.Count > 0)
								{A.ShipsHelper.Refresh(A.ShipsHelper.Items);}
							break;
							case U.MenuDomain.Personnel: L.Log("Personnel-View", SessionID, 1);
								if (A.PersonnelHelper != null && A.PersonnelHelper.Items != null && A.PersonnelHelper.Items.Count > 0)
								{A.PersonnelHelper.Refresh(A.PersonnelHelper.Items);}
							break;
							case U.MenuDomain.Beacons: L.Log("Beacons-View", SessionID, 1);
								if (A.BeaconsHelper != null && A.BeaconsHelper.Items != null && A.BeaconsHelper.Items.Count > 0)
								{A.BeaconsHelper.Refresh(A.BeaconsHelper.Items);}
							break;
							case U.MenuDomain.RoleGroups: L.Log("RoleGroups-View", SessionID, 1);
								if (A.RoleGroupsHelper != null && A.RoleGroupsHelper.Items != null && A.RoleGroupsHelper.Items.Count > 0)
								{A.RoleGroupsHelper.Refresh(A.RoleGroupsHelper.Items);}
							break;
							case U.MenuDomain.Roles: L.Log("Roles-View", SessionID, 1);
								if (A.RolesHelper != null && A.RolesHelper.Items != null && A.RolesHelper.Items.Count > 0)
								{A.RolesHelper.Refresh(A.RolesHelper.Items);}
							break;
							case U.MenuDomain.Users: L.Log("Users-View", SessionID, 1);
								if (A.UsersHelper != null && A.UsersHelper.Items != null && A.UsersHelper.Items.Count > 0)
								{A.UsersHelper.Refresh(A.UsersHelper.Items);}
							break;
							case U.MenuDomain.LinkSitesToPlanets: L.Log("LinkSitesToPlanets-View", SessionID, 1);
								if (A.LinkSitesToPlanetsHelper != null && A.LinkSitesToPlanetsHelper.Items != null && A.LinkSitesToPlanetsHelper.Items.Count > 0)
								{A.LinkSitesToPlanetsHelper.Refresh(A.LinkSitesToPlanetsHelper.Items);}
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
				case U.MenuDomain.Planets:
					if (A.PlanetsHelper != null && ApplyHiddenRules(A.PlanetsHelper.Page) && doReIndex)
					{A.PlanetsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Sites:
					if (A.SitesHelper != null && ApplyHiddenRules(A.SitesHelper.Page) && doReIndex)
					{A.SitesHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Buildings:
					if (A.BuildingsHelper != null && ApplyHiddenRules(A.BuildingsHelper.Page) && doReIndex)
					{A.BuildingsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Fleets:
					if (A.FleetsHelper != null && ApplyHiddenRules(A.FleetsHelper.Page) && doReIndex)
					{A.FleetsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Ships:
					if (A.ShipsHelper != null && ApplyHiddenRules(A.ShipsHelper.Page) && doReIndex)
					{A.ShipsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Personnel:
					if (A.PersonnelHelper != null && ApplyHiddenRules(A.PersonnelHelper.Page) && doReIndex)
					{A.PersonnelHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Beacons:
					if (A.BeaconsHelper != null && ApplyHiddenRules(A.BeaconsHelper.Page) && doReIndex)
					{A.BeaconsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.RoleGroups:
					if (A.RoleGroupsHelper != null && ApplyHiddenRules(A.RoleGroupsHelper.Page) && doReIndex)
					{A.RoleGroupsHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Roles:
					if (A.RolesHelper != null && ApplyHiddenRules(A.RolesHelper.Page) && doReIndex)
					{A.RolesHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.Users:
					if (A.UsersHelper != null && ApplyHiddenRules(A.UsersHelper.Page) && doReIndex)
					{A.UsersHelper.ReIndexDisplayId();}
					break;
				case U.MenuDomain.LinkSitesToPlanets:
					if (A.LinkSitesToPlanetsHelper != null && ApplyHiddenRules(A.LinkSitesToPlanetsHelper.Page) && doReIndex)
					{A.LinkSitesToPlanetsHelper.ReIndexDisplayId();}
					break;
				//{5ED05F9F-E960-4964-AD0F-89E21CCCD9F5}
                default:
                    break;
            }
        }
    }
}
