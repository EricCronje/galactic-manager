﻿using U = ArchCorpUtilities.Utilities.UniversalUtilities;

namespace ArchCorpUtilities.Models.Helper
{
    public interface IHelper<T>
    {
        public string? SessionID { get; set; }
        Patina.Patina Page { get; set; }

        public bool IsItemsOnThePage();

        /// <summary>
        /// Used to view the items - cal the generic.
        /// GenView
        /// </summary>
        /// <param name="navigate"></param>
        public bool View(U.Navigation navigate = U.Navigation.FirstPage);

        /// <summary>
        /// Refresh on the add, remove, edit, search, view page
        /// can be called from the "DefaultTasks"
        /// </summary>
        public bool Refresh(U.Navigation navigate = U.Navigation.FirstPage);

        /// <summary>
        /// Call Add(T)
        /// Called from the "DefaultTask"
        /// Used to add new items into the model list.
        /// </summary>
        /// <returns></returns>
        public bool Add(int? simChoice = null, string[]? simInput = null);

        /// <summary>
        /// Called from the "Add()"
        /// Used to add new items into the model list.
        /// </summary>
        //public virtual bool Add(T modelItem)
        //{
        //    if (modelItem != null && ModelList != null)
        //    {
        //        ModelList.Add(modelItem);
        //        return true;
        //    }
        //    else
        //        return false;
        //}
        /// <summary>
        /// Called from the "DefaultTask"
        /// Call Add<typeparamref name="T"/> and Remove<typeparamref name="T"/>
        /// </summary>
        public bool Edit(int? simChoice, string[]? simInput);

        /// <summary>
        /// Called from "DefaultTask"
        /// pass through the simChoice = Simulation Choice
        /// pass the simInput[] for the new item name
        /// </summary>
        /// <param name="simChoice"></param>
        /// <param name="simInput"></param>
        /// <returns></returns>
        public bool Search(int? simChoice = null, string[]? simInput = null);

        /// <summary>
        /// Load from a file
        /// </summary>
        /// <param name="simChoice"></param>
        /// <param name="simInput"></param>
        /// <returns></returns>
        public bool Load(int? simChoice = null, string[]? simInput = null);

        /// <summary>
        /// Save to a file
        /// </summary>
        /// <param name="simChoice"></param>
        /// <param name="simInput"></param>
        /// <returns></returns>
        public bool Save(int? simChoice = null, string[]? simInput = null);

        /// <summary>
        /// Call the Remove<typeparamref name="T"/>
        /// </summary>
        public bool Remove(int? simChoice = null, string[]? simInput = null);

        public void ResetPageMaxCount();

    }
}
