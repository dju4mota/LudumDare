using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprite;
    bool isCharged = false;

    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[0];
    }
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.CompareTag("Player") && isCharged == false){
            StartCoroutine(Load());
        }
    }

    public void OnTriggerExit2D(Collider2D col){
        StopCoroutine(Load());
    }   

    public IEnumerator Load(){
        for(int i = 0; i < sprite.Length -1; i++){
             spriteRenderer.sprite = sprite[i];
        yield return new WaitForSeconds(0.2f);
        }
        isCharged = true;
        GameManager.Instance.checkpoint = transform;
    }
}
