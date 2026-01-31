using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RandomIntervalEvent : MonoBehaviour
{
    public UnityEvent Invoked;
    [SerializeField] private float minInterval, maxInterval;

    private void Start()
    {
        StartCoroutine(WaitAndInvoke());
    }

    private IEnumerator WaitAndInvoke()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minInterval, maxInterval));
        Invoked.Invoke();
        StartCoroutine(WaitAndInvoke());
    }
}
