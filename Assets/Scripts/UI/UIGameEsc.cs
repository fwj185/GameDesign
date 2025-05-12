using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public class UIGameEscData : UIPanelData
	{
	}
	public partial class UIGameEsc : UIPanel
    {
        public static UIGameEsc Default;
		protected override void OnInit(IUIData uiData = null)
        {
            
            Default = this;
			mData = uiData as UIGameEscData ?? new UIGameEscData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
            Default = null;
		}
	}
}
