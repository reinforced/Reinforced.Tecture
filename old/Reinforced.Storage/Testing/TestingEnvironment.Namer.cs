using System;
using System.Collections.Generic;
using System.Reflection;
using Reinforced.Storage.Adapters;
using Reinforced.Storage.Testing.Namer;

namespace Reinforced.Storage.Testing
{
    public partial class TestingEnvironment: IMapper
    {

        private readonly MapperRepository _mapperRepository;
        public virtual string GetTableName(Type t)
        {
            return _mapperRepository.GetTableName(t);
        }

        public virtual string GetColumnName(Type t, string propertyName)
        {
            return _mapperRepository.GetColumnName(t, propertyName);
        }

        public virtual bool IsEntityType(Type t)
        {
            return _mapperRepository.IsEntityType(t);
        }

        public virtual IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            return _mapperRepository.GetJoinKeys(sourceEntity, sourceColumn);
        }
    }
}
