using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;

namespace IndexersDeepDive {

    public sealed class SingletonIndexer {

        private static readonly Lazy<SingletonIndexer> lazy =
            new Lazy<SingletonIndexer>(() => new SingletonIndexer());
        private readonly ConcurrentDictionary<string, DataStoreClass> _properties;

        public static SingletonIndexer Instance {
            get { return lazy.Value; }
        }
        private SingletonIndexer() {
            _properties = new ConcurrentDictionary<string, DataStoreClass>();
        }
        private void SetProperty(string name, DataStoreClass value) {
            _properties.TryAdd(name, value);
        }
        private DataStoreClass GetProperty(string name) {
            DataStoreClass value;
            _properties.TryGetValue(name, out value);
            return value;
        }
        public int GetCount() {
            return _properties.Count;
        }
        public DataStoreClass this[string index] {
            get { return GetProperty(index); }
            set { SetProperty(index, value); }
        }
    }

    public sealed class DataStoreClass {
        public int Id { get; set; }
        public string Attribute { get; set; }
        public string SomeOtherAttribute { get; set; }
    }

    public class SingletonIndexerNonGeneric {
        [Fact]
        public void singleton_indexer_add_items() {
            SingletonIndexer.Instance["Key1"] = new DataStoreClass();
            Assert.IsType<DataStoreClass>(SingletonIndexer.Instance["Key1"]);
        }
    }
}