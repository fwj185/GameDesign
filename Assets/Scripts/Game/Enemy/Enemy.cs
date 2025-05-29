using QAssetBundle;
using QFramework;
using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace PrjectSurvivor
{
    /// <summary>
    /// 敌人类
    /// </summary>
    public partial class Enemy : ViewController, IEnemy
    {
        public int StopTime = 0;
        public float HP = 3f;
        public float MoveSpeed = 2.0f;
        private bool mIgnoreHurt = false;
        public Color diosColor = Color.yellow;
        public bool TreasureChestEnemy = false;
        public
        void Start()
        {
            EnemyGenerator.EnemyNum.Value++;
        }
        private void OnDisable()
        {
            EnemyGenerator.EnemyNum.Value--;
        }
        private void Update()
        {

            if (HP <= 0f)
            {
                Global.GeneratPowerUp(gameObject, TreasureChestEnemy);
                // FxController.Play(Triangle, diosColor); // 移除或注释掉这行
                this.DestroyGameObjGracefully();
                AudioKit.PlaySound(Sfs.ENEMY_DIE);
            }
        }
        private void FixedUpdate()
        {
            if (StopTime > 0)
            {
                SelfRigidbody2D.velocity = Vector3.zero;
                StopTime--;

                return;
            }
            if (mIgnoreHurt)
                return;
            if (Player.Default)
            {
                //var player = FindObjectOfType<Player>();
                var dir = (Player.Default.transform.position - transform.position).normalized;
                //var dirNor = (Player.Default.transform.position - transform.position).magnitude;
                //if()

                // 计算朝向玩家的角度
                // Mathf.Atan2 返回的是弧度，需要转换为角度
                // dir.y 是Y轴方向，dir.x 是X轴方向
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                // 应用旋转，使敌人朝向玩家
                // 对于2D游戏，通常是绕Z轴旋转
                // 如果您的敌人Sprite默认是朝右的，这个角度是正确的
                // 如果默认朝向不同，您可能需要调整角度，例如默认朝上则需要 angle - 90f
                transform.rotation = Quaternion.Euler(0f, 0f, angle);

                SelfRigidbody2D.velocity = dir * MoveSpeed;
            }
            else
            {
                SelfRigidbody2D.velocity = Vector3.zero;
            }
        }
        public void Hurt(float hp, bool force = false, bool critical = false)
        {
            if (mIgnoreHurt && !force)
            {
                return;
            }
            SelfRigidbody2D.velocity = Vector3.zero;
            mIgnoreHurt = true;
            var enemyRef = this;
            FloatingTextController.Player(transform.position, hp, critical);
            Triangle.color = Color.red;
            AudioKit.PlaySound("hit");
            enemyRef.HP -= hp;
            ActionKit.Delay(0.3f, () =>
            {
                //enemyRef.HP -= hp;
                mIgnoreHurt = false;
                enemyRef.Triangle.color = Color.white;
            }).Start(this);
        }

        public void SetSpeedScale(float scale)
        {
            MoveSpeed *= scale;
        }

        public void SetHPScale(float scale)
        {
            HP *= scale;
        }
    }
}
