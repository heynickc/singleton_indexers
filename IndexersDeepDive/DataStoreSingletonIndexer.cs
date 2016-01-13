using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IndexersDeepDive {
    public sealed class DataStoreSingletonIndexer {
        private static readonly Lazy<DataStoreSingletonIndexer> lazy =
            new Lazy<DataStoreSingletonIndexer>(() => new DataStoreSingletonIndexer());
        private readonly ConcurrentDictionary<string, object> _properties;
        public static DataStoreSingletonIndexer Instance { get { return lazy.Value; } }
        private DataStoreSingletonIndexer() {  
            _properties = new ConcurrentDictionary<string, object>();  
        }
        private void SetProperty<T>(string name, T value) {
            _properties.TryAdd(name, value);
        }
        private dynamic GetProperty(string name) {
            dynamic value;
            _properties.TryGetValue(name, out value);
            return value;
        }
        public int GetCount() {
            return _properties.Count;
        }
        public dynamic this[string index] {
            get { return GetProperty(index); }
            set { SetProperty(index, value); }
        }
    }

    public class SingletonIndexerTests {
        [Fact()]
        public void singleton_instantiation_property_manipulation() {
            DataStoreSingletonIndexer.Instance["Key1"] = "String";
            DataStoreSingletonIndexer.Instance["Key2"] = 55;
            DataStoreSingletonIndexer.Instance["Key4"] = DateTime.Now;
            DataStoreSingletonIndexer.Instance["Key3"] = new KeyValuePair<string, int>("KV", 10);

            Assert.Equal("String", DataStoreSingletonIndexer.Instance["Key1"]);
            Assert.Equal(10, DataStoreSingletonIndexer.Instance["Key3"].Value);
        }

        [Fact(Skip = "Only use to test thread safety as needed")]
        public void thread_safety_of_properties_collection() {
            var t1 = new Thread(SetManyPropertiesEven);
            var t2 = new Thread(SetManyPropertiesOdd);
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            Assert.Equal(10000, DataStoreSingletonIndexer.Instance.GetCount());
        }

        public void SetManyPropertiesEven() {
            for (int i = 0; i < 10000; i += 2) {
                DataStoreSingletonIndexer.Instance[i.ToString()] = i;
            }
        }
        public void SetManyPropertiesOdd() {
            for (int i = 1; i < 10000; i += 2) {
                DataStoreSingletonIndexer.Instance[i.ToString()] = i;
            }
        }
    }
}
