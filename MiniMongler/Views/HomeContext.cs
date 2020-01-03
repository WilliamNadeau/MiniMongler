using System.Windows;

namespace MiniMongler.Views {
    public class HomeContext : ViewModel {
        
        public HomeContext(bool canSearch, MiniMonglerApp app) {
            this.CanSearch = canSearch;

            this.GoToSearchCommand = new LambdaCommand(
                (l, p) => CanSearch,
                (l, p) => app.ShowSearch()
                );

            this.GoToSelectWorkingDirectoryCommand = new LambdaCommand(
                (l, p) => app.ShowWorkingDirectorySelection()
                );

        }

        public bool CanSearch { get; }
        public LambdaCommand GoToSearchCommand { get; }
        public LambdaCommand GoToSelectWorkingDirectoryCommand { get; }

        public Visibility MessageVisibiltiy => CanSearch ? Visibility.Visible : Visibility.Collapsed;
    }
}
