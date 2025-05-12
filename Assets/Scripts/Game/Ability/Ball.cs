using QAssetBundle;
using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class Ball : ViewController
    {
        void Start()
        {
            SelfRigidbody2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1, 1f))
                 * Random.Range(Global.BasketBallspeed.Value - 2, Global.BasketBallspeed.Value + 2);
            HurtBox.OnTriggerEnter2DEvent(collider =>
            {

                var hurtBox = collider.GetComponent<HitHubBox>();
                if (hurtBox && hurtBox.Owner.CompareTag("Enemy"))
                {
                    var enemy = hurtBox.Owner.GetComponent<IEnemy>();
                    var damageTimes = Global.SuperBasketBall.Value ? Random.Range(2, 4) : 1;
                    if (enemy != null)
                        DamageSystem.CalculateDamage(Global.BasketBallDamage.Value * damageTimes, enemy);
                    //enemy.Hurt(Global.BasketBallDamage.Value);
                    if (Random.Range(0, 1f) < 0.5f && collider?.attachedRigidbody && Player.Default)
                    {
                        collider.attachedRigidbody.velocity = (collider.NormalizedDirection2DFrom(this) * 5 +
                        (collider.NormalizedDirection2DFrom(Player.Default) * 10)
                        );
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.SuperBasketBall.RegisterWithInitValue(_ =>
            {
                if (_)
                {
                    this.LocalScale(3);
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            AudioKit.PlaySound(Sfs.BALL);
            var normal = other.GetContact(0).normal;
            if (normal.x > normal.y)
            {
                SelfRigidbody2D.velocity = new Vector2(SelfRigidbody2D.velocity.x,
                    Mathf.Sign(SelfRigidbody2D.velocity.y) * Random.Range(0.5f, 1.5f)
                    * Random.Range(Global.BasketBallspeed.Value - 2, Global.BasketBallspeed.Value + 2)
                    );
                SelfRigidbody2D.angularVelocity = Random.Range(-360, 360);
            }
            else
            {
                SelfRigidbody2D.velocity = new Vector2(Mathf.Sign(SelfRigidbody2D.velocity.x) * Random.Range(0.5f, 1.5f)
                    * Random.Range(Global.BasketBallspeed.Value - 2, Global.BasketBallspeed.Value + 2)
                    , SelfRigidbody2D.velocity.y
                    );
                SelfRigidbody2D.angularVelocity = Random.Range(-360, 360);
            }
        }
    }
}
