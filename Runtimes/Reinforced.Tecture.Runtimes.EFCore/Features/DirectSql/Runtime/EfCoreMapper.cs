using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reinforced.Tecture.Features.SqlStroke.Infrastructure;

namespace Reinforced.Tecture.Runtimes.EFCore.Features.DirectSql.Runtime
{
    class EfCoreMapper : IMapper
    {
        private readonly ILazyDisposable<DbContext> _context;

        public EfCoreMapper(ILazyDisposable<DbContext> context)
        {
            _context = context;
        }

        public string GetTableName(Type t)
        {
            var mapping = _context.Value.Model.FindEntityType(t);
            if (mapping==null)
                throw new EfCoreDirectSqlException($"Mapping for '{t}' was not found");

            var tableName = mapping.GetTableName();
            return tableName;
        }

        public string GetColumnName(Type t, PropertyInfo property)
        {
            var mapping = _context.Value.Model.FindEntityType(t);
            if (mapping == null)
                throw new EfCoreDirectSqlException($"Mapping for '{t}' was not found");
            var p = mapping.GetProperties().FirstOrDefault(x=>x.Name==property.Name);
            if (p==null)
                throw new EfCoreDirectSqlException($"Column '{property.Name}' was not found in mapping for '{t}'");
            return p.GetColumnName();
        }

        
        public bool IsEntityType(Type t)
        {
            return _context.Value.Model.FindEntityType(t) != null;
        }

        public IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            throw new NotImplementedException();
        }
    }
}
