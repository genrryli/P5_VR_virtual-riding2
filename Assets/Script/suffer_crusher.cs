using UnityEngine;
using System.Collections;

public class suffer_crusher : MonoBehaviour {

    public float duration = 7;

    private float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;     
        if (transform.parent.parent.tag=="com"|| transform.parent.parent.tag == "Player")
        {
            transform.parent.root.gameObject.GetComponent<bike_con>().reversal = 0.1f;
            if (timer >= duration)
            {
                timer = 0; gameObject.SetActive(false);
                transform.parent.root.gameObject.GetComponent<bike_con>().reversal = 1;
            }
        }	
	}
}
