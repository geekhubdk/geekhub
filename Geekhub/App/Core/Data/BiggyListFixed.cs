using System;
using System.Collections.Generic;
using System.IO;
using Biggy;
using Newtonsoft.Json;

namespace Geekhub.App.Core.Data
{

    public class BiggyListFixed<T> : InMemoryList<T> where T : new()
    {

        public string DbDirectory { get; set; }
        public bool InMemory { get; set; }
        public string DbFileName { get; set; }
        public string DbName { get; set; }

        public string DbPath
        {
            get
            {
                return Path.Combine(DbDirectory, DbFileName);
            }
        }

        public bool HasDbFile
        {
            get
            {
                return File.Exists(DbPath);
            }
        }

        public BiggyListFixed(string dbPath = "current", bool inMemory = false, string dbName = "")
        {

            this.InMemory = inMemory;

            if (String.IsNullOrWhiteSpace(dbName)) {
                var thingyType = this.GetType().GenericTypeArguments[0].Name;
                this.DbName = Inflector.Inflector.Pluralize(thingyType).ToLower();
            } else {
                this.DbName = dbName.ToLower();
            }
            this.DbFileName = this.DbName + ".json";
            this.SetDataDirectory(dbPath);
            _items = TryLoadFileData(this.DbPath);
        }



        public void SetDataDirectory(string dbPath)
        {
            var dataDir = dbPath;
            if (dbPath == "current") {
                var currentDir = Directory.GetCurrentDirectory();
                if (currentDir.EndsWith("Debug") || currentDir.EndsWith("Release")) {
                    var projectRoot = Directory.GetParent(@"..\..\").FullName;
                    dataDir = Path.Combine(projectRoot, "Data");
                }
            } else {
                dataDir = Path.Combine(dbPath, "Data");
            }
            Directory.CreateDirectory(dataDir);
            this.DbDirectory = dataDir;

        }


        public List<T> TryLoadFileData(string path)
        {

            List<T> result = new List<T>();
            if (File.Exists(path)) {
                //format for the deserializer...
                var json = File.ReadAllText(path);
                result = JsonConvert.DeserializeObject<List<T>>(json);
            }

            FireLoadedEvents();

            return result;
        }

        public void Reload()
        {
            _items = TryLoadFileData(this.DbPath);
        }

        public override int Update(T item)
        {
            var i = base.Update(item);
            this.FlushToDisk();
            return i;
        }

        public override void Add(T item)
        {
            base.Add(item);
            this.FlushToDisk();
        }

        public override void Clear()
        {
            base.Clear();
            this.FlushToDisk();
        }


        public override bool Remove(T item)
        {
            var removed = base.Remove(item);
            this.FlushToDisk();
            return removed;
        }


        public bool FlushToDisk()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(this.DbPath, json);
            return true;
        }

    }
}