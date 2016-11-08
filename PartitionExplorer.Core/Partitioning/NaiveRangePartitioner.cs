namespace PartitionExplorer.Core.Partitioning
{
    public class NaiveRangePartitioner : IPartitioner<int>
    {
        public string Name { get { return "Naive range partitioner"; } }
        public int GetPartitionIndex(int hashedPartitionKey, int partitionCount)
        {
            return (int.MaxValue / partitionCount) * hashedPartitionKey;
        }
    }
}
