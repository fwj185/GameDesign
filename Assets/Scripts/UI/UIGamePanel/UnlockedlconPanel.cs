/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace PrjectSurvivor
{
    public partial class UnlockedlconPanel : UIElement, IController
    {
        private Dictionary<string, System.Tuple<ExpUpgradeitem, Image>> mUnlockedKeys =
            new Dictionary<string, System.Tuple<ExpUpgradeitem, Image>>();

        private ResLoader mResLoader = ResLoader.Allocate();

        private void Awake()
        {
            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");
            foreach (var expUpgradeltem in this.GetSystem<ExpupgradeSystem>().items)
            {
                ExpUpgradeitem cachedItem = expUpgradeltem;
                expUpgradeltem.CurrentLevel.RegisterWithInitValue(level =>
                {
                    if (level > 0)
                    {
                        if (!mUnlockedKeys.ContainsKey(cachedItem.Key))
                        {
                            //不包含新的键就进行添加
                            UnlockedlconTemplate.InstantiateWithParent(UnlockedlconRoot)
                                .Self(self =>
                                {
                                    self.sprite = iconAtlas.GetSprite(cachedItem.IconName);
                                    mUnlockedKeys.Add(cachedItem.Key,
                                        new System.Tuple<ExpUpgradeitem, Image>(cachedItem, self));
                                }).Show();
                        }
                    }
                }).UnRegisterWhenGameObjectDestroyed(gameObject);
            }
            Global.SuperKnife.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_knife"))
                    {
                        var item = mUnlockedKeys["simple_knife"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_knife"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperKnife.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_knife"))
                    {
                        var item = mUnlockedKeys["simple_knife"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_knife"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
                        Global.SuperKnife.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_knife"))
                    {
                        var item = mUnlockedKeys["simple_knife"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_knife"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperRotateSword.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("rotate_sword"))
                    {
                        var item = mUnlockedKeys["rotate_sword"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["rotate_sword"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperBasketBall.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("basket_ball"))
                    {
                        var item = mUnlockedKeys["basket_ball"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["basket_ball"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperBomb.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_bomb"))
                    {
                        var item = mUnlockedKeys["simple_bomb"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_bomb"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            
            Global.SuperSword.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey("simple_sword"))
                    {
                        var item = mUnlockedKeys["simple_sword"].Item1;
                        var sprite = iconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys["simple_sword"].Item2.sprite = sprite;
                    }
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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