/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Text Title;
		[SerializeField] public UnityEngine.UI.Button BtnExpUpgradeltemTemplate;
		[SerializeField] public UnityEngine.UI.Text PairedUpgradeName;
		[SerializeField] public UnityEngine.UI.Image Icon;
		[SerializeField] public Transform UpGradeRoot;

		public void Clear()
		{
			Title = null;
			BtnExpUpgradeltemTemplate = null;
			PairedUpgradeName = null;
			Icon = null;
			UpGradeRoot = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
