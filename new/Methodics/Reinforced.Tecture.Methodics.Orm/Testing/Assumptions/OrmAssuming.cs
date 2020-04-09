using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Integrate;
using Reinforced.Tecture.Testing.Assumptions;

namespace Reinforced.Tecture.Methodics.Orm.Testing.Assumptions
{
    public class OrmAssuming
    {
        internal Assuming _orig;

        internal OrmAssuming(Assuming orig)
        {
            _orig = orig;
        }
    }
}
