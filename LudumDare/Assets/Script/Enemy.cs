using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player;
    
    void Start()
    {
        player = GetComponent<Player>();    
    }


    public void Slow()
    {
        player.speed = 5;
    }

    public void LowJump()
    {
        player.jumpForce = 2.5f;
    }



}
