using ArchCorpUtilities.Models.Helper;
using ArchCorpUtilities.Models;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using E = EnumLib.EnumLib;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using System.Text;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.GeneratedModels.~Entity~Model
{
    public class ~Entity~Helper : IHelper<Entity>, IDisposable
	{
        public string? SessionID { get; set; }
        public List<Entity>? EntitiesOnThePage { get; set; }
        public Patina.Patina? Page { get; set; }
        public MockRepository<Entity>? Repository { get; set; }
        public ~Entity~Helper(string? sessionID, string postFix = "")
		{
            SessionID = sessionID;
            Repository = new(postFix);
            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Repository?.Count()));
		}
        public bool View(E.Navigation navigate = E.Navigation.FirstPage, string heading = "")
		{
            EntitiesOnThePage = U.View(navigate, heading, Page, Repository?.OrderByIndex(), System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            return true;
		}
        public bool Add(int? simChoice = null, string[]? simInput = null)
        {
            U.Log(SessionID, System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            string Input = U.GetStringInput("Please provide the item name: ", simInput?[0]);
            string Feedback = U.IsValidInput(Input, "Invalid Name or empty - No item added.");
            Feedback = string.IsNullOrWhiteSpace(Feedback) ? DuplicateFound(Input) ? "Duplicate entry found - operation aborted." : string.Empty : string.Empty;
            if (string.IsNullOrWhiteSpace(Feedback))
            {
                Repository?.Add(new(Input, 0));
                CH.Feedback("Item added.");
                ResetIndexAndPage();
                return true;
            }
            else
                CH.Feedback(Feedback);

            return false;
        }
        public void Dispose()
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            GC.SuppressFinalize(this);
        }
        public bool Edit(int? simChoice, string[]? simInput)
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            try
            {
                Entity? Entity = ViewAndSelectItem(simInput?[0], "Select an item to edit");

                if (Entity != null)
                {
                    CH.Feedback("Please enter a new name");
                    var Input = CH.GetInput(simInput?[1]);
                    if (!string.IsNullOrWhiteSpace(Input))
                    {
                        var NotFound = Repository?.GetByName(Input);
                        if (NotFound == null)
                        {
                            EntitiesOnThePage = [new(Input, 0)];
                            Repository?.Add(EntitiesOnThePage[0]);
                            if (Entity != null)
                                Repository?.Remove(Entity);
                            Page = new Patina.Patina(1, 1);
                            ResetIndexAndPage(true);
                            CH.Feedback("Item was modified");
                            return true;
                        }
                        else
                            CH.Feedback("Duplicate entry found - operation aborted.");
                    }
                    else
                        CH.Feedback("No name entered");
                }
                else
                {
                    if (EntitiesOnThePage != null)
                        CH.Feedback("No Item selected");
                }

                ResetIndexAndPage(true);
                return false;

            }
            catch (Exception err)
            {
                L.Log($"Error - Edit an Item Helper -- {err.Message} --- {err.InnerException?.Message}");
                return false;
            }
        }
        public bool IsItemsOnThePage()
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            return !(EntitiesOnThePage == null || EntitiesOnThePage != null && EntitiesOnThePage.Count == 0);
        }
        public bool Search(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            CH.Feedback("Enter an item name to search for");

            var Input = CH.GetInput(simInput?[0]);

            if (string.IsNullOrWhiteSpace(Input))
                CH.Feedback("No Items Name Was Entered");
            else
            {
                List<Entity>? Entities = Repository?.GetAllContainingName(Input);
                if (Entities != null && Entities.Count > 0)
                {
                    Page = new Patina.Patina(5, Convert.ToUInt32(Entities.Count));
                    EntitiesOnThePage = U.ViewWithPagination("Search Items", Page, Entities);
                    return true;
                }
                else
                {
                    CH.Feedback("No Items Was Found");
                    EntitiesOnThePage = [new Entity("None", 1)];
                    Page = new Patina.Patina(1, 1);
                    return false;
                }
            }

            ResetIndexAndPage();

            return false;
        }        
        public bool Load(int? simChoice = null, string[]? simInput = null, string path = "~Entity~")
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            try
            {
                if (File.Exists(path))
                {
                    CH.Feedback($"Items Loaded Successfully {path} - {U.GetCurrentDate()}");
                    string FileInput = File.ReadAllText(path);
                    bool SkipFirstLine = true;
                    foreach (string line in FileInput.Split("\r\n"))
                    {
                        if (!SkipFirstLine)
                        {
                            string[] LineItem = line.Split("|");
                            if (LineItem.Length > 0 && LineItem.Length > 1)
                            {
                                string Name = LineItem[0].Trim();
                                string GUID = CH.IsSimulate ? "<GUID>" : LineItem[1].Trim();
                                var Entity = Repository?.GetByGUID(GUID)?.ToList()[0];
                                var EntityItem = Repository?.GetByName(Name)?.ToList()[0];
                                var OldGUID = CH.IsSimulate ? "<GUID>" : EntityItem?.Guid_;

                                if (Entity == null)
                                {
                                    if (EntityItem != null)
                                    {
                                        CH.Feedback($"Item Exists - No action - Old item: {EntityItem.Name} - {OldGUID} - New Item: {Name} - {GUID}");
                                    }
                                    else
                                    {
                                        if (SessionID != null)
                                            L.Log($"Item found - {Name}", SessionID);
                                        Repository?.Add(new Entity(Name, 0, GUID));
                                        CH.Feedback($"Item Added - New Item: {Name} - {GUID}");
                                        ResetIndexAndPage();
                                        ResetEntitiesOnThePage();
                                    }
                                }
                                else
                                    CH.Feedback($"No action - Old item : {EntityItem?.Name} - {OldGUID} - New Item: {Name} - {GUID}");
                            }
                        }
                        SkipFirstLine = false;
                    }

                    ResetIndexAndPage(true);
                    return true;
                }
                else
                {
                    if (SessionID != null)
                        L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error - Import file not found", SessionID, 8);
                    CH.Feedback($"Loading The Items Failed - {path}");
                }
            }
            catch (Exception ex)
            {
                if (SessionID != null)
                    L.Log($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name} - Error message - {ex.Message}", SessionID, 9);
                CH.Feedback($"Error in the loading of the items {ex.Message} {path}");
            }
            return false;
        }
        public bool Refresh(E.Navigation navigate = E.Navigation.FirstPage, string heading = "")
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            ResetIndexAndPage();
            var orderedEntities = Repository?.OrderByIndex();
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, navigate);
            return true;
        }        
        public bool Remove(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }

            //List the items
            var Current = ViewAndSelectItem(simInput?[0], "Select the item to remove");

            if (Repository != null && Current != null)
                if (Repository.Remove(Current))
                {
                    CH.Feedback("Item removed.");
                    ResetIndexAndPage(true);
                    return true;
                }
                else
                    CH.Feedback("Item was not removed.");
            else
            {
                if (EntitiesOnThePage != null)
                    CH.Feedback("Nothing selected.");
            }

            ResetIndexAndPage(true);

            return false;
        }       
        public bool Save(int? simChoice = null, string[]? simInput = null, string Path = "~Entity~")
		{
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            if (Repository?.Count() > 0)
            {
                StringBuilder sb = new();
                try
                {
                    sb.AppendLine($"Name|Guid_");
                    var OrderedList = Repository.OrderByName();
                    if (OrderedList != null)
                    {
                        foreach (Entity item in OrderedList)
                        {
                            sb.AppendLine($"{item.Name}|{item.Guid_}");
                        }
                        if (File.Exists(Path))
                            File.Delete(Path);
                        File.AppendAllText(Path, sb.ToString());
                        CH.Feedback($"Saved the item successfully - {U.GetCurrentDate()}");
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    CH.Feedback($"Items was not saved. - Error {ex.Message}");
                    return false;
                }
                finally
                {
                    sb?.Clear();
                }
            }
            else
            {
                CH.Feedback("No data to save");
            }
            return false;
        }
        
        public void SetLinkItem(string? simInput, Entity entity, bool linked = true)
        {
            entity.IsLinked = linked;
        }
        public Entity? ViewAndSelectItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        public Entity? ViewAndSelectLinkedItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex()?.Where(p => p.IsLinked).ToList();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        public Entity? ViewAndSelectLinkItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex()?.Where(p => !p.IsLinked).ToList();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        internal string? GetGuid(string? name)
        {
            if (name == null) { return null; }
            return Repository?.GetByName(name)?.ToList()[0].Guid_;
        }
        internal string? GetName(string? guid)
        {
            if (guid == null) { return null; }
            return Repository?.GetByGUID(guid)?.ToList()[0].Name;
        }

        private void ResetIndexAndPage(bool resetEntitiesOnThePage = false)
        {
            ReIndexDisplayId();
            ResetPageMaxCount();
            if (resetEntitiesOnThePage)
                ResetEntitiesOnThePage();
        }
        private Entity? ViewAndSelectInternal(string? simInput, string heading, E.Navigation navigation, List<Entity>? orderedEntities)
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, navigation);
            if (EntitiesOnThePage == null) { return null; }
            CH.Feedback(heading);
            _ = int.TryParse(CH.GetInput(simInput), out int Choice);
            return EntitiesOnThePage?.FirstOrDefault(p => p.DisplayId == Choice);
        }
        public void ResetEntitiesOnThePage()
        {
            if (SessionID != null) { L.Log("Entities was reset.", SessionID, 4); }
            EntitiesOnThePage = null;
        }
        public void ResetPageMaxCount()
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(Repository?.Count()));
        }
        public void ReIndexDisplayId()
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            var OrderedModels = Repository?.OrderByName();
            if (OrderedModels != null)
                for (int i = 0; i < OrderedModels.Count; i++)
                {
                    Entity? item = OrderedModels[i];
                    item.DisplayId = i + 1;
                    item.Id = item.DisplayId;
                    item.Index = item.DisplayId;
                }
        }
        public bool LoadDefaults()
        {
            Repository?.All()?.ToList(); ReIndexDisplayId(); return true;
        }
        public bool DuplicateFound(string Input)
        {
            return Repository?.GetByName(Input)?.Count() > 0;
        }                
        public void Clear()
        {
            Repository?.ClearAll();
        }        
    }
}