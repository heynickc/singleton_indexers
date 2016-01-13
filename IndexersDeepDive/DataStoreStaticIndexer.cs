using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IndexersDeepDive {
    static class DataStoreStaticIndexer {
        private static readonly Dictionary<string, object> _properties = new Dictionary<string, object>();
        //public dynamic this[string index] // throws 'DataStoreStaticIndexer.this[string]': cannot declare indexers in a static class
        //{
        //    get {
        //        dynamic value;
        //        _properties.TryGetValue(index, out value);
        //        return value;
        //    }
        //    set { _properties.Add(index, value); }
        //}
    }

    public class DataStoreStaticIndexerTests {
        [Fact]
        public void try_do_static_indexer() {
            //DataStoreStaticIndexer["Key1"] = "A String";
        }
    }

    // https://stackoverflow.com/questions/401232/static-indexers?lq=1
    static class ConfigurationManager {

        //public object this[string name] // throws 'ConfigurationManager.this[string]': cannot declare indexers in a static class

        //{
        //    get
        //    {
        //        return ConfigurationManager.getProperty(name);
        //    }
        //    set
        //    {
        //        ConfigurationManager.editProperty(name, value);
        //    }
        //}

        /// <summary>
        /// This will write the value to the property. Will overwrite if the property is already there
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="value">Value to be wrote (calls ToString)</param>
        public static void editProperty(string name, object value) {
            DataSet ds = new DataSet();
            FileStream configFile = new FileStream("./config.xml", FileMode.OpenOrCreate);
            ds.ReadXml(configFile);
            if (ds.Tables["config"] == null) {
                ds.Tables.Add("config");
            }
            DataTable config = ds.Tables["config"];
            if (config.Rows[0] == null) {
                config.Rows.Add(config.NewRow());
            }
            if (config.Columns[name] == null) {
                config.Columns.Add(name);
            }
            config.Rows[0][name] = value.ToString();
            ds.WriteXml(configFile);
            configFile.Close();
        }

        public static void addProperty(string name, object value) {
            ConfigurationManager.editProperty(name, value);
        }

        public static object getProperty(string name) {
            DataSet ds = new DataSet();
            FileStream configFile = new FileStream("./config.xml", FileMode.OpenOrCreate);
            ds.ReadXml(configFile);
            configFile.Close();
            if (ds.Tables["config"] == null) {
                return null;
            }
            DataTable config = ds.Tables["config"];
            if (config.Rows[0] == null) {
                return null;
            }
            if (config.Columns[name] == null) {
                return null;
            }
            return config.Rows[0][name];
        }
    }
}
