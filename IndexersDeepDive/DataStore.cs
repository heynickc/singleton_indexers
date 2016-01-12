using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDeepDive {
    // https://www.reddit.com/r/csharp/comments/3ve8pc/i_feel_like_ive_created_an_unholy_alliance/
    //public class DataStore {
    //    public string Id { get; set; }
    //    private static DataStore instance = default(DataStore);

    //    public static DataStore Instance {
    //        get {
    //            if (instance == default(DataStore)) {
    //                instance = new DataStore();
    //            }
    //            return instance;
    //        }
    //    }

    //    private static Dictionary<string, DataStore> instances = new Dictionary<string, DataStore>();

    //    public DataStore this[string id] {
    //        get {
    //            if (!instances.ContainsKey(id)) {
    //                if (instances.Keys.Count <= 0) {
    //                    instances.Add(id, instance);
    //                    instance.Id = id;
    //                } else {
    //                    DataStore store = new DataStore();
    //                    store.Id = id;
    //                    instances.Add(id, store);
    //                }
    //            }
    //            return instances[id];
    //        }
    //    }
    //}
}
