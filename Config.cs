using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCToolshop {
    public class Config {

        public UnitQuantity workArea { get; set; }
        public UnitQuantity feedRate { get; set; }
        public UnitQuantity spindleSpeed { get; set; }

        public Config(UnitQuantity workArea, UnitQuantity feedRate, UnitQuantity spindleSpeed) {
            this.workArea = workArea;
            this.feedRate = feedRate;
            this.spindleSpeed = spindleSpeed;
        }

    }

    public class UnitQuantity {

        public List<double> mags { get; set; }
        public int unitsIndex { get; set; }

        [JsonConstructor]
        public UnitQuantity(List<double> mags, int unitsIndex) {
            this.mags = mags;
            this.unitsIndex = unitsIndex;
        }

        public UnitQuantity(double magnitude, int unitsIndex) {
            this.mags = new List<double>() { magnitude };
            this.unitsIndex = unitsIndex;
        }

    }
}
