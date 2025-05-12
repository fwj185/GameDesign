using QFramework;
using System;
using UnityEngine;

namespace PrjectSurvivor
{
    public class ExpUpgradeitem
    {
        //public EasyEvent OnChanged = new();
        public ExpUpgradeitem(bool isWeapon)
        {
            IsWeapon = isWeapon;
        }

        public string IconName { get; private set; }
        public string PairedName {get;private set;}
        public string PairedDescription { get;private set;}
        public string PairedIconName { get; private set;}
        public bool IsWeapon = false;
        public string Key { get; private set; }
        public string Description => mDescriptionFactory(CurrentLevel.Value + 1);
        public bool UpgradeFinish => CurrentLevel.Value >= MaxLv;
        private Action<ExpUpgradeitem, int> mOnupGrade;

        private Func<ExpUpgradeitem, bool> mCondition;

        //public int CurrentLevel { get; private set; } = 0;
        public BindableProperty<int> CurrentLevel = new BindableProperty<int>(0);
        public int MaxLv { get; private set; }

        public BindableProperty<bool> Visible = new BindableProperty<bool>(false);
        private Func<int, string> mDescriptionFactory;
        public string Name {get;private set;}
        public void Upgrade()
        {
            CurrentLevel.Value++;
            ExpupgradeSystem.CheckAllUnlockedFinish();
            mOnupGrade?.Invoke(this, CurrentLevel.Value);

            //CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger();
        }

        public ExpUpgradeitem Withkey(string key)
        {
            Key = key;
            return this;
        }

        public ExpUpgradeitem Withdescription(Func<int, string> description)
        {
            mDescriptionFactory = description;
            return this;
        }

        /// <summary>
        /// 升级回调
        /// </summary>
        /// <param name="onupgrade"></param>
        /// <returns></returns>
        public ExpUpgradeitem OnUpgrade(Action<ExpUpgradeitem, int> onupgrade)
        {
            mOnupGrade = onupgrade;
            return this;
        }

        /// <summary>
        /// 上一级升级状态
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ExpUpgradeitem Condition(Func<ExpUpgradeitem, bool> condition)
        {
            mCondition = condition;
            return this;
        }

        /// <summary>
        /// 设置当前对象的最大等级。传入的值会减一
        /// </summary>
        /// <param name="lv">传入的等级值。实际设置的最大等级将是此值减去1。</param>
        /// <returns>返回当前对象本身，以便支持链式调用。</returns>
        public ExpUpgradeitem WithMaxLv(int lv)
        {
            MaxLv = lv - 1;
            return this;
        }

        public ExpUpgradeitem WithPairedName(string pairedName)
        {
            this.PairedName = pairedName;
            return this;
        }
        public ExpUpgradeitem WithName(string name)
        {
            this.Name = name;
            return this;
        }
        public ExpUpgradeitem WithIconName(string iconName)
        {
            this.IconName = iconName;
            return this;
        }
        public ExpUpgradeitem WithPairedIconName(string pairedIconName)
        {
            this.PairedIconName = pairedIconName;
            return this;
        }
        public ExpUpgradeitem WithPairedDescription(string pairedDescription)
        {
            this.PairedDescription = pairedDescription;
            return this;
        }
    }
}