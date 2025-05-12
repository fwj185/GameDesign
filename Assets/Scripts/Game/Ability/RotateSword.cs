using QFramework;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class RotateSword : ViewController
    {
        private List<CircleCollider2D> mSwords = new();
        void Start()
        {
            // Code Here
            Sword.Hide();
            Global.RotateSwordCount.Or(Global.AdditionalFlyThingCount).Register(() =>
            {
                this.CreateBall();
            });

            Global.RotateSwordRange.Register((_) =>
            {
                UpdateCirclePos();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.CreateBall();
            this.UpdateCirclePos();
        }
        void UpdateCirclePos()
        {
            if (mSwords.Count == 0)
                return;
            var radius = Global.RotateSwordRange.Value;
            var durationDegrees = 360 / mSwords.Count;
            for (int i = 0; i < mSwords.Count; i++)
            {
                var circleLocalPos = new Vector2(Mathf.Cos(durationDegrees * i * Mathf.Deg2Rad), Mathf.Sin(durationDegrees * i * Mathf.Deg2Rad)) * radius;
                mSwords[i].LocalPosition(circleLocalPos.x, circleLocalPos.y)
                .LocalEulerAnglesZ(durationDegrees * i - 90);
            }

        }
        private void Update()
        {
            var damageSpeed = Global.SuperRotateSword.Value ? 4 : 1;
            var degree = Time.frameCount * Global.RotateSwordSpeed.Value * damageSpeed;

            this.LocalEulerAnglesZ(-degree);
        }
        private void CreateBall()
        {
            var count = Global.RotateSwordCount.Value + Global.AdditionalFlyThingCount.Value;
            var toAddCount = mSwords.Count;
            for (int j = toAddCount; j < count; j++)
            {
                var sw = Sword.InstantiateWithParent(this).Show();
                mSwords.Add(sw);
                sw.OnTriggerEnter2DEvent(collider =>
                {
                    var hurtBox = collider.GetComponent<HitHubBox>();
                    if (hurtBox != null)
                    {
                        if (hurtBox.Owner.CompareTag("Enemy"))
                        {
                            var damageTimes = Global.SuperRotateSword.Value ? Random.Range(2, 4) : 1;
                            var enemy = hurtBox.Owner.GetComponent<Enemy>();
                            DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value * damageTimes, enemy);
                            //hurtBox.Owner.GetComponent<Enemy>().Hurt(Global.RotateSwordDamage.Value);
                            if (Random.Range(0, 1f) < 0.5f)
                            {
                                collider.attachedRigidbody.velocity = collider.NormalizedDirection2DFrom(Player.Default) * 10;
                                //collider.NormalizedDirection2DFrom(sw) * 5;

                            }
                        }
                    }
                }).UnRegisterWhenGameObjectDestroyed(sw);
            }
            UpdateCirclePos();


        }
    }

}
