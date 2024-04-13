using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{
    [SerializeField] Player player;


    private void Start()
    {
        player = GetComponent<Player>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plyaer"))
        {
            player.energy++;
        }
    }
}

