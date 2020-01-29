using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSuck : MonoBehaviour
{
    [SerializeField] private float speed = 3;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed);
    }
}
