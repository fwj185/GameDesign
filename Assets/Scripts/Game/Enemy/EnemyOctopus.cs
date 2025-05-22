using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    /// <summary>
    /// 章鱼怪敌人
    /// </summary>
    public partial class EnemyOctopus : ViewController, IEnemy
    {
        public float HP = 20f;
        public float MoveSpeed = 2.0f;
        public float AttackRange = 5.0f;
        public float AttackValue = 10.0f;
        private bool mIgnoreHurt = false;
        public Color diosColor = new Color(0.8f, 0.4f, 0.8f); // 粉紫色
        
        // 攻击冷却时间
        private float mAttackCooldown = 1.5f;
        private float mCurrentCooldown = 0f;
        
        // 添加必要的组件引用
        public SpriteRenderer Triangle;
        public Rigidbody2D SelfRigidbody2D;
        
        void Start()
        {
            EnemyGenerator.EnemyNum.Value++;
            // 设置章鱼怪的颜色 - 粉紫色
            Triangle.color = diosColor;
        }
        
        private void OnDisable()
        {
            EnemyGenerator.EnemyNum.Value--;
        }
        
        private void Update()
        {
            if (HP <= 0f)
            {
                Global.GeneratPowerUp(gameObject, false);
                FxController.Play(Triangle, diosColor);
                this.DestroyGameObjGracefully();
                AudioKit.PlaySound("enemy_die"); // 修改为字符串形式
            }
            
            // 处理攻击冷却
            if (mCurrentCooldown > 0)
            {
                mCurrentCooldown -= Time.deltaTime;
            }
            
            // 检查是否可以攻击
            if (Player.Default && mCurrentCooldown <= 0)
            {
                float distanceToPlayer = (Player.Default.transform.position - transform.position).magnitude;
                if (distanceToPlayer <= AttackRange)
                {
                    // 触手攻击
                    AttackPlayer();
                    mCurrentCooldown = mAttackCooldown;
                }
            }
        }
        
        private void AttackPlayer()
        {
            // 触手攻击逻辑
            if (Player.Default)
            {
                // 直接减少玩家生命值
                Global.HP.Value--;
                if (Global.HP.Value <= 0)
                {
                    AudioKit.PlaySound("die");
                    Player.Default.DestroyGameObjGracefully();
                    UIKit.OpenPanel<UIGameOverPanel>();
                }
                else
                {
                    AudioKit.PlaySound("hurt");
                }
                
                // 攻击时短暂停顿
                SelfRigidbody2D.velocity = Vector3.zero;
                ActionKit.Delay(0.5f, () => {
                    // 攻击后恢复移动
                }).Start(this);
            }
        }
        
        private void FixedUpdate()
        {
            if (mIgnoreHurt)
                return;
                
            if (Player.Default)
            {
                var dir = (Player.Default.transform.position - transform.position).normalized;
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
                mIgnoreHurt = false;
                enemyRef.Triangle.color = diosColor; // 恢复原来的颜色
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