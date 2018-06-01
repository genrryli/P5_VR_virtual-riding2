using UnityEngine;
using System.Collections;

public class lookat : MonoBehaviour {

    public Transform target;

    private data_manager data;
    private float timer;
    private int randum_target;
    private GameObject[] com_list;

    void Start()
    {
        if (transform.root.tag == "com")
        {
            //获取所有player和com的gameobject
            com_list = GameObject.FindGameObjectsWithTag("com");
            randum_target = Random.Range(0, com_list.Length);
            target = com_list[randum_target].transform;
        }
    }

    void Update()
    {
        if (transform.root.tag == "com")
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                randum_target = Random.Range(0, com_list.Length);
                target = com_list[randum_target].transform;
                timer = 0;
            }
            if (target.name == transform.root.name)
            {
                target = GameObject.Find("player").transform;
            }
        }
        transform.LookAt(target);
    }
}
