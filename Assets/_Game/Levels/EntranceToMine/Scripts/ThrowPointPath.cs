using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ThrowPointPath : MonoBehaviour, IPointPath
{
    [SerializeField] private GameObject _trolley;
    public IPointPath GetPoint()
    {
        var rigidbody = _trolley.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(_trolley.transform.forward * 10, ForceMode.Impulse);
        return null;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
