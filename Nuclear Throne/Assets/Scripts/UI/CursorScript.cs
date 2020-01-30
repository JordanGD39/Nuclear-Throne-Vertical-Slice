using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    private void Update()
    {
        Vector3 mov = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mov.z = 0;

        transform.position = mov;
    }
}
