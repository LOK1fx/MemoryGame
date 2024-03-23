using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PointTrolley : MonoBehaviour
{
    public bool IsTurn;
    public Action OnSetPoint;
    public PointTrolley Path => _currentPath;

    [SerializeField] private PointTrolley _straightPath;
    [SerializeField] private PointTrolley _otherPath;

    private PointTrolley _currentPath;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetStraightPath();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetOtherPath();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetNonePath();
        }
    }

    public void SetStraightPath()
    {
        _currentPath = _straightPath;
        OnSetPoint?.Invoke();
    }

    public void SetOtherPath()
    {
        _currentPath = _otherPath;
        OnSetPoint?.Invoke();
    }

    public void SetNonePath()
    {
        _currentPath = null;
        OnSetPoint?.Invoke();
    }
}
