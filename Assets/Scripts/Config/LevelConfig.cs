
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PrjectSurvivor
{
    [CreateAssetMenu(fileName = "关卡配置")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField]
        public List<EnemyWaveGroup> EnemyWaveGroups = new List<EnemyWaveGroup>();
    }
    [Serializable]
    public class EnemyWaveGroup
    {
        public string Name;
        [TextArea] public string Description = string.Empty;
        [SerializeField]
        public List<EnemyWave> Waves = new List<EnemyWave>();
    }
    /// <summary>
    /// 敌人的波次
    /// </summary>
    [Serializable]
    public class EnemyWave
    {
        public string Name;
        public bool Active = true;
        public float GenerateDuration = 1;//生成间隔时间
        public GameObject EnemPrefab;
        public int Seconds = 10;//生成总时长
        public float HPScale = 1.0f;
        public float SpeedScale = 1.0f;
    }
}
