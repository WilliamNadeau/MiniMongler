namespace MiniMongler.Views {
    public class MainContext : ViewModel {
        private readonly MiniMonglerApp app;

        public MainContext(MiniMonglerApp app) {
            this.app = app;
        }

        private object selected;
        public object Selected {
            get => selected;
            set {
                selected = value;
                Notify();
            }
        }
    }
}
