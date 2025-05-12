/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class AchivementListPanel
	{
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public UnityEngine.UI.Button AchivementitemTemplate;
		[SerializeField] public RectTransform AchivementitemRoot;

		public void Clear()
		{
			BtnClose = null;
			AchivementitemTemplate = null;
			AchivementitemRoot = null;
		}

		public override string ComponentName
		{
			get { return "AchivementListPanel";}
		}
	}
}
