using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PartitionExplorer.Core
{
    public class LoadableLoader
    {
        private readonly List<ILoadable> _loadables;

        public LoadableLoader()
        {
            _loadables = new List<ILoadable>();
        }

        public IEnumerable<ILoadable> GetLoadables()
        {
            return _loadables;
        }

        public IEnumerable<TLoadable> GetLoadables<TLoadable>() where TLoadable : class, ILoadable
        {
            //TODO this could be much faster I'm sure, but measure perf first
            List<TLoadable> loadablesOfType = new List<TLoadable>();
 
            foreach(ILoadable loadable in _loadables)
            {
                TLoadable loadableOfType = loadable as TLoadable;
                if(loadableOfType != null)
                {
                    loadablesOfType.Add(loadableOfType);
                }
            }

            return loadablesOfType;
        }

        public void AddAssembly(Assembly assembly)
        {
            Type type = typeof(ILoadable);
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(p => type.IsAssignableFrom(p));
            foreach(Type t in types)
            {
                var ci = t.GetConstructor(new Type[0]); //find all empty constructors

                if(ci != null)
                {
                    ILoadable loadable = ci.Invoke(null) as ILoadable;
                    if(loadable != null)
                    {
                        _loadables.Add(loadable);
                    }
                }
            }

        }

    }
}
