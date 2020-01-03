using MiniMongler.Models;
using System.Collections.Generic;

namespace MiniMongler.Views {
    public class ItemContext {
        public Mini Miniature { get; }

        public ItemContext(MiniMonglerApp app, Mini miniature) {
            Miniature = miniature;
        }

        public IEnumerable<string> OtherTags { get; } = new[] { "one", "two" };

        public string AddedTag { get; set; }
    }
}
