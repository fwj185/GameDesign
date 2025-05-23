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

        // 添加三种不同敌人的预制体
        public GameObject EnemyPrefab1;
        public GameObject EnemyPrefab2;
        public GameObject EnemyPrefab3;

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
                    
                    // 生成三个不同的敌人
                    SpawnEnemy(currenwave.EnemPrefab, 0);
                    SpawnEnemy(EnemyPrefab1 != null ? EnemyPrefab1 : currenwave.EnemPrefab, 1);
                    SpawnEnemy(EnemyPrefab2 != null ? EnemyPrefab2 : currenwave.EnemPrefab, 2);
                }
                if (mCurrentWaveSeconds > currenwave.Seconds)
                {
                    currenwave = null;
                }
            }
        }

        // 新增生成敌人的方法
        private void SpawnEnemy(GameObject enemyPrefab, int index)
        {
            if (enemyPrefab == null) return;

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

            // 添加一些偏移，避免敌人重叠
            Vector2 offset = new Vector2(index * 0.5f, index * 0.5f);
            pos += offset;

            enemyPrefab.Instantiate().Position(pos)
                .Self((self) =>
                {
                    var enemy = self.GetComponent<IEnemy>();
                    if (enemy != null)
                    {
                        enemy.SetSpeedScale(currenwave.SpeedScale);
                        enemy.SetHPScale(currenwave.HPScale);
                    }
                })
                .Show();
        }
    }
}