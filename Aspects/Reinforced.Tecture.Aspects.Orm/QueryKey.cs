
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Reinforced.Tecture.Aspects.Orm.Commands.Add;
using Reinforced.Tecture.Aspects.Orm.PrimaryKey;

namespace Reinforced.Tecture.Aspects.Orm
{
    public abstract partial class Query
    {
         
        internal (T1, T2) Key<T1, T2>(IAddition<IPrimaryKey<T1, T2>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties2<T1, T2>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1]);
            }
            else
            {
                result = Aux.Get<(T1, T2)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties2<T1, T2>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression(); 
        }
         
        internal (T1, T2, T3) Key<T1, T2, T3>(IAddition<IPrimaryKey<T1, T2, T3>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2, T3) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties3<T1, T2, T3>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1], (T3) tmp[2]);
            }
            else
            {
                result = Aux.Get<(T1, T2, T3)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties3<T1, T2, T3>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2, T3>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression();  
            yield return pk.Item3.AsPropertyExpression(); 
        }
         
        internal (T1, T2, T3, T4) Key<T1, T2, T3, T4>(IAddition<IPrimaryKey<T1, T2, T3, T4>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2, T3, T4) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties4<T1, T2, T3, T4>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1], (T3) tmp[2], (T4) tmp[3]);
            }
            else
            {
                result = Aux.Get<(T1, T2, T3, T4)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties4<T1, T2, T3, T4>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2, T3, T4>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression();  
            yield return pk.Item3.AsPropertyExpression();  
            yield return pk.Item4.AsPropertyExpression(); 
        }
         
        internal (T1, T2, T3, T4, T5) Key<T1, T2, T3, T4, T5>(IAddition<IPrimaryKey<T1, T2, T3, T4, T5>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2, T3, T4, T5) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties5<T1, T2, T3, T4, T5>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1], (T3) tmp[2], (T4) tmp[3], (T5) tmp[4]);
            }
            else
            {
                result = Aux.Get<(T1, T2, T3, T4, T5)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties5<T1, T2, T3, T4, T5>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2, T3, T4, T5>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression();  
            yield return pk.Item3.AsPropertyExpression();  
            yield return pk.Item4.AsPropertyExpression();  
            yield return pk.Item5.AsPropertyExpression(); 
        }
         
        internal (T1, T2, T3, T4, T5, T6) Key<T1, T2, T3, T4, T5, T6>(IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2, T3, T4, T5, T6) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties6<T1, T2, T3, T4, T5, T6>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1], (T3) tmp[2], (T4) tmp[3], (T5) tmp[4], (T6) tmp[5]);
            }
            else
            {
                result = Aux.Get<(T1, T2, T3, T4, T5, T6)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties6<T1, T2, T3, T4, T5, T6>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2, T3, T4, T5, T6>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression();  
            yield return pk.Item3.AsPropertyExpression();  
            yield return pk.Item4.AsPropertyExpression();  
            yield return pk.Item5.AsPropertyExpression();  
            yield return pk.Item6.AsPropertyExpression(); 
        }
         
        internal (T1, T2, T3, T4, T5, T6, T7) Key<T1, T2, T3, T4, T5, T6, T7>(IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2, T3, T4, T5, T6, T7) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties7<T1, T2, T3, T4, T5, T6, T7>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1], (T3) tmp[2], (T4) tmp[3], (T5) tmp[4], (T6) tmp[5], (T7) tmp[6]);
            }
            else
            {
                result = Aux.Get<(T1, T2, T3, T4, T5, T6, T7)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties7<T1, T2, T3, T4, T5, T6, T7>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2, T3, T4, T5, T6, T7>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression();  
            yield return pk.Item3.AsPropertyExpression();  
            yield return pk.Item4.AsPropertyExpression();  
            yield return pk.Item5.AsPropertyExpression();  
            yield return pk.Item6.AsPropertyExpression();  
            yield return pk.Item7.AsPropertyExpression(); 
        }
         
        internal (T1, T2, T3, T4, T5, T6, T7, T8) Key<T1, T2, T3, T4, T5, T6, T7, T8>(IAddition<IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>> keyedAddition)
        {
            var a = (Add)keyedAddition;
            if (!a.IsExecuted) 
                throw new TectureOrmAspectException($"Cannot obtain primary key: addition of '{a.Entity}' did not happen yet");

            (T1, T2, T3, T4, T5, T6, T7, T8) result;
            string hash = Aux.IsHashRequired ? $"ORM_AdditionPK_{a.Order}" : string.Empty;
            if (Aux.IsEvaluationNeeded)
            {
                var tmp = GetKey(a, GetKeyProperties8<T1, T2, T3, T4, T5, T6, T7, T8>(a)).ToArray();
                result = ((T1) tmp[0], (T2) tmp[1], (T3) tmp[2], (T4) tmp[3], (T5) tmp[4], (T6) tmp[5], (T7) tmp[6], (T8) tmp[7]);
            }
            else
            {
                result = Aux.Get<(T1, T2, T3, T4, T5, T6, T7, T8)>(hash, "ORM addition PK retrieval");
            }

            if (Aux.IsTracingNeeded)
            {
                if (!Aux.IsEvaluationNeeded)
                {
                    Aux.Query(hash,"test data","ORM addition PK retrieval");
                }
                else
                {
                    Aux.Query(hash, result, "ORM addition PK retrieval");
                }
            }

            return result;

        }

        private IEnumerable<PropertyInfo> GetKeyProperties8<T1, T2, T3, T4, T5, T6, T7, T8>(Add addition)
        {
            var e = (IPrimaryKey<T1, T2, T3, T4, T5, T6, T7, T8>)addition.Entity;
            var pk = e.PrimaryKey; 
             
            yield return pk.Item1.AsPropertyExpression();  
            yield return pk.Item2.AsPropertyExpression();  
            yield return pk.Item3.AsPropertyExpression();  
            yield return pk.Item4.AsPropertyExpression();  
            yield return pk.Item5.AsPropertyExpression();  
            yield return pk.Item6.AsPropertyExpression();  
            yield return pk.Item7.AsPropertyExpression();  
            yield return pk.Item8.AsPropertyExpression(); 
        }
            
    }
}

