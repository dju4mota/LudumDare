using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("End", 2f);   
        }
    }

    void End()
    {
        GameManager.Instance.LoadScene(GameManager.Scene.EndMenu);
    }
}

