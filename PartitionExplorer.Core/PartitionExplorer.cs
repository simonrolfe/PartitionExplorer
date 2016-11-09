using PartitionExplorer.Core.Hashing;
using PartitionExplorer.Core.Partitioning;
using PartitionExplorer.Core.PartitionKeyGeneration;
using System.Collections.Generic;

namespace PartitionExplorer.Core
{
    public class PartitionExplorer<THashOutput>
    {
        private readonly List<IPartitionKeyGenerator> _partitionKeyGenerators;
        private readonly List<IPartitioner<THashOutput>> _partitioners;
        private readonly List<IStringHasher<THashOutput>> _hashers;

        public PartitionExplorer()
        {
            _partitionKeyGenerators = new List<IPartitionKeyGenerator>();
            _partitioners = new List<IPartitioner<THashOutput>>();
            _hashers = new List<IStringHasher<THashOutput>>();
        }

        public void AddPartitioner(IPartitioner<THashOutput> partitioner)
        {
            _partitioners.Add(partitioner);
        }

        public void AddHasher(IStringHasher<THashOutput> hasher)
        {
            _hashers.Add(hasher);
        }

        public void AddPartitionKeyGenerator(IPartitionKeyGenerator partitionKeyGenerator)
        {
            _partitionKeyGenerators.Add(partitionKeyGenerator);
        }

        public IEnumerable<IPartitionKeyGenerator> GetPartitionKeyGenerators()
        {
            return _partitionKeyGenerators;
        }

        public IEnumerable<Partitions<THashOutput>> TestPartitioners(IEnumerable<string> partitionKeys, int partitionCount)
        {
            List<Partitions<THashOutput>> partitionsList = new List<Partitions<THashOutput>>(); 

            foreach(IPartitionKeyGenerator partitionKeyGenerator in _partitionKeyGenerators)
            {
                foreach(IStringHasher<THashOutput> hasher in _hashers)
                {
                    foreach(IPartitioner<THashOutput> partitioner in _partitioners)
                    {
                        Partitions<THashOutput> partitions = new Partitions<THashOutput>(partitionKeyGenerator, hasher, partitioner, partitionCount);
                        foreach(string partitionKey in partitionKeys)
                        {
                            partitions.AddItem(partitionKey);
                        }

                        partitionsList.Add(partitions);
                    }
                }
            }

            return partitionsList;
        }
    }
}
