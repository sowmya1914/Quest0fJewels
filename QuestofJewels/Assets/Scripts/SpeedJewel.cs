using UnityEngine;
using Gamekit2D;

public class SpeedJewel : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] float speedIncrease = 3;
    [Range(1, 10)]
    [SerializeField] float speedDuration = 5;
    float defaultSpeed;

    PlayerCharacter playerCharacter;
    BoxCollider2D col;
    SpriteRenderer rend;
    bool startCountdown = false;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (startCountdown)
        {
            speedDuration -= 1 * Time.deltaTime;
            if(speedDuration <= 0)
            {
                playerCharacter.maxSpeed = defaultSpeed;
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rend.enabled = false;
            col.enabled = false;
            playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            defaultSpeed = playerCharacter.maxSpeed;
            playerCharacter.maxSpeed += speedIncrease;
            startCountdown = true;
        }
    }
}
