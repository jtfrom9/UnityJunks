using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace UnityJunks.Tests.UniTaskTest
{
    public class UniTaskTest
    {
        [UnityTest]
        public IEnumerator Test2() => UniTask.ToCoroutine(async () =>
        {
            Debug.Log($"[1] {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            await Task.Run(async () =>
            {
                Debug.Log($"[task] {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                await UniTask.Run(() =>
                {
                    Debug.Log($"[task.uni] {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                });
                Debug.Log($"[task2] {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            });
            await UniTask.Run(() =>
            {
                Debug.Log($"[uni] {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            });
            Debug.Log($"[2] {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        });



        IEnumerator testCase(System.Func<Task> testFunc)
        {
            LogAssert.ignoreFailingMessages = true;
            System.Exception exp = null;
            yield return UniTask.ToCoroutine(async () =>
            {
                try
                {
                    await testFunc();
                }
                catch (System.Exception e)
                {
                    exp = e;
                    throw new System.AggregateException(e.Message, e);
                }
            });
            if (exp != null)
            {
                Assert.Fail(exp.Message);
            }
        }

        [UnityTest]
        public IEnumerator FailTest1() => testCase(async () =>
        {
            Debug.LogError("error");
            Assert.Fail("test fail");
            Debug.Log("ok");
        });

        [UnityTest]
        public IEnumerator FailTest2() => testCase(async () =>
        {
            Debug.LogError("error");
            await Task.Run(() =>
            {
                Assert.Fail("fail in task");
            });
            Debug.Log("ok");
        });

        [UnityTest]
        public IEnumerator FailTest3() => testCase(async () =>
        {
            Debug.LogError("error");
            throw new System.Exception("fail");
            Debug.Log("ok");
        });
    }
}
