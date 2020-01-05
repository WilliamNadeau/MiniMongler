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
            Key = Guid.NewGuid(),
            Saved = false,
        };
                
        public Guid Key { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string MainImage { get; set; }
        public bool Saved { get; set; }
        public string GetDirectoryName(string workingDirectory) => $"{workingDirectory}/{Name}_{Key}";
        public string GetFileName(string workingDirectory) => $"{GetDirectoryName(workingDirectory)}/mini.json";
        public string OldDirectory { get; set; }

        public void AddTag(string addedTag) {
            this.Tags = this.Tags.Concat(new[] { addedTag.ToLower() }).Distinct().ToList();  
        }

        public void DeleteTag(string tag) {
            this.Tags = this.Tags.Except(new[] { tag.ToLower() }).Distinct().ToList();
        }
    }
}
