using UnityEngine;
using System.Collections;

public class weapon_chrusher : MonoBehaviour {

    public GameObject Item;
    public GameObject aimpoint;

    private bool ready = false;
    private string big_button_data;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on" || Input.GetButtonDown("Fire1")) { ready = true; }
        if (transform.root.tag == "Player") { aimpoint.SetActive(ready); }

        if (Input.GetButtonUp("Fire1") || transform.parent.root.tag == "com" || (big_button_data == "off" && ready))
        {
            ready = false;
            GameObject go= Instantiate(Item, transform.position, transform.rotation)as GameObject;
            go.name = transform.parent.root.name + "_";
            if (transform.root.tag == "Player") { aimpoint.SetActive(false); }
            gameObject.SetActive(false);
        }
    }
}
