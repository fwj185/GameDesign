using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public abstract class PowerUp :GamePlayObj
    {
        public bool isFay { get; set; }

        private int FlyingToPlayerFrameCount = 0;

        protected abstract void Expe();
        
        private void Update()
        {
            if (isFay)
            {
                if (FlyingToPlayerFrameCount == 0)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = 5;
                }

                FlyingToPlayerFrameCount++;

                if (Player.Default)
                {
                    var direction = Player.Default.DirectionFrom(this);
                    var distance = direction.magnitude;

                    if (FlyingToPlayerFrameCount <= 15)
                    {
                        transform.Translate(direction.normalized * -2 * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(direction.normalized * 7.5f * Time.deltaTime);
                    }

                    if (distance < 0.1f)
                    {
                        Expe();
                    }
                }
                
            }
            
        }
    }
}