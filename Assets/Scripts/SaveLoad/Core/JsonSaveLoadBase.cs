using UnityEngine;

namespace SaveLoad.Core
{
    public class JsonSaveLoadBase
    {
        protected string SaveFolderPath;
        protected string FileName;

        protected JsonSaveLoadBase(string fileName)
        {
            SaveFolderPath = Application.persistentDataPath;
            FileName = fileName;
        }
    }
}