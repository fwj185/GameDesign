using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public class UIGameOverPanelData : UIPanelData
    {
    }
    public partial class UIGameOverPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            Time.timeScale = 0;
            mData = uiData as UIGameOverPanelData ?? new UIGameOverPanelData();
            BtnBackToStart.onClick.AddListener(() =>
            {
                CloseSelf();
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
