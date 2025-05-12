using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	// Generate Id:bbc8544f-c22b-4766-949f-012cd0bb7533
	public partial class UIGameEsc
	{
		public const string Name = "UIGameEsc";
		
		[SerializeField]
		public ExeChestPanel ExeChestPanel;
		
		private UIGameEscData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			ExeChestPanel = null;
			
			mData = null;
		}
		
		public UIGameEscData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameEscData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameEscData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
