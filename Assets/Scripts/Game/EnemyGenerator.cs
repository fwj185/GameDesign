using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor
{
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public partial class EnemyGenerator : ViewController
    {
        [Header("关卡配置")]
        public LevelConfig defaultConfig; // 默认关卡配置,第一关
        public LevelConfig secondConfig;  // 第二关
        public LevelConfig thirdConfig;   // 第三关

        public LevelConfig[] allConfigs; // 所有关卡配置的数组
        private int currentLevelIndex = 0; // 当前关卡索引
        private LevelConfig currentConfig; // 当前使用的关卡配置

        private float mCurrentSeconds = 0f; // 当前生成时间
        private float mCurrentWaveSeconds = 0f; // 当前波次时间
        private Queue<EnemyWave> enemiesQueue = new();
        public bool isLast => enemiesQueue.Count == 0;
        private bool LastWave => isLast; // 未使用的属性
        private int WaveCunt = 0; // 未使用的变量

        public EnemyWave Currentwave { get { return currenwave; } }

        public static BindableProperty<int> EnemyNum = new BindableProperty<int>(0);

        private EnemyWave currenwave = null;

        void Start()
        {
            // 初始化关卡配置数组
            allConfigs = new LevelConfig[] { defaultConfig, secondConfig, thirdConfig };

            // 使用默认配置或通过代码设置的配置
            SetCurrentLevel(defaultConfig);
        }

        // 设置当前关卡配置
        public void SetCurrentLevel(LevelConfig config)
        {
            if (config != null)
            {
                // 查找配置在数组中的索引
                for (int i = 0; i < allConfigs.Length; i++)
                {
                    if (allConfigs[i] == config)
                    {
                        currentLevelIndex = i;
                        break;
                    }
                }

                currentConfig = config;
                InitializeLevel();
            }
            else
            {
                Debug.LogError("尝试设置空的关卡配置！");
            }
        }

        private void InitializeLevel()
        {
            if (currentConfig == null)
            {
                Debug.LogWarning("没有可用的关卡配置，使用默认配置...");
                currentConfig = defaultConfig;
            }

            enemiesQueue.Clear();
            foreach (var item in currentConfig.EnemyWaveGroups)
            {
                item.Waves.ForEach(wave =>
                {
                    if (wave.Active) // 只添加启用的波次
                        enemiesQueue.Enqueue(wave);
                });
            }
            mCurrentSeconds = 0;
            mCurrentWaveSeconds = 0;
            currenwave = null;
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

        public void StartNextLevel()
        {
            // 查找当前场景中是否存在 UIGamePanel 实例
            UIGamePanel existingPanel = FindObjectOfType<UIGamePanel>();

            // 如果存在则显示，否则创建新的面板
            if (existingPanel != null)
            {
                existingPanel.ShowSelf();
            }
            else
            {
                // 如果面板不存在，则重新打开
                UIKit.OpenPanel<UIGamePanel>();
            }
            // 检查关卡配置数组
            if (allConfigs == null || allConfigs.Length == 0)
            {
                Debug.LogError("没有配置任何关卡！");
                return;
            }

            // 计算下一个关卡索引
            int nextLevelIndex = currentLevelIndex + 1;

            // 检查是否有下一关
            if (nextLevelIndex < allConfigs.Length && allConfigs[nextLevelIndex] != null)
            {
                currentLevelIndex = nextLevelIndex;
                SetCurrentLevel(allConfigs[currentLevelIndex]);
                Debug.Log($"加载第 {currentLevelIndex + 1} 关");
            }
            else
            {
                Debug.Log("已经是最后一关，无法加载下一关");
                // 这里可以添加游戏通关的逻辑
            }
        }

        public void RestartLevel()
        {

            // 查找当前场景中是否存在 UIGamePanel 实例
            UIGamePanel existingPanel = FindObjectOfType<UIGamePanel>();

            // 如果存在则显示，否则创建新的面板
            if (existingPanel != null)
            {
                existingPanel.ShowSelf();
            }
            else
            {
                // 如果面板不存在，则重新打开
                UIKit.OpenPanel<UIGamePanel>();
            }

            if (currentConfig != null)
            {
                InitializeLevel();
                Debug.Log($"重新加载第 {currentLevelIndex + 1} 关");
            }
            else
            {
                Debug.LogError("没有当前关卡配置，无法重新加载");
            }
        }
    }
}