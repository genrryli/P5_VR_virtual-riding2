using UnityEngine;
using System.Collections;

public class weapon_boomer_child : MonoBehaviour {

    public float hit_target_force;
    public GameObject boom;
    public AudioClip shound;

    private float timer=0;

	// Use this for initialization
	void Start () {
        AudioSource.PlayClipAtPoint(shound, transform.position, 1000);
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 1.5)
        {
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(gameObject);            
        }
	}

    void OnCollisionEnter(Collision target)
    {
        if (target.collider.name == transform.name+"_") { return; }//防止误中shooter
        if (target.gameObject.tag == "com" || target.gameObject.tag == "Player")
        {
            Rigidbody ri = target.gameObject.GetComponent<Rigidbody>();
            ri.AddForce(transform.forward * -hit_target_force);
            GameObject InstanceItem = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
        if (target.gameObject.tag == ("block"))
        {
            GameObject InstanceItem = Instantiate(boom, transform.position, transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }
}
