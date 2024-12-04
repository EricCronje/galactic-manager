namespace MenuEnumLib
{
    public class MenuEnumLib
    {
        public enum CodeTemplateEnum
        {
            Helper,
            POCO,
            POCOLink,
            HelperLink,
            MockRepository,
            POCOHierarchy,
            HierarchyHelper,
            MockRepositoryHierarchy,
            MockRepositoryLink
        }

        public enum MenuTypeEnum
        {
            None,
            Link,
            Manage,
            Hierarchy
        }

        public enum MenuActionEnum
        {
            View,
            Add,
            Remove,
            Edit,
            Save,
            Load,
            back,
            exit,
            Search,
            Clear,
        }
    }
}
