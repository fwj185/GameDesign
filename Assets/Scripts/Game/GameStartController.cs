using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class GameStartController : ViewController
    {
        void Start()
        {
            // Code Here
            UIKit.OpenPanel<UIGameStartPanel>();
        }
    }
}
