using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public class UIGameStartPanelData : UIPanelData
    {

    }
    public partial class UIGameStartPanel : UIPanel, IController
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();

            // please add init code here
            Time.timeScale = 1.0f;
            BtnStartGame.onClick.AddListener(() =>
            {
                Global.ResetData();
                this.CloseSelf();
                SceneManager.LoadScene("Game");
            });
            BtnCoinUpgrade.onClick.AddListener(() =>
            {
                CoinUpgradePanel.Show();
            });
            BtnAchivement.onClick.AddListener(() =>
            {
                AchivementPanel.Show();
            });
            BtnOverGame.onClick.AddListener(() =>
            {
                Application.Quit();
            });
            this.GetSystem<CoinUpgradeSystem>().Say();
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
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}
