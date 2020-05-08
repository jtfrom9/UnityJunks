using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;

namespace UnityJunks.Tests.UniRx
{
    public class UniRxTset
    {
        [Test]
        public void RangeTest()
        {
            Observable.Range(0, 3).Subscribe(x => { Debug.Log(x); });
            {
                var subject = new Subject<Unit>();
                subject.Subscribe(_ => { Debug.Log("ok"); });
                subject.OnNext(Unit.Default);
                subject.OnNext(Unit.Default);
            }
        }

        [Test]
        public void ZipTest()
        {
            var subject1 = new Subject<int>();
            var subject2 = new Subject<int>();

            Observable
                .Zip(subject1, subject2)
                .Subscribe(x =>
                {
                    foreach (var item in x)
                    {
                        Debug.Log("next." + item);
                    }
                }
                , () => Debug.Log("completed."));
            subject1.OnNext(1); // 流れない
            subject2.OnNext(2); // next: 1.  >  next: 2.
            subject2.OnNext(3); // 流れない
            subject2.OnNext(4); // 流れない
            subject1.OnNext(5); // next: 5.  >  next: 3.
            subject1.OnCompleted();
            subject2.OnCompleted(); // completed
        }

        [Test]
        public void SubjectTest()
        {
            {
                var subject = new Subject<int>();
                subject.Repeat().Subscribe(x => { Debug.Log(x); });
                for (int i = 0; i < 5; i++)
                {
                    subject.OnNext(i);
                }
                subject.OnCompleted();
                for (int i = 0; i < 5; i++)
                {
                    subject.OnNext(i);
                }
            }
        }

        [Test]
        public void BoolTogglerSelectTest()
        {
            bool flag = false;
            Observable.Range(1, 10)
                .Select(x =>
                {
                    flag = !flag;
                    return (index:x, flag:!flag);
                }).Subscribe(v =>
                {
                    Debug.Log($"{v.index}, {v.flag}");
                });
        }
    }
}
