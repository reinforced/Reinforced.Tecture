using Reinforced.Tecture.Aspects.DirectSql;
using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyFactory.Logic.Channels
{
    public interface Db :
        CommandQueryChannel<Tecture.Aspects.Orm.Command, Tecture.Aspects.Orm.Query>,
        CommandQueryChannel<Command, Query>
    { }

    public interface IEntity
    {
        int Id { get; }
    }
}
