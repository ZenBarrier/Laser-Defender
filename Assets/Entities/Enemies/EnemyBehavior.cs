using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public float health = 150f;
    public float attackSpeed = 10f;
    public GameObject attack;
    public float shotsPerSecond = 0.5f;

	void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.GetComponent<Projectile>();
        if (missile && missile.tag != "Enemy")
        {
            health -= missile.GetDamage();
            missile.hit();
            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        float probability = shotsPerSecond * Time.deltaTime;
        if (Random.value < probability)
        {
            FireAttack();
        }
    }

    void FireAttack()
    {
        GameObject fire = Instantiate(attack, this.transform.position, Quaternion.identity) as GameObject;
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -attackSpeed);
    }
}
