using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	// Generate Id:87d7f8b9-01dd-4789-a125-3420a30845bc
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnOverGame;
		[SerializeField]
		public UnityEngine.UI.Button BtnAchivement;
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		[SerializeField]
		public AchivementListPanel AchivementPanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnCoinUpgrade = null;
			BtnOverGame = null;
			BtnAchivement = null;
			BtnStartGame = null;
			CoinUpgradePanel = null;
			AchivementPanel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
