using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDeepDive {
    public class AreaObjectCollection {
        private Dictionary<int, Dictionary<int, List<AreaObject>>> _map = new Dictionary<int, Dictionary<int, List<AreaObject>>>();

        public void Add(AreaId id, AreaObject obj) {
            int x = id.X;
            int y = id.Y;
            if (!_map.ContainsKey(x)) {
                _map.Add(x, new Dictionary<int, List<AreaObject>>());
            }
            if (!_map[x].ContainsKey(y)) {
                _map[x].Add(y, new List<AreaObject>());
            }
            _map[x][y].Add(obj);
        }
    }
    public class AreaId {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class AreaObject {
        public string AreaId { get; set; }
    }
}
