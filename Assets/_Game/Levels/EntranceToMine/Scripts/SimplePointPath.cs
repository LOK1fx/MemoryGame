using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimplePointPath : MonoBehaviour, IPointPath
{
    [SerializeField] private GameObject _nextPointPath;

    public IPointPath GetPoint()
    {
        IPointPath point;
        if (_nextPointPath) point = _nextPointPath.GetComponent<IPointPath>();
        else point = null;
        return point;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
