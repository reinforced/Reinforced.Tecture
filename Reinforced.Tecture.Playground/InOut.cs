using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Integrate;

namespace Reinforced.Tecture.Playground
{

    //interface Methodic { }
    //class DirectSql : Methodic { }
    //class ObjectDb : Methodic { }

    //interface Input { }
    //interface Supports<T> : Input where T : Methodic { }

    //interface Db : Supports<DirectSql>, Supports<ObjectDb> { }

    //interface Bound<out Inp> where Inp : Input {  /* pipeline here */ }
    //interface Bound<out Inp, out E1> : Bound<Inp> where Inp : Input { /* pipeline here */ }
    ///* and t4 goes here */

    //static class Ext
    //{
    //    public static void Add<E1>(this Bound<Supports<DirectSql>, E1> w, E1 entity)
    //    {
    //        /* reflection magic to obtain DirectSql here */
    //        Console.WriteLine("works");
    //    }

    //    public static E1 Get<E1>(this Bound<Supports<DirectSql>, E1> w)
    //    {
    //        Console.WriteLine("works");
    //        return default(E1);
    //    }

    //}
    //abstract class Service<E1>
    //{
    //    protected Bound<T, E1> To<T>() where T : Input
    //    {
    //        return null; /* reflection magic */
    //    }
    //}

    //class Sample {}

    //class SampleService : Service<Sample>
    //{
    //    void Do()
    //    {
    //        To<Db>().Add(new Sample());

    //        var d = To<Db>();
    //        var entity = d.Get();
    //    }
    //}
}
