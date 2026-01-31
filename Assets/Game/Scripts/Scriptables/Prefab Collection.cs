using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabCollection", menuName = "Scriptable Objects/PrefabCollection")]
public class PrefabCollection : ScriptableObject
{
    public PrefabIntPair[] prefabs;
}

[Serializable]
public class PrefabIntPair
{
    public GameObject prefab;
    public int count;
}
