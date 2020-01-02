﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}