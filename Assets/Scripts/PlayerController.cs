using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 12f;

    public float spriteWidth = 1;

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
	}
}
