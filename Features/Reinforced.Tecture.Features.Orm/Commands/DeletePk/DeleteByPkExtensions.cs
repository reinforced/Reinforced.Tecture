

using Reinforced.Tecture.Features.Orm.PrimaryKey;

namespace Reinforced.Tecture.Features.Orm.Commands.DeletePk
{
    public static partial class Extensions
    {
     
        public static DeletePk ByPk<T1>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1>> c, T1 v1)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1);
        }
     
        public static DeletePk ByPk<T1, T2>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2>> c, T1 v1, T2 v2)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2);
        }
     
        public static DeletePk ByPk<T1, T2, T3>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3>> c, T1 v1, T2 v2, T3 v3)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3);
        }
     
        public static DeletePk ByPk<T1, T2, T3, T4>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4>> c, T1 v1, T2 v2, T3 v3, T4 v4)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4);
        }
     
        public static DeletePk ByPk<T1, T2, T3, T4, T5>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4, T5>> c, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4, v5);
        }
     
        public static DeletePk ByPk<T1, T2, T3, T4, T5, T6>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4, T5, T6>> c, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4, v5, v6);
        }
     
        public static DeletePk ByPk<T1, T2, T3, T4, T5, T6, T7>
            (this IPrimaryKeyOperation<DeletePk, IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> c, T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7)
        {
            var a = (DeletePkOperationBase) c;
            return DeletePkCore(a.Write, a.EntityType, v1, v2, v3, v4, v5, v6, v7);
        }
        }
}

