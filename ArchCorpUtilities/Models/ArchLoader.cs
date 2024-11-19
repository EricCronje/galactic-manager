// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:44 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:44 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:43 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:43 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:43 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:43 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:42 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:42 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:42 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:41 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/19 19:23:41 - {2a4371ca-84c4-469c-a5be-31fe909f01e4}
// Generated Code - Version: 23.11.25 - 2024/11/18 21:08:58 - {3a6d9981-e6ad-4c67-848b-61b518dfa8f8}

using ArchCorpUtilities.Utilities;
using MH = ArchCorpUtilities.Models.Menus.MenuHelper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using L = Logger.Logger;
using MMH = ArchCorpUtilities.Models.Menus.MenuMaintenanceHelper;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using TTH = ArchCorpUtilities.Models.TargetTaskHelper;
using CodeGen = ArchCorpUtilities.Utilities.CodeGenHelper;

//{0ACDC688-3120-452F-94AE-2DD1771A9991}
using PlanetsHelper = ArchCorpUtilities.GeneratedModels.PlanetsModel.PlanetsHelper;
using SitesHelper = ArchCorpUtilities.GeneratedModels.SitesModel.SitesHelper;
using BuildingsHelper = ArchCorpUtilities.GeneratedModels.BuildingsModel.BuildingsHelper;
using FleetsHelper = ArchCorpUtilities.GeneratedModels.FleetsModel.FleetsHelper;
using ShipsHelper = ArchCorpUtilities.GeneratedModels.ShipsModel.ShipsHelper;
using PersonnelHelper = ArchCorpUtilities.GeneratedModels.PersonnelModel.PersonnelHelper;
using BeaconsHelper = ArchCorpUtilities.GeneratedModels.BeaconsModel.BeaconsHelper;
using RoleGroupsHelper = ArchCorpUtilities.GeneratedModels.RoleGroupsModel.RoleGroupsHelper;
using RolesHelper = ArchCorpUtilities.GeneratedModels.RolesModel.RolesHelper;
using UsersHelper = ArchCorpUtilities.GeneratedModels.UsersModel.UsersHelper;
using LinkSitesToPlanetsHelper = ArchCorpUtilities.GeneratedModels.LinkSitesToPlanetsModel.LinkSitesToPlanetsHelper;
//{0ACDC688-3120-452F-94AE-2DD1771A9991}

using ArchCorpUtilities.Models.Menus;

namespace ArchCorpUtilities.Models
{
    public static class ArchLoader
    {
        
        public static string SessionID { get; }

        //{048A4DD6-2F1B-4178-A732-E3B50D3F0791}
		internal static PlanetsHelper? PlanetsHelper = null;
		internal static SitesHelper? SitesHelper = null;
		internal static BuildingsHelper? BuildingsHelper = null;
		internal static FleetsHelper? FleetsHelper = null;
		internal static ShipsHelper? ShipsHelper = null;
		internal static PersonnelHelper? PersonnelHelper = null;
		internal static BeaconsHelper? BeaconsHelper = null;
		internal static RoleGroupsHelper? RoleGroupsHelper = null;
		internal static RolesHelper? RolesHelper = null;
		internal static UsersHelper? UsersHelper = null;
		internal static LinkSitesToPlanetsHelper? LinkSitesToPlanetsHelper = null;
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
			PlanetsHelper = new(SessionID);
			SitesHelper = new(SessionID);
			BuildingsHelper = new(SessionID);
			FleetsHelper = new(SessionID);
			ShipsHelper = new(SessionID);
			PersonnelHelper = new(SessionID);
			BeaconsHelper = new(SessionID);
			RoleGroupsHelper = new(SessionID);
			RolesHelper = new(SessionID);
			UsersHelper = new(SessionID);
			LinkSitesToPlanetsHelper = new(SessionID);
			//{9ED7AF33-DE0E-45C3-821F-4669558AD744}

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
			BuildingsHelper?.LoadDefaults();
			FleetsHelper?.LoadDefaults();
			ShipsHelper?.LoadDefaults();
			PersonnelHelper?.LoadDefaults();
			BeaconsHelper?.LoadDefaults();
			RoleGroupsHelper?.LoadDefaults();
			RolesHelper?.LoadDefaults();
			UsersHelper?.LoadDefaults();
			LinkSitesToPlanetsHelper?.LoadDefaults();
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

        static int ShowMenu(int? page, string? menuHeading, int? simChoice = null)
        {
            CH.Feedback(MH.ShowMenu(menuHeading, page));

            if (simChoice == null)
            {
                _ = int.TryParse(CH.GetInput(), out int Choice);
                simChoice = Choice;
            }

            return simChoice.Value;
        }

    }
}
