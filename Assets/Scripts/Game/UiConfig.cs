using UnityEngine;
using QFramework;

namespace PrjectSurvivor
{
	public partial class UiConfig : ViewController
	{
		void Start()
		{
			// Code Here
            UIKit.Root.SetResolution(1920,1080,0);
            //Debug.LogError(UIRoot.Instance.CanvasScaler.referenceResolution);
		}
	}
}
