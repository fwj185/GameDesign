using QFramework;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PrjectSurvivor
{
    public class UIGamePanelData : UIPanelData
    {
    }
    public partial class UIGamePanel : UIPanel
    {
        public static EasyEvent FlashScreen = new EasyEvent();
        public static EasyEvent OpenTreasurePanel = new EasyEvent();

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePanelData ?? new UIGamePanelData();
            // please add init code here
            Global.Exp.RegisterWithInitValue((exp) =>
            {
                ExpValue.fillAmount = (exp / (float)Global.ExpToNextLevel());
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            Global.LV.RegisterWithInitValue((lv) =>
            {
                LV.text = "LV:" + lv;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            ExpUpgradePanel.Hide();
            Global.LV.Register((lv) =>
            {
                //Time.timeScale = 0;
                //ExpUpgradePanel.Show();
                //AudioKit.PlaySound("level_up");
            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            Global.Exp.RegisterWithInitValue((exp) =>
            {
                if (exp >= Global.ExpToNextLevel())
                {
                    Global.Exp.Value -= Global.ExpToNextLevel();

                    Global.LV.Value++;
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            ////点击升级
            //BtnUpgrade.onClick.AddListener(() =>
            //{
            //    Time.timeScale = 1f;
            //    Global.SimpleAbillity.Value *= 1.5f;
            //    UpGradeRoot.Hide();
            //    AudioKit.PlaySound("ability_level_up");
            //});
            //BtnSimpleDurationUpgrade.onClick.AddListener(() =>
            //{
            //    Time.timeScale = 1f;
            //    Global.SimplAbiltyDuration.Value *= 0.7f;
            //    UpGradeRoot.Hide();
            //    AudioKit.PlaySound("ability_level_up");
            //});
            //注册时间变化监听
            Global.CurrentSeconds.RegisterWithInitValue((sec) =>
            {
                if (Time.frameCount % 30 == 0)
                {
                    var sesInt = Mathf.FloorToInt(sec);
                    var seconds = sesInt % 60;
                    var min = sesInt / 60;
                    mGameTime.text = "时间：" + $"{min:00}:{seconds:00}";

                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //注册敌人变化
            EnemyGenerator.EnemyNum.RegisterWithInitValue((sec) =>
            {
                EnemNum.text = "剩余敌人数量:" + sec;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.OnUpdate.Register(() =>
            {
                EnemyGenerator EnemGame = FindAnyObjectByType<EnemyGenerator>();
                if (EnemGame &&
                    EnemGame.Currentwave == null &&
                    EnemGame.isLast &&
                    EnemyGenerator.EnemyNum.Value == 0)
                {
                    // 修改为隐藏面板
                    this.HideSelf();
                    UIKit.OpenPanel<UIGamePasPanel>();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ActionKit.OnUpdate.Register(() =>
            {
                Global.CurrentSeconds.Value += Time.deltaTime;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            //注册金币变化监听
            Global.Coin.RegisterWithInitValue((coin) =>
            {
                CoinText.text = "金币：" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            FlashScreen.Register(() =>
            {
                ActionKit.Sequence()
                   .Lerp(0, 0.5f, 0.1f, alpha => ScreenColor.ColorAlpha(alpha))
                   .Lerp(0.5f, 0.2f, 0.2f, alpha => ScreenColor.ColorAlpha(alpha), () => ScreenColor.ColorAlpha(0))
                   .Start(this);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            //UpGradeRoot.Hide();
            OpenTreasurePanel.Register(() =>
            {
                Time.timeScale = 0.0f;
                TreasureChestPanel.Show();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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

        public void ClosePanelIndirectly()
        {
            CloseSelf();
        }
        // 公共方法，用于重新显示面板
        public void ShowSelfAgain()
        {
            this.ShowSelf();
        }
        public void HideSelf()
        {
            gameObject.SetActive(false);
        }
        public void ShowSelf()
        {
            gameObject.SetActive(true);
        }
        private void CloseSelf()
        {
            Destroy(gameObject);
        }
        // 新增一个公共方法来间接调用 CloseSelf
        public void PublicClose()
        {
            CloseSelf();
        }
    }
}