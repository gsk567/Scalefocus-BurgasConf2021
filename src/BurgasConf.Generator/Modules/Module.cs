using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BurgasConf.Generator.Modules
{
    public abstract class Module
    {
        private IServiceProvider serviceProvider;

        public Module(string name, bool locked)
        {
            this.Name = name;
            this.Locked = locked;
            this.Files = new List<ModuleFile>();
            this.Folders = new List<ModuleFolder>();
        }

        public string Id => this.Name.Replace(" ", "-").ToLower();

        public int Order { get; set; }

        public string Name { get; set; }

        public List<ModuleFile> Files { get; private set; }

        public List<ModuleFolder> Folders { get; private set; }

        public bool Generated { get; set; }

        public bool Locked { get; set; }

        public string SourceDirectory { get; set; }

        public TService GetService<TService>() =>
            (TService)this.serviceProvider.GetService(typeof(TService));

        public void AddFile(ModuleFile file)
        {
            this.Files.Add(file);
        }

        public ModuleFile GetFile(string referenceId)
        {
            return this.Files.FirstOrDefault(x => x.ReferenceId == referenceId);
        }

        public void AddFolder(ModuleFolder folder)
        {
            this.Folders.Add(folder);
        }

        public ModuleFolder GetFolder(string name)
        {
            return this.Folders.FirstOrDefault(x => x.Name == name);
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            if (this.serviceProvider == null)
            {
                this.serviceProvider = serviceProvider;
            }
        }

        public abstract void SetupFiles();

        public abstract void SetupFolders();

        public void Generate()
        {
            foreach (var folder in this.Folders)
            {
                var folderPath = Path.Combine(this.SourceDirectory, folder.RelativePath, folder.Name);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }

            foreach (var file in this.Files)
            {
                string fileContent = file.RenderFunction.Invoke(file);
                string filePath = Path.Combine(this.SourceDirectory, file.RelativePath, file.Name);
                if (!File.Exists(filePath) || this.Locked)
                {
                    File.WriteAllText(filePath, fileContent);
                }
            }
        }
    }
}