using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float damage = 100f;

    public void hit()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
