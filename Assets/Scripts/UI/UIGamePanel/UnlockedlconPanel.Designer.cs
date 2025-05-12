/****************************************************************************
 * 2024.11 WIN-2ECIE49EOAK
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PrjectSurvivor
{
	public partial class UnlockedlconPanel
	{
		[SerializeField] public UnityEngine.UI.Image UnlockedlconTemplate;
		[SerializeField] public RectTransform UnlockedlconRoot;

		public void Clear()
		{
			UnlockedlconTemplate = null;
			UnlockedlconRoot = null;
		}

		public override string ComponentName
		{
			get { return "UnlockedlconPanel";}
		}
	}
}
