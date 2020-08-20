using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Channels;

namespace Reinforced.Samples.ToyStory.Logic.Channels
{
    public interface Db :
        CommandQueryChannel<Reinforced.Tecture.Features.Orm.Command, Reinforced.Tecture.Features.Orm.Query>,
        CommandQueryChannel<Reinforced.Tecture.Features.SqlStroke.Command, Reinforced.Tecture.Features.SqlStroke.Query>
    { }
}
