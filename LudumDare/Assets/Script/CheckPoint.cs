using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprite;
    private int i = 0;

    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[0];
    }

    private void Update(){
        spriteRenderer.sprite = sprite[i];
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player")){
            StopCoroutine(Unload());
            StartCoroutine(Load());
        }
    }

    public void OnTriggerExit2D(Collider2D col){
         if(col.gameObject.CompareTag("Player")){
            StopCoroutine(Load());
            StartCoroutine(Unload());
        }
    }   

    public IEnumerator Load(){
        i++;
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator Unload(){
        i--;
        yield return new WaitForSeconds(0.2f);
    }
}
