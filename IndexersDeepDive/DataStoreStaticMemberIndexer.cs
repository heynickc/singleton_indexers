using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace IndexersDeepDive {

    internal sealed class DataStoreStaticMemberIndexer {
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

        public dynamic this[string index]
        {
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
            DataStoreStaticMemberIndexer["Key1"] = "String";
            //DataStoreStaticMemberIndexer["Key2"] = 55;
            //DataStoreStaticMemberIndexer["Key3"] = new KeyValuePair<string, int>("Key", 10);
            //DataStoreStaticMemberIndexer["Key4"] = DateTime.Now;
        }
    }
}