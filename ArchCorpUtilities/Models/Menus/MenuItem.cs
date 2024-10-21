namespace ArchCorpUtilities.Models.Menus
{
    public class MenuItem
    {
        public string IDGUIDMenu { get; }
        public string DisplayMenuItem { get; }
        public bool IsExitOption { get; }
        public int Index { get; }
        public int ParentPage { get; }
        public int Page { get; }
        public int DisplayNumber { get; }
        public string DisplayName { get; }
        public string PageHeading { get; }
        public int TargetPage { get; }
        public string TargetTask { get; }
        public bool IsBack { get; }
        public bool IsDefaultChoice { get; }

        public MenuItem(
            string displayName,
            int displayNumber,
            int index,
            int page,
            string pageHeading,
            int parentPage = 0,
            bool isExitOption = false,
            int targetPage = 0,
            string targetTask = "None",
            bool isBack = false,
            bool isDefaultChoice = false
            )
        {
            IDGUIDMenu = Guid.NewGuid().ToString();
            DisplayNumber = displayNumber;
            DisplayName = displayName;
            DisplayMenuItem = $"{DisplayNumber}) {DisplayName}";
            IsExitOption = isExitOption;
            Index = index;
            ParentPage = parentPage;
            Page = page;
            PageHeading = pageHeading;
            TargetPage = targetPage;
            TargetTask = targetTask;
            IsBack = isBack;
            IsDefaultChoice = isDefaultChoice;
        }
    }
}