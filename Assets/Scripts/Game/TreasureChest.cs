using QAssetBundle;
using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class TreasureChest : GamePlayObj
    {
        void Start()
        {
            // Code Here
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                UIGamePanel.OpenTreasurePanel.Trigger();
                AudioKit.PlaySound(Sfs.TREASUER_CHEST);
                this.DestroyGameObjGracefully();
            }


        }
        protected override Collider2D Collider => SelfCollider2D;
    }
}
