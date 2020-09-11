using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;
public class AnimatedDamage : MonoBehaviour
{
    public Damager damager;
    public bool damageEnabled = false;
    private void Start()
    {
        if (damager == null)
        {
            damager = GetComponent<Damager>();
        }
    }
    public void EnableDamage()
    {
        if(damager != null)
        {
            damager.enabled = true;
        }
    }

    public void DisableDamage()
    {
        if (damager != null)
        {
            damager.enabled = false;
        }
    }
}
