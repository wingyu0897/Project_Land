using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingObject
{
    public PoolableMono prefab;
    public int count;
    public string parent;
}

[CreateAssetMenu(menuName = "SO/Pool/List")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolingObject> poolingList;
}
