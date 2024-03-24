using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForkPointPath : MonoBehaviour, IPointPath
{
    public ESwitcherPosition SwitcherPosition;

    [SerializeField] private GameObject _straightPointPath;
    [SerializeField] private GameObject _otherPointPath;

    private IPointPath _currentPointPath;

    public IPointPath GetPoint()
    {
        return _currentPointPath;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetStraightPath()
    {
        _currentPointPath = _straightPointPath.GetComponent<IPointPath>();
        SwitcherPosition = ESwitcherPosition.Straight;
    }

    public void SetOtherPath()
    {
        _currentPointPath = _otherPointPath.GetComponent<IPointPath>();
        SwitcherPosition = ESwitcherPosition.Other;
    }

    public void SetNeutralPath()
    {
        _currentPointPath = null;
        SwitcherPosition = ESwitcherPosition.Neutral;
    }
}
