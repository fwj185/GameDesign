/****************************************************************************
 * Copyright (c) 2015 - 2024 liangxiegame UNDER MIT License
 * 
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/

using System;
using System.Collections;

namespace QFramework
{
// - 协程包装器：将 Unity 协程封装为可序列化的动作
// - 双生命周期管理：同时遵循 ActionKit 和 Unity 的协程机制
// - 延迟执行模式：通过 Func
// 延迟获取协程实例
    internal class CoroutineAction : IAction
    {
        private static SimpleObjectPool<CoroutineAction> mPool =
            new SimpleObjectPool<CoroutineAction>(() => new CoroutineAction(), null, 10);

        private Func<IEnumerator> mCoroutineGetter = null;

        private CoroutineAction(){}
// - 协程工厂模式：通过委托延迟创建实际协程
// - ID 自增机制：保证每个动作唯一标识
// - 重置保障：显式调用 Reset() 初始化状态        
        public static CoroutineAction Allocate(Func<IEnumerator> coroutineGetter)
        {
            var coroutineAction = mPool.Allocate();
            coroutineAction.ActionID = ActionKit.ID_GENERATOR++;
            coroutineAction.Deinited = false;
            coroutineAction.Reset();
            coroutineAction.mCoroutineGetter = coroutineGetter;
            return coroutineAction;
        }

        public bool Paused { get; set; }
        public void Deinit()
        {
            if (!Deinited)
            {
                Deinited = true;
                mCoroutineGetter = null;

                ActionQueue.AddCallback(new ActionQueueRecycleCallback<CoroutineAction>(mPool,this));
            }
        }

        public void Reset()
        {
            Paused = false;
            Status = ActionStatus.NotStart;
        }

        public bool Deinited { get; set; }
        public ulong ActionID { get; set; }
        public ActionStatus Status { get; set; }

// - 专用 MonoBehaviour 驱动：通过单例保证协程执行环境
// - 完成回调机制：协程结束后自动标记动作完成
// - 异常处理：依赖 Unity 的协程异常捕获机制

        public void OnStart()
        {
            ActionKitMonoBehaviourEvents.Instance.ExecuteCoroutine(mCoroutineGetter(), () =>
            {
                Status = ActionStatus.Finished;
            });
        }

        public void OnExecute(float dt)
        {
        }

        public void OnFinish()
        {
        }
    }
    
    public static class CoroutineExtension
    {

// - 双向转换：协程 ↔ 动作
// - 流式接口：支持链式编程风格
// - 隐式封装：隐藏对象池分配细节
        public static ISequence Coroutine(this ISequence self, Func<IEnumerator> coroutineGetter)
        {
            return self.Append(CoroutineAction.Allocate(coroutineGetter));
        }
        
        public static IAction ToAction(this IEnumerator self)
        {
            return CoroutineAction.Allocate(() => self);
        }
    }
}