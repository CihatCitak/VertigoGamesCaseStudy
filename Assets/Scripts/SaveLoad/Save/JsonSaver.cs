using System.IO;
using UnityEngine;
using SaveLoad.Core;
using SaveLoad.Save.Interfaces;

namespace SaveLoad.Save
{
    public class JsonSaver<T> : JsonSaveLoadBase, ISaveService<T>
    {
        public JsonSaver(string fileName): base(fileName)
        {
        }
        
        public void Save(T data)
        {
            var json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Path.Combine(SaveFolderPath, FileName), json);
        }
    }
}