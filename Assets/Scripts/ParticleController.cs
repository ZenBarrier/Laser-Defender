using UnityEngine;
using System.Collections;

public class ParticleController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (!(this.GetComponent<ParticleSystem>().IsAlive()))
        {
            Destroy(this.gameObject);
        }
    }
}
