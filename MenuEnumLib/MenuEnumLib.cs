﻿namespace MenuEnumLib
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
            None,
            View,
            Add,
            Remove,
            Edit,
            Save,
            Load,
            Back,
            Exit,
            Search,
            Clear,
            Manage,
            Link,
            Hierarchy,
            Main,
            Refresh,
            Next,
            Last,
            First,
            Previous,
            ClearAll
        }
    }
}
