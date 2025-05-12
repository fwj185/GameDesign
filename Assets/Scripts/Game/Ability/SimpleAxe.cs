using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    /// <summary>
    /// 不做这个功能了
    /// </summary>
    public partial class SimpleAxe : ViewController
    {
        void Start()
        {
            // Code Here
        }
        private float mCurrentSeconds = 0f;
        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
            if (mCurrentSeconds > 1.0f)
            {
                Axe.Instantiate()
                    .Show()
                    .Position(this.Position())
                    .Self((self) =>
                    {
                        var rigidbody2D = self.GetComponent<Rigidbody2D>();
                        var randomX = RandomUtility.Choose(-8, -5, -3, 3, 5, 8);
                        var randomY = RandomUtility.Choose(3, 5, 8);
                        rigidbody2D.velocity = new Vector2(randomX, randomY);
                        self.OnTriggerEnter2DEvent((collider) =>
                        {
                            var hurtBox = collider.GetComponent<HitHubBox>();
                            if (hurtBox != null)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    hurtBox.Owner.GetComponent<Enemy>().Hurt(1);
                                }
                            }
                        }).UnRegisterWhenGameObjectDestroyed(self);
                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (Player.Default)
                            {
                                if (Player.Default.Position().y - self.Position().y > 15)
                                {

                                    self.DestroyGameObjGracefully();
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);
                        mCurrentSeconds = 0;
                    });
            }
        }
    }
}
