using System.Collections;
using System.Collections.Generic;
using Reinforced.Tecture.Cloning;
using Reinforced.Tecture.Tracing.Promises;

namespace Reinforced.Tecture.Aspects.Orm.Queries.Traced.Enumerators.Nongeneric
{
    class Demander : IDemander
    {
        private readonly Demanding<IEnumerable> _demanding;
        
        private readonly string _hash;
        private readonly DescriptionHolder _description;
        private readonly List<object> _data;
        private long _currentIndex = -1;
        private long _indexBeforeReset = -1;

        public Demander(Demanding<IEnumerable> demanding, string hash, DescriptionHolder description)
        {
            _demanding = demanding;
            _hash = hash;
            _description = description;
            _data = new List<object>();
        }
        

        public void MoveNext(object current)
        {
            _currentIndex++;
            if (_currentIndex > _indexBeforeReset)
            {
                _data.Add(DeepCloner.DeepClone(current));
            }
        }

        public void Reset()
        {
            _currentIndex = -1;
            _indexBeforeReset = _currentIndex;
        }

        public void Dispose()
        {
            _demanding.Fullfill(_data, _data, _hash, _description.Description);
        }
    }
}
