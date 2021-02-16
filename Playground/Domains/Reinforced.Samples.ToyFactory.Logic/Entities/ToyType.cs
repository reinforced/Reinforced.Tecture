using System;
using System.Linq.Expressions;
using Reinforced.Samples.ToyFactory.Logic.Channels;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;
using Reinforced.Tecture.Commands;

namespace Reinforced.Samples.ToyFactory.Logic.Entities
{
    /// <summary>
    /// Type of toy, most basic element for toys  
    /// </summary>
    public class ToyType : IPrimaryKey<int>, IEntity, IDescriptive
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public Expression<Func<int>> PrimaryKey
        {
            get { return () => Id; }
        }

        public string Describe()
        {
            return $"toy type {Name}";
        }
    }
}
