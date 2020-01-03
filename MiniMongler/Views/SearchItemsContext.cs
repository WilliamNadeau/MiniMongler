using MiniMongler.Models;
using System.Collections.Generic;
using System.IO;

namespace MiniMongler.Views {
    public class SearchItemsContext {
        public SearchItemsContext(MiniMonglerApp app, string workingDirectory, IEnumerable<Mini> minis) {
            
            this.WorkingDirectory = workingDirectory;
            
            Minis = minis;

            this.AddMiniCommand = new LambdaCommand(
                (l, p) => app.ShowAddMini()
                );

        }

        public string WorkingDirectory { get; internal set; }

        public IEnumerable<Mini> Minis { get; }
        public LambdaCommand AddMiniCommand { get; }
    }
}
