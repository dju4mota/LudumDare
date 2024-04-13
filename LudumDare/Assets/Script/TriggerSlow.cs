using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlow : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
       Enemy.Instance.Slow();     
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Enemy.Instance.RevertSlow();
    }

}
