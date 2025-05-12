using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class PowerUpManager : ViewController
    {
        public static PowerUpManager Default;
        void Start()
        {
            // Code Here
        }
        private void Awake()
        {
            Default = this;
        }
        private void OnDestroy()
        {
            Default = null;
        }
    }
}
