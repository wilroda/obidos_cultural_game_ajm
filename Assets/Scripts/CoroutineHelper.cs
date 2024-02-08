using System;
using System.Collections;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static Coroutine Start(IEnumerator routine)
    {
        if (_instance == null)
        {
            new GameObject("CoroutineHelper").AddComponent<CoroutineHelper>();
        }
        return _instance.StartCoroutine(routine);
    }
    public static Coroutine PerformAfterSeconds(float seconds, Action callback)
    {
        if (_instance == null)
        {
            new GameObject("CoroutineHelper").AddComponent<CoroutineHelper>();
        }

        IEnumerator CCallback()
        {
            yield return new WaitForSecondsRealtime(seconds);
            callback?.Invoke();
        }

        return _instance.StartCoroutine(CCallback());
    }
    public static Coroutine PerformAfterFrames(int frames, Action callback)
    {
        if (_instance == null)
        {
            new GameObject("CoroutineHelper").AddComponent<CoroutineHelper>();
        }

        IEnumerator CCallback()
        {
            int elapsedFrames = 0;
            while (elapsedFrames < frames)
            {
                elapsedFrames++;
                yield return null;
            }
            callback?.Invoke();
        }

        return _instance.StartCoroutine(CCallback());
    }
}

// This class creates a singleton instance of itself on the first call to StartCoroutine, 
// and reuses that instance for all subsequent calls. This allows you to start coroutines from any class,
// without needing to inherit from MonoBehaviour.





