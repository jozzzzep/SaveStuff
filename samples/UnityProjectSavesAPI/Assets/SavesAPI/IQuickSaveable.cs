namespace SavesAPI
{
    /*/ 
     * 
     *  - Properties -------
     *      Name          - The name of the saveable Object
     *      LastUsage     - The last time the save has been created or modified
     *      SavingMethod  - Which saving method to use when saving/loading the file
     *  --------------------
     *  
    /*/

    /// <summary>
    /// Interface for creating saveable class for the <see cref="QuickSaveSystem"/>
    /// <para>Make sure to add attribute <see cref="[System.Serializable]"/></para>
    /// </summary>
    public interface IQuickSaveable : ISaveable
    {
        /// <summary>
        /// Which saving method to use when saving/loading the file
        /// </summary>
        public SavingMethod SavingMethod { get; }
    }
}
