/****************************************************************************
 * 2024.6 WIN-2ECIE49EOAK
 ****************************************************************************/

using QFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public partial class CoinUpgradePanel : UIElement, IController
    {
        private void Awake()
        {
            CoinUpgradeltemRoot.DestroyChildren();
            foreach (var item in this.GetSystem<CoinUpgradeSystem>().items.Where(a => !a.UpgradeFinish))
            {
                CoinUpgradeltemTemplate.InstantiateWithParent(CoinUpgradeltemRoot).Self((self) =>
                {
                    var itemCache = item;
                    self.GetComponentInChildren<Text>().text = item.Description + $" {item.Price}金币";
                    self.onClick.AddListener(() =>
                    {
                        AudioKit.PlaySound("ability_level_up");
                        itemCache.Upgrade();
                    });
                    var selfCache = self;
                    Global.Coin.RegisterWithInitValue((coin) =>
                    {
                        if (coin >= itemCache.Price)
                        {
                            selfCache.interactable = true;
                        }
                        else
                        {
                            selfCache.interactable = false;

                        }
                    }).UnRegisterWhenGameObjectDestroyed(gameObject);
                    item.OnChanged.Register(() =>
                    {

                        if (item.ConditionCheck())
                        {
                            selfCache.Show();
                        }
                        else
                        {
                            selfCache.Hide();
                        }
                    }).UnRegisterWhenGameObjectDestroyed(selfCache);
                    if (item.ConditionCheck())
                    {
                        selfCache.Show();
                    }
                    else
                    {
                        selfCache.Hide();
                    }
                });
            };
            Global.Coin.RegisterWithInitValue((coin) =>
            {
                CoinText.text = "金币：" + coin;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            BtnClose.onClick.AddListener(() =>
            {
                this.Hide();
            });
        }

        protected override void OnBeforeDestroy()
        {
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}