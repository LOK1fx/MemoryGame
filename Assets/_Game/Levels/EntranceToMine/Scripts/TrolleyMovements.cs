using System.Collections;
using UnityEngine;


public class TrolleyMovements : MonoBehaviour
{ 
    [SerializeField] private Transform _trolley;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _speed = 2;

    private IPointPath _point;
    private float _currentSpeed;

    private void Start()
    {
        _point = _startPoint.GetComponent<IPointPath>();
        StopTrolley();
    }

    private void Update()
    {
        if (_currentSpeed > 0)
        {
            UpdateRotation();
        }

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    StopTrolley();
        //}
        
    }

    private void UpdateRotation()
    {
        Transform point = null;

        if (_point != null)
            point = _point.GetTransform();

        if(point != null)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, point.eulerAngles, 8f * Time.deltaTime);
        }
    }

    public void StopTrolley()
    {
        _currentSpeed = 0;
    }

    public void StartTrolley()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget());
        _currentSpeed = _speed;
    }

    public void SetSpeed(float value)
    {
        _currentSpeed = value;
    }

    private IEnumerator MoveToTarget()
    {
        Transform pointTransform = _point.GetTransform();

        while (Vector3.Distance(transform.position, pointTransform.position) > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointTransform.position, _currentSpeed * Time.deltaTime);
            yield return null;
        }

        NextPoint();
    }

    private void NextPoint()
    {
        if (_point.GetPoint() != null)
        {
            _point = _point.GetPoint();
            //transform.LookAt(_point.GetTransform());
            StartTrolley();
        }
        else
        {
            StopTrolley();
        }
    }
}
