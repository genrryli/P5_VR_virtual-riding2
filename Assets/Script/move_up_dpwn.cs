using UnityEngine;
using System.Collections;

public class move_up_dpwn : MonoBehaviour {

    public float speed;
    public float duration;
    public Vector3 direction;
    public Space space_;
    public bool loop;

    private float timer;
    private float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer < duration)
        {
            distance = speed * timer;
            transform.Translate(direction * speed * Time.deltaTime, space_);
        }
        else if(!loop)
        {
            transform.Translate(-direction * distance);
            timer = 0;
        }
        else if(timer<2*duration)
        {
            transform.Translate(-direction * speed * Time.deltaTime, space_);
        }
        else { timer = 0; }

    }
}
