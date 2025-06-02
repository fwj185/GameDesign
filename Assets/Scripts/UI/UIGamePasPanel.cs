using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public class UIGamePasPanelData : UIPanelData
    {
        public int CurrentLevel;
    }

    public partial class UIGamePasPanel : UIPanel
    {
        private UIGamePanel mUIGamePanel;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePasPanelData ?? new UIGamePasPanelData();
            AudioKit.PlaySound("game_pass");
            Time.timeScale = 0;

            // 获取 UIGamePanel 实例
            mUIGamePanel = FindObjectOfType<UIGamePanel>(); 
                BtnBackToStart.onClick.AddListener(() =>
                {
                    if (mUIGamePanel != null)
                    {
                        mUIGamePanel.ClosePanelIndirectly();
                    }
                    this.CloseSelf();
                    SceneManager.LoadScene("GameStart");
                });

            BtnRestartLevel.onClick.AddListener(() =>
            {
                if (mUIGamePanel != null)
                {
                    mUIGamePanel.ShowSelfAgain();
                }
                this.CloseSelf();
                Time.timeScale = 1;
                var enemyGenerator = FindObjectOfType<EnemyGenerator>();
                enemyGenerator.RestartLevel();
            });

            BtnStartNextLvel.onClick.AddListener(() =>
            {
                if (mUIGamePanel != null)
                {
                    mUIGamePanel.ShowSelfAgain();
                }
                this.CloseSelf();
                Time.timeScale = 1;
                var enemyGenerator = FindObjectOfType<EnemyGenerator>();
                enemyGenerator.StartNextLevel();
            });

            // 到达第三关时开始下一关的按钮不能用
            if (mData.CurrentLevel >= 2)
            {
                BtnStartNextLvel.interactable = false;
            }
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