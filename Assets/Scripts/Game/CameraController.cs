using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class CameraController : ViewController
    {
        private Vector2 mTargetPosition = Vector2.zero;//玩家位置
        private Vector3 mCurrentCameraPos;//临时位置
        private bool mShake = false;//是否震动
        private int mShakeFrame = 0;//震动帧数
        private float mShakeA = 2.0f;// 振幅
        public static CameraController Default;
        public static Transform mLB => Default.LB;
        public static Transform mRT => Default.RT;
        private void Awake()
        {
            Default = this;
        }

        public static void Shake()
        {
            Default.mShake = true;
            Default.mShakeFrame = 30;
            Default.mShakeA = 0.2f;
        }
        private void OnDestroy()
        {
            Default = null;
        }
        void Start()
        {
            Application.targetFrameRate = 60;
            // Code Here
        }
        private void Update()
        {
            if (Player.Default)
            {
                mTargetPosition = Player.Default.transform.position;
                mCurrentCameraPos.x = Mathf.Lerp(transform.position.x, mTargetPosition.x, 1.0f - Mathf.Exp(-Time.deltaTime * 5));
                mCurrentCameraPos.y = Mathf.Lerp(transform.position.y, mTargetPosition.y, 1.0f - Mathf.Exp(-Time.deltaTime * 5));
                mCurrentCameraPos.z = transform.position.z;
                //mCurrentCameraPos = transform.position;
                if (mShake)
                {
                    if (mShakeFrame <= 0)
                    {
                        mShake = false;
                    }
                    var ShakeA = Mathf.Lerp(mShakeA, 0.0f, (mShakeFrame / 30.0f));
                    transform.position = new Vector3(mCurrentCameraPos.x + Random.Range(-ShakeA, ShakeA), mCurrentCameraPos.y + Random.Range(-ShakeA, ShakeA), mCurrentCameraPos.z);
                    mShakeFrame--;
                }
                else
                {
                    transform.PositionX(mCurrentCameraPos.x);
                    transform.PositionY(mCurrentCameraPos.y);
                }

            }
        }
    }
}
