using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float health = 150f;

	void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.hit();
            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
