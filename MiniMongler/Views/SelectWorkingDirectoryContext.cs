using System.IO;
using System.Windows;

namespace MiniMongler.Views {
    public class SelectWorkingDirectoryContext : ViewModel {
        private readonly MiniMonglerApp app;

        public SelectWorkingDirectoryContext(MiniMonglerApp app, string priorWorkingDirectory) {
            this.app = app;

            SaveCommand = new LambdaCommand(
                (l, p) => CanSave,
                (l, p) => Save()
            );

            this.WorkingDirectory = priorWorkingDirectory;

        }

        private string workingDirectory;

        public string WorkingDirectory {
            get { return workingDirectory; }
            set {
                workingDirectory = value;
                Notify();
                Validate();
                SaveCommand.TriggerPredicateUpdated();
            }
        }

        private Visibility validMessageVisibility;

        public Visibility ValidMessageVisibility {
            get { return validMessageVisibility; }
            set { validMessageVisibility = value; Notify(); }
        }

        private bool canSave;

        public bool CanSave {
            get { return canSave; }
            set { canSave = value; Notify(); }
        }

        public LambdaCommand SaveCommand { get; }

        public void Save() {
            if (!CanSave) {
                return;
            }

            app.SaveWorkingDirectory(WorkingDirectory);

            app.ShowSearch();
        }

        private void Validate() {
            var exists = Directory.Exists(workingDirectory);
            ValidMessageVisibility = exists ? Visibility.Collapsed : Visibility.Visible;
            CanSave = exists;
        }
    }
}
