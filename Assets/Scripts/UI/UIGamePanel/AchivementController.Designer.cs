/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class AchivementController
	{
		[SerializeField] public UnityEngine.UI.Image Achivementltem;
		[SerializeField] public UnityEngine.UI.Text Description;
		[SerializeField] public UnityEngine.UI.Text Title;
		[SerializeField] public UnityEngine.UI.Image Icon;

		public void Clear()
		{
			Achivementltem = null;
			Description = null;
			Title = null;
			Icon = null;
		}

		public override string ComponentName
		{
			get { return "AchivementController";}
		}
	}
}
