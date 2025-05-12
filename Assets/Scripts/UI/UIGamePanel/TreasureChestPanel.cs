using QFramework;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
namespace PrjectSurvivor
{
    public partial class TreasureChestPanel : UIElement, IController
    {
        private ResLoader mResLoader = ResLoader.Allocate();
        private void Awake()
        {
            BtnSure.onClick.AddListener(() =>
            {
                this.Hide();
                Time.timeScale = 1f;
            });
        }
        private void OnEnable()
        {
            var expUpgradeSystem = this.GetSystem<ExpupgradeSystem>();
            var matchedPairedItems = expUpgradeSystem.items.Where(item =>
             {
                 if (item.CurrentLevel.Value >= 7)
                 {
                     var containsInPair = expUpgradeSystem.Pairs.ContainsKey(item.Key);
                     var pairedItemKey = expUpgradeSystem.Pairs[item.Key];
                     var pairedItemStartUpgrade = expUpgradeSystem.dictionary[pairedItemKey].CurrentLevel.Value > 0;
                     var pairedUnlocked = expUpgradeSystem.PairedProperties[item.Key].Value;
                     return containsInPair && pairedItemStartUpgrade && !pairedUnlocked;
                 }
                 return false;
             });
            if (matchedPairedItems.Any())
            {
                var item = matchedPairedItems.ToList().GetRandomItem();
                Content.text = "<b>" + item.PairedName + "</b>\n" + item.PairedDescription;
                while (!item.UpgradeFinish)
                {
                    item.Upgrade();
                }

                Icon.sprite =  mResLoader.LoadSync<SpriteAtlas>("Icon").GetSprite(item.PairedIconName);
                Icon.SetNativeSize();
                Icon.Show();
                expUpgradeSystem.PairedProperties[item.Key].Value = true;
            }
            else
            {

                var upgradeltems = expUpgradeSystem.items.Where(item => item.CurrentLevel.Value > 0 && !item.UpgradeFinish).ToList();
                if (upgradeltems.Any())
                {
                    var item = upgradeltems.GetRandomItem();
                    Content.text = item.Description;
                    item.Upgrade();
                    Icon.sprite =  mResLoader.LoadSync<SpriteAtlas>("Icon").GetSprite(item.IconName);
                    Icon.SetNativeSize();
                    Icon.Show();
                }
                else
                {       
                    Icon.sprite =  mResLoader.LoadSync<SpriteAtlas>("Icon").GetSprite("icon");
                    Content.text = "增加50金币";
                    Global.Coin.Value += 50;
                    Icon.Hide();
                }
            }

        }
        protected override void OnBeforeDestroy()
        {
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}