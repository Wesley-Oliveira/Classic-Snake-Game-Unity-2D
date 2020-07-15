using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public _GC gameController;

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Food":
                gameController.Eat();
                break;
            case "Tail":
                print("morreu");
                break;
        }
    }
}
