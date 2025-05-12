using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class CollectableArea : ViewController
    {
        void Start()
        {
            Global.CollectableArea.RegisterWithInitValue(valve =>
            {
                GetComponent<CircleCollider2D>().radius = valve;
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
