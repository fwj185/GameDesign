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

        // 添加三种海怪的预制体引用
        public GameObject LanternFishPrefab;
        public GameObject OctopusPrefab;
        public GameObject SharkPrefab;

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
                    
                    // 随机选择一种海怪生成
                    GameObject enemyPrefab = GetRandomEnemyPrefab();
                    Vector3 enemyScale = Vector3.one; // 默认缩放值，以防万一

                    // 根据选择的预制体设置对应的缩放值
                    if (enemyPrefab == LanternFishPrefab)
                    {
                        enemyScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                    else if (enemyPrefab == OctopusPrefab)
                    {
                        enemyScale = new Vector3(0.12f, 0.12f, 0.12f);
                    }
                    else if (enemyPrefab == SharkPrefab) // 假设 SharkPrefab 是第三种敌人
                    {
                        enemyScale = new Vector3(0.16f, 0.16f, 0.16f);
                    }
                    
                    enemyPrefab.Instantiate().Position(pos)
                        .Self((self) =>
                        {
                            self.transform.localScale = enemyScale; // 在这里设置缩放
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
        
        // 根据不同的难度和波次，随机选择一种海怪
        private GameObject GetRandomEnemyPrefab()
        {
            // 根据游戏进度或难度调整各种敌人的出现概率
            float gameProgress = Mathf.Clamp01(Time.timeSinceLevelLoad / 300f); // 假设游戏最长5分钟
            
            float lanternFishChance = 0.7f - gameProgress * 0.4f; // 随着游戏进行，灯笼鱼出现概率降低
            float octopusChance = 0.2f + gameProgress * 0.1f;    // 章鱼怪概率略微增加
            float sharkChance = 0.1f + gameProgress * 0.3f;      // 鲨鱼怪概率大幅增加
            
            float randomValue = Random.value;
            
            if (randomValue < lanternFishChance)
            {
                return LanternFishPrefab;
            }
            else if (randomValue < lanternFishChance + octopusChance)
            {
                return OctopusPrefab;
            }
            else
            {
                return SharkPrefab;
            }
        }
    }
}
