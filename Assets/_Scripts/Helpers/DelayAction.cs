using System;
using System.Collections;
using _Scripts.Patterns;
using UnityEngine;

namespace _Scripts.Helpers
{
    [AutoCreateSingelton]
    public sealed class DelayAction : Singleton<DelayAction>
    {
        public void Abort(Coroutine delayAction) { StopCoroutine(delayAction); }
        public Coroutine WaitForSeconds(Action action, float delay) { return StartCoroutine(WaitForSecondsCoroutine(action, delay));}
        public Coroutine WaitWhile(Action action, Func<bool> waitFunk) { return StartCoroutine(WaitWhileCoroutine(action, waitFunk));}
        public Coroutine WaitUntil(Action action, Func<bool> waitFunk) { return StartCoroutine(WaitUntilCoroutine(action, waitFunk));}

        private IEnumerator WaitForSecondsCoroutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Invoke();
        }
        
        private IEnumerator WaitUntilCoroutine(Action action, Func<bool> waitFunk)
        {
            yield return new WaitUntil(waitFunk);
            action.Invoke();
        }
        
        private IEnumerator WaitWhileCoroutine(Action action, Func<bool> waitFunk)
        {
            yield return new WaitWhile(waitFunk);
            action.Invoke();
        }
    }
}