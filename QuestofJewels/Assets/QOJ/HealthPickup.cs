using UnityEngine;
using Gamekit2D;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    Damageable player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(player.startingHealth < 5)
            {
                player.startingHealth += healAmount;
                gameObject.SetActive(false);
            }
        }
    }
}