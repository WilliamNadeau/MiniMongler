using Microsoft.Win32;
using MiniMongler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace MiniMongler.Views {
    public class ItemContext : ViewModel {
        private readonly MiniMonglerApp app;

        public Mini Miniature { get; }

        private readonly IEnumerable<string> allTags;

        public LambdaCommand SaveCommand { get; }
        public LambdaCommand AddPictureCommand { get; }
        public LambdaCommand AddTagCommand { get; }

        public ItemContext(MiniMonglerApp app, Mini miniature, IEnumerable<string> allTags) {
            this.app = app;
            Miniature = miniature;
            this.allTags = allTags.Except(miniature.Tags);

            this.SaveCommand = new LambdaCommand((l, p) => CanSave, (l, p) => Save());
            this.AddPictureCommand = new LambdaCommand(CanAddPicture, (l, p) => AddPicture());
            this.AddTagCommand = new LambdaCommand(CanAddTag, AddTag);
        }

        private bool CanAddPicture(LambdaCommand arg1, object arg2) => Miniature.Saved;

        private void AddTag(LambdaCommand arg1, object arg2) {
            Miniature.AddTag(AddedTag);
            Notify(nameof(OtherTags));
            Notify(nameof(Tags));
            Notify(nameof(ShowTags));
            Notify(nameof(ShowNoTags));
        }

        private bool CanAddTag(LambdaCommand arg1, object arg2) => !string.IsNullOrWhiteSpace(AddedTag);

        private void AddPicture() {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() ?? false) {

                app.AddPictureToMini(Miniature, dialog.FileName);

            }
        }

        private void Save() {
            app.SaveMiniature(Miniature);
        }

        public IEnumerable<string> OtherTags => allTags.Except(Miniature.Tags);

        public string Name {
            get { return Miniature.Name; }
            set { Miniature.Name = value; Notify(); SaveCommand.Notify(); }
        }

        public IEnumerable<TagContext> Tags => Miniature.Tags.Select(ConvertToTagModel);

        private TagContext ConvertToTagModel(string tag) {
            return new TagContext(tag, new LambdaCommand((l, p) => DeleteTag(tag)));
        }

        private void DeleteTag(string tag) {
            Miniature.DeleteTag(tag);
            Notify(nameof(OtherTags));
            Notify(nameof(Tags));
            Notify(nameof(ShowTags));
            Notify(nameof(ShowNoTags));
        }
        private string addedTag;
        public string AddedTag {
            get {
                return addedTag;
            }
            set {
                this.addedTag = value;
                Notify();
                AddTagCommand.Notify();
            }
        }
        public bool CanSave {
            get {
                return !string.IsNullOrWhiteSpace(Miniature.Name);
            }
        }

        public class TagContext : ViewModel {
            public string Value { get; }
            public LambdaCommand DeleteCommand { get; }

            public TagContext(string value, LambdaCommand deleteCommand) {
                Value = value;
                this.DeleteCommand = deleteCommand;
            }
        }

        public Visibility ShowTags => Miniature.Tags.Any() ? Visibility.Visible : Visibility.Collapsed;
        public Visibility ShowNoTags => Miniature.Tags.Any() ? Visibility.Collapsed : Visibility.Visible;
    }
}
