using QAssetBundle;
using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class Player : ViewController
    {
        public float moveSpeed = 5f;
        public static Player Default;
        private bool mFaceRight = true;
        private AudioPlayer mAudioKit;
        private void Awake()
        {
            Default = this;
        }
        private void OnDestroy()
        {
            Default = null;
        }
        void Start()
        {
            // Code Here
            //Square.LogInfo();
            HurtBox.OnTriggerEnter2DEvent(Collider2D =>
            {

                var hitBox = Collider2D.GetComponent<HitHubBox>();

                if (hitBox)
                {
                    var Enemy = hitBox.Owner.GetComponent<Enemy>();
                    if (Enemy && hitBox.Owner.CompareTag("Enemy"))
                    {
                        Global.HP.Value--;
                        if (Global.HP.Value <= 0)
                        {
                            AudioKit.PlaySound("die");
                            this.DestroyGameObjGracefully();
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else
                        {
                            Enemy.StopTime = 30;
                            AudioKit.PlaySound("hurt");
                        }

                    }

                }


            }).UnRegisterWhenCurrentSceneUnloaded();
            void UpHp()
            {
                HPValue.fillAmount = (Global.HP.Value / (float)Global.MaxHP.Value);
            }
            Global.HP.RegisterWithInitValue((HP) =>
            {
                UpHp();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHP.RegisterWithInitValue((MaxHP) =>
            {
                UpHp();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {

            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var dir = new Vector2(horizontal, vertical).normalized * moveSpeed * Global.MovementSpeedRate.Value;
            PlayerSelfRigidbody2D.velocity = Vector2.Lerp(PlayerSelfRigidbody2D.velocity, dir, 1.0f - Mathf.Exp(-Time.deltaTime * 5));

            if (horizontal == 0 && vertical == 0)
            {
                if (mFaceRight)
                {

                    Square.Play("PlayeridleRight");
                }
                else
                {
                    Square.Play("PlayeridleLift");
                }
                if (mAudioKit != null)
                {
                    mAudioKit.Stop();
                    mAudioKit = null;
                }
            }
            else
            {
                if (mAudioKit == null)
                {
                    mAudioKit = AudioKit.PlaySound(Sfs.WALK, true);
                }
                if (horizontal > 0)
                {
                    mFaceRight = true;
                }
                else if (horizontal < 0)
                {
                    mFaceRight = false;
                }
                if (mFaceRight)
                {
                    Square.Play("PlayerwarkRight");

                }
                else
                {

                    Square.Play("PlayerwarkLift");
                }
            }
        }
    }
}
