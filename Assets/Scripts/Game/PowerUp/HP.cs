using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class HP : PowerUp
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
        protected override void Expe()
        {
            AudioKit.PlaySound("coin");
            Global.HP.Value++;
            this.DestroyGameObjGracefully();
        }
        protected override Collider2D Collider => SelfCollider2D;
    }
}
