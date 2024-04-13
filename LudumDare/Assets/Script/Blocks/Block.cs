using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{

    private Transform poosition;
    [SerializeField] private float duration = 0.5f;
    private int id;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Invoke("CooldownPassed", duration);
        }
    }

    private void CooldownPassed()
    {
            Destroy(gameObject);
    }

}
