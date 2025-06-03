using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
namespace PrjectSurvivor
{
    public class ExpupgradeSystem : AbstractSystem
    {
        public static bool AllUnlockedFinish = false;
        public List<ExpUpgradeitem> items { get; } = new List<ExpUpgradeitem>();
        public Dictionary<string, ExpUpgradeitem> dictionary = new();

        public static void CheckAllUnlockedFinish()
        {
            AllUnlockedFinish = Global.Interface.GetSystem<ExpupgradeSystem>().items.All(i=>i.UpgradeFinish);
        }
        public Dictionary<string, string> Pairs = new Dictionary<string, string>()
        {
            {"simple_sword", "simple_critical" },
            {"simple_bomb", "simple_fly_count" },
            {"simple_knife", "damage_rate"},
            {"basket_ball","movement_speed_rate" },
            {"rotate_sword","simple_exp"},
            {"simple_critical", "simple_sword" },
            {"simple_fly_count", "simple_bomb" },
            {"damage_rate", "simple_knife"},
            {"movement_speed_rate","basket_ball" },
            {"simple_exp","rotate_sword"},

        };
        public Dictionary<string, BindableProperty<bool>> PairedProperties = new() {
            {"simple_sword", Global.SuperSword },
            {"simple_bomb", Global.SuperBomb },
            {"simple_knife", Global.SuperKnife },
            {"basket_ball",Global.SuperBasketBall },
            {"rotate_sword", Global.SuperRotateSword },
        };
        public ExpUpgradeitem Add(ExpUpgradeitem item)
        {

            items.Add(item);
            return item;
        }
        protected override void OnInit()
        {
            ResetData();
            Global.LV.Register((lv) =>
            {
                Roll();
            });
        }
        public void Roll()
        {
            System.Random random = new System.Random();
            foreach (var item in items)
            {
                item.Visible.Value = false;
            }
            var a = items.Where(item1 => !item1.UpgradeFinish).OrderBy(x => random.Next()).Take(4).ToList();
            if (a.Count > 0)
            {
                a.ForEach(item =>
                {
                    if (item != null)
                    {
                        item.Visible.Value = true;
                        Time.timeScale = 0;
                        UIKit.GetPanel<UIGamePanel>().ExpUpgradePanel.Show();
                        AudioKit.PlaySound("level_up");
                    }

                });

            }


        }
        public void ResetData()
        {
            items.Clear();
            Add(new ExpUpgradeitem(true)
                .Withkey("simple_sword")
                .WithName("锚")
                .WithIconName("simple_sword_icon")
                .WithPairedName("合成后的锚")
                .WithPairedIconName("paired_simple_sword_icon")
                .WithPairedDescription("攻击力翻倍攻击范围翻倍")
                .Withdescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"锚LV{lv}:攻击身边的敌人",
                        2 => $"锚LV{lv}:\n攻击力+3，数量+2",
                        3 => $"锚LV{lv}:\n攻击力+2 间隔+0.25s",
                        4 => $"锚LV{lv}:\n攻击力+2 间隔+0.25s",
                        5 => $"锚LV{lv}:\n攻击力+3 数量+2",
                        6 => $"锚LV{lv}:\n范围+1 间隔+0.25s",
                        7 => $"锚LV{lv}:\n攻击力+3 数量+2",
                        8 => $"锚LV{lv}:\n攻击力+2 范围+1",
                        9 => $"锚LV{lv}:\n攻击力+3 间隔+0.25s",
                        10 => $"锚LV{lv}:\n攻击力3 数量+2",
                        11 => $"锚LV{lv}:\n攻击力3 数量+2",
                        _ => null,
                    };
                })
                .WithMaxLv(11)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.SimpleAbilityUnlocked.Value = true;
                            break;
                        case 2:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimplAbiltyDuration.Value -= 0.25f;
                            break;
                        case 4:
                            Global.SimpleAbilityDamage.Value += 2;
                            Global.SimplAbiltyDuration.Value -= 0.25f;
                            break;
                        case 5:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 6:
                            Global.SimpleSwordRange.Value += 1;
                            Global.SimplAbiltyDuration.Value -= 0.25f;
                            break;
                        case 7:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 8:
                            Global.SimpleSwordRange.Value += 1;
                            Global.SimpleAbilityDamage.Value += 2;
                            break;
                        case 9:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimplAbiltyDuration.Value -= 0.25f;
                            break;
                        case 10:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                        case 11:
                            Global.SimpleAbilityDamage.Value += 3;
                            Global.SimpleSwordCount.Value += 2;
                            break;
                    }
                }));
            Add(new ExpUpgradeitem(true)
                .Withkey("rotate_sword")
                .WithName("三叉戟")
                .WithIconName("rotate_sword_icon")
                .WithPairedName("合成后的三叉戟")
                .WithPairedIconName("paired_rotate_sword_icon")
                .WithPairedDescription("攻击力翻倍 旋转速度翻倍")
                .Withdescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"三叉戟LV{lv}:环绕主角身边的三叉戟",
                        2 => $"三叉戟LV{lv}:\n数量+2",
                        3 => $"三叉戟LV{lv}:\n速度+50%",
                        4 => $"三叉戟LV{lv}:\n攻击力+5",
                        5 => $"三叉戟LV{lv}:\n速度+25% 数量+2",
                        6 => $"三叉戟LV{lv}:\n速度+25% 数量+2",
                        7 => $"三叉戟LV{lv}:\n速度+25% 数量+2",
                        8 => $"三叉戟LV{lv}:\n速度+25% 数量+2",
                        _ => null,
                    };
                })
                .WithMaxLv(8)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.RotateSwordUnlocked.Value = true;
                            break;
                        case 2:
                            Global.RotateSwordCount.Value += 2;
                            break;
                        case 3:
                            Global.RotateSwordSpeed.Value /= 0.5f;
                            break;
                        case 4:
                            Global.RotateSwordDamage.Value += 5;
                            break;
                        case 5:
                            Global.RotateSwordSpeed.Value /= 0.75f;
                            Global.RotateSwordCount.Value += 2;
                            break;
                        case 6:
                            Global.RotateSwordSpeed.Value /= 0.75f;
                            Global.RotateSwordCount.Value += 2;
                            break;
                        case 7:
                            Global.RotateSwordSpeed.Value /= 0.75f;
                            Global.RotateSwordCount.Value += 2;
                            break;
                        case 8:
                            Global.RotateSwordSpeed.Value /= 0.75f;
                            Global.RotateSwordCount.Value += 2;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(true)
                .Withkey("simple_knife")
                .WithName("飞刀")
                .WithIconName("simple_knife_icon")
                .WithPairedName("合成后的飞刀")
                .WithPairedIconName("paired_simple_knife_icon")
                .WithPairedDescription("攻击力翻倍")
                .Withdescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"飞刀LV{lv}:攻击身边的敌人",
                        2 => $"飞刀LV{lv}:\n数量+2",
                        3 => $"飞刀LV{lv}:\n间隔-0.5s",
                        4 => $"飞刀LV{lv}:\n穿透+1",
                        5 => $"飞刀LV{lv}:\n攻击力+3",
                        6 => $"飞刀LV{lv}:\n攻击力+4",
                        7 => $"飞刀LV{lv}:\n攻击力+4",
                        8 => $"飞刀LV{lv}:\n攻击力+4",
                        _ => null,
                    };
                })
                .WithMaxLv(8)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.SimpleKnifUnlocked.Value = true;
                            break;
                        case 2:
                            Global.SimpleKnifCount.Value += 2;
                            break;
                        case 3:
                            Global.SimpleKnifDuration.Value -= 0.5f;
                            break;
                        case 4:
                            Global.SimpleAttackCount.Value++;
                            break;
                        case 5:
                            Global.SimpleKnifDamage.Value += 3;
                            break;
                        case 6:
                            Global.SimpleKnifDamage.Value += 4;
                            break;
                        case 7:
                            Global.SimpleKnifDamage.Value += 4;
                            break;
                        case 8:
                            Global.SimpleKnifDamage.Value += 4;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(true)
                 .Withkey("basket_ball")
                 .WithName("铅球")
                 .WithIconName("ball_icon")
                 .WithPairedName("合成后的铅球")
                 .WithPairedIconName("paired_ball_icon")
                 .WithPairedDescription("攻击力翻倍 体积变大")
                 .Withdescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"铅球LV{lv}:在屏幕之间反弹",
                        2 => $"铅球LV{lv}:\n攻击力+1",
                        3 => $"铅球LV{lv}:\n攻击力+3",
                        4 => $"速度LV{lv}:\n+50%",
                        5 => $"铅球LV{lv}:\n数量+1",
                        6 => $"铅球LV{lv}:\n攻击力+3",
                        7 => $"铅球LV{lv}:\n攻击力+3",
                        8 => $"铅球LV{lv}:\n攻击力+3",
                        _ => null,
                    };
                })
                .WithMaxLv(8)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.BasketBallUnlocked.Value = true;
                            break;
                        case 2:
                            Global.BasketBallDamage.Value += 2;
                            break;
                        case 3:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 4:
                            Global.BasketBallspeed.Value *= 1.8f;
                            break;
                        case 5:
                            Global.BasketBallCount.Value++;
                            break;
                        case 6:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 7:
                            Global.BasketBallDamage.Value += 3;
                            break;
                        case 8:
                            Global.BasketBallDamage.Value += 3;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("simple_bomb")
                 .WithName("炸弹")
                 .WithIconName("bomb_icon")
                 .WithPairedName("合成后的炸弹")
                 .WithPairedIconName("paired_bomb_icon")
                 .WithPairedDescription("每隔 15 秒爆炸一次")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"炸弹LV{lv}:现在可以掉落",
                         2 => $"炸弹LV{lv}:\n攻击力+10",
                         3 => $"炸弹LV{lv}:\n概率+10%",
                         4 => $"炸弹LV{lv}:\n攻击力+10",
                         5 => $"炸弹LV{lv}:\n概率+10%",
                         6 => $"炸弹LV{lv}:\n攻击力+10",
                         7 => $"炸弹LV{lv}:\n攻击力+10",
                         8 => $"炸弹LV{lv}:\n攻击力+10",
                         _ => null,
                     };
                 })
                .WithMaxLv(8)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.Bombunlocked.Value = true;
                            break;
                        case 2:
                            Global.BombDamage.Value += 10;
                            break;
                        case 3:
                            Global.BombPercent.Value *= 1.1f;
                            break;
                        case 4:
                            Global.BombDamage.Value *= 1.8f;
                            break;
                        case 5:
                            Global.BombPercent.Value *= 1.1f;
                            break;
                        case 6:
                            Global.BombDamage.Value += 10;
                            break;
                        case 7:
                            Global.BombDamage.Value += 10;
                            break;
                        case 8:
                            Global.BombDamage.Value += 10;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("simple_critical")
                 .WithName("暴击")
                 .WithIconName("critical_icon")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"暴击率LV{lv}:\n15%",
                         2 => $"暴击率LV{lv}:\n30%",
                         3 => $"暴击率LV{lv}:\n45%",
                         4 => $"暴击率LV{lv}:\n60%",
                         5 => $"暴击率LV{lv}:\n90%",
                         _ => null,
                     };
                 })
                .WithMaxLv(5)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.CriticalRate.Value = 0.15f;
                            break;
                        case 2:
                            Global.CriticalRate.Value = 0.30f;
                            break;
                        case 3:
                            Global.CriticalRate.Value = 0.45f;
                            break;
                        case 4:
                            Global.CriticalRate.Value = 0.60f;
                            break;
                        case 5:
                            Global.CriticalRate.Value = 0.90f;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("damage_rate")
                 .WithName("额外伤害")
                 .WithIconName("damage_icon")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"额外伤害LV{lv}:\n+15%",
                         2 => $"额外伤害LV{lv}:\n+30%",
                         3 => $"额外伤害LV{lv}:\n+45%",
                         4 => $"额外伤害LV{lv}:\n+60%",
                         5 => $"额外伤害LV{lv}:\n+100%",
                         _ => null,
                     };
                 })
                .WithMaxLv(5)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.DamageRate.Value = 1.15f;
                            break;
                        case 2:
                            Global.DamageRate.Value = 1.30f;
                            break;
                        case 3:
                            Global.DamageRate.Value = 1.45f;
                            break;
                        case 4:
                            Global.DamageRate.Value = 1.60f;
                            break;
                        case 5:
                            Global.DamageRate.Value = 2f;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("simple_fly_count")
                 .WithIconName("fly_icon")
                 .WithName("额外投掷物")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"额外投掷物数量LV{lv}:+1",
                         2 => $"额外投掷物数量LV{lv}:+2",
                         3 => $"额外投掷物数量LV{lv}:+3",
                         4 => $"额外投掷物数量LV{lv}:+4",
                         5 => $"额外投掷物数量LV{lv}:+5",
                         _ => null,
                     };
                 })
                .WithMaxLv(5)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.AdditionalFlyThingCount.Value = 1;
                            break;
                        case 2:
                            Global.AdditionalFlyThingCount.Value = 2;
                            break;
                        case 3:
                            Global.AdditionalFlyThingCount.Value = 3;
                            break;
                        case 4:
                            Global.AdditionalFlyThingCount.Value = 4;
                            break;
                        case 5:
                            Global.AdditionalFlyThingCount.Value = 5;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("movement_speed_rate")
                 .WithName("移动速度")
                 .WithIconName("movement_icon")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"速度加成LV{lv}:+10%",
                         2 => $"速度加成LV{lv}:+20%",
                         3 => $"速度加成LV{lv}:+30%",
                         4 => $"速度加成LV{lv}:+40%",
                         5 => $"速度加成LV{lv}:+50%",
                         _ => null,
                     };
                 })
                .WithMaxLv(5)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.MovementSpeedRate.Value = 1.1f;
                            break;
                        case 2:
                            Global.MovementSpeedRate.Value = 1.2f;
                            break;
                        case 3:
                            Global.MovementSpeedRate.Value = 1.3f;
                            break;
                        case 4:
                            Global.MovementSpeedRate.Value = 1.4f;
                            break;
                        case 5:
                            Global.MovementSpeedRate.Value = 1.5f;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("simple_collectable_area")
                 .WithName("拾取范围")
                 .WithIconName("collectable_icon")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"拾取范围LV{lv}:+10%",
                         2 => $"拾取范围LV{lv}:+20%",
                         3 => $"拾取范围LV{lv}:+30%",
                         4 => $"拾取范围LV{lv}:+40%",
                         5 => $"拾取范围LV{lv}:+50%",
                         _ => null,
                     };
                 })
                .WithMaxLv(5)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.CollectableArea.Value = 1.1f;
                            break;
                        case 2:
                            Global.CollectableArea.Value = 1.2f;
                            break;
                        case 3:
                            Global.CollectableArea.Value = 1.3f;
                            break;
                        case 4:
                            Global.CollectableArea.Value = 1.4f;
                            break;
                        case 5:
                            Global.CollectableArea.Value = 1.5f;
                            break;
                    }
                })
                );
            Add(new ExpUpgradeitem(false)
                 .Withkey("simple_exp")
                 .WithName("经验值")
                 .WithIconName("exp_icon")
                 .Withdescription(lv =>
                 {
                     return lv switch
                     {
                         1 => $"经验掉落概率LV{lv}:+10%",
                         2 => $"经验掉落概率LV{lv}:+20%",
                         3 => $"经验掉落概率LV{lv}:+30%",
                         4 => $"经验掉落概率LV{lv}:+40%",
                         5 => $"经验掉落概率LV{lv}:+50%",
                         _ => null,
                     };
                 })
                .WithMaxLv(5)
                .OnUpgrade((item, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.AdditionalExpPercent.Value = 0.1f;
                            break;
                        case 2:
                            Global.AdditionalExpPercent.Value = 0.2f;
                            break;
                        case 3:
                            Global.AdditionalExpPercent.Value = 0.3f;
                            break;
                        case 4:
                            Global.AdditionalExpPercent.Value = 0.4f;
                            break;
                        case 5:
                            Global.AdditionalExpPercent.Value = 0.5f;
                            break;
                    }
                }));
            dictionary = items.ToDictionary(i => i.Key);
        }
    }
}
