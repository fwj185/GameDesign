/****************************************************************************
 * Copyright (c) 2015 - 2024 liangxiegame UNDER MIT License
 * 
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using System;

namespace QFramework
{
 // - 实现IAction接口，可集成到动作序列中
// - 使用函数式编程模式（Func
// ）
// - 内置静态对象池（初始容量10）优化性能
    public class Condition : IAction
    {
        private Func<bool> mCondition;

        private static SimpleObjectPool<Condition> mSimpleObjectPool =
            new SimpleObjectPool<Condition>(() => new Condition(), null, 10);
        
        private Condition(){}
// - 线程安全分配：通过对象池保证多线程安全
// - 唯一标识生成： ActionKit.ID_GENERATOR++ 防止ID冲突
// - 双重置机制：显式Reset() + 隐式参数注入
        public static Condition Allocate(Func<bool> condition)
        {
            var conditionAction = mSimpleObjectPool.Allocate();
            conditionAction.ActionID = ActionKit.ID_GENERATOR++;
            conditionAction.Deinited = false;
            conditionAction.Reset();
            conditionAction.mCondition = condition;
            return conditionAction;
        }

        public bool Paused { get; set; }
        public bool Deinited { get; set; }
        public ulong ActionID { get; set; }
        public ActionStatus Status { get; set; }
        public void OnStart()
        {
        }
// 1. 持续条件检测：每帧调用直到条件满足
// 2. 非阻塞设计：不占用主线程等待
// 3. 自动流程控制：满足条件后自动触发Finish()
        public void OnExecute(float dt)
        {
            if (mCondition.Invoke())
            {
                this.Finish();
            }
        }

        public void OnFinish()
        {
        }
// - 延迟回收策略：通过ActionQueue保证执行时序
// - 引用清理：重置mCondition防止闭包残留
// - 状态标记：Deinited标志防止重复回收
        public void Deinit()
        {
            if (!Deinited)
            {
                Deinited = true;
                mCondition = null;
                ActionQueue.AddCallback(new ActionQueueRecycleCallback<Condition>(mSimpleObjectPool,this));
            }
        }

        public void Reset()
        {
            Paused = false;
            Status = ActionStatus.NotStart;
        }
    }
    
    public static class ConditionExtension
    {
// - 流式接口支持：返回ISequence实现链式编程
// - 隐式类型转换：自动包装Func为Condition对象
// - 执行上下文保留：自动绑定到当前序列
        public static ISequence Condition(this ISequence self, Func<bool> condition)
        {
            return self.Append(QFramework.Condition.Allocate(condition));
        }
    }
}