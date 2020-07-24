using Reinforced.Tecture.Channels;

namespace Reinforced.Tecture.Playground
{
    interface Db :
        // можно так
        QueryChannel<Reinforced.Tecture.Features.Orm.Query>,
        CommandChannel<Reinforced.Tecture.Features.Orm.Command>,

        // а можно эдак
        CommandQueryChannel<
            Reinforced.Tecture.Features.SqlStroke.Command,
            Reinforced.Tecture.Features.SqlStroke.Query
        >
    { }
}
