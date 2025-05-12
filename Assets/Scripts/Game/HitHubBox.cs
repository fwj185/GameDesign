using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{

    public partial class HitHubBox : GamePlayObj
    {
        public GameObject Owner;
        void Start()
        {
            if (Owner == null)
            {
                Owner = transform.parent.gameObject;
            }
            // Code Here
        }
        protected override Collider2D Collider => SelfCollider2D;
    }
}
