using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    [SerializeField] private Player player;
    [SerializeField] public int slow;
    [SerializeField] public int lowJump;
    [SerializeField] public float timerMax;
    [SerializeField] public float timerAtual;
    [SerializeField] public bool onDanger = false;
    [SerializeField] public GameObject timerBarObj;
    [SerializeField] public TimerBar timerBar;


    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
    
        if(onDanger)
        {
            if(timerAtual > 0)
            {
                timerAtual -= Time.deltaTime;
            }
            else
            {
                player.StartCoroutine(player.Die());
            }
        }
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
        player.jumpForce = 10;
    }

    public void StartTimer()
    {
        onDanger = true;
        timerAtual = timerMax;
        timerBarObj.SetActive(true);
        timerBar.Restart(timerMax);
    }

    public void StopTimer()
    {
        onDanger = false;
        timerBarObj.SetActive(false);
    }


}
