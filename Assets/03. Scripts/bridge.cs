using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;

public class bridge : unityE
{
    Rigidbody2D rb2d1;
    void Start()
    {
        rb2d1 = GetComponent<Rigidbody2D>();
    }

    public void falldown()
    {
        Debug.Log("외나무다리가 떨어짐");
        rb2d1.bodyType = RigidbodyType2D.Dynamic;
    }
    
}
