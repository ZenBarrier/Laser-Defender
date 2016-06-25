using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 2f;
    public float spawnDelay = 0.5f;

    Vector3 direction;
    private float minX, maxX;
    private float leftEnemyWidth, rightEnemyWidth;
    private float leftMost, rightMost;
    private bool move = false;

	// Use this for initialization
	void Start ()
    {
        SpawnEnemies();
        move = true;
        direction = new Vector3(-1, 0, 0);
        leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        GetBounds();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            MoveFormation();
        }
        if (IsFormationEmpty())
        {
            Debug.Log("all dead");
            SpawnUntilFull();
        }
        else
        {
            GetBounds();
        }
    }

    void GetBounds()
    {
        minX = 100f;
        maxX = -minX;
        foreach (Transform child in transform)
        {
            if(child.childCount > 0)
            {
                if(minX > child.transform.localPosition.x)
                {
                    minX = child.transform.localPosition.x;
                    leftEnemyWidth = child.GetComponentInChildren<BoxCollider2D>().size.x;
                }
                if (maxX < child.transform.localPosition.x)
                {
                    maxX = child.transform.localPosition.x;
                    rightEnemyWidth = child.GetComponentInChildren<BoxCollider2D>().size.x;
                }
            }
        }
        minX = leftMost - minX + leftEnemyWidth/2;
        maxX = rightMost - maxX - rightEnemyWidth/2;
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child.transform;
        }
    }

    void SpawnUntilFull()
    {
        Transform nextFreePosition = NextFreePosition();
        if (nextFreePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextFreePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = nextFreePosition.transform;
            
        }
        if (nextFreePosition)
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
        else
        {
            move = true;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector3(width, height));
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

    Transform NextFreePosition()
    {
        foreach (Transform child in this.transform)
        {
            if (child.childCount == 0)
            {
                return child;
            }
        }
        return null;
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
        move = false;
        transform.position = new Vector2(0, transform.position.y);
        return true;
    }
}
