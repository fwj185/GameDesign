using UnityEngine;
using QFramework;
using System;
using System.Collections;
using Random = UnityEngine.Random;

namespace PrjectSurvivor
{
	public partial class TestMaxPowerUpCount : ViewController
	{
        private int mPowerUpCount = 0;
		IEnumerator Start()
        {
            // Code Here
            // var powerUpManager = FindObjectOfType<PowerUpManager>();
            // powerUpManager.GetAllExp.InstantiateWithParent(this)
            //     .Show()
            //     .Position(gameObject.Position())
            //     ;
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    gameObject.Position(Random.Range(3,20)*Random.Range(1,2), Random.Range(3,20)*Random.Range(1,2));
                    mPowerUpCount++;
                    Global.GeneratPowerUp(gameObject, false);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnGUI()
        {
            var cached = GUI.matrix;
            IMGUIHelper.SetDesignResolution(960,450);
            GUILayout.Space(10);
            GUILayout.Label(mPowerUpCount.ToString());
            GUI.matrix = cached;
        }
    }
}
