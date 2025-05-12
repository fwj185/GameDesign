using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class BasketBallAbility : ViewController
    {
        private List<Ball> mBalls = new();
        void Start()
        {
            Global.BasketBallCount.Or(Global.AdditionalFlyThingCount).Register(() =>
            {
                this.CreateBall();
            });
            this.CreateBall();
        }
        private void CreateBall()
        {
            var count = Global.BasketBallCount.Value + Global.AdditionalFlyThingCount.Value - mBalls.Count;
            for (int i = 0; i < count; i++)
            {
                mBalls.Add(Ball.Instantiate().SyncPosition2DFrom(this).Show());

            }
        }
    }
}
