using MiniMongler.Views;
using System.Windows;

namespace MiniMongler {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var initialContext = new MainContext(MiniMonglerApp.Instance);

            MiniMonglerApp.Instance.SwitchContext = v => initialContext.Selected = v;

            MiniMonglerApp.Instance.InitializeFromPriorAppData();

            this.DataContext = initialContext;
        }

    }
}
