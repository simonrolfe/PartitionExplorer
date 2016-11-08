using PartitionExplorer.Core.Hashing;
using PartitionExplorer.Core.Partitioning;
using PartitionExplorer.Core.PartitionKeyGeneration;
using System.Collections.Generic;

namespace PartitionExplorer.Core
{
    public class Partitions<THashOutput>
    {
        private readonly int _partitionCount;
        private readonly IPartitionKeyGenerator _partitionKeyGenerator;
        private readonly Partition[] _partitions;
        private readonly IStringHasher<THashOutput> _hasher;
        private readonly IPartitioner<THashOutput> _partitioner;

        public IEnumerable<Partition> AllPartitions
        {
            get
            {
                return _partitions;
            }
        }       

        public Partitions(IPartitionKeyGenerator partitionKeyGenerator, IStringHasher<THashOutput> hasher, IPartitioner<THashOutput> partitioner, int partitionCount)
        {
            _partitionCount = partitionCount;
            _partitionKeyGenerator = partitionKeyGenerator;
            _partitions = new Partition[_partitionCount];
            _partitioner = partitioner;
        }

        public void AddItem(string item)
        {
            string partitionId = _partitionKeyGenerator.GetPartitionKey(item);
            THashOutput hashedPartitionId = _hasher.Hash(partitionId);
            int partitionIndex = _partitioner.GetPartitionIndex(hashedPartitionId, _partitionCount);
            _partitions[partitionIndex].AddItem(item);
        }

    }
}
