using QFramework;
using UnityEngine;
namespace PrjectSurvivor
{
    public abstract class GamePlayObj : ViewController
    {
        public bool inScreen;
        protected abstract Collider2D Collider { get; }


        private void OnBecameVisible()
        {
            Collider.enabled = true;
            inScreen = true;
        }
        private void OnBecameInvisible()
        {
            Collider.enabled = false;
            inScreen = false;
        }
    }
}
