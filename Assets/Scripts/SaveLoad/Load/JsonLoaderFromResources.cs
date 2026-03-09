using UnityEngine;
using SaveLoad.Load.Interfaces;

namespace SaveLoad.Load
{
    public class JsonLoaderFromResources<T> : ILoadService<T>
    {
        private readonly string _fileName;

        public JsonLoaderFromResources(string fileName)
        {
            _fileName = fileName;
        }
        
        public T Load()
        {
            // Load default config from Resources
            var localJson = Resources.Load<TextAsset>(_fileName);
            if (localJson == null)
            {
                Debug.LogError("Resources JSON not found!");
                return default;
            }

            return JsonUtility.FromJson<T>(localJson.text);
        }
    }
}