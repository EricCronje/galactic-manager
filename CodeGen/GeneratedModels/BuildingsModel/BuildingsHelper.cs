// Generated Code - Version: 17.11.23 - 2024/11/13 16:36:49 - {1b17a421-2756-4978-aa59-8e0a01171811}


using L = Logger.Logger;
using U = ArchCorpUtilities.Utilities.UniversalUtilities;
using CH = ArchCorpUtilities.Utilities.ConsoleHelper;
using ArchCorpUtilities.Models.BuildingsModel.Buildings;
using ArchCorpUtilities.Models.Helper;
using System.Text;

namespace ArchCorpUtilities.GeneratedModels.BuildingsModel
{
    public class BuildingsHelper : IHelper<Buildings>, IDisposable
	{
        public string? SessionID { get; set; }
        public List<Buildings>? Items { get; set; }
        public List<Buildings>? EntitiesOnThePage { get; set; }
        public Patina.Patina Page { get; set; }

       public BuildingsHelper(string? sessionID)
		{
            SessionID = sessionID;
            Items = MockData();
            Page = new(Convert.ToUInt32(5), Convert.ToUInt32(Items?.Count));
		}
        private List<Buildings>? MockData()
			{
            if (Items == null || Items.Count == 0)
                Items = [];

            Items.Add(new Buildings("Alpha-Buildings", 1));
            Items.Add(new Buildings("Beta-Buildings", 2));
            Items.Add(new Buildings("Charlie-Buildings", 3));
            Items.Add(new Buildings("Delta-Buildings", 3));
            Items.Add(new Buildings("Echo-Buildings", 3));
            Items.Add(new Buildings("Foxtrot-Buildings", 3));
            Items.Add(new Buildings("Golf-Buildings", 3));
            Items.Add(new Buildings("Hotel-Buildings", 3));
            return Items;
			}

        public bool View(U.Navigation navigate = U.Navigation.FirstPage)
			{
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            var orderedEntities = Items?.OrderBy(p => p.Index).ToList();
           EntitiesOnThePage = U.ViewWithPagination("Buildings", Page, orderedEntities, navigate);
            return true;
			}

        public bool Add(int? simChoice = null, string[]? simInput = null)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Please provide the item name: ");
            var Input = CH.GetInput(simInput?[0]);
            if(!string.IsNullOrWhiteSpace(Input))
            {
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
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            GC.SuppressFinalize(this);
        }

        public bool Edit(int? simChoice, string[]? simInput)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

	        try
	        {
	            Buildings? Entity = ViewAndSelectItem(simInput?[0], "Select an item to edit");
	
	            if (Entity != null)
	            {
	                CH.Feedback("Please enter a new name");
	                var Input = CH.GetInput(simInput?[1]);
	                if (!string.IsNullOrWhiteSpace(Input))
	                {
	                    var NotFound = Items?.FirstOrDefault(i => (i.Name != null && i.Name.Equals(Input, StringComparison.CurrentCultureIgnoreCase)));
	                    if (NotFound == null)
	                    {
	                        Items?.Add(new(Input, 0));
	                        Items?.Remove(Entity);
	                        Page = new Patina.Patina(1, 1);
	                        EntitiesOnThePage = [new(Input, 0)];
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
	                CH.Feedback("No Item selected");
	
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
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            return !(EntitiesOnThePage == null || (EntitiesOnThePage != null && EntitiesOnThePage.Count == 0));
        }

        public bool Search(int? simChoice = null, string[]? simInput = null)
			{
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            CH.Feedback("Enter an item name to search for");

            var Input = CH.GetInput(simInput?[0]);

            if (string.IsNullOrWhiteSpace(Input))
                CH.Feedback("No Items Name Was Entered");
            else 
            {

                List<Buildings>? Entities = Items?.Where(c => c.Name != null && c.Name.ToUpper().Contains(Input.ToUpper(), StringComparison.CurrentCultureIgnoreCase)).ToList<Buildings>();

                if (Entities != null && Entities.Count > 0)
                {
                    Page = new Patina.Patina(5, Convert.ToUInt32(Entities.Count));
                    EntitiesOnThePage = U.ViewWithPagination("Search Items", Page, Entities);
                    return true;
                }
                else 
                {
                    CH.Feedback("No Items Was Found");
                    EntitiesOnThePage = [new Buildings("None", 1)];
                    Page = new Patina.Patina(1, 1);
                    return false;
                }
            }

            ReIndexDisplayId();
            ResetPageMaxCount();

            return false;
			}

        public bool Refresh(List<Buildings> modelList, U.Navigation navigate = U.Navigation.FirstPage)
        {
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            ReIndexDisplayId();
            ResetPageMaxCount();
            var orderedEntities = modelList?.OrderBy(p => p.Index).ToList();
           EntitiesOnThePage = U.ViewWithPagination("Buildings", Page, orderedEntities, navigate);
            return true;
        }

        public bool Remove(int? simChoice = null, string[]? simInput = null)
			{
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);


            //List the items
            var CurrentBuildings = ViewAndSelectItem(simInput?[0], "Select the item to remove");

            if (Items != null && CurrentBuildings != null)
                if (Items.Remove(CurrentBuildings))
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
                CH.Feedback("Nothing selected.");

            ReIndexDisplayId();
            ResetPageMaxCount();
            ResetEntitiesOnThePage();

            return false;
			}

        public bool Save(int? simChoice = null, string[]? simInput = null)
			{
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);


            var Path = "Buildings";

            if (Items != null && Items.Count > 0)
            {
                StringBuilder sb = new();
                try
                {
                    sb.AppendLine($"BuildingsName|BuildingsGuid");
                    foreach (var item in Items.OrderBy(c => c.Name))
                    {
                        sb.AppendLine($"{ item.Name}|{ item.BuildingsGuid}");
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
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);

	        var path = "Buildings";
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
	                            var Entity = Items?.FirstOrDefault(c => c.BuildingsGuid == GUID);
	                            var EntityItem = Items?.FirstOrDefault(c => c.Name == Name);
                                var OldGUID = CH.IsSimulate ? "<GUID>" : EntityItem?.BuildingsGuid;

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
                                            Items?.Add(new Buildings(Name, 0, GUID));
                                            CH.Feedback($"Item Added - New Item: {Name} - {GUID}");
                                            ReIndexDisplayId();
                                            ResetPageMaxCount();
                                            ResetEntitiesOnThePage();
                                            return true;
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
            if (SessionID != null)
                L.Log(System.Reflection.MethodBase.GetCurrentMethod()?.Name, SessionID);


            var OrderedModels = Items?.OrderBy(c => c.Name).ToList();

            int counter = 1;
            if (OrderedModels != null)
            {
                foreach (var item in OrderedModels)
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
        private Buildings? ViewAndSelectItem(string? simInput, string heading)
        {
            var orderedEntities = EntitiesOnThePage ?? Items?.OrderBy(p => p.Index).ToList();
            Page = new Patina.Patina(5, Convert.ToUInt32(orderedEntities?.Count));
            EntitiesOnThePage = U.ViewWithPagination(heading, Page, orderedEntities, U.Navigation.FirstPage);

            CH.Feedback(heading);

            string ItemInput = CH.GetInput(simInput);
            //If none selected - do nothing
            if (!string.IsNullOrWhiteSpace(ItemInput))
            {
                _ = Int32.TryParse(ItemInput, out int Choice);
                //If item selected
                Buildings? SelectedEntity = Items?.FirstOrDefault(p => p.DisplayId == Choice);
                if (SelectedEntity != null)
                {
                    return SelectedEntity;
                }
            }
            return null;
        }

        private void ResetEntitiesOnThePage()
        {
            if (SessionID != null)
                L.Log("Entities was reset.", SessionID, 4);

            EntitiesOnThePage = null;
        }

        public bool LoadDefaults()
        {
            Items?.Clear();
            Items = MockData();
            return true;
        }
    }
}