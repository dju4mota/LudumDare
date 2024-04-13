using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private Transform poosition;
    private int duration;
    private int id;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            LooseDuration();
        }
    }

    private void LooseDuration()
    {
        duration--;
        if(duration <= 0)
        {
            Destroy(this,0.3f);
        }
    }

}
