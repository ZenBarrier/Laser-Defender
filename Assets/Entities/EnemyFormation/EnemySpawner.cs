using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 2f;

    Vector3 direction;
    private float minX, maxX;

	// Use this for initialization
	void Start ()
    {
        SpawnEnemies();
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        minX = leftMost.x + width/2;
        maxX = rightMost.x - width/2;
        direction = new Vector3(-1, 0, 0);
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child.transform;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height));
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveFormation();
        if (IsFormationEmpty())
        {
            Debug.Log("all dead");
            SpawnEnemies();
        }
	}

    void MoveFormation()
    {
        transform.position += direction * Time.deltaTime * speed;
        float bounded = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector2(bounded, transform.position.y);
        if (minX == bounded || maxX == bounded)
        {
            direction *= -1;
        }
    }

    bool IsFormationEmpty()
    {
        foreach(Transform child in this.transform)
        {
            if(child.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
