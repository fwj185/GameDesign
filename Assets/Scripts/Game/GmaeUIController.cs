using QFramework;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace PrjectSurvivor
{
    public partial class GmaeUIController : ViewController
    {


        void Start()
        {
            // Code Here
            UIKit.OpenPanel<UIGamePanel>();
            UIKit.OpenPanel<UIGameEsc>();
            UIKit.Root.SetResolution(1920,1080,0);
        }
        private void OnDestroy()
        {
            UIKit.ClosePanel<UIGameEsc>();
            UIKit.ClosePanel<UIGamePanel>();
        }

         void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                var ExeChestPanel =  UIKit.GetPanel<UIGameEsc>().ExeChestPanel;
                if (ExeChestPanel.isActiveAndEnabled)
                {
                    ExeChestPanel.Hide();
                }
                else
                {
                    ExeChestPanel.Show();
                    
                }
 
            }  
        }
    }
}