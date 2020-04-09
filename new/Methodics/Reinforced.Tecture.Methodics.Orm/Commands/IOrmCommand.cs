using System;
using System.Collections.Generic;
using System.Text;

namespace Reinforced.Tecture.Methodics.Orm.Commands
{
    public interface IOrmCommand
    {
        object Entity { get; }

        Type EntityType { get; }
    }
}
