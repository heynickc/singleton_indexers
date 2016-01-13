using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Configuration;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace IndexersDeepDive {

    public class DataStore {
        private readonly Dictionary<string, object> _properties;
        public DataStore() {
            this._properties = new Dictionary<string, object>();
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

    public class DataStoreTests {
        private readonly ITestOutputHelper _output;
        public DataStoreTests(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void can_create_heterogeneous_dictionary() {
            var dataStore = new DataStore();
            dataStore.SetProperty<string>("Key1", "String");
            dataStore.SetProperty<int>("Key2", 55);
            dataStore.SetProperty("Key3", new KeyValuePair<string, int>("Key", 10));
            dataStore.SetProperty("Key4", DateTime.Now);

            Assert.IsType<KeyValuePair<string, int>>(dataStore.GetProperty("Key3"));
            Assert.Equal(10, dataStore.GetProperty("Key3").Value);
        }

        [Fact]
        public void dictionary_trygetvalue_was_a_good_choice() {
            var dataStore = new DataStore();
            dataStore.SetProperty<string>("Key1", "String");
            dataStore.SetProperty<int>("Key2", 55);
            dataStore.SetProperty("Key3", new KeyValuePair<string, int>("KV", 10));

            Assert.Equal(null, dataStore.GetProperty("Whatevs.."));
        }

        [Fact]
        public void set_properties_using_indexer() {
            var dataStore = new DataStore();
            dataStore["Key1"] = "String";
            dataStore["Key2"] = 55;
            dataStore["Key4"] = DateTime.Now;
            dataStore["Key3"] = new KeyValuePair<string, int>("KV", 10);

            Assert.Equal("String", dataStore["Key1"]);
            Assert.Equal(10, dataStore["Key3"].Value);
        }
    }

    public static class JsonHelpers {
        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}

