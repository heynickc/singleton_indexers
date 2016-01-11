using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDeepDive {
    // https://www.reddit.com/r/csharp/comments/3ve8pc/i_feel_like_ive_created_an_unholy_alliance/
    public class BadSingletonIndexer {
        public string Id { get; set; }
        private static BadSingletonIndexer instance = default(BadSingletonIndexer);

        public static BadSingletonIndexer Instance {
            get {
                if (instance == default(BadSingletonIndexer)) {
                    instance = new BadSingletonIndexer();
                }
                return instance;
            }
        }

        private static Dictionary<string, BadSingletonIndexer> instances = new Dictionary<string, BadSingletonIndexer>();

        public BadSingletonIndexer this[string id] {
            get {
                if (!instances.ContainsKey(id)) {
                    if (instances.Keys.Count <= 0) {
                        instances.Add(id, instance);
                        instance.Id = id;
                    } else {
                        BadSingletonIndexer store = new BadSingletonIndexer();
                        store.Id = id;
                        instances.Add(id, store);
                    }
                }
                return instances[id];
            }
        }
    }
}
