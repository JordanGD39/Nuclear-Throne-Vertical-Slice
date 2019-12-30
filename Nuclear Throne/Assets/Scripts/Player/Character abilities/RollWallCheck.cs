using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollWallCheck : MonoBehaviour
{
    private Roll roll;
    [SerializeField] private bool hori = true;

    // Start is called before the first frame update
    void Start()
    {
        roll = transform.parent.parent.GetChild(0).GetComponent<Roll>();
    }

    private void Update()
    {
        transform.position = roll.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision + gameObject.name);

        if (hori)
        {
            roll.WallHori = true;
        }
        else
        {
            roll.WallVert = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hori)
        {
            roll.WallHori = false;
        }
        else
        {
            roll.WallVert = false;
        }
    }
}
