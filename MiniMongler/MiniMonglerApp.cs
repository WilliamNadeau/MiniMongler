﻿using MiniMongler.Models;
using MiniMongler.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniMongler {
    public class MiniMonglerApp {
        public static MiniMonglerApp Instance => app.Value;
        public static Lazy<MiniMonglerApp> app = new Lazy<MiniMonglerApp>(() => new MiniMonglerApp());

        public Action<object> SwitchContext { get; set; } = c => { };

        public void ShowSearch() =>
            SwitchContext(new SearchItemsContext(this, GetApplicationData().WorkingDirectory, LoadMinis()));

        private IEnumerable<Mini> LoadMinis() {
            var workingDir = GetApplicationData().WorkingDirectory;

            if (!Directory.Exists(workingDir)) {
                return Enumerable.Empty<Mini>();
            }

            return Directory.EnumerateDirectories(workingDir)
                .Select(directory => directory + @"/mini.json")
                .Where(fileName => File.Exists(fileName))
                .Select(fileName => File.ReadAllText(fileName))
                .Select(fileData => JsonConvert.DeserializeObject<Mini>(fileData));
        }

        public void ShowMini(Mini mini) => SwitchContext(new ItemContext(this, mini, GetAllTags()));
        
        public void ShowAddMini() => SwitchContext(new ItemContext(this, Mini.NewMini(), GetAllTags()));

        public IEnumerable<string> GetAllTags() => GetAllMinis()
            .SelectMany(mini => mini.Tags)
            .Distinct();

        public IEnumerable<Mini> GetAllMinis() => Directory.EnumerateDirectories(WorkingDirectory)
            .Select(directory => $"{directory}/mini.json")
            .Where(File.Exists)
            .Select(File.ReadAllText)
            .Select(JsonConvert.DeserializeObject<Mini>);

        public void SaveMiniature(Mini miniature) {

            var dir = miniature.GetDirectoryName(WorkingDirectory);
            if (Directory.Exists(miniature.OldDirectory) && !Directory.Exists(dir)) {
                Directory.Move(miniature.OldDirectory, dir);
            } else {
                EnsureDirectory(dir);
            }

            miniature.Saved = true;
            miniature.OldDirectory = dir;
            var file = miniature.GetFileName(WorkingDirectory);
            File.WriteAllText(file, JsonConvert.SerializeObject(miniature));
            ShowSearch();
        }

        private static void EnsureDirectory(string dir) {
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }
        }

        public void AddPictureToMini(Mini miniature, string fileName) {
            var dir = miniature.GetDirectoryName(WorkingDirectory);
            EnsureDirectory(dir);
            File.Copy(fileName, $"{dir}/{Path.GetFileName(fileName)}");
        }

        public string WorkingDirectory => GetApplicationData()?.WorkingDirectory;

        public void SaveWorkingDirectory(string workingDirectory) {
            Directory.CreateDirectory(ApplicationDirectory);
            File.WriteAllText(
                ApplicationFileUri,
                JsonConvert.SerializeObject(
                    new ApplicationData { WorkingDirectory = workingDirectory }));
        }

        public void InitializeFromPriorAppData() {

            var canSearch = Directory.Exists(GetApplicationData()?.WorkingDirectory);

            SwitchContext(new HomeContext(canSearch, this));
        }

        private static ApplicationData GetApplicationData() {
            if (!File.Exists(ApplicationFileUri)) {
                return null;
            }
            var fileData = File.ReadAllText(ApplicationFileUri);
            var applicationData = JsonConvert.DeserializeObject<ApplicationData>(fileData);
            return applicationData;
        }

        public void ShowWorkingDirectorySelection() =>
            SwitchContext(new SelectWorkingDirectoryContext(this, GetApplicationData()?.WorkingDirectory));

        private static string ApplicationFileUri {
            get {
                var appDir = ApplicationDirectory;
                var applicationFile = appDir + "/config.json";
                return applicationFile;
            }
        }

        private static string ApplicationDirectory {
            get {
                var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var appDir = appDataDir + "/minimongler";
                return appDir;
            }
        }
    }
}
