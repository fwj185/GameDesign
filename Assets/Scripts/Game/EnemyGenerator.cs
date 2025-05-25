using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor
{

    /// <summary>
    /// 敌人的波次
    /// </summary>
    //[Serializable]
    //public class EnemyWave
    //{
    //    public float GenerateDuration = 1;
    //    public GameObject EnemPrefab;
    //    public int Seconds = 10;
    //}
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public partial class EnemyGenerator : ViewController
    {
        public LevelConfig Config;
        private float mCurrentSeconds = 0f;//当前生成时间
        private float mCurrentWaveSeconds = 0f;//当前波次时间
        private Queue<EnemyWave> enemiesQueue = new();
        public bool isLast => enemiesQueue.Count == 0;
        private bool LastWave => isLast;//没用的
        private int WaveCunt = 0;//没用的
        public EnemyWave Currentwave { get { return currenwave; } }//public EnemyWave Currentwave => currenwave;

        public static BindableProperty<int> EnemyNum = new BindableProperty<int>(0);


        private EnemyWave currenwave = null;
        void Start()
        {
            foreach (var item in Config.EnemyWaveGroups)
            {
                item.Waves.ForEach(wave =>
                {
                    enemiesQueue.Enqueue(wave);
                });

            }
        }
        private void Update()
        {

            if (currenwave == null && enemiesQueue.Count > 0)
            {
                currenwave = enemiesQueue.Dequeue();
                mCurrentSeconds = 0;
                mCurrentWaveSeconds = 0;
            }

            if (currenwave != null)
            {
                mCurrentSeconds += Time.deltaTime;
                mCurrentWaveSeconds += Time.deltaTime;

                if (mCurrentSeconds > currenwave.GenerateDuration && Player.Default != null)
                {
                    mCurrentSeconds = 0f;
                    var xOry = RandomUtility.Choose(-1, 1);
                    var pos = Vector2.zero;
                    if (xOry == -1)
                    {
                        pos.x = RandomUtility.Choose(CameraController.mLB.position.x, CameraController.mRT.position.x);
                        pos.y = Random.Range(CameraController.mLB.position.y, CameraController.mRT.position.y);
                    }
                    else
                    {
                        pos.y = RandomUtility.Choose(CameraController.mLB.position.y, CameraController.mRT.position.y);
                        pos.x = Random.Range(CameraController.mLB.position.x, CameraController.mRT.position.x);
                    }
                    currenwave.EnemPrefab.Instantiate().Position(pos)
                        .Self((self) =>
                        {
                            var enemy = self.GetComponent<IEnemy>();
                            enemy.SetSpeedScale(currenwave.SpeedScale);
                            enemy.SetHPScale(currenwave.HPScale);
                        })
                        .Show();
                }
                if (mCurrentWaveSeconds > currenwave.Seconds)
                {
                    currenwave = null;
                }

            }


        }
    }
}
