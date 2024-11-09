// Generated Code - Version: 1.0.0 - 2024/11/09 22:15:42 - {2991c9ad-4969-45a7-87cc-0857a2dd7797} 

//Generated code : 2024/11/09 22:15:42 - Session : 2991c9ad-4969-45a7-87cc-0857a2dd7797
using ArchCorpUtilities.Models.Helper;
using ArchCorpUtilities.GeneratedModels.BeaconsModel;
using ArchCorpUtilities.Utilities;
using ArchCorpUtilities.Models.BeaconsModel.Beacons;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;

namespace ArchCorpUtilities.GeneratedModels.BeaconsModel
{
    public class BeaconsHelper : IHelper<Beacons>, IDisposable
    {
        public string? SessionID { get; set; }
        public List<Beacons>? Items { get; set; }
        public List<Beacons>? EntitiesOnThePage { get; set; }
        public Patina.Patina Page { get; set; }

        public BeaconsHelper(string? sessionID)
        {
            SessionID = sessionID;
            Items = MockData();
            Page = new Patina.Patina(5, Convert.ToUInt32(Items?.Count));
        }

        private List<Beacons>? MockData()
        {
            if (Items == null || Items.Count == 0)
                Items = [];
        
            Items.Add(new Beacons("Alpha-Beacons", 1));
            Items.Add(new Beacons("Beta-Beacons", 2));
            Items.Add(new Beacons("Charlie-Beacons", 3));
            Items.Add(new Beacons("Delta-Beacons", 3));
            Items.Add(new Beacons("Echo-Beacons", 3));
            Items.Add(new Beacons("Foxtrot-Beacons", 3));
            Items.Add(new Beacons("Golf-Beacons", 3));
            Items.Add(new Beacons("Hotel-Beacons", 3));
            return Items;
        }

        public bool View(U.Navigation navigate = U.Navigation.FirstPage)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            var orderedEntities = Items?.OrderBy(p => p.Index).ToList();
           EntitiesOnThePage = U.ViewWithPagination("Beacons", Page, orderedEntities, navigate);
            return true;
        }

        public bool Add(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Please provide the beacon name:");
            var Input = CH.GetInput(simInput?[0]);
            if(!string.IsNullOrWhiteSpace(Input))
            {
                Items?.Add(new(Input, 0));
                CH.Feedback("Beacon(s) added.");
                ResetPageMaxCount();                
                ReIndexDisplayId();
                return true;
            }
            else
                CH.Feedback("Invalid Beacon Name or empty - No beacons added.");

            return false;
        }

        public void Dispose()
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Under Construction");
        }

        public bool Edit(int? simChoice, string[]? simInput)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Under Construction");
            return true;
        }

        public bool IsItemsOnThePage()
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            return !(EntitiesOnThePage == null || (EntitiesOnThePage != null && EntitiesOnThePage.Count == 0));
        }

        public bool Load(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Under Construction");
            return true;
        }

        public bool Refresh(List<Beacons> modelList, U.Navigation navigate = U.Navigation.FirstPage)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            var orderedEntities = modelList?.OrderBy(p => p.Index).ToList();
           EntitiesOnThePage = U.ViewWithPagination("Beacons", Page, orderedEntities, navigate);
            return true;
        }

        public bool Remove(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Under Construction");
            return true;
        }

        public bool Save(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Under Construction");
            return true;
        }

        public bool Search(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Under Construction");
            return true;
        }

        public void ReIndexDisplayId()
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);


            var sites = Items?.OrderBy(c => c.Name).ToList();

            int counter = 1;
            if (sites != null)
            {
                foreach (var item in sites)
                {
                    item.DisplayId = counter++;
                    item.Id = item.DisplayId;
                    item.Index = item.DisplayId;
                }
            }
        }

        public void ResetPageMaxCount()
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(Items?.Count));
        }
    }
}
