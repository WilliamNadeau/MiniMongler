using MiniMongler.Models;
using System.Collections.Generic;
using System.Linq;

namespace MiniMongler.Views {
    public class SearchItemsContext {
        public SearchItemsContext(MiniMonglerApp app, string workingDirectory, IEnumerable<Mini> minis) {

            this.app = app;
            this.WorkingDirectory = workingDirectory;
            
            Minis = minis.Select(CreateMiniContext).ToList();

            this.AddMiniCommand = new LambdaCommand(
                (l, p) => app.ShowAddMini()
                );

        }

        private MiniContext CreateMiniContext(Mini mini) {
            return new MiniContext(mini, new LambdaCommand((l, r) => {
                app.ShowMini(mini);
            }));
        }

        private readonly MiniMonglerApp app;

        public string WorkingDirectory { get; internal set; }

        public IEnumerable<MiniContext> Minis { get; }
        public LambdaCommand AddMiniCommand { get; }

        public class MiniContext : ViewModel {
            public Mini Mini;
            public LambdaCommand ShowCommand;

            public MiniContext(Mini mini, LambdaCommand showCommand) {
                this.Mini = mini;
                this.ShowCommand = showCommand;
            }
        }
    }
}
