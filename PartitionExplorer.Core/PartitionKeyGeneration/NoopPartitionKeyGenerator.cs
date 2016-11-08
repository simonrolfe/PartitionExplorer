namespace PartitionExplorer.Core.PartitionKeyGeneration
{
    /// <summary>
    /// Does nothing, simply returns the item as the key.
    /// </summary>
    public class NoopPartitionKeyGenerator : IPartitionKeyGenerator
    {
        public string Name { get { return "No-op partition key generator"; } }
        public string GetPartitionKey(string item)
        {
            return item;
        }
    }
}
