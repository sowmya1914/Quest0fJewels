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
    private void Start()
    {
        thePlayerCharacter = GameObject.FindObjectOfType<PlayerCharacter>();
        myRenderer = GetComponent<SpriteRenderer>();

        if (myRenderer != null)
        {
            if (portalToActivate != null)
            {
                if (offSprite != null && onSprite != null)
                {
                    if (portalToActivate.IS_PORTAL_OPEN == true)
                    {
                        myRenderer.sprite = onSprite;
                    }
                    else
                    {
                        myRenderer.sprite = offSprite;
                    }
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (thePlayerCharacter != null)
        {
            if (collision.collider.GetComponent<PlayerCharacter>())
            {
                if (portalToActivate != null)
                {
                    portalToActivate.IS_PORTAL_OPEN = !portalToActivate.IS_PORTAL_OPEN;

                    if (myRenderer != null)
                    {

                        if (offSprite != null && onSprite != null)
                        {
                            if (portalToActivate.IS_PORTAL_OPEN == true)
                            {
                                myRenderer.sprite = onSprite;
                            }
                            else
                            {
                                myRenderer.sprite = offSprite;
                            }
                        }

                    }
                }
            }
        }
    }
}
