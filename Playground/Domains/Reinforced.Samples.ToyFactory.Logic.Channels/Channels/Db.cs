using Reinforced.Tecture.Aspects;
using Reinforced.Tecture.Aspects.DirectSql;
using Reinforced.Tecture.Aspects.Orm;
using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyFactory.Logic.Channels
{
    public interface Db :
        CommandQueryChannel<Orm.Command, Orm.Query>,
        CommandQueryChannel<DirectSql.Command,DirectSql.Query>
    { }

    public interface IEntity
    {
        int Id { get; }
    }
}
