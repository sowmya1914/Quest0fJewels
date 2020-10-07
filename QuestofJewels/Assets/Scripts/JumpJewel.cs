using UnityEngine;
using Gamekit2D;

public class JumpJewel : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] float jumpIncrease = 3;
    [Range(1, 10)]
    [SerializeField] float jumpBoostDuration = 5;

    float defaultJumpSpeed;
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
            jumpBoostDuration -= 1 * Time.deltaTime;
            if (jumpBoostDuration <= 0)
            {
                playerCharacter.jumpSpeed = defaultJumpSpeed;
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rend.enabled = false;
            col.enabled = false;
            playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            defaultJumpSpeed = playerCharacter.jumpSpeed;
            playerCharacter.jumpSpeed += jumpIncrease;
            startCountdown = true;
        }
    }
}