using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class superBoll : ViewController
    {
        private float mCurrentSeconds = 0;
        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
            //它写到父节点上了
            if (mCurrentSeconds > 15 && Global.SuperBomb.Value)
            {
                mCurrentSeconds = 0;
                Bomb.Execute();
            }

        }
    }
}
