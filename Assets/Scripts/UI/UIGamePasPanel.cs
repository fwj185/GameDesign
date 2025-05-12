using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public class UIGamePasPanelData : UIPanelData
    {
    }
    public partial class UIGamePasPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePasPanelData ?? new UIGamePasPanelData();
            // please add init code here
            AudioKit.PlaySound("game_pass");
            Time.timeScale = 0;
            BtnBackToStart.onClick.AddListener(() =>
            {
                this.CloseSelf();
                SceneManager.LoadScene("GameStart");
            });
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
            Time.timeScale = 1;
            Global.ResetData();
        }
    }
}
