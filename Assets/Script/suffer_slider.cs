using UnityEngine;
using System.Collections;

public class suffer_slider : MonoBehaviour {

    public float duration = 10.0f;
    private float timer;
    private float timer2;
    void Start () {	
	}
	
	void Update () {
        timer += Time.deltaTime;
        if (timer > duration) { Destroy(gameObject); }
	}

    void OnTriggerEnter(Collider target)
    {
        if (target.name + "_" == gameObject.name) { return; }
        if (target.gameObject.tag == "com" || target.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<bike_con>().reversal = 0.1f;
        }
    }

    void OnTriggerStay(Collider target)//当gameobject进入时，持续时间停止计算。当gameobject走出时，才结算
    {
        if (target.name + "_" == gameObject.name) { return; }
        timer -= Time.deltaTime;//抵消停留的时间
        timer2 += Time.deltaTime;//记录被抵消的时间，用于结算最终的时间
    }

    void OnTriggerExit(Collider target)
    {
        if (target.name + "_" == gameObject.name) { return; }
        timer += timer2;//结算最终时间
        if (target.gameObject.tag == "com" || target.gameObject.tag == "Player")
        {
            target.gameObject.GetComponent<bike_con>().reversal = 1f;
        }
    }
}
