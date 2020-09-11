using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public class PortalActivator : MonoBehaviour
{
    public PortalScript portalToActivate = null;
    private PlayerCharacter thePlayerCharacter = null;
    public Sprite offSprite = null;
    public Sprite onSprite = null;
    private SpriteRenderer myRenderer = null;

    private bool switchOn = false;
    private void Start()
    {
        thePlayerCharacter = GameObject.FindObjectOfType<PlayerCharacter>();
        myRenderer = GetComponent<SpriteRenderer>();
        portalToActivate.setPA(this);
        changeSprint();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (thePlayerCharacter != null)
        {
            if (collision.collider.GetComponent<PlayerCharacter>())
            {
                if (portalToActivate != null)
                {
                    switching(true);
                    portalToActivate.IS_PORTAL_OPEN = true;
                }
            }
        }
    }

    public void switching(bool b)
    {
        switchOn = b;
        changeSprint();
    }

    private void changeSprint()
    {
        if (myRenderer != null)
        {
            if (offSprite != null && onSprite != null)
            {
                myRenderer.sprite = switchOn ? onSprite : offSprite;
                return;
            }
        }
        Debug.LogError("Portal ERROR");
    }
}
