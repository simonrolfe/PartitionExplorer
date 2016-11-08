namespace PartitionExplorer.Core.Partitioning
{
    public interface IPartitioner<THashOutput> : ILoadable
    {
        int GetPartitionIndex(THashOutput hashedPartitionKey, int partitionCount);
    }
}
