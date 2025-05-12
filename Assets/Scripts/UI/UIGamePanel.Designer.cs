using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	// Generate Id:5f15c3d0-1d4a-44df-a497-d213d0d2218b
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";
		
		[SerializeField]
		public UnlockedlconPanel UnlockedlconPanel;
		[SerializeField]
		public UnityEngine.UI.Text LV;
		[SerializeField]
		public UnityEngine.UI.Text mGameTime;
		[SerializeField]
		public UnityEngine.UI.Text EnemNum;
		[SerializeField]
		public UnityEngine.UI.Text CoinText;
		[SerializeField]
		public ExpUpgradePanel ExpUpgradePanel;
		[SerializeField]
		public UnityEngine.UI.Image ExpValue;
		[SerializeField]
		public UnityEngine.UI.Image ScreenColor;
		[SerializeField]
		public TreasureChestPanel TreasureChestPanel;
		[SerializeField]
		public AchivementController AchivementController;
		
		private UIGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			UnlockedlconPanel = null;
			LV = null;
			mGameTime = null;
			EnemNum = null;
			CoinText = null;
			ExpUpgradePanel = null;
			ExpValue = null;
			ScreenColor = null;
			TreasureChestPanel = null;
			AchivementController = null;
			
			mData = null;
		}
		
		public UIGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
