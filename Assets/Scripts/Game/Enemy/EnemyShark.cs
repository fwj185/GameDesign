using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    /// <summary>
    /// 鲨鱼怪敌人
    /// </summary>
    public partial class EnemyShark : ViewController, IEnemy
    {
        public float HP = 50f;
        public float MoveSpeed = 1.0f;
        public float AttackRange = 5.0f;
        public float AttackValue = 30.0f;
        private bool mIgnoreHurt = false;
        public Color diosColor = new Color(0.5f, 0.0f, 0.5f); // 暗紫色
        
        // 添加必要的组件引用
        public SpriteRenderer Triangle;
        public Rigidbody2D SelfRigidbody2D;
        
        // 攻击状态
        private enum States
        {
            Chasing,    // 追逐玩家
            Preparing,  // 准备攻击
            Attacking,  // 攻击中
            Cooldown    // 冷却
        }
        
        private States mCurrentState = States.Chasing;
        private float mStateTimer = 0f;
        private Vector3 mAttackDirection;
        
        void Start()
        {
            EnemyGenerator.EnemyNum.Value++;
            // 设置鲨鱼怪的颜色 - 暗紫色
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
                Global.GeneratPowerUp(gameObject, true); // 鲨鱼怪掉落更好的道具
                FxController.Play(Triangle, diosColor);
                this.DestroyGameObjGracefully();
                AudioKit.PlaySound("enemy_die"); // 修改为字符串形式
            }
            
            // 状态机更新
            UpdateState();
        }
        
        private void UpdateState()
        {
            mStateTimer += Time.deltaTime;
            
            switch (mCurrentState)
            {
                case States.Chasing:
                    // 在追逐状态下，检查是否可以进入准备攻击状态
                    if (Player.Default)
                    {
                        float distanceToPlayer = (Player.Default.transform.position - transform.position).magnitude;
                        if (distanceToPlayer <= AttackRange)
                        {
                            // 进入准备攻击状态
                            mCurrentState = States.Preparing;
                            mStateTimer = 0f;
                            SelfRigidbody2D.velocity = Vector3.zero;
                            Triangle.color = Color.red; // 变红表示准备攻击
                        }
                    }
                    break;
                    
                case States.Preparing:
                    // 准备攻击状态，持续1秒
                    if (mStateTimer >= 1.0f)
                    {
                        // 进入攻击状态
                        mCurrentState = States.Attacking;
                        mStateTimer = 0f;
                        
                        // 记录攻击方向
                        if (Player.Default)
                        {
                            mAttackDirection = (Player.Default.transform.position - transform.position).normalized;
                        }
                        else
                        {
                            mAttackDirection = Vector3.right;
                        }
                    }
                    break;
                    
                case States.Attacking:
                    // 攻击状态，持续0.5秒
                    // 高速冲向记录的方向
                    SelfRigidbody2D.velocity = mAttackDirection * MoveSpeed * 3;
                    
                    // 检查是否击中玩家
                    if (Player.Default)
                    {
                        float distanceToPlayer = (Player.Default.transform.position - transform.position).magnitude;
                        if (distanceToPlayer < 1.0f)
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
                        }
                    }
                    
                    if (mStateTimer >= 0.5f)
                    {
                        // 进入冷却状态
                        mCurrentState = States.Cooldown;
                        mStateTimer = 0f;
                        SelfRigidbody2D.velocity = Vector3.zero;
                    }
                    break;
                    
                case States.Cooldown:
                    // 冷却状态，持续2秒
                    if (mStateTimer >= 2.0f)
                    {
                        // 回到追逐状态
                        mCurrentState = States.Chasing;
                        mStateTimer = 0f;
                        Triangle.color = diosColor; // 恢复原来的颜色
                    }
                    break;
            }
        }
        
        private void FixedUpdate()
        {
            if (mIgnoreHurt)
                return;
                
            // 只在追逐状态下移动
            if (mCurrentState == States.Chasing && Player.Default)
            {
                var dir = (Player.Default.transform.position - transform.position).normalized;
                SelfRigidbody2D.velocity = dir * MoveSpeed;
            }
        }
        
        public void Hurt(float hp, bool force = false, bool critical = false)
        {
            if (mIgnoreHurt && !force)
            {
                return;
            }
            
            mIgnoreHurt = true;
            var enemyRef = this;
            FloatingTextController.Player(transform.position, hp, critical);
            
            // 保存当前颜色
            Color originalColor = Triangle.color;
            Triangle.color = Color.red;
            
            AudioKit.PlaySound("hit");
            enemyRef.HP -= hp;
            
            ActionKit.Delay(0.3f, () =>
            {
                mIgnoreHurt = false;
                enemyRef.Triangle.color = originalColor; // 恢复原来的颜色
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