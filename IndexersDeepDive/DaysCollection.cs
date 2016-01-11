using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IndexersDeepDive {
    class DaysCollection {
        string[] days = { "Sun", "Mon", "Tues", "Wed", "Thurs", "Fri", "Sat" };
        private int GetDay(string day) {
            for (int i = 0; i < days.Length; i++) {
                if (days[i] == day) {
                    return i;
                }
            }
            throw new System.ArgumentOutOfRangeException(day, "day must be in the form \"Sun\", \"Mon\", etc");
        }
        public int this[string day] {
            get {
                return (GetDay(day));
            }
        }
    }

    public class DaysCollectionTests {

        [Fact]
        public void Get_value_from_indexer() {

            DaysCollection week = new DaysCollection();

            Assert.Equal(5, week["Fri"]);
        }

        [Fact]
        public void Throws_error_if_value_doesnt_exist() {

            DaysCollection week = new DaysCollection();

            Assert.Throws<ArgumentOutOfRangeException>(() => week["Whatever"]);
        }
    }
}
