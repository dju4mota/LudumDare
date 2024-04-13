using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    private Player player;
    [SerializeField] public int slow;
    [SerializeField] public int lowJump;
    [SerializeField] public int timer;
    [SerializeField] public bool isOutOfDanger = false;


    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        player = GetComponent<Player>();    
    }


    public void Slow()
    {
        player.speed = slow;
    }

    public void RevertSlow()
    {
        player.speed = 10;
    }

    public void LowJump()
    {
        player.jumpForce = lowJump;    
    }

    public void RevertJump()
    {
        player.jumpForce = 5;
    }

    public void StartTimer()
    {
        isOutOfDanger = false;
        Invoke("KillPlayer", timer);
    }
    public void KillPlayer()
    {
        if (!isOutOfDanger)
        {
            player.StartCoroutine(player.Die());
        }
    }

    public void StopTimer()
    {
        isOutOfDanger = true;
    }


}
