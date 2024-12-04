using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using ArchCorpUtilities.Models.Helper;
using System.Text;
using AL = ArchCorpUtilities.Models.ArchLoader;
using E = EnumLib.EnumLib;
using ArchCorpUtilities.Models;
namespace ArchCorpUtilities.GeneratedModels.~Entity~Model
{
    public class ~Entity~Helper : IHelper<Hierarchy>, IDisposable
	{
        public string? SessionID { get; set; }
        public List<Hierarchy>? EntitiesOnThePage { get; set; }
        public Patina.Patina? Page { get; set; }
        public MockRepositoryHierarchy<Hierarchy, Entity>? Repository { get; set; }
        public ~Entity~Helper(string? sessionID, string postfix = "")
		{
            SessionID = sessionID;
            Repository = new(postfix);
            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Repository?.Count()));
		}
        public bool View(E.Navigation navigate = E.Navigation.FirstPage, string postFix = "")
		{
            EntitiesOnThePage = U.View(navigate, postFix , Page, Repository?.OrderByIndex(), System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);
            return true;
		}
        public bool Add(int? simChoice = null, string[]? simInput = null)
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            CH.Feedback("Please provide the item name: ");
            var Input = CH.GetInput(simInput?[0]);
            if(!string.IsNullOrWhiteSpace(Input))
            {

                if (DuplicateFound(Input))
                {
                    CH.Feedback("Duplicate entry found - operation aborted.");
                    return false;
                }

                string? ParentGuid = null;
                string? ChildGuid = null;

                var InputLinks = "";
				bool Continue = true;
                AL.~LhLink~Helper?.ReIndexDisplayId();
                while (Continue)
                {
                    var Parent = AL.~LhLink~Helper;
                    var selectionHeading = "Select the first item (Parent) from the ~LhLink~ to be linked.";
                    var heading = "Select the ~LhLink~ item";
                    if (AL.~LhLink~Helper?.Repository?.GetLinked()?.Count() == 0)
                    {
						//All items,
						ParentGuid = U.SelectEntityFromTheList(simInput, ref InputLinks, heading, selectionHeading, Parent)?.Guid_;
						if (InputLinks.ToLower() == "a") { Continue = false; }
					}
					else
					{
						//Only what is selected,
						ParentGuid = U.SelectEntityFromTheList(simInput, ref InputLinks, heading, selectionHeading, Parent, false)?.Guid_;
                        if (InputLinks.ToLower() == "a") { Continue = false; }
                    }
                    
                    if (Continue)
                    {
                        var Child = AL.~LhLink~Helper;
                        selectionHeading = "Select the item from the ~LhLink~ (Child) (To link to the first item.)";
                        heading = "Select the ~LhLink~ item";
                        ChildGuid = U.SelectEntityFromTheList(simInput, ref InputLinks, heading, selectionHeading, Child, true)?.Guid_;
                        if (InputLinks.ToLower() == "a") { Continue = false; }
                    }
                    
                    if (Continue)
                        Continue = !(U.IsValidGuid(ParentGuid) && U.IsValidGuid(ChildGuid));
                }

                if (ParentGuid != null && ChildGuid != null && U.IsValidGuid(ParentGuid) && U.IsValidGuid(ChildGuid))
                {
                    Hierarchy Entity_ = new (Input, 0, "", ParentGuid, ChildGuid);
                    Entity_.LinkRepository = AL.~LhLink~Helper?.Repository;
                    Repository?.Add(Entity_);
                    CH.Feedback("Item added.");
                    ResetPageMaxCount();
                    ReIndexDisplayId();
                    return true;
                }
                else
                    CH.Feedback("Invalid name or empty or invalid links - No item added.");

                return false;
            }
            else
                CH.Feedback("Invalid Name or empty - No item added.");

            return false;
        }
        public void Dispose()
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            GC.SuppressFinalize(this);
        }
        public bool Edit(int? simChoice, string[]? simInput)
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
	        try
	        {
	            Hierarchy? Entity = ViewAndSelectItem(simInput?[0], "Select an item to edit");
	
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
	                        ResetPageMaxCount();
	                        ReIndexDisplayId();
	                        ResetEntitiesOnThePage();
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
	
	            ResetPageMaxCount();
	            ReIndexDisplayId();
	            ResetEntitiesOnThePage();
	
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
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            return !(EntitiesOnThePage == null || (EntitiesOnThePage != null && EntitiesOnThePage.Count == 0));
        }
        public bool Search(int? simChoice = null, string[]? simInput = null)
			{
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            CH.Feedback("Enter an item name to search for");

            var Input = CH.GetInput(simInput?[0]);

            if (string.IsNullOrWhiteSpace(Input))
                CH.Feedback("No Items Name Was Entered");
            else 
            {
                List<Hierarchy>? Entities = Repository?.GetAllContainingName(Input);
                if (Entities != null && Entities.Count > 0)
                {
                    Page = new Patina.Patina(5, Convert.ToUInt32(Entities.Count));
                    EntitiesOnThePage = U.ViewWithPagination("Search Items", Page, Entities);
                    return true;
                }
                else 
                {
                    CH.Feedback("No Items Was Found");
                    EntitiesOnThePage = [new Hierarchy("None", 1)];
                    Page = new Patina.Patina(1, 1);
                    return false;
                }
            }

            ReIndexDisplayId();
            ResetPageMaxCount();

            return false;
			}
        public bool Refresh(E.Navigation navigate = E.Navigation.FirstPage, string heading = "")
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            ReIndexDisplayId();
            ResetPageMaxCount();
            var orderedEntities = Repository?.OrderByIndex();
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, navigate);
            return true;
        }
        public bool Remove(int? simChoice = null, string[]? simInput = null)
			{
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}

            //List the items
            var Current~Entity~ = ViewAndSelectItem(simInput?[0], "Select the item to remove");

            if (Repository != null && Current~Entity~ != null)
                if (Repository.Remove(Current~Entity~))
                {
                    CH.Feedback("Item removed.");
                    ResetPageMaxCount();
                    ReIndexDisplayId();
                    ResetEntitiesOnThePage();
                    return true;
                }
                else
                    CH.Feedback("Item was not removed.");
            else
                {
                if (EntitiesOnThePage != null)
                    CH.Feedback("Nothing selected.");
                }

            ReIndexDisplayId();
            ResetPageMaxCount();
            ResetEntitiesOnThePage();

            return false;
			}
        public bool Save(int? simChoice = null, string[]? simInput = null, string Path = "~Entity~")
		{
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
			if (Repository?.Count() > 0)
			{
				StringBuilder sb = new();
				try
					{
					sb.AppendLine($"BuidlingTreeName|BuidlingTreeGuid|ParentGuid|ParentName|ChildGuid|ChildName");
					var OrderedList = Repository.OrderByName();
					if(OrderedList != null)
					{
						foreach (Hierarchy item in OrderedList)
						{
							sb.AppendLine($"{ item.Name}|{ item.Guid_}|{item.Parent}|{AL.~Entity~Helper?.GetName(item.Parent)}|{item.Child}|{AL.~Entity~Helper?.GetName(item.Child)}");
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
        public bool Load(int? simChoice = null, string[]? simInput = null, string path = "~Entity~")
		{
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
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
                                var Parent = LineItem[2];
                                var Child = LineItem[4];

								if (Entity == null)
								{
									if (EntityItem != null)
									{
										CH.Feedback($"Item Exists - No action - Old item: { EntityItem.Name} - {OldGUID} - New Item: {Name} - {GUID}");
									}
									else
									{
									if (SessionID != null)
										L.Log($"Item found - {Name}", SessionID);
									Repository?.Add(new Hierarchy(Name, 0, GUID, Parent, Child));
									CH.Feedback($"Item Added - New Item: {Name} - {GUID} - {Parent} - {Child}");
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
	
	                ReIndexDisplayId();
	                ResetPageMaxCount();
	                ResetEntitiesOnThePage();
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
        public void ReIndexDisplayId()
        {
			if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            var OrderedModels = Repository?.OrderByName();
            if (OrderedModels != null)
                for (int i = 0; i < OrderedModels.Count; i++)
                {
                    Hierarchy? item = OrderedModels[i];
                    item.DisplayId = i + 1;
                    item.Id = item.DisplayId;
                    item.Index = item.DisplayId;
                }
        }
        public void ResetPageMaxCount()
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(Repository?.Count()));
        }
        public Hierarchy? ViewAndSelectItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        private Hierarchy? ViewAndSelectInternal(string? simInput, string heading, E.Navigation navigation, List<Hierarchy>? orderedEntities)
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, navigation);
            if (EntitiesOnThePage == null) { return null; }
            CH.Feedback(heading);
            _ = Int32.TryParse(CH.GetInput(simInput), out int Choice);
            return EntitiesOnThePage?.FirstOrDefault(p => p.DisplayId == Choice);
        }
        public Hierarchy? ViewAndSelectLinkItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex()?.Where(p => !p.IsLinked).ToList();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        public Hierarchy? ViewAndSelectLinkedItem(string? simInput, string heading, E.Navigation navigation = E.Navigation.FirstPage)
        {
            var orderedEntities = EntitiesOnThePage ?? Repository?.OrderByIndex()?.Where(p => p.IsLinked).ToList();
            return ViewAndSelectInternal(simInput, heading, navigation, orderedEntities);
        }
        public void SetLinkItem(string? simInput, Hierarchy entity, bool linked = true)
        {
            entity.IsLinked = linked;
        }
        public void ResetEntitiesOnThePage()
        {
            if (SessionID != null) { L.Log("Entities was reset.", SessionID, 4);}
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
        public void Clear()
        {
            Repository?.ClearAll();
        }        
	}
}