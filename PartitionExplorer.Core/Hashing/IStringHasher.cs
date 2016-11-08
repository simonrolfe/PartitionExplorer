using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartitionExplorer.Core.Hashing
{
    public interface IStringHasher<T> : ILoadable
    {
        T Hash(string input);
    }
}
