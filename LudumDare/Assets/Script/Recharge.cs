using UnityEngine;

public class Recharge : MonoBehaviour
{
    [SerializeField] Player player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.energy += 4;
            Destroy(gameObject);
        }
    }
}

