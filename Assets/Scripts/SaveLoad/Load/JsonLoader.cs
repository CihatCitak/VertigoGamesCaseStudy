using System.IO;
using UnityEngine;
using SaveLoad.Core;
using SaveLoad.Load.Interfaces;

namespace SaveLoad.Load
{
    public class JsonLoader<T> : JsonSaveLoadBase, ILoadService<T> where T : new()
    {
        public JsonLoader(string fileName) : base(fileName)
        {
        }
        
        public T Load()
        {
            var path = Path.Combine(SaveFolderPath, FileName);
            if (!File.Exists(path))
                return new T();

            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
    }
}