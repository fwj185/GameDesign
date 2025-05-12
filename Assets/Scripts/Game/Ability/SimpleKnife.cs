using QAssetBundle;
using QFramework;
using System;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class SimpleKnife : ViewController
    {
        void Start()
        {
            // Code Here
        }
        private float mCurrentSeconds = 0f;
        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
            if (mCurrentSeconds > Global.SimpleKnifDuration.Value && Player.Default)
            {
                var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .OrderBy(enemy => Player.Default.Distance2D(enemy))
                .Take(Global.SimpleKnifCount.Value + Global.AdditionalFlyThingCount.Value);
                var i = 0;
                enemies.ForEach(enem =>
                {
                    if (enem)
                    {
                        if (i < 4)
                        {
                            ActionKit.DelayFrame(11 * i, () =>
                            {
                                AudioKit.PlaySound(Sfs.KNIFE);
                            }).StartGlobal();

                            i++;
                        }

                        Kinfe.Instantiate()
                        .Show()
                        .Position(this.Position())
                        .Self((self) =>
                        {

                            var selfCache = self;
                            var direction = enem.NormalizedDirection2DFrom(Player.Default);
                            self.transform.up = direction;
                            var rigidbody2D = self.GetComponent<Rigidbody2D>();
                            rigidbody2D.velocity = direction * 10;
                            var hit = 0;
                            self.OnTriggerEnter2DEvent((collider) =>
                            {
                                var hurtBox = collider.GetComponent<HitHubBox>();
                                if (hurtBox != null)
                                {
                                    if (hurtBox.Owner.CompareTag("Enemy"))
                                    {
                                        hit++;
                                        if (hit <= Global.SimpleAttackCount.Value)
                                        {
                                            var damageTimes = Global.SuperKnife.Value ? UnityEngine.Random.Range(2, 4) : 1;
                                            var enemy = hurtBox.Owner.GetComponent<Enemy>();
                                            DamageSystem.CalculateDamage(Global.SimpleKnifDamage.Value * damageTimes, enemy);
                                        }
                                        if (hit >= Global.SimpleAttackCount.Value)
                                            selfCache.DestroyGameObjGracefully();
                                    }
                                }
                            }).UnRegisterWhenGameObjectDestroyed(self);
                            ActionKit.OnUpdate.Register(() =>
                            {
                                if (Player.Default)
                                {
                                    if (Player.Default.Distance2D(self) > 20)
                                    {
                                        selfCache.DestroyGameObjGracefully();
                                    }
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);
                        });
                        mCurrentSeconds = 0f;
                    }

                });


            }
        }
    }
}
