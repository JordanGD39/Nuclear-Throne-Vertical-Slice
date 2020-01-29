using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusHealth : ObjectHealth
{
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] destroyedSprites;

    private SpriteRenderer rendr;
    private int rndmInt;

    protected override void Start()
    {
        rendr = GetComponent<SpriteRenderer>();
        rndmInt = Random.Range(0, 6);
        rendr.sprite = normalSprites[rndmInt];

        base.Start();
    }

    protected override void Update()
    {
        if (health <= 0 && active)
        {
            active = false;
        }

        base.Update();

        if (!active)
        {
            rendr.sprite = destroyedSprites[rndmInt];
        }
    }
}
