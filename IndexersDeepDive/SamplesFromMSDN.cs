using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexersDeepDive {
    public static class TemperatureConverter {
        public static double CelsiusToFahrenheit(string temperatureCelsius) {
            // Convert argument to double for calculations.
            double celsius = Double.Parse(temperatureCelsius);

            // Convert Celsius to Fahrenheit.
            double fahrenheit = (celsius * 9 / 5) + 32;

            return fahrenheit;
        }

        public static double FahrenheitToCelsius(string temperatureFahrenheit) {
            // Convert argument to double for calculations.
            double fahrenheit = Double.Parse(temperatureFahrenheit);

            // Convert Fahrenheit to Celsius.
            double celsius = (fahrenheit - 32) * 5 / 9;

            return celsius;
        }
    }

    public class Automobile {
        public static int NumberOfWheels = 4;
        public static int SizeOfGasTank {
            get {
                return 15;
            }
        }
        public static void Drive() { }

        // Other non-static fields and properties...
    }

    class SampleCollection<T> {
        // Declare an array to store the data elements.
        private T[] arr = new T[100];

        // Define the indexer, which will allow client code
        // to use [] notation on the class instance itself.
        // (See line 2 of code in Main below.)        
        public T this[int i] {
            get {
                // This indexer is very simple, and just returns or sets
                // the corresponding element from the internal array.
                return arr[i];
            }
            set {
                arr[i] = value;
            }
        }
    }
}
