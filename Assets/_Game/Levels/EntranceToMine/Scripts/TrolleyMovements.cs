using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrolleyMovements : MonoBehaviour
{
    [SerializeField] private PointTrolley _point;
    [SerializeField] private float _speed = 2;

    private float _currentSpeed;

    private void Start()
    {
        StopTrolley();
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, _point.transform.position) > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, _point.transform.position, _currentSpeed * Time.deltaTime);
            yield return null;
        }
        NextPoint();
    }

    private void NextPoint()
    {
        if(_point.Path == null)
        {
            _point.OnSetPoint += SetPoint;
        }
        else
        {
            _point = _point.Path;
        }
    }

    private void SetPoint()
    {
        _point = _point.Path;
    }

    public void StopTrolley()
    {
        _currentSpeed = 0;
    }

    public void StartTrolley()
    {
        if (_point != null)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToTarget());
            _currentSpeed = _speed;
        }
        else
        {
            StopTrolley();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartTrolley();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StopTrolley();
        }
    }

}
