using QFramework;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor
{
    /// <summary>
    /// 主角的能力
    /// </summary>
    public partial class SimpleSword : ViewController
    {
        private float mCurrentSeconds = 0;
        void Update()
        {
            mCurrentSeconds += Time.deltaTime;
            if (mCurrentSeconds > Global.SimplAbiltyDuration.Value)
            {
                mCurrentSeconds = 0;
                var countTimes = Global.SuperSword.Value ? 2 : 1;
                var damageTimes = Global.SuperSword.Value ? Random.Range(2, 4) : 1;
                var disTanceTimes = Global.SuperSword.Value ? 2 : 1;
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                foreach (Enemy enemy in enemies.OrderBy(e => Player.Default.Direction2DFrom(e).magnitude)
                    .Where(e => e.Direction2DFrom(Player.Default).magnitude < Global.SimpleSwordRange.Value * disTanceTimes)
                    .Take((Global.SimpleSwordCount.Value + Global.AdditionalFlyThingCount.Value) * countTimes))
                {
                    var dir = Player.Default.Direction2DFrom(enemy).magnitude;

                    //if (dir < 6.5f)
                    //{
                    //enemy.Hurt(Global.SimpleAbillity.Value);
                    Sword.Instantiate()
                        .Position(enemy.Position() + Vector3.left * 0.25f)
                        .Show()
                        .Self(self =>
                        {
                            var selfCache = self;
                            selfCache.OnTriggerEnter2DEvent(collider2D =>
                            {
                                var hurtBox = collider2D.GetComponent<HitHubBox>();
                                if (hurtBox != null)
                                {
                                    if (enemy && hurtBox.Owner.CompareTag("Enemy"))
                                    {
                                        var enemy2 = hurtBox.Owner.GetComponent<Enemy>();
                                        DamageSystem.CalculateDamage(Global.SimpleAbilityDamage.Value * damageTimes, enemy2);
                                    }
                                }
                            }).UnRegisterWhenGameObjectDestroyed(gameObject);


                            //动画效果
                            ActionKit.Sequence()
                            .Callback(() => selfCache.enabled = false)
                            .Parallel(p =>
                            {
                                p.Lerp(0, 10, 0.2f, (z) =>
                                {
                                    selfCache.LocalEulerAnglesZ(z);
                                });
                                p.Append(ActionKit.Sequence()
                                    .Lerp(0, 1.25f, 0.1f, scale =>
                                    {
                                        selfCache.LocalScale(scale);
                                    })
                                    .Lerp(1.25f, 1f, 0.1f, scale =>
                                    {
                                        selfCache.LocalScale(scale);
                                    })
                                    );
                            })
                            .Callback(() => selfCache.enabled = true)
                            .Parallel(p =>
                            {
                                p.Lerp(10, -180, 0.1f, z =>
                                {
                                    selfCache.LocalEulerAnglesZ(z);
                                });
                                p.Append(ActionKit.Sequence()
                                .Lerp(1, 1.25f, 0.05f, scale => selfCache.LocalScale(scale))
                                .Lerp(1.25f, 1f, 0.05f, scale => selfCache.LocalScale(scale))
                                    );
                            })
                            .Callback(() => selfCache.enabled = false)
                            .Lerp(-180, 0, 0.3f, z =>
                            {
                                selfCache.LocalEulerAnglesZ(z);
                                selfCache.LocalScale(z.Abs() / 180);
                            })
                            .Start(this, () =>
                            {
                                selfCache.DestroyGameObjGracefully();
                            });
                        });

                    //}
                }
            }
        }
    }
}
