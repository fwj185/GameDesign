using PrjectSurvivor.Achievement;
using QFramework;
using System.Runtime.InteropServices;
using UnityEngine;

namespace PrjectSurvivor
{
    public class Global : Architecture<Global>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tool/Clear All Data")]
        public static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
        }
#endif
        /// <summary>
        /// hp
        /// </summary>
        public static BindableProperty<int> HP = new(3);

        public static BindableProperty<int> MaxHP = new(3);

        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new(0);

        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> LV = new(1);

        /// <summary>
        /// 解锁
        /// </summary>
        public static BindableProperty<bool> SimpleAbilityUnlocked = new(false);

        /// <summary>
        /// 攻击力
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new(Config.InitSimpleSwordDamage);

        /// <summary>
        /// 攻击间隔
        /// </summary>
        public static BindableProperty<float> SimplAbiltyDuration = new(Config.InitSimpleSwordDuration);

        /// <summary>
        /// 简单能力初始攻击个数
        /// </summary>
        public static BindableProperty<int> SimpleSwordCount = new(Config.InitSimpleSwordCount);

        /// <summary>
        /// 简单能力初始攻击范围
        /// </summary>
        public static BindableProperty<float> SimpleSwordRange = new(Config.InitSimpleSwordRange);

        /// <summary>
        /// 解锁刀
        /// </summary>
        public static BindableProperty<bool> SimpleKnifUnlocked = new(false);

        /// <summary>
        /// 小刀攻击力
        /// </summary>
        public static BindableProperty<float> SimpleKnifDamage = new(Config.InitSimpleKnifeDamage);

        /// <summary>
        /// 小刀CD
        /// </summary>
        public static BindableProperty<float> SimpleKnifDuration = new(Config.InitSimpleKnifeDuration);

        /// <summary>
        /// 小刀数量
        /// </summary>
        public static BindableProperty<int> SimpleKnifCount = new(Config.InitSimpleknifeCount);

        /// <summary>
        /// 小刀穿透数量
        /// </summary>
        public static BindableProperty<int> SimpleAttackCount = new(Config.InitSimpleAttackCount);

        /// <summary>
        /// 解锁守卫剑
        /// </summary>
        public static BindableProperty<bool> RotateSwordUnlocked = new(false);

        /// <summary>
        /// 守卫剑伤害
        /// </summary>
        public static BindableProperty<float> RotateSwordDamage = new(Config.InitRotateSwordDamage);

        /// <summary>
        /// 守卫剑数量
        /// </summary>
        public static BindableProperty<float> RotateSwordCount = new(Config.InitRotateSwordCount);

        /// <summary>
        /// 守卫剑速度
        /// </summary>
        public static BindableProperty<float> RotateSwordSpeed = new(Config.InitRotateSwordSpeed);

        /// <summary>
        /// 守卫剑半径
        /// </summary>
        public static BindableProperty<float> RotateSwordRange = new(Config.InitRotateSwordRange);

        /// <summary>
        /// 解锁篮球
        /// </summary>
        public static BindableProperty<bool> BasketBallUnlocked = new(false);

        /// <summary>
        /// 篮球伤害
        /// </summary>
        public static BindableProperty<float> BasketBallDamage = new(defaultValue: Config.InitBasketBallDamage);

        /// <summary>
        /// 篮球速度
        /// </summary>
        public static BindableProperty<float> BasketBallspeed = new(defaultValue: Config.InitBasketBallSpeed);

        /// <summary>
        /// 篮球数量
        /// </summary>
        public static BindableProperty<int> BasketBallCount = new(defaultValue: Config.InitBasketBallCount);

        /// <summary>
        /// 炸弹解锁
        /// </summary>
        public static BindableProperty<bool> Bombunlocked = new(false);

        /// <summary>
        /// 炸弹伤害
        /// </summary>
        public static BindableProperty<float> BombDamage = new(defaultValue: Config.InitBombDamage);

        /// <summary>
        /// 炸弹概率
        /// </summary>
        public static BindableProperty<float> BombPercent = new(defaultValue: Config.InitBombPercent);

        /// <summary>
        /// 暴击率
        /// </summary>
        public static BindableProperty<float> CriticalRate = new(defaultValue: Config.InitCriticalRate);

        /// <summary>
        /// 额外伤害
        /// </summary>
        public static BindableProperty<float> DamageRate = new(1f);

        /// <summary>
        /// 额外投掷物数量
        /// </summary>
        public static BindableProperty<int> AdditionalFlyThingCount = new(0);

        /// <summary>
        /// 速度加成
        /// </summary>
        public static BindableProperty<float> MovementSpeedRate = new(1f);

        /// <summary>
        /// 拾取范围加成
        /// </summary>
        public static BindableProperty<float> CollectableArea = new(Config.InitCollectableArea);

        /// <summary>
        /// 经验掉落概率加成
        /// </summary>
        public static BindableProperty<float> AdditionalExpPercent = new(0);

        /// <summary>
        /// 游戏时间
        /// </summary>
        public static BindableProperty<float> CurrentSeconds = new(0);

        /// <summary>
        /// 金币
        /// </summary>
        public static BindableProperty<int> Coin = new(0);

        /// <summary>
        /// 永久升级经验
        /// </summary>
        public static BindableProperty<float> ExpPercent = new(0.3f);

        /// <summary>
        /// 永久升级金币
        /// </summary>
        public static BindableProperty<float> CoinPercent = new(0.1f);

        //超武
        public static BindableProperty<bool> SuperKnife = new(false);
        public static BindableProperty<bool> SuperSword = new(false);
        public static BindableProperty<bool> SuperRotateSword = new(false);
        public static BindableProperty<bool> SuperBomb = new(false);
        public static BindableProperty<bool> SuperBasketBall = new(false);

        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInGlobalFrames;
            ResKit.Init();
            UIKit.Root.SetResolution(1920, 1080, 1);
            MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            Coin.Value = PlayerPrefs.GetInt("coin", 0);
            ExpPercent.Value = PlayerPrefs.GetFloat("ExpPercent", 0.3f);
            CoinPercent.Value = PlayerPrefs.GetFloat("CoinPercent", 0.1f);
            HP.Value = MaxHP.Value;
            //注册金币变化监听
            Coin.Register((coin) =>
            {
                PlayerPrefs.SetInt("coin", coin);
            });

            ExpPercent.Register((Exp) =>
            {
                PlayerPrefs.SetFloat("ExpPercent", Exp);
            });

            CoinPercent.Register((Coin) =>
            {
                PlayerPrefs.SetFloat("CoinPercent", Coin);
            });
            MaxHP.Register((maxhp) =>
            {
                PlayerPrefs.SetInt(nameof(MaxHP), maxhp);
            });
        }

        public static void ResetData()
        {
            HP.Value = MaxHP.Value;
            Exp.Value = 0;
            LV.Value = 1;
            SimpleAbilityDamage.Value = Config.InitSimpleSwordDamage;
            SimplAbiltyDuration.Value = Config.InitSimpleSwordDuration;
            SimpleSwordCount.Value = Config.InitSimpleSwordCount;
            SimpleSwordRange.Value = Config.InitSimpleSwordRange;
            SimpleKnifDamage.Value = Config.InitSimpleKnifeDamage;
            SimpleKnifDuration.Value = Config.InitSimpleKnifeDuration;
            SimpleKnifCount.Value = Config.InitSimpleknifeCount;
            SimpleAttackCount.Value = Config.InitSimpleAttackCount;
            RotateSwordDamage.Value = Config.InitRotateSwordDamage;
            RotateSwordCount.Value = Config.InitRotateSwordCount;
            RotateSwordSpeed.Value = Config.InitRotateSwordSpeed;
            RotateSwordRange.Value = Config.InitRotateSwordRange;
            BasketBallCount.Value = Config.InitBasketBallCount;
            BasketBallDamage.Value = Config.InitBasketBallDamage;
            BasketBallspeed.Value = Config.InitBasketBallSpeed;
            BombDamage.Value = Config.InitBombDamage;
            BombPercent.Value = Config.InitBombPercent;
            CriticalRate.Value = Config.InitCriticalRate;
            Bombunlocked.Value = false;
            BasketBallUnlocked.Value = false;
            RotateSwordUnlocked.Value = false;
            SimpleAbilityUnlocked.Value = false;
            SimpleKnifUnlocked.Value = false;
            AdditionalFlyThingCount.Value = 0;
            DamageRate.Value = 1f;
            CurrentSeconds.Value = 0;
            MovementSpeedRate.Value = 1f;
            EnemyGenerator.EnemyNum.Value = 0;
            AdditionalExpPercent.Value = 0;
            SuperKnife.Value = false;
            SuperBomb.Value = false;
            SuperRotateSword.Value = false;
            SuperSword.Value = false;
            SuperBasketBall.Value = false;

            CollectableArea.Value = Config.InitCollectableArea;
            Interface.GetSystem<ExpupgradeSystem>().ResetData();
            //EnemyGenerator.EnemyNum.Value = 0;
        }

        public static int ExpToNextLevel()
        {
            return LV.Value * 5;
        }

        public static void GeneratPowerUp(GameObject gameObject, bool genTreasureChest)
        {
            if (genTreasureChest)
            {
                PowerUpManager.Default.TreasureChest.Instantiate()
                    .Position(gameObject.transform.position)
                    .Show();
            }

            var random = Random.Range(0, 1f);
            if (random < ExpPercent.Value + AdditionalExpPercent.Value)
            {
                PowerUpManager.Default.Exp.Instantiate().Position(gameObject.Position()).Show();
            }

            random = Random.Range(0, 1f);
            if (random < CoinPercent.Value)
            {
                PowerUpManager.Default.Coin.Instantiate().Position(gameObject.Position()).Show();
            }

            random = Random.Range(0, 1f);
            if (random < 0.1f)
            {
                PowerUpManager.Default.HP.Instantiate().Position(gameObject.Position()).Show();
            }

            if (Bombunlocked.Value && (Object.FindObjectsOfType<Bomb>().Length < 4))
            {
                random = Random.Range(0, 1f);
                if (random < BombPercent.Value)
                {
                    PowerUpManager.Default.Bomb.Instantiate().Position(gameObject.Position()).Show();
                }
            }


            random = Random.Range(0, 1f);
            if (random < 0.05f)
            {
                PowerUpManager.Default.GetAllExp.Instantiate().Position(gameObject.Position()).Show();
            }

            var _ = Interface;
        }

        protected override void Init()
        {
            // throw new System.NotImplementedException();
            //注册的操作
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new ExpupgradeSystem());
            this.RegisterSystem(new AchievementSystem());
        }
    }
}