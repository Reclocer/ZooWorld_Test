namespace SUBS.Core.Variables
{
    /// <summary>
    /// Mode for select random value
    /// </summary>
    public enum RandomizedValueMode
    {
        /// <summary> Simple value </summary>
        Default = 0,
        /// <summary> Random value, selected between two values </summary>
        RandomBetweenTwoValues = 1,
        /// <summary> Random one value, selected from array values</summary>
        RandomFromValues = 2,
    }
}
