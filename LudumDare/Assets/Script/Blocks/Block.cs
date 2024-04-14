using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    private AnimationController anim;
    [SerializeField] private float duration = 2f;
    private float timer;

    private void Start(){
        anim = GetComponent<AnimationController>();
        timer = duration;
    }

    private void Update(){
        timer -= Time.deltaTime;
        if(timer > 2*duration/3){
            anim.ChangeAnimationState("1");
        }
        if(timer == 2*duration/3){
            anim.ChangeAnimationState("2");
        }
        else if(timer == duration/3){
            anim.ChangeAnimationState("3");
        }
    }



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
