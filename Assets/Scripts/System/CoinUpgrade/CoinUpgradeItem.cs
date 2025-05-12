

using QFramework;
using System;
using UnityEngine;
namespace PrjectSurvivor
{
    public class CoinUpgradeItem
    {
        public EasyEvent OnChanged = new();
        public string Key { get; private set; }
        public string Description { get; private set; }
        public int Price { get; private set; }
        public bool UpgradeFinish { get; set; } = false;
        private Action<CoinUpgradeItem> mOnupGrade;
        private Func<bool> mCondition;
        private CoinUpgradeItem mNext = null;

        public CoinUpgradeItem Next(CoinUpgradeItem next)
        {
            mNext = next;
            mNext.Condition(() => UpgradeFinish);//给下一个存下自己是否解锁
            return mNext;
        }

        public void TriggerOnChanged()
        {
            OnChanged.Trigger();
            mNext?.TriggerOnChanged();
        }
        
        public void Upgrade()
        {
            mOnupGrade?.Invoke(this);
            UpgradeFinish = true;
            TriggerOnChanged();
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
        }
        
        public bool ConditionCheck()
        {

            if (mCondition != null)
            {
                //Debug.Log(this.Key + ":" + (!UpgradeFinish && mCondition.Invoke(this)));
                //自己没解锁,上一个解锁了
                return !UpgradeFinish && mCondition.Invoke();
            }
            else
            {
                //Debug.Log(this.Key + ":" + (!UpgradeFinish));
                return !UpgradeFinish;
            }

        }

        public CoinUpgradeItem Withkey(string key)
        {
            Key = key;
            return this;
        }
        public CoinUpgradeItem Withdescription(string description)
        {
            Description = description;
            return this;
        }
        public CoinUpgradeItem WithPrice(int price)
        {

            Price = price;
            return this;
        }
        public CoinUpgradeItem OnUpgrade(Action<CoinUpgradeItem> onupgrade)
        {
            mOnupGrade = onupgrade;
            return this;
        }
        public CoinUpgradeItem Condition(Func< bool> condition)
        { mCondition = condition; return this; }
    }
}
