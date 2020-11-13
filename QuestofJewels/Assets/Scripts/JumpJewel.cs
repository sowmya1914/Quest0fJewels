using UnityEngine;
using Gamekit2D;

public class JumpJewel : MonoBehaviour
{
    #region Variables

    [Range(1, 10)]
    [SerializeField] float jumpIncrease = 3;
    [Range(1, 10)]
    [SerializeField] float jumpBoostDuration = 5;

    float defaultJumpSpeed;
    PlayerCharacter playerCharacter;
    BoxCollider2D col;
    SpriteRenderer rend;
    Animator anim;
    bool startCountdown = false;

    #endregion

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
            gameObject.SetActive(false);
            rend.enabled = false;
            col.enabled = false;
            playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            defaultJumpSpeed = playerCharacter.jumpSpeed;
            playerCharacter.jumpSpeed += jumpIncrease;
            startCountdown = true;
        }
    }
}