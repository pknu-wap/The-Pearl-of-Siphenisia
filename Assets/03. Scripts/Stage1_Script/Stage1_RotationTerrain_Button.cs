using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_RotationTerrain_Button : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("side1").GetComponent<Stage1_Rotation>().RotateTerrain();
        }
    }
}