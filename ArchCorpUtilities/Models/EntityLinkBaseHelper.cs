using ArchCorpUtilities.Models.Helper;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using E = EnumLib.EnumLib;
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using System.Text;
using AL = ArchCorpUtilities.Models.ArchLoader;

namespace ArchCorpUtilities.Models
{
    public class EntityLinkBaseHelper <T,Q,R> : IHelper<EntityLinkBase>, IDisposable where Q : EntityBase, new() where R : EntityBase, new()
    {
        public string? SessionID { get; set; }
        public List<EntityLinkBase>? EntitiesOnThePage { get; set; }
        public Patina.Patina? Page { get; set; }
        public MockRepositoryLink<EntityLinkBase, Q, R>? Repository { get; set; }
        public EntityLinkBaseHelper(string? sessionID, string postFix = "", string postFixLhLink = "", string postFixRhLink = "")
        {
            SessionID = sessionID;
            Repository = new(postFix, postFixLhLink, postFixRhLink);
            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Repository?.Count()));
        }
        public bool View(E.Navigation navigate = E.Navigation.FirstPage, string postFix = "EntityLinkBase")
        {
            EntitiesOnThePage = U.View(navigate, postFix, Page, Repository?.OrderByIndex(), System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            return true;
        }
        public bool Add(int? simChoice = null, string[]? simInput = null)
        {
            U.Log(SessionID, System.Reflection.MethodBase.GetCurrentMethod()?.Name);
            CH.Feedback("Please provide the item name: ");
            var Input = CH.GetInput(simInput?[0]);
            if (!string.IsNullOrWhiteSpace(Input))
            {
                if (DuplicateFound(Input))
                {
                    CH.Feedback("Duplicate entry found - operation aborted.");
                    return false;
                }

                string? LhGuid = null;
                string? RhGuid = null;

                var InputLinks = "";

                while (!(U.IsValidGuid(LhGuid) && U.IsValidGuid(RhGuid)))
                {
                    //var LhHelper = AL.BuildingsHelper;
                    //var RhHelper = AL.BuildingTypesHelper;
                    //var selectionHeading = "Select the item from the Buildings";
                    //var heading = "Select the Buildings item";
                    //LhGuid = U.SelectEntityFromTheList(simInput, ref InputLinks, heading, selectionHeading, LhHelper)?.Guid_;
                    //RhGuid = U.SelectEntityFromTheList(simInput, ref InputLinks, heading, selectionHeading, RhHelper)?.Guid_;
                    //if (InputLinks.Equals("a", StringComparison.CurrentCultureIgnoreCase)) { break; }
                }

                if (U.IsValidGuid(LhGuid) && U.IsValidGuid(RhGuid))
                {
                    Repository?.Add(new(Input, 0, LhGuid, RhGuid, "", ""));
                    CH.Feedback("Item added.");
                    ResetPageMaxCount();
                    ReIndexDisplayId();
                    return true;
                }
                else
                    CH.Feedback("Invalid name or empty or invalid links - No item added.");

                return false;
            }

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
                EntityLinkBase? Entity = ViewAndSelectItem(simInput?[0], "Select an item to edit");

                if (Entity != null)
                {
                    CH.Feedback("Please enter a new name");
                    var Input = CH.GetInput(simInput?[1]);
                    if (!string.IsNullOrWhiteSpace(Input))
                    {
                        var NotFound = Repository?.GetByName(Input);
                        if (NotFound == null)
                        {
                            EntitiesOnThePage = [new(Input, 0, Entity?.LhGuid, Entity?.RhGuid, Entity?.RhName, Entity?.LhName)];
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
                List<EntityLinkBase>? Entities = Repository?.GetAllContainingName(Input);
                if (Entities != null && Entities.Count > 0)
                {
                    Page = new Patina.Patina(5, Convert.ToUInt32(Entities.Count));
                    EntitiesOnThePage = U.ViewWithPagination("Search Items", Page, Entities);
                    return true;
                }
                else
                {
                    CH.Feedback("No Items Was Found");
                    EntitiesOnThePage = [new EntityLinkBase("None", 1, null, null,null, null)];
                    Page = new Patina.Patina(1, 1);
                    return false;
                }
            }

            ResetIndexAndPage();

            return false;
        }
        public bool Refresh(E.Navigation navigate = E.Navigation.FirstPage, string heading = "EntityLinkBase")
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            ResetIndexAndPage();
            var orderedEntities = Repository?.OrderByIndex();
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, navigate);
            return true;
        }
        public bool Remove(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }

            //List the items
            var CurrentEntityLinkBase = ViewAndSelectItem(simInput?[0], "Select the item to remove");

            if (Repository != null && CurrentEntityLinkBase != null)
                if (Repository.Remove(CurrentEntityLinkBase))
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
        public bool Save(int? simChoice = null, string[]? simInput = null, string Path = "EntityLinkBase")
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            if (Repository?.Count() > 0)
            {
                StringBuilder sb = new();
                try
                {
                    sb.AppendLine($"EntityLinkBaseName|lhLink|rhLink|lhLinkName|rhLinkName|Guid|lhLinkGuid|rhLinkGuid");
                    var OrderedList = Repository.OrderByName();
                    if (OrderedList != null)
                    {
                        foreach (EntityLinkBase item in OrderedList)
                        {
                            sb.AppendLine($"{item.Name}|Buildings|BuildingTypes|{item.LhName}|{item.RhName}|{item.Guid_}|{item.LhGuid}|{item.RhGuid}");
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
        public bool Load(int? simChoice = null, string[]? simInput = null, string path = "EntityLinkBase")
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
                                string GUID = CH.IsSimulate ? "<GUID>" : LineItem[5].Trim();
                                string lhLinkGuid = LineItem[6].Trim();
                                string rhLinkGuid = LineItem[7].Trim();
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
                                        Repository?.Add(new EntityLinkBase(Name, 0, lhLinkGuid, rhLinkGuid, "", "",GUID));
                                        CH.Feedback($"Item Added - New Item: {Name} - {GUID}");
                                        ReIndexDisplayId();
                                        ResetPageMaxCount();
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

        private void ResetIndexAndPage(bool resetEntitiesOnThePage = false)
        {
            ReIndexDisplayId();
            ResetPageMaxCount();
            if (resetEntitiesOnThePage)
                ResetEntitiesOnThePage();
        }

        public void ReIndexDisplayId()
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID); }
            var OrderedModels = Repository?.OrderByName();
            if (OrderedModels != null)
                for (int i = 0; i < OrderedModels.Count; i++)
                {
                    EntityLinkBase? item = OrderedModels[i];
                    item.DisplayId = i + 1;
                    item.Id = item.DisplayId;
                    item.Index = item.DisplayId;
                }
        }
        public void ResetPageMaxCount()
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(Repository?.Count()));
        }
        public EntityLinkBase? ViewAndSelectItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        private EntityLinkBase? ViewAndSelectInternal(string? simInput, string heading, E.Navigation navigation, List<EntityLinkBase>? orderedEntities)
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, navigation);
            if (EntitiesOnThePage == null) { return null; }
            CH.Feedback(heading);
            _ = int.TryParse(CH.GetInput(simInput), out int Choice);
            return EntitiesOnThePage?.FirstOrDefault(p => p.DisplayId == Choice);
        }
        public EntityLinkBase? ViewAndSelectLinkItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex()?.Where(p => !p.IsLinked).ToList();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        public EntityLinkBase? ViewAndSelectLinkedItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex()?.Where(p => p.IsLinked).ToList();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        public void SetLinkItem(string? simInput, EntityLinkBase entity, bool linked = true)
        {
            entity.IsLinked = linked;
        }
        public void ResetEntitiesOnThePage()
        {
            if (SessionID != null) { L.Log("Entities was reset.", SessionID, 4); }
            EntitiesOnThePage = null;
        }
        public bool LoadDefaults()
        {
            Repository?.All()?.ToList(); ReIndexDisplayId(); return true;
        }
        public bool DuplicateFound(string Input)
        {
            return Repository?.GetByName(Input)?.Count() > 0;
        }
        internal string? GetName(string? guid)
        {
            if (guid == null) { return null; }
            return Repository?.GetByGUID(guid)?.ToList()[0].Name;
        }
        internal string? GetGuid(string? name)
        {
            if (name == null) { return null; }
            return Repository?.GetByName(name)?.ToList()[0].Guid_;
        }
    }
}
