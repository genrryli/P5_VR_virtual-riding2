using UnityEngine;
using System.Collections;

public class suffer_defencer : MonoBehaviour {

    public GameObject boom;
    public float duration=10;
    private  float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider Trigger)
    {
        string[] name_list = Trigger.name.Split('_');//分析武器来自哪个shooter
        string shooter_name = name_list[0];
        if (shooter_name == transform.root.name) { return; }//若shooter为自己则不触发碰撞
        else if (Trigger.tag == "attack")
        {
            Destroy(Trigger.gameObject);
            Instantiate(boom, Trigger.transform.position,Trigger.transform.rotation);
        }
    }

    public float _timer { set { timer = value; } }
}
