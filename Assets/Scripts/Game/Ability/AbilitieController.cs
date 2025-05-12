using QFramework;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class AbilitieController : ViewController, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }

        void Start()
        {
            // Code Here
            Global.BasketBallUnlocked.RegisterWithInitValue((Vale) =>
            {
                if (Vale)
                {
                    BasketBallAbility.Show();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            Global.RotateSwordUnlocked.RegisterWithInitValue((Vale) =>
            {
                if (Vale)
                {
                    RotateSword.Show();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            Global.SimpleAbilityUnlocked.RegisterWithInitValue((Vale) =>
            {
                if (Vale)
                {
                    SimpleSword.Show();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            Global.SimpleKnifUnlocked.RegisterWithInitValue((Vale) =>
            {
                if (Vale)
                {
                    SimpleKnife.Show();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.GetSystem<ExpupgradeSystem>().items.Where(items => items.IsWeapon).ToList().GetRandomItem().Upgrade();
        }
    }
}
