using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class GetAllExp : GamePlayObj
    {
        void Start()
        {
            // Code Here
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                AudioKit.PlaySound("get_all_exp");
                PowerUpManager.Default.StartCoroutine(FlyToPlayerStart());
                this.DestroyGameObjGracefully();
            }
        }

        IEnumerator FlyToPlayerStart()
        {
            IEnumerable<PowerUp> exps = FindObjectsByType<PowerUp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            IEnumerable<PowerUp> icon = FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            int count = 0;
            foreach (var item in  exps.Concat(icon)
                         .OrderByDescending(e => e.inScreen)
                         .ThenBy(e=>e.Distance2D(Player.Default))
                     )
            {
                    if (item.inScreen)
                    {
                        if (count > 20)
                        {
                            count = 0;
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    else
                    {
                        if (count > 10)
                        {
                            count = 0;
                            yield return new WaitForEndOfFrame();
                        }
                    }
                    count++;
                    ActionKit.OnUpdate.Register(() =>
                    {
                        var player = Player.Default;
                        if (player != null)
                        {
                            var dir = (player.Position() - item.Position()).normalized;
                            item.transform.Translate(dir * Time.deltaTime * player.moveSpeed * 2);
                        }
                    }).UnRegisterWhenGameObjectDestroyed(item);
            }
            
        }

        protected override Collider2D Collider => SelfCollider2D;
    }
}