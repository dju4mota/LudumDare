using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private float maxTimer = 10;
    [SerializeField] private float timer;
    [SerializeField] private float offSetY;
    [SerializeField] private Image img;
    [SerializeField] private Player player;


    public void Restart(float timerM)
    {
        maxTimer = timerM;
        timer = maxTimer;
        img.fillAmount = timer / maxTimer;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            img.fillAmount = timer / maxTimer;
        }
        Vector3 dir = player.transform.position + new Vector3(0,offSetY,0);
        gameObject.transform.position = dir;
    }

}
