/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class ExeChestPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnSure1;
		[SerializeField] public UnityEngine.UI.Button BtnSureEse;

		public void Clear()
		{
			BtnSure1 = null;
			BtnSureEse = null;
		}

		public override string ComponentName
		{
			get { return "ExeChestPanel";}
		}
	}
}
