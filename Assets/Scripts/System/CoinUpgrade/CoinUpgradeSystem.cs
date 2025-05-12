using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PrjectSurvivor
{
    public class CoinUpgradeSystem : AbstractSystem, ICanSave
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();
        public List<CoinUpgradeItem> items { get; } = new List<CoinUpgradeItem>();
        private HashSet<string> Keys = new HashSet<string>();

        public CoinUpgradeItem Add(CoinUpgradeItem item)
        {
            items.Add(item);
            return item;
        }

        protected override void OnInit()
        {
            Add(new CoinUpgradeItem()
                    .Withkey("coin_percent1")
                    .Withdescription("金币掉落概率提升")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    }))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("coin_percent2")
                    .Withdescription("金币掉落概率提升lv2")
                    .WithPrice(1000)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("coin_percent3")
                    .Withdescription("金币掉落概率提升lv3")
                    .WithPrice(2000)
                    .OnUpgrade((item) =>
                    {
                        Global.CoinPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })));
            //
            //
            // coinPercentLv1.OnChanged.Register(() =>
            // {
            //     if (coinPercentLv1.UpgradeFinish)
            //         coinPercentLv2.OnChanged.Trigger();
            // });
            Add(new CoinUpgradeItem()
                    .Withkey("exp_percent1")
                    .Withdescription("经验掉落概率提升1")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    }))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("exp_percent2")
                    .Withdescription("经验掉落概率提升2")
                    .WithPrice(1000)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("exp_percent3")
                    .Withdescription("经验掉落概率提升3")
                    .WithPrice(2000)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("exp_percent4")
                    .Withdescription("经验掉落概率提升4")
                    .WithPrice(3000)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("exp_percent5")
                    .Withdescription("经验掉落概率提升5")
                    .WithPrice(4000)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("exp_percent6")
                    .Withdescription("经验掉落概率提升6")
                    .WithPrice(5000)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("exp_percent7")
                    .Withdescription("经验掉落概率提升7")
                    .WithPrice(9999)
                    .OnUpgrade((item) =>
                    {
                        Global.ExpPercent.Value += 0.1f;
                        Global.Coin.Value -= item.Price;
                    })));
            Add(new CoinUpgradeItem()
                    .Withkey("hp_max1")
                    .Withdescription("最大血量1")
                    .WithPrice(9999)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    }))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max2")
                    .Withdescription("最大血量2")
                    .WithPrice(9999)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max3")
                    .Withdescription("最大血量3")
                    .WithPrice(9999)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max4")
                    .Withdescription("最大血量4")
                    .WithPrice(9999)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max5")
                    .Withdescription("最大血量5")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max6")
                    .Withdescription("最大血量6")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max7")
                    .Withdescription("最大血量7")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max8")
                    .Withdescription("最大血量8")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max9")
                    .Withdescription("最大血量9")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max10")
                    .Withdescription("最大血量10")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max11")
                    .Withdescription("最大血量11")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                .Next(Add(new CoinUpgradeItem()
                    .Withkey("hp_max12")
                    .Withdescription("最大血量12")
                    .WithPrice(100)
                    .OnUpgrade((item) =>
                    {
                        Global.MaxHP.Value += 1;
                        Global.Coin.Value -= item.Price;
                    })))
                ;
            this.Load();
            OnCoinUpgradeSystemChanged.Register(() =>
            {
                Save();
            });
        }

        public void Say()
        {
        }

        public void Save()
        {
            var saveSys = this.GetSystem<SaveSystem>();
            foreach (var item in items)
            {
                saveSys.SaveBool(item.Key, item.UpgradeFinish);
            }
        }

        public void Load()
        {
            var saveSys = this.GetSystem<SaveSystem>();
            foreach (var item in items)
            {
                item.UpgradeFinish = saveSys.LoadBool(item.Key, false);
                //item.UpgradeFinish = false; this.Save();
                //item.UpgradeFinish = PlayerPrefs.GetInt(item.Key, 0) == 1;
            }
        }
    }
}