using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class Bomb : GamePlayObj
    {
        void Start()
        {
            // Code Here
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.GetComponent<CollectableArea>())
            {
                Execute();
                this.DestroyGameObjGracefully();
            }

        }
        static public void Execute()
        {
            foreach (var item in FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                DamageSystem.CalculateDamage(Global.BombDamage.Value, item);
                //item.Hurt(Mathf.Min(item.HP, Global.BombDamage.Value), true);
            };
            AudioKit.PlaySound("bomb");
            UIGamePanel.FlashScreen.Trigger();
            CameraController.Shake();

        }
        protected override Collider2D Collider => SelfCollider2D;
    }
}
