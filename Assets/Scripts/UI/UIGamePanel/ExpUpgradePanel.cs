/****************************************************************************
 * 2024.6 WIN-2ECIE49EOAK
 ****************************************************************************/

using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public partial class ExpUpgradePanel : UIElement, IController
    {
        // ReSharper disable Unity.PerformanceAnalysis
        private ResLoader mResLoader;
        private void Awake()
        {
            mResLoader = ResLoader.Allocate();
            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");
            //var pairedSimpleKnifeIcon = iconAtlas.GetSprite("paired_simple_knife_icon");
            var expupgradeSystem = this.GetSystem<ExpupgradeSystem>();
            foreach (var item in expupgradeSystem.items)
            {
                BtnExpUpgradeltemTemplate.InstantiateWithParent(UpGradeRoot).Self((self) =>
                {
                    var itemCache = item;
                    var selfCache = self;
                    self.transform.Find("Icon").GetComponent<Image>().sprite = iconAtlas.GetSprite(itemCache.IconName);
                    self.GetComponentInChildren<Text>().text = item.Description;
                    self.onClick.AddListener(() =>
                    {
                        Time.timeScale = 1f;
                        AudioKit.PlaySound("ability_level_up");
                        itemCache.Upgrade();
                        this.Hide();
                    });
                    itemCache.Visible.RegisterWithInitValue((vis) =>
                    {
                        if (vis)
                        {
                            self.Show();
                            var pairedUpgradeName = selfCache.transform.Find("PairedUpgradeName");
                            if (expupgradeSystem.Pairs.TryGetValue(itemCache.Key, out var parName))
                            {
                                var pairedItem = expupgradeSystem.dictionary[parName];
                                if (pairedItem.CurrentLevel.Value > 0 && itemCache.CurrentLevel.Value == 0)
                                {
                                    var parNameText = pairedUpgradeName;
                                    parNameText.Find("Icon").GetComponent<Image>().sprite =
                                        iconAtlas.GetSprite(pairedItem.IconName);
                                    parNameText.Show();
                                }
                                else
                                {
                                    pairedUpgradeName.Hide();
                                }

                            }
                            else
                            {
                                pairedUpgradeName.Hide();
                            }
                        }
                        else
                        {
                            self.Hide();
                        }
                    }).UnRegisterWhenGameObjectDestroyed(selfCache);
                    itemCache.CurrentLevel.RegisterWithInitValue(lv =>
                    {
                        selfCache.GetComponentInChildren<Text>().text = item.Description;
                    }).UnRegisterWhenGameObjectDestroyed(selfCache);
                });
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