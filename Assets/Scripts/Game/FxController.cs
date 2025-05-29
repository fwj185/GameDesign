using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class FxController : ViewController
    {
        private static FxController _instance;
        private void Awake()
        {
            _instance = this;
        }
        private void OnDestroy()
        {
            _instance = null;
        }
        public static void Play(SpriteRenderer sprite, Color dissolveColoer)
        {
            _instance.EnemyDieFx.Instantiate()
                .Position(sprite.Position())
                // .LocalScale(sprite.LocalScale())
                .Self(s =>
                {
                    s.GetComponent<DissoLve>().DissolveColor = dissolveColoer;
                    s.sprite = sprite.sprite;
                }).Show();
        }
    }
}
