using System.Collections;
using UnityEngine;


public class TrolleyMovements : MonoBehaviour
{
    public float CurrentSpeed { get; private set; }

    [SerializeField] private Transform _trolley;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _speed = 2;

    private IPointPath _point;

    private void Start()
    {
        _point = _startPoint.GetComponent<IPointPath>();
        StopTrolley();
    }

    private void Update()
    {
        if (CurrentSpeed > 0)
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
            transform.rotation = Quaternion.Lerp(transform.rotation, point.rotation, 8f * Time.deltaTime);
        }
    }

    public void StopTrolley()
    {
        CurrentSpeed = 0;
    }

    public void StartTrolley()
    {
        StopAllCoroutines();
        StartCoroutine(MoveToTarget());
        CurrentSpeed = _speed;
    }

    public void SetSpeed(float value)
    {
        CurrentSpeed = value;
    }

    private IEnumerator MoveToTarget()
    {
        Transform pointTransform = _point.GetTransform();

        while (Vector3.Distance(transform.position, pointTransform.position) > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointTransform.position, CurrentSpeed * Time.deltaTime);
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
