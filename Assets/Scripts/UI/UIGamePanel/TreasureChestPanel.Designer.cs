/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class TreasureChestPanel
	{
		[SerializeField] public UnityEngine.UI.Text Content;
		[SerializeField] public UnityEngine.UI.Button BtnSure;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			Content = null;
			BtnSure = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "TreasureChestPanel";}
		}
	}
}
