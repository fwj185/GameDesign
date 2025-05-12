/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class CoinUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Text CoinText;
		[SerializeField] public RectTransform CoinUpgradeltemRoot;
		[SerializeField] public UnityEngine.UI.Button CoinUpgradeltemTemplate;

		public void Clear()
		{
			BtnClose = null;
			CoinText = null;
			CoinUpgradeltemRoot = null;
			CoinUpgradeltemTemplate = null;
		}

		public override string ComponentName
		{
			get { return "CoinUpgradePanel";}
		}
	}
}
