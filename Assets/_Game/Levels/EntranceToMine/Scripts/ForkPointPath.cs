using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForkPointPath : MonoBehaviour, IPointPath
{
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
    }

    public void SetOtherPath()
    {
        _currentPointPath = _otherPointPath.GetComponent<IPointPath>();
    }

    public void SetNonePath()
    {
        _currentPointPath = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SetStraightPath();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetOtherPath();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetNonePath();
        }
    }
}
