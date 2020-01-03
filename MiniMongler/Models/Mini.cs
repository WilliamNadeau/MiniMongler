using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniMongler.Models {
    public class Mini {

        public static Mini NewMini() => new Mini {
            Name = "",
            Tags = Enumerable.Empty<string>(),
            MainImage = "",
        };

        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string MainImage { get; set; }
    }
}
