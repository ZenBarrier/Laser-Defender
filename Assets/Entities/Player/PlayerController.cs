using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 12f;

    public float spriteWidth = 1f;
    public GameObject attack;
    public float attackSpeed = 10f;
    public float firingRate = 0.3f;
    public float health = 150f;
    public int lives = 1;
    public GameObject damageEffect;

    float minX;
    float maxX;

    // Use this for initialization
    void Start () {
        Vector2 leftMost = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 rightMost = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

        minX = leftMost.x + spriteWidth/2;
        maxX = rightMost.x - spriteWidth/2;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float bounded = Mathf.Clamp(this.transform.position.x, minX, maxX);

        this.transform.position = new Vector2(bounded, this.transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("FireAttack", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("FireAttack");
        }
	}

    void FireAttack()
    {
        GameObject fire = Instantiate(attack, this.transform.position, Quaternion.identity) as GameObject;
        fire.GetComponent<Rigidbody2D>().velocity = new Vector2(0, attackSpeed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile missile = col.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.hit();
            Instantiate(damageEffect, transform.position, Quaternion.identity);
            if (health <= 0f)
            {
                Destroy(gameObject);
                lives--;
                if (lives <= 0)
                {
                    LevelManager.LoadNextLevel();
                }
            }
        }
    }
}
