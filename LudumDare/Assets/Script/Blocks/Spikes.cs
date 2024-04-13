using UnityEngine;

public class Spikes : MonoBehaviour 
{
    [SerializeField] Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.StartCoroutine(player.Die());
        }
    }
}
