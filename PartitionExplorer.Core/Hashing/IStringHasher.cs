namespace PartitionExplorer.Core.Hashing
{
    public interface IStringHasher<T> : ILoadable
    {
        T Hash(string input);

        void SetSeed(uint seed);
    }
}
