using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.Adapters;
using Reinforced.Storage.Testing.Namer;

namespace Reinforced.Storage.Defaults.EntityFramework
{
    public class NamerRepositoryConstructor
    {
        private readonly DbContext _dataContext;

        public NamerRepositoryConstructor(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public MapperRepository ExtractNames()
        {
            var entityTypes = _dataContext.GetType().GetProperties()
                .Where(d => d.PropertyType.IsGenericType && d.PropertyType.GetGenericTypeDefinition()==typeof(DbSet<>))
                .Select(d=>d.PropertyType.GetGenericArguments()[0]);

            MapperRepository result = new MapperRepository();

            foreach (var entityType in entityTypes)
            {
                var name = entityType.FullName;
                result.FullEntityNames.Add(name);
                result.TypesToTableNames[name] = GetTableName(entityType);
                var cols = new Dictionary<string,string>();
                var keysColl = new Dictionary<string,AssociationFields[]>();

                var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var propertyInfo in props)
                {
                    bool hasColumn = false;
                    try
                    {
                        var colName = GetColumnName(entityType, propertyInfo.Name);
                        if (!string.IsNullOrEmpty(colName))
                        {
                            cols[propertyInfo.Name] = colName;
                            hasColumn = true;
                        }
                    }
                    catch { }

                    if (!hasColumn)
                    {
                        try
                        {
                            var keys = GetJoinKeys(entityType, propertyInfo);
                            if (keys.Any())
                            {
                                keysColl[propertyInfo.Name] = keys.ToArray();
                            }
                        }
                        catch { }

                    }
                }

                if (cols.Any()) result.TypesToColumnToNames[name] = cols;
                if (keysColl.Any()) result.TypeToColumnToAssociations[name] = keysColl;
            }

            return result;
        }

        private string GetTableName(Type t)
        {
            return EntitiesMapper.EntitiesMapper.GetTableName(_dataContext, t);
        }

        private string GetColumnName(Type t, string propertyName)
        {
            return EntitiesMapper.EntitiesMapper.GetColumnName(_dataContext, t, propertyName);
        }

        private IEnumerable<AssociationFields> GetJoinKeys(Type sourceEntity, PropertyInfo sourceColumn)
        {
            return EntitiesMapper.EntitiesMapper.GetJoinKeys(_dataContext, sourceEntity, sourceColumn);
        }
    }
}
