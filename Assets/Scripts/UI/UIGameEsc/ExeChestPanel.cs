/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace PrjectSurvivor
{
	public partial class ExeChestPanel : UIElement
	{
		private void Awake()
		{   
            //回到主页
            BtnSure1.onClick.AddListener(() =>
            {
                UIKit.ClosePanel(UIGameEsc.Default);
                SceneManager.LoadScene("GameStart");    
            });
            //退出游戏
            BtnSureEse.onClick.AddListener(() =>
            {
                Application.Quit();
            });
		}
        
		protected override void OnBeforeDestroy()
		{
            Time.timeScale = 1f;
		}

        protected override  void OnShow()
        {
            Time.timeScale = 0.0f;
            Debug.Log("xxxx");
        }
        protected override  void OnHide()
        {
            Time.timeScale = 1f;
        }
	}
}