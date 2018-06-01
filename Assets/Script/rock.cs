using UnityEngine;
using System.Collections;

public class rock : MonoBehaviour {

    public GameObject rock_break;

	void Start () {	
	}
	
	void Update () {	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "road"|| collision.gameObject.tag == "ground") { return; }
        Instantiate(rock_break, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
