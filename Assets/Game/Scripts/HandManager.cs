using System;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    public Transform RightPalm => _rightPalm;
    public Transform LeftPalm => _leftPalm;
    [SerializeField] private Transform _rightPalm, _leftPalm;

    private void Awake()
    {
        instance = this;
    }
}
