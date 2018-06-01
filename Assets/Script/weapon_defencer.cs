using UnityEngine;
using System.Collections;

public class weapon_defencer : MonoBehaviour {

    private string big_button_data;
    private bool ready;
    public  AudioClip shound;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on") { ready = true; }

        if (Input.GetButtonDown("Fire1") || transform.parent.root.tag == "com" || (big_button_data == "off" && ready))
        {
            ready = false;
            transform.parent.root.FindChild("suffers").FindChild("suffer_defencer").gameObject.SetActive(true);
            transform.parent.root.FindChild("suffers").FindChild("suffer_defencer").gameObject.GetComponent<suffer_defencer>()._timer = 0;
            AudioSource.PlayClipAtPoint(shound, transform.position, 1);
            gameObject.SetActive(false);
        }
    }
}
