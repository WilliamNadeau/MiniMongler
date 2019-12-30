using MiniMongler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace MiniMongler.Views
{
    public class MainContext : INotifyPropertyChanged
    {
        private object selected;
        public object Selected 
        { 
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InitializeFromPriorAppData()
        {
            var appDataDir = new Uri(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            var appDir = new Uri(appDataDir, "minimongler");
            var applicationFile = new Uri(appDir, "minimongler/config.json");

            if (File.Exists(applicationFile.ToString()))
            {
                var fileData = File.ReadAllText(applicationFile.ToString());
                var applicationData = JsonConvert.DeserializeObject<ApplicationData>(fileData);

                if (Directory.Exists(applicationData.WorkingDirectory))
                {
                    Selected = new SearchItemsContext
                    {
                        WorkingDirectory = applicationData.WorkingDirectory
                    };
                    return;
                }

            }

            Selected = new SelectWorkingDirectoryContext();
        }
    }
}
