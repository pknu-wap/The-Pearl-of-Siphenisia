using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Tree_in_player_effect : MonoBehaviour
{ 
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("bridge").GetComponent<Stage1_Diappeared_Bridge>().DisapearBridge();
        }
        else if (other.gameObject.tag == "key")
        {
            GameObject.Find("Roots").GetComponent<Stage1_Roots>().DisapearRoots();
        }
    }
}