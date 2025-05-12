
using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public class SaveSystem : AbstractSystem
    {
        public void Save()
        {
        }
        public void Load()
        {

        }
        public void SaveBool(string key, bool vale) 
        {
            PlayerPrefs.SetInt(key, vale ? 1 : 0);
        }
        public bool LoadBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }

        public void SaveInt(string key, int vale)
        {
            PlayerPrefs.SetInt(key, vale);
        }
        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SaveString(string key, string vale)
        {
            PlayerPrefs.SetString(key, vale);
        }
        public string LoadString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        protected override void OnInit()
        {

        }
    }
}
