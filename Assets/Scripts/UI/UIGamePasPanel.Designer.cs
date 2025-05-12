using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	// Generate Id:5c71e5d8-b74c-447f-a696-ae15d4323409
	public partial class UIGamePasPanel
	{
		public const string Name = "UIGamePasPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnBackToStart;
		
		private UIGamePasPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnBackToStart = null;
			
			mData = null;
		}
		
		public UIGamePasPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGamePasPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGamePasPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
