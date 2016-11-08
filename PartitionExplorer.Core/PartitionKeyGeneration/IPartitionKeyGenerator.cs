namespace PartitionExplorer.Core.PartitionKeyGeneration
{
    public interface IPartitionKeyGenerator : ILoadable
    {
        string GetPartitionKey(string item);
    }
}
