using System;
using UnityEngine;
using UnityEngine.Events;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    public Transform RightPalm => _rightPalm;
    public Transform LeftPalm => _leftPalm;
    [SerializeField] private Transform _rightPalm, _leftPalm;

    public UnityEvent OnLeftGrab, OnRightGrab, OnLeftPoke, OnRightPoke;

    private void Awake()
    {
        instance = this;
    }
}
