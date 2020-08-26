using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyFactory.Logic.Channels
{
    public interface Db :
        CommandQueryChannel<Reinforced.Tecture.Features.Orm.Command, Reinforced.Tecture.Features.Orm.Query>,
        CommandQueryChannel<Reinforced.Tecture.Features.SqlStroke.Command, Reinforced.Tecture.Features.SqlStroke.Query>
    { }

    public interface IEntity
    {
        int Id { get; }
    }
}
