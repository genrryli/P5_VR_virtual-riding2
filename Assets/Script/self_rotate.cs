using UnityEngine;
using System.Collections;

public class self_rotate : MonoBehaviour {

    public Vector3 direction;
    public float duration;
    public float speed;
    public Space space_;
    public bool loop;

    private float timer;
    private float angle;

    // Use this for initialization
    void Start () {
        if (duration > 9999) { duration = Mathf.Infinity; }
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer < duration)
        {
            angle = speed * timer;
            transform.Rotate(direction * speed * Time.deltaTime, space_);
        }
        else if (!loop)
        {
            transform.Translate(-direction * angle);
            timer = 0;
        }
        else if (timer < 2 * duration)
        {
            transform.Rotate(-direction * speed * Time.deltaTime, space_);
        }
        else { timer = 0; }
    }
}
