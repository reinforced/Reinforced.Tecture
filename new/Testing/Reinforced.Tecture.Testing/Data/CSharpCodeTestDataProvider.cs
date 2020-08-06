using System;
using System.Collections.Generic;
using System.Text;
using Reinforced.Tecture.Testing.Query;

namespace Reinforced.Tecture.Testing.Data
{
    public class CSharpCodeTestCollector : Collecting
    {
        private readonly Queue<ITestDataRecord> _records = new Queue<ITestDataRecord>();

        public void Put<T>(string hash, T result, string description)
        {
            _records.Enqueue(new TestDataRecord<T>()
            {
                Data = result,
                Description = description,
                Hash = hash
            });
        }
    }
}
