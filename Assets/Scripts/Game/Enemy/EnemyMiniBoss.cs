using QFramework;
using System;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class EnemyMiniBoss : ViewController, IEnemy
    {
        public float HP = 50f;
        private bool mIgnoreHurt = false;
        public float MoveSpeed = 3.0f;
        public enum States
        {
            FlowingPlayer,//默认
            Warning,//警戒
            Dash,//冲向主角
            Wait//等待时间
        };
        public FSM<States> FSM = new FSM<States>();
        void Start()
        {
            EnemyGenerator.EnemyNum.Value++;
            FSM.State(States.FlowingPlayer)
                .OnFixedUpdate(() =>
                {

                    if (Player.Default)
                    {
                        //var player = FindObjectOfType<Player>();
                        var dir = (Player.Default.transform.position - transform.position).normalized;
                        SelfRigidbody2D.velocity = dir * MoveSpeed;

                        if ((Player.Default.transform.position - transform.position).magnitude < 5)
                        {
                            FSM.ChangeState(States.Warning);
                        }
                    }
                    else
                    {
                        SelfRigidbody2D.velocity = Vector3.zero;
                    }
                })
                ;
            var dashstartPos = Vector3.zero;
            var dashStartDistanceToPlayer = 0f;
            FSM.State(States.Warning)
                .OnEnter(() =>
            {
                SelfRigidbody2D.velocity = Vector3.zero;
            })
                .OnUpdate(() =>
            {
                var frames = 3 + (60 * 3 - FSM.FrameCountOfCurrentState) / 10;
                if (FSM.FrameCountOfCurrentState / frames % 2 == 0)
                {
                    Sprit.color = Color.red;
                }
                else
                {
                    Sprit.color = Color.white;
                }
                if (FSM.FrameCountOfCurrentState >= 60 * 3)
                {

                    FSM.ChangeState(States.Dash);
                }
            }).OnExit(() =>
            {
                Sprit.color = Color.yellow;
            });
            FSM.State(States.Dash)
                .OnEnter(() =>
                {
                    dashstartPos = this.Position();
                    dashStartDistanceToPlayer = (Player.Default.transform.position - transform.position).magnitude;
                    var dir = (Player.Default.transform.position - transform.position).normalized;
                    SelfRigidbody2D.velocity = dir * 15;

                })
                .OnUpdate(() =>
                {
                    var distance = (this.Position() - dashstartPos).magnitude;
                    if (distance > dashStartDistanceToPlayer + 5)
                    {
                        FSM.ChangeState(States.FlowingPlayer);
                    }
                    if (FSM.FrameCountOfCurrentState >= 60 * 3)
                    {
                        FSM.ChangeState(States.Wait);
                    }
                });

            FSM.State(States.Wait).OnEnter(() =>
            {
                SelfRigidbody2D.velocity = Vector3.zero;
            }).OnUpdate(() =>
            {
                if (FSM.FrameCountOfCurrentState >= 60 * 1.5)
                {
                    FSM.StartState(States.FlowingPlayer);
                }
            });
            FSM.StartState(States.FlowingPlayer);
        }
        private void OnDisable()
        {
            EnemyGenerator.EnemyNum.Value--;
        }

        private void FixedUpdate()
        {
            FSM.FixedUpdate();

        }
        private void Update()
        {
            FSM.Update();
            //FSM.Update();
            if (HP <= 0f)
            {
                Global.GeneratPowerUp(gameObject, true);
                this.DestroyGameObjGracefully();
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
            Triangle.color = Color.red;
            AudioKit.PlaySound("hit");
            ActionKit.Delay(0.3f, () =>
            {
                mIgnoreHurt = false;
                enemyRef.HP -= hp;
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
