using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor.Achievement
{
    public class AchievementSystem:AbstractSystem
    {
        public AchievementItem Add(AchievementItem item)
        {
            Items.Add(item);
            return item;
        }

        public List<AchievementItem> Items = new List<AchievementItem>();
        public static EasyEvent<AchievementItem> OnAchievementUnlocked = new EasyEvent<AchievementItem>();
        protected override void OnInit()
        {
            var saveSystem = this.GetSystem<SaveSystem>();
            ActionKit.OnUpdate.Register(() =>
            {
                if (Time.frameCount % 10 == 0)
                {
                    foreach (var achievementItem in Items.Where(achievementItem =>
                                 !achievementItem.Unlocked && achievementItem.ConditionCheck()))
                    {
                        achievementItem.Unlock(saveSystem);
                    }
                }
            });
            Add(new AchievementItem()
                    .WithKey("3_minutes")
                    .WithName("坚持三分钟")
                    .WithDescription("坚持 3 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition(i => { return Global.CurrentSeconds.Value >= 60 * 3;})
                    // .Condition(() => Global.CurrentSeconds.Value >= 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
                        Add(new AchievementItem()
                    .WithKey("5_minutes")
                    .WithName("坚持五分钟")
                    .WithDescription("坚持 5 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition((_) => Global.CurrentSeconds.Value >= 60 * 5)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("10_minutes")
                    .WithName("坚持十分钟")
                    .WithDescription("坚持 10 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition((_) => Global.CurrentSeconds.Value >= 60 * 10)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("15_minutes")
                    .WithName("坚持 15 分钟")
                    .WithDescription("坚持 10 分钟\n奖励 1000 金币")
                    .WithIconName("achievement_time_icon")
                    .Condition((_) => Global.CurrentSeconds.Value >= 60 * 15)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("lv30")
                    .WithName("30 级")
                    .WithDescription("第一次升级到 30 级\n奖励 1000 金币")
                    .WithIconName("achievement_level_icon")
                    .Condition((_) => Global.LV.Value >= 30)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("lv50")
                    .WithName("50 级")
                    .WithDescription("第一次升级到 50 级\n奖励 1000 金币")
                    .WithIconName("achievement_level_icon")
                    .Condition((_) => Global.LV.Value >= 50)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_ball")
                    .WithName("合成后的球")
                    .WithDescription("第一次解锁合成后的球\n奖励 1000 金币")
                    .WithIconName("paired_ball_icon")
                    .Condition((_) => Global.SuperBasketBall.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_bomb")
                    .WithName("合成后的炸弹")
                    .WithDescription("第一次解锁合成后的炸弹\n奖励 1000 金币")
                    .WithIconName("paired_bomb_icon")
                    .Condition((_) => Global.SuperBomb.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_sword")
                    .WithName("合成后的船锚")
                    .WithDescription("第一次解锁合成后的船锚\n奖励 1000 金币")
                    .WithIconName("paired_simple_sword_icon")
                    .Condition((_) => Global.SuperSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_knife")
                    .WithName("合成后的飞刀")
                    .WithDescription("第一次解锁合成后的飞刀\n奖励 1000 金币")
                    .WithIconName("paired_simple_knife_icon")
                    .Condition((_) => Global.SuperKnife.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("合成后的三叉戟")
                    .WithDescription("第一次解锁合成后的三叉戟\n奖励 1000 金币")
                    .WithIconName("paired_rotate_sword_icon")
                    .Condition((_) => Global.SuperRotateSword.Value)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
            Add(new AchievementItem()
                    .WithKey("first_time_paired_circle")
                    .WithName("全部能力升级")
                    .WithDescription("全部能力升级完成\n奖励 1000 金币")
                    .WithIconName("achievement_all_icon")
                    .Condition((_) => ExpupgradeSystem.AllUnlockedFinish)
                    .OnUnlocked(_ => { Global.Coin.Value += 1000; }))
                .Load(saveSystem);
            
        }

        
    }
}