/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using PrjectSurvivor.Achievement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;
using UnityEngine.U2D;

namespace PrjectSurvivor
{
    public partial class AchivementListPanel : UIElement, IController
    {
        private ResLoader mResLoader = ResLoader.Allocate();

        private void Awake()
        {
            AchivementitemTemplate.Hide();
            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");
            foreach (var achievementItem in this.GetSystem<AchievementSystem>().Items
                         .OrderByDescending(item => item.Unlocked))
            {
                AchivementitemTemplate.InstantiateWithParent(AchivementitemRoot)
                    .Self(s =>
                    {
                        s.GetComponentInChildren<Text>().text = "<b>" + achievementItem.Name +
                                                                (achievementItem.Unlocked
                                                                    ? "<color=green>【已完成】</color>"
                                                                    : "") + "</b>\n" +
                                                                achievementItem.Description;
                        var sprite = iconAtlas.GetSprite(achievementItem.IconName);
                        s.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
                    }).Show();
                BtnClose.onClick.AddListener(() =>
                {
                    this.Hide();
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