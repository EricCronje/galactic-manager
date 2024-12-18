// Generated Code - Version: 23.11.25 - 2024/12/18 17:52:37 - {551ec3b9-4773-44f0-bcaf-84a745825ca5}

using ArchCorpUtilities.Utilities;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using TTH = ArchCorpUtilities.Models.TargetTaskHelper;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

//{0ACDC688-3120-452F-94AE-2DD1771A9991}
using LinkSiteToPlanetHelper = ArchCorpUtilities.Models.LinkHelper;
using LinkSectionToSiteHelper = ArchCorpUtilities.Models.LinkHelper;
using LinkBuildingToSectionHelper = ArchCorpUtilities.Models.LinkHelper;
using LinkBuildingToBuildingsTypesHelper = ArchCorpUtilities.Models.LinkHelper;
using LinkShipToShipTypeHelper = ArchCorpUtilities.Models.LinkHelper;
using LinkShipToFleetHelper = ArchCorpUtilities.Models.LinkHelper;
//{0ACDC688-3120-452F-94AE-2DD1771A9991}
//{99B979B3-BA78-4173-959C-1F116C96BB04}
using PlanetsHelper = ArchCorpUtilities.Models.DefaultHelper;
using SitesHelper = ArchCorpUtilities.Models.DefaultHelper;
using SectionsHelper = ArchCorpUtilities.Models.DefaultHelper;
using BuildingsHelper = ArchCorpUtilities.Models.DefaultHelper;
using BuildingTypesHelper = ArchCorpUtilities.Models.DefaultHelper;
using ShipsHelper = ArchCorpUtilities.Models.DefaultHelper;
using ShipTypesHelper = ArchCorpUtilities.Models.DefaultHelper;
using FleetsHelper = ArchCorpUtilities.Models.DefaultHelper;
using BeaconsHelper = ArchCorpUtilities.Models.DefaultHelper;
//{99B979B3-BA78-4173-959C-1F116C96BB04}
//{5334A0BE-D696-4065-B673-A2113B7907A9}
using BuildingHierarchyHelper = ArchCorpUtilities.Models.HierarchyHelper;
//{5334A0BE-D696-4065-B673-A2113B7907A9}

using ArchCorpUtilities.Models.Menus;

namespace ArchCorpUtilities.Models
{
    public static class ArchLoader
    {
        
        public static string SessionID { get; }

        //{048A4DD6-2F1B-4178-A732-E3B50D3F0791}
		internal static PlanetsHelper? PlanetsHelper = null;
		internal static SitesHelper? SitesHelper = null;
		internal static SectionsHelper? SectionsHelper = null;
		internal static BuildingsHelper? BuildingsHelper = null;
		internal static LinkSiteToPlanetHelper? LinkSiteToPlanetHelper = null;
		internal static LinkSectionToSiteHelper? LinkSectionToSiteHelper = null;
		internal static LinkBuildingToSectionHelper? LinkBuildingToSectionHelper = null;
		internal static BuildingTypesHelper? BuildingTypesHelper = null;
		internal static LinkBuildingToBuildingsTypesHelper? LinkBuildingToBuildingsTypesHelper = null;
		internal static ShipsHelper? ShipsHelper = null;
		internal static ShipTypesHelper? ShipTypesHelper = null;
		internal static LinkShipToShipTypeHelper? LinkShipToShipTypeHelper = null;
		internal static FleetsHelper? FleetsHelper = null;
		internal static LinkShipToFleetHelper? LinkShipToFleetHelper = null;
		internal static BeaconsHelper? BeaconsHelper = null;
		internal static BuildingHierarchyHelper? BuildingHierarchyHelper = null;
		//{048A4DD6-2F1B-4178-A732-E3B50D3F0791}

        static ArchLoader()
        {
            SessionID = Guid.NewGuid().ToString();
            MH.SessionID = SessionID;
            MMH.SessionID = SessionID;
            U.SessionID = SessionID;
            TTH.SessionID = SessionID;
            CodeGen.SessionID = SessionID;

            //{9ED7AF33-DE0E-45C3-821F-4669558AD744}
			PlanetsHelper = new(SessionID, "Planets", "Planets");
			SitesHelper = new(SessionID, "Sites", "Sites");
			SectionsHelper = new(SessionID, "Sections", "Sections");
			BuildingsHelper = new(SessionID, "Buildings", "Buildings");
			BuildingTypesHelper = new(SessionID, "BuildingTypes", "BuildingTypes");
			ShipsHelper = new(SessionID, "Ships", "Ships");
			ShipTypesHelper = new(SessionID, "ShipTypes", "ShipTypes");
			FleetsHelper = new(SessionID, "Fleets", "Fleets");
			BeaconsHelper = new(SessionID, "Beacons", "Beacons");
			//{9ED7AF33-DE0E-45C3-821F-4669558AD744}
            //{298F4945-829B-4881-AF7C-9427FB5FCC59}
			LinkSiteToPlanetHelper = new(SessionID, "LinkSiteToPlanet", "LinkSiteToPlanet", SitesHelper, PlanetsHelper, "Sites", "Planets");
			LinkSectionToSiteHelper = new(SessionID, "LinkSectionToSite", "LinkSectionToSite", SectionsHelper, SitesHelper, "Sections", "Sites");
			LinkBuildingToSectionHelper = new(SessionID, "LinkBuildingToSection", "LinkBuildingToSection", BuildingsHelper, SectionsHelper, "Buildings", "Sections");
			LinkBuildingToBuildingsTypesHelper = new(SessionID, "LinkBuildingToBuildingsTypes", "LinkBuildingToBuildingsTypes", BuildingsHelper, BuildingTypesHelper, "Buildings", "BuildingTypes");
			LinkShipToShipTypeHelper = new(SessionID, "LinkShipToShipType", "LinkShipToShipType", ShipsHelper, ShipTypesHelper, "Ships", "ShipTypes");
			LinkShipToFleetHelper = new(SessionID, "LinkShipToFleet", "LinkShipToFleet", ShipsHelper, FleetsHelper, "Ships", "Fleets");
			//{298F4945-829B-4881-AF7C-9427FB5FCC59}
            //{12C91723-1389-4BCC-866B-FB3E3C50D267}
			BuildingHierarchyHelper = new(SessionID, "BuildingHierarchy", "BuildingHierarchy", BuildingsHelper, "Buildings");
			//{12C91723-1389-4BCC-866B-FB3E3C50D267}

        }

        public static void RunArch(List<Command>? commands = null, int logLevel = 1)
        {
            if (commands != null || commands?.Count > 0)
            {
                CH.IsSimulate = true;
            }

            //{E4C217C0-AC0D-4571-95E4-16CE056F35A5}
			PlanetsHelper?.LoadDefaults();
			SitesHelper?.LoadDefaults();
			SectionsHelper?.LoadDefaults();
			BuildingsHelper?.LoadDefaults();
			LinkSiteToPlanetHelper?.LoadDefaults();
			LinkSectionToSiteHelper?.LoadDefaults();
			LinkBuildingToSectionHelper?.LoadDefaults();
			BuildingTypesHelper?.LoadDefaults();
			LinkBuildingToBuildingsTypesHelper?.LoadDefaults();
			ShipsHelper?.LoadDefaults();
			ShipTypesHelper?.LoadDefaults();
			LinkShipToShipTypeHelper?.LoadDefaults();
			FleetsHelper?.LoadDefaults();
			LinkShipToFleetHelper?.LoadDefaults();
			BeaconsHelper?.LoadDefaults();
			BuildingHierarchyHelper?.LoadDefaults();
			//{E4C217C0-AC0D-4571-95E4-16CE056F35A5}

            if (File.Exists(CodeGenHelper.CurrentGuid))
                MMH.Load(CodeGenHelper.CurrentGuid);
			
            L.CurrentLevel = logLevel;
            L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            int? SimChoice = null;
            string? SimInput = null;
            string[]? SimInputValues = null;

            if (commands == null || commands.Count == 0)
            {
                CH.IsSimulate = false;
            }
            if (commands != null)
            {
                CH.IsSimulate = true;
                //Simulate
                SimChoice = 1; // just to get in the loop
                SimInput = "";
                SimInputValues = null;
            }
            //Retrieve starting page
            int? TargetPage = MH.Menu?.FirstOrDefault(c => c.IsStartPage == true)?.Page;
            
            if (TargetPage != null)
            {
                var PrevPagenumber = TargetPage;
                var PageHeading = MH.Menu?.FirstOrDefault(c => c.Page == TargetPage)?.PageHeading;


                int Choice = ShowMenu(TargetPage, PageHeading, SimChoice);


                var ExitOption = MH.Menu?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;

                L.Log($"Before loop - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 0);

                int Counter = 0;
                while (Choice != ExitOption)
                {

                    if (CH.IsSimulate && commands != null && Counter < commands?.Count)
                    {
                        SimInput = commands[Counter].Value;
                        SimChoice = commands[Counter].Choice;
                        SimInputValues = commands[Counter].Values;
                        if (SimChoice != null)
                            Choice = SimChoice.Value;
                        CH.Feedback($"Simulate: {SimInput} -- {SimChoice} --- {SimInputValues?.ToString()}");
                        Counter++;
                        L.Log($"Inside loop - Simulation - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);
                    }



                    if (!CH.IsSimulate) { Console.Clear(); }
                    if (CH.IsSimulate) { CH.Feedback(Resource.ClearConsoleMessage); }

                    var menuItem = MH.Menu?.FirstOrDefault(c => c.DisplayNumber == Choice && c.Page == TargetPage && c.IsHidden == false);

                    if (menuItem != null && menuItem.TargetPage != 0) { TargetPage = menuItem.TargetPage; }

                    PageHeading = MH.Menu?.FirstOrDefault(c => c.Page == TargetPage.Value)?.PageHeading;

                    ExitOption = MH.Menu?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage.Value)?.DisplayNumber;

                    L.Log($"Inside loop - Before PerformDefaultTasks - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);


                    TTH.PerformDefaultTasks(TargetPage.Value, PrevPagenumber.Value, Choice, SimChoice, SimInput, SimInputValues);
                                      
                    Choice = ShowMenu(TargetPage.Value, PageHeading, SimChoice);

                    PrevPagenumber = TargetPage.Value;

                    if (Counter == commands?.Count)
                        ExitOption = Choice;
                    else
                        ExitOption = MH.Menu?.FirstOrDefault(c => c.IsExitOption && c.Page == TargetPage)?.DisplayNumber;
                }
                if (CH.IsSimulate) { CH.Feedback(Resource.ExitIndicator); }
                L.Log($"After loop - Choice: {Choice} TargetPage: {TargetPage.Value}", SessionID, 2);
            }
            else
            {
                CH.Feedback(Resource.TargetPageNotSet);
                L.Log($"TargetPage is not marked - fix the menu structure", SessionID, 9);
            }
        }

        //{0EFC2DF7-9635-48A9-8A37-ED03992483F6}
		//Generated - 2024/12/18 17:52:37 - 551ec3b9-4773-44f0-bcaf-84a745825ca5
		public static int ShowMenu(int? page, string? menuHeading, int? simChoice = null)
		{
			CH.Feedback(MH.ShowMenu(menuHeading, page));
			if (simChoice != null) { return simChoice.Value; }
			_ = int.TryParse(CH.GetInput(), out int Choice); return Choice;
		}
		//{0EFC2DF7-9635-48A9-8A37-ED03992483F6}

    }
}
