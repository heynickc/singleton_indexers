using System;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;
using Xunit.Abstractions;

namespace IndexersDeepDive {

    public class DataStoreStaticMemberIndexer {
        private static readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public DataStoreStaticMemberIndexer() {
            //this._properties = new Dictionary<string, object>();
        }

        public void SetProperty<T>(string name, T value) {
            _properties.Add(name, value);
        }

        public dynamic GetProperty(string name) {
            dynamic value;
            _properties.TryGetValue(name, out value);
            return value;
        }

        public dynamic this[string index] {
            get { return GetProperty(index); }
            set { SetProperty(index, value); }
        }
    }

    public class DataStoreStaticMemberIndexerTests {
        private readonly ITestOutputHelper _output;

        public DataStoreStaticMemberIndexerTests(ITestOutputHelper output) {
            _output = output;
        }

        [Fact]
        public void can_create_heterogeneous_dictionary() {
            //DataStoreStaticMemberIndexer["Key1"] = "String"; // 'DataStoreStaticMemberIndexer' is a type, which is not valid in the given context
            //DataStoreStaticMemberIndexer["Key2"] = 55;
            //DataStoreStaticMemberIndexer["Key3"] = new KeyValuePair<string, int>("Key", 10);
            //DataStoreStaticMemberIndexer["Key4"] = DateTime.Now;
        }
    }

    //https://stackoverflow.com/questions/154489/are-static-indexers-not-supported-in-c
    public static class StaticDataStore {
        private static readonly Dictionary<string, object> _properties = new Dictionary<string, object>();
        public static void SetProperty<T>(string name, T value) {
            _properties.Add(name, value);
        }
        public static dynamic GetProperty(string name) {
            dynamic value;
            _properties.TryGetValue(name, out value);
            return value;
        }
    }
    public class PretendStaticDataStoreIndexer {
        public sealed class DataStoreIndexer {
            public dynamic this[string index] {
                get { return StaticDataStore.GetProperty(index); }
                set { StaticDataStore.SetProperty(index, value); }
            }
        }

        private static DataStoreIndexer StaticIndexer;

        public static DataStoreIndexer Items {
            get { return StaticIndexer ?? (StaticIndexer = new DataStoreIndexer()); }
        }
    }

    public class PretendStaticDataStoreIndexerTests {
        [Fact]
        public void pretend_static_indexer_works() {
            PretendStaticDataStoreIndexer.Items["Key1"] = "String";
            PretendStaticDataStoreIndexer.Items["Key2"] = 55;
            PretendStaticDataStoreIndexer.Items["Key4"] = DateTime.Now;
            PretendStaticDataStoreIndexer.Items["Key3"] = new KeyValuePair<string, int>("KV", 10);
        }
    }
}