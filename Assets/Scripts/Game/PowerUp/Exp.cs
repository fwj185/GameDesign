using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class Exp : PowerUp
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
            AudioKit.PlaySound("exp");
            Global.Exp.Value+=1;
            this.DestroyGameObjGracefully();
        }

        protected override Collider2D Collider => SelfCollider2D;
    }
}