// Generated Code - Version: 23.11.25 - 2024/11/20 17:29:23 - {64b62547-252c-4706-a962-7f21c734af25}
using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using ArchCorpUtilities.Models.Helper;
using System.Text;
using AL = ArchCorpUtilities.Models.ArchLoader;
namespace ArchCorpUtilities.GeneratedModels.RolesModel
{
    public class RolesHelper : IHelper<Roles>, IDisposable
	{
        public string? SessionID { get; set; }
        public List<Roles>? Items { get; set; }
        public List<Roles>? EntitiesOnThePage { get; set; }
        public Patina.Patina Page { get; set; }
        public RolesMockRepository<Roles> Repository { get; set; }
       public RolesHelper(string? sessionID)
		{
            SessionID = sessionID;
            Items = [];
            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Items?.Count));
            Repository = new("-Roles");
		}
        public bool View(U.Navigation navigate = U.Navigation.FirstPage)
			{
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            var orderedEntities = Items?.OrderBy(p => p.Index).ToList();
           EntitiesOnThePage = U.ViewWithPagination("Roles", Page, orderedEntities, navigate);
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

                Items?.Add(new(Input, 0));
                CH.Feedback("Item added.");
                ResetPageMaxCount();
                ReIndexDisplayId();
                return true;
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
	            Roles? Entity = ViewAndSelectItem(simInput?[0], "Select an item to edit");
	
	            if (Entity != null)
	            {
	                CH.Feedback("Please enter a new name");
	                var Input = CH.GetInput(simInput?[1]);
	                if (!string.IsNullOrWhiteSpace(Input))
	                {
	                    var NotFound = Items?.FirstOrDefault(i => (i.Name != null && i.Name.Equals(Input, StringComparison.CurrentCultureIgnoreCase)));
	                    if (NotFound == null)
	                    {
	                        EntitiesOnThePage = [new(Input, 0)];
	                        Items?.Add(EntitiesOnThePage[0]);
	                        if (Entity != null)
		                      Items?.Remove(Entity);
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

                List<Roles>? Entities = Items?.Where(c => c.Name != null && c.Name.ToUpper().Contains(Input.ToUpper(), StringComparison.CurrentCultureIgnoreCase)).ToList<Roles>();

                if (Entities != null && Entities.Count > 0)
                {
                    Page = new Patina.Patina(5, Convert.ToUInt32(Entities.Count));
                    EntitiesOnThePage = U.ViewWithPagination("Search Items", Page, Entities);
                    return true;
                }
                else 
                {
                    CH.Feedback("No Items Was Found");
                    EntitiesOnThePage = [new Roles("None", 1)];
                    Page = new Patina.Patina(1, 1);
                    return false;
                }
            }

            ReIndexDisplayId();
            ResetPageMaxCount();

            return false;
			}
        public bool Refresh(List<Roles> modelList, U.Navigation navigate = U.Navigation.FirstPage)
        {
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
            ReIndexDisplayId();
            ResetPageMaxCount();
            var orderedEntities = modelList?.OrderBy(p => p.Index).ToList();
           EntitiesOnThePage = U.ViewWithPagination("Roles", Page, orderedEntities, navigate);
            return true;
        }
        public bool Remove(int? simChoice = null, string[]? simInput = null)
			{
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}

            //List the items
            var CurrentRoles = ViewAndSelectItem(simInput?[0], "Select the item to remove");

            if (Items != null && CurrentRoles != null)
                if (Items.Remove(CurrentRoles))
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
        public bool Save(int? simChoice = null, string[]? simInput = null)
			{
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}

            var Path = "Roles";

            if (Items != null && Items.Count > 0)
            {
                StringBuilder sb = new();
                try
                {
                    sb.AppendLine($"RolesName|RolesGuid");
                    foreach (var item in Items.OrderBy(c => c.Name))
                    {
                        sb.AppendLine($"{ item.Name}|{ item.RolesGuid}");
                    }

                    if (File.Exists(Path))
                        File.Delete(Path);

                    File.AppendAllText(Path, sb.ToString());
                    CH.Feedback($"Saved the item successfully - {U.GetCurrentDate()}");
                    return true;
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
        public bool Load(int? simChoice = null, string[]? simInput = null)
			{
            if (SessionID != null) { L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);}
	        var path = "Roles";
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
	                            var Entity = Items?.FirstOrDefault(c => c.RolesGuid == GUID);
	                            var EntityItem = Items?.FirstOrDefault(c => c.Name == Name);
								var OldGUID = CH.IsSimulate ? "<GUID>" : EntityItem?.RolesGuid;

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
									Items?.Add(new Roles(Name, 0, GUID));
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
            var OrderedModels = Items?.OrderBy(c => c.Name).ToList();
            if (OrderedModels != null)
                for (int i = 0; i < OrderedModels.Count; i++)
                {
                    Roles? item = OrderedModels[i];
                    item.DisplayId = i + 1;
                    item.Id = item.DisplayId;
                    item.Index = item.DisplayId;
                }
        }
        public void ResetPageMaxCount()
        {
            Page = new Patina.Patina(5, Convert.ToUInt32(Items?.Count));
        }
        private Roles? ViewAndSelectItem(string? simInput, string heading)
        {
            var orderedEntities = EntitiesOnThePage ?? Items?.OrderBy(p => p.Index).ToList();
            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, U.Navigation.FirstPage);
            if (EntitiesOnThePage == null) { return null; }
            CH.Feedback(heading);
            _ = Int32.TryParse(CH.GetInput(simInput), out int Choice);
            return EntitiesOnThePage?.FirstOrDefault(p => p.DisplayId == Choice);
        }
        private void ResetEntitiesOnThePage()
        {
            if (SessionID != null) { L.Log("Entities was reset.", SessionID, 4);}
               EntitiesOnThePage = null;
        }
        public bool LoadDefaults()
        {
            Items?.Clear(); Items = Repository.All()?.ToList(); return true;
        }
        private bool DuplicateFound(string Input)
        {
            return Items?.FirstOrDefault(p => p.Name != null && p.Name.Length > 0 && p.Name.Equals(Input)) != null;
        }
        internal string? GetName(string? guid)
        {
            return Items?.FirstOrDefault(p => p.RolesGuid != null && p.RolesGuid.Equals(guid))?.Name;
        }
        internal string? GetGuid(string? name)
        {
            return Items?.FirstOrDefault(p => p.Name != null && p.Name.Equals(name))?.RolesGuid;
        }
	}
}
