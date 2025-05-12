/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using PrjectSurvivor.Achievement;
using QAssetBundle;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace PrjectSurvivor
{
	public partial class AchivementController : UIElement
	{
        ResLoader mResLoader = ResLoader.Allocate();
		private void Awake()
        {
            var originLocalPosY = Achivementltem.LocalPositionY();
            var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");
            AchievementSystem.OnAchievementUnlocked.Register(item =>
            {
                Title.text = $"<b>成就 {item.Name} 达成!</b>";
                Description.text = item.Description;
                var sprite =  iconAtlas.GetSprite(item.IconName);
                Icon.sprite = sprite;
                Achivementltem.Show();
                Achivementltem.LocalPositionY(-200);
                AudioKit.PlaySound(Sfs.ACHIEVEMENT);
                ActionKit.Sequence()
                    .Lerp(-200, originLocalPosY, 0.3f, (y) => Achivementltem.LocalPositionY(y))
                    .Delay(2)
                    .Lerp(originLocalPosY, -200, 0.3f, (y) => Achivementltem.LocalPositionY(y), () =>
                    {
                        Achivementltem.Hide();
                    })
                    .Start(this);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void OnBeforeDestroy()
        {
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }
	}
}