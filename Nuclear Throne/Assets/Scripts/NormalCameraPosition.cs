using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCameraPosition : MonoBehaviour
{
    //This Class is bound to the 'CameraPositionReference'-gameobject
    //The angle of the 'PlayerCameraRelation'-script's Raycast will remain the same now

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField] private float smoothSpeed = 0.125f;

    public Vector3 Offset { get { return _offset; } set { _offset = value; } }

    private float rotationSpeed = 3.0f;
    private bool stop;

    public bool Stop { get { return stop; } set { stop = value; } }

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;        
    }

    private void LateUpdate()
    {
        Vector3 desiredPos = new Vector3(_target.position.x, _target.position.y, transform.position.z) + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
    }
}
