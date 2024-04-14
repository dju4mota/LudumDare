using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    private float timer;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprite;

    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = duration;
        Invoke("CooldownPassed", duration);
    }

    private void Update(){
        timer -= Time.deltaTime;
        if(timer > 2*duration/3){
            spriteRenderer.sprite = sprite[0];
        }
        else if(timer <= 2*duration/3 && timer > duration/3){
            spriteRenderer.sprite = sprite[1];
        }
        else{
            spriteRenderer.sprite = sprite[2];
        }
    }

    private void CooldownPassed()
    {
            Destroy(gameObject);
    }

}
