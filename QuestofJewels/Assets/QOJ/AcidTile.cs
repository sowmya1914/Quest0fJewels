using UnityEngine;
using Gamekit2D;

public class AcidTile : Damager
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if((hittableLayers & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            Damageable damageable = other.GetComponent<Damageable>();

            if (damageable)
            {
                pos = getPos(other);
                OnDamageableHit.Invoke(this, damageable);
                damageable.TakeDamage(this, ignoreInvincibility);
                if (disableDamageAfterHit)
                    DisableDamage();
            }
            else
            {
                OnNonDamageableHit.Invoke(this);
            }
        }   
    }

    Vector3 getPos(Collider2D other)
    {
        bool flipx = other.gameObject.GetComponent<SpriteRenderer>().flipX;
        if (flipx)
            return new Vector3(-1000f, 0);
        else
            return new Vector3(1000, 0);
    }
}
