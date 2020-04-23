using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using System.Linq;

namespace UnityJunks.Tests.Csharp
{
    public class JunkTest
    {
        [UnityTest]
        public IEnumerator yeildTest()
        {
            yield return Task.Run(() => { Debug.Log("task."); return 1; }).SuccessThen(task =>
            {
                Debug.Log($"callback1, result={task.Result}");
            });
            yield return Task.Run(() => { Debug.Log("task2."); return "foo"; }).SuccessThen(task =>
            {
                Debug.Log($"callback2, result={task.Result}");
            });
            yield return Task.Run(() => { Debug.Log("task3."); }).SuccessThen(task =>
            {
                Debug.Log($"callback3");
                // The yield statement cannot be used inside an anonymous method or lambda expression [PlayMode]
                // yield return _wait(Task.Run(() => { Debug.Log("task4."); }));
            });
        }

        class LogBehaviour : MonoBehaviour
        {
            public int X = 0;
            void Awake()
            {
                Debug.Log($"Awake x={X}");
            }

            void Start()
            {
                Debug.Log($"Start x={X}");
            }
        }

        [UnityTest]
        public IEnumerator BehaviorTest()
        {
            Debug.Log("Test start");
            var b = new GameObject("Mock").AddComponent<LogBehaviour>();
            b.X = 1;
            Debug.Log("after initialize");
            yield return null;
            Debug.Log("after yeild");
        }
    }

    public class TestClass
    {
        public void Test1()
        {
            Assert.Pass();
        }
        public void Test2()
        {
            Assert.Pass();
        }
        public Task Test3()
        {
            return Task.Run(() =>
            {
                Debug.Log("Test3");
            });
        }
        public async Task<int> Test4()
        {
            await Task.Delay(10);
            return 1;
        }
    }

    public class JunkTest2
    {
        TestClass test;
        [SetUp]
        public void SetUp()
        {
            test = new TestClass();
        }
        [Test]
        public void Test1()
        {
            test.Test1();
        }
        [Test]
        public void Test2()
        {
            test.Test2();
        }
        [UnityTest]
        public IEnumerator Test3()
        {
            yield return test.Test3().Success();
            Assert.Pass();
        }
        [Test]
        public async void Test4InvalidPass()
        {
            var ret = await test.Test4();
            Assert.That(ret, Is.EqualTo(0));
        }

        [Test, Explicit]
        public void Test4Freeze()
        {
            var task = test.Test4();
            Assert.That(task.Result, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator Test4()
        {
            yield return test.Test4().SuccessThen(task =>
            {
                Assert.That(task.Result, Is.EqualTo(0));
            });
        }
    }

    abstract public class JunkTestAbstract
    {
        protected abstract void init();

        [SetUp]
        public void SetUp()
        {
            init();
        }

        [Test]
        public void Test1()
        {
            Debug.Log("Test1");
            Assert.Pass();
        }
        [Test]
        public virtual void Test2()
        {
            Debug.Log("Test2");
            Assert.Pass();
        }
    }

    public class JunkTest3 : JunkTestAbstract
    {
        protected override void init()
        {
            Debug.Log("JunkTest3");
        }
    }
    public class JunkTest4 : JunkTestAbstract
    {
        protected override void init()
        {
            Debug.Log("JunkTest4");
        }

        [Test]
        public override void Test2()
        {
            Debug.Log("??");
            base.Test2();
            Assert.Fail();
        }
    }
    public class AsyncExceptionTest
    {
        async Task<int> error()
        {
            throw new System.Exception("test error");
            await Task.Delay(10);
            return 0;
        }
        async Task func()
        {
            try
            {
                var x = await error();
            }
            catch (System.AggregateException e)
            {
                Debug.Log($">>> catch aggregate: {e.InnerException.Message}");
            }
            catch (System.Exception e)
            {
                Debug.Log($">>> catch except: {e.Message}");
            }
        }
        async Task func2()
        {
            try
            {
                var x = error().Result;
            }
            catch (System.AggregateException e)
            {
                Debug.Log($">>> catch aggregate: {e.InnerException.Message}");
            }
            catch (System.Exception e)
            {
                Debug.Log($">>> catch except: {e.Message}");
            }
        }
        [UnityTest]
        public IEnumerator catchInContinueWithTest()
        {
            yield return Task.Run(() => func2()).Success();
        }
        async Task func3()
        {
            await error().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log($">>> catch except: {task.Exception.InnerException.Message}");
                }
            });
        }
        [UnityTest]
        public IEnumerator catchInContinueWith2Test()
        {
            yield return Task.Run(() => func3()).Success();
        }
    }

    public class JunkTest5
    {
        class Data
        {
            public int x;
            public int y;
        };
        [Test]
        public void test()
        {
            var dict = new Dictionary<string, Data>()
            {
                {"hoge", new Data() { x = 1, y = 2}},
                {"foo", new Data() { x = 3, y = 4}},
            };
            var e = dict.Where(x => x.Value.y == 4).First();
            Assert.That(e.Key, Is.EqualTo("foo"));
            var e2 = dict.Where(x => x.Value.y == 6).FirstOrDefault();
            Assert.That(e2, Is.Null);
        }

        [Test]
        public void nextlocktest()
        {
            var lk = new object();
            bool result = false;
            lock (lk)
            {
                Debug.Log("out");
                lock (lk)
                {
                    Debug.Log("in");
                    result = true;
                }
            }
            Assert.That(result, Is.True);
        }

        static IEnumerable<string> assetsPath(string path)
        {
            bool found = false;
            foreach (var p in path.Split('/'))
            {
                if (found || p == "Assets")
                {
                    found = true;
                    yield return p;
                }
            }
        }

        [Test]
        public void filepathtest()
        {
            var file = "/Users/jtachikawa/work/UnityStudy/MultiARSDK/Projects/ARCore/Assets/Main/GridVisualizer/Scripts/MockTraceSessionManager.cs";
            foreach (var p in assetsPath(file))
            {
                Debug.Log(p);
            }
            Debug.Log(string.Join("/", assetsPath(file)));
        }
    }

    public class AsyncTest
    {
        [UnityTest]
        public IEnumerator test()
        {
            yield return Task.Run(async () =>
            {
                Debug.Log($"0 ({System.Threading.Thread.CurrentThread.ManagedThreadId})");
                var t = Task.Run(() =>
                {
                    Task.Delay(1000).Wait();
                    Debug.Log($"1 ({System.Threading.Thread.CurrentThread.ManagedThreadId})");
                }).ContinueWith(task =>
                {
                    Task.Delay(1000).Wait();
                    Debug.Log($"2 ({System.Threading.Thread.CurrentThread.ManagedThreadId})");
                });
                Debug.Log($"3 ({System.Threading.Thread.CurrentThread.ManagedThreadId})");
                await t;
                Debug.Log($"4 ({System.Threading.Thread.CurrentThread.ManagedThreadId})");
            }).Success();
        }
    }
}
