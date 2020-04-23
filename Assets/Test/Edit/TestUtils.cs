using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;

namespace UnityJunks.Tests
{
    static class TestUtilsExtensions
    {
        static IEnumerable<string> assetsPath(string path)
        {
            bool found = false;
            foreach(var p in path.Split('/')) {
                if(found || p=="Assets") {
                    found = true;
                    yield return p;
                }
            }
        }

        public static IEnumerator _Expect(this Task task, Func<Task, bool> expect,
            string member,
            string file,
            int line)
        {
            yield return new WaitUntil(() => task.IsCompleted);
            var msg = (!task.IsFaulted) ? "" :
                string.Format("{0}(\"{1}\") @ {2}:{3} (in {4})",
                    task.Exception.InnerException.GetType().Name,
                    task.Exception.InnerException.Message,
                    string.Join("/", assetsPath(file)),
                    line,
                    member);
            if (!expect(task))
            {
                Debug.LogError(msg);
                Debug.LogException(task.Exception);
                Assert.Fail(msg);
            }
            else if (task.IsFaulted)
            {
                Debug.Log($"<Failed As Expected> {msg}");
            }
        }

        public static IEnumerator Expect(this Task task,
            Func<Task, bool> expect,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return _Expect(task, expect, member, file, line);
        }

        public static IEnumerator Success(this Task task,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return _Expect(task, (t => !t.IsFaulted), member, file, line);
        }

        public static IEnumerator Fail(this Task task,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return _Expect(task, (t => t.IsFaulted), member, file, line);
        }

        public static IEnumerator SuccessThen(this Task task, Action<Task> callback = null,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return Success(task, member, file, line);
            if (callback != null)
                callback(task);
        }

        public static IEnumerator SuccessThen<Type>(this Task<Type> task, Action<Task<Type>> callback = null,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return Success(task, member, file, line);
            if (callback != null)
                callback(task);
        }

        public static IEnumerator FailThen(this Task task, Action<Task> callback = null,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return Fail(task, member, file, line);
            if (callback != null)
                callback(task);
        }

        public static IEnumerator FailThen<Type>(this Task<Type> task, Action<Task<Type>> callback = null,
            [CallerMemberName] string member = "",
            [CallerFilePath] string file = "",
            [CallerLineNumber] int line = 0)
        {
            yield return Fail(task, member, file, line);
            if (callback != null)
                callback(task);
        }
    }
}
