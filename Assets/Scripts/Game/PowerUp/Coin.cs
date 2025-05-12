using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class Coin : PowerUp
    {
        void Start()
        {
            // Code Here
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                isFay = true;
            }


        }
        protected override Collider2D Collider => SelfCollider2D;
        protected override void Expe()
        {
            AudioKit.PlaySound("coin");
            Global.Coin.Value++;
            this.DestroyGameObjGracefully();
        }
    }
}
