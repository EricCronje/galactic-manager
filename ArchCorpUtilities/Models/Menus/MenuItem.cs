using ArchCorpUtilities.Utilities;
using E = EnumLib.EnumLib;
using MEL = MenuEnumLib.MenuEnumLib;

namespace ArchCorpUtilities.Models.Menus
{
    public class MenuItem
    {
        public string IDGUIDMenu { get; }
        public string DisplayMenuItem { get; set; }
        public bool IsExitOption { get; }
        public int Index { get; set; }
        public int ParentPage { get; }
        public int Page { get; }
        public int DisplayNumber { get; set; }
        public string DisplayName { get; set; }
        public string PageHeading { get; }
        public int TargetPage { get; }
        public string TargetTask { get; }
        public bool IsBack { get; }
        public bool IsDefaultChoice { get; }
        public string? HideRule { get; }
        public bool IsHidden { get; set; }
        public bool IsPagination { get; set; }
        public string? Source { get; set; }
        public bool IsStartPage { get; }
        public E.MenuDomain Domain { get; set; }
        public int Level { get; set; }
        public MEL.MenuActionEnum Action { get; set; }

        public MenuItem(
            string displayName,
            int page,
            string pageHeading,
            bool isExitOption = false,
            int targetPage = 0,
            string targetTask = "None",
            bool isBack = false,
            bool isDefaultChoice = false,
            string? hideRule = "None",
            bool isHidden = false,
            string? source = "Internal",
            string? idGuidMenu = null,
            E.MenuDomain domain = E.MenuDomain.None,
            bool isStartPage = false,
            int level = 0
            )
        {
            if (string.IsNullOrWhiteSpace(idGuidMenu))
            {
                IDGUIDMenu = Guid.NewGuid().ToString();
            }
            else
            {
                IDGUIDMenu = idGuidMenu;
            }

            DisplayName = displayName;
            DisplayMenuItem = $"{DisplayNumber}) {DisplayName}";
            IsExitOption = isExitOption;
            Page = page;
            PageHeading = pageHeading;
            TargetPage = targetPage;
            TargetTask = targetTask;
            IsBack = isBack;
            IsDefaultChoice = isDefaultChoice;
            HideRule = hideRule;
            IsHidden = isHidden;
            Source = source;
            IsStartPage = isStartPage;
            Domain = domain;
            Level = level;
        }

        public override string ToString()
        {
            return $"{DisplayMenuItem}) {DisplayName}";
        }
    }
}