using UnityEngine;
using System.Collections;

public class weapon_attracter : MonoBehaviour {

    public GameObject aimpoint;

    private string big_button_data;
    private bool ready;
    public Transform target;
    private string target_name;
    private GameObject shooter;
    public float force;
    public float duration;
    private float timer;

    // Use this for initialization
    void Start () {
        //com = GameObject.FindGameObjectsWithTag("com");
        shooter = GameObject.Find(transform.parent.parent.name);
    }
	
	// Update is called once per frame
	void Update () {

        // 以gameobject所在位置为起点，创建一条向前发射的射线  
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layerMask = 1 << 11;//定义射线需要忽略的图层，11为图层的编号，可在设置里查看
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,~layerMask))
        {
            // 如果射线与平面碰撞，打印碰撞物体信息  
            Debug.Log("碰撞对象: " + hit.collider.name);
            // 在场景视图中绘制射线  
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }

        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on" || Input.GetButtonDown("Fire1")) { ready = true; }
        if (transform.root.tag == "Player") { aimpoint.SetActive(ready); }

        if (Input.GetButtonUp("Fire1") || transform.parent.root.tag == "com" || (big_button_data == "off" && ready))
        {
            ready = false;
            if (hit.collider == null) { return; }
            if ((hit.collider.tag == "com" || hit.collider.tag == "Player")&& hit.collider.tag != transform.root.name)
            { target_name = hit.collider.name;}
        }

        if ( target_name != null)
        {
            target = GameObject.Find(target_name).transform;
            gameObject.GetComponent<lookat>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = true;
            transform.LookAt(target);
            Vector3 force_direction = (target.position - shooter.transform.position);
            shooter.gameObject.GetComponent<Rigidbody>().AddForce(force_direction * force);
            target.gameObject.GetComponent<Rigidbody>().AddForce(force_direction * -force);

            timer += Time.deltaTime;
            if (timer > duration)
            {
                timer = 0;
                target_name = null;
                target = null;
                gameObject.GetComponent<lookat>().enabled = true;
                gameObject.GetComponent<AudioSource>().enabled = false;
                if (transform.root.tag == "Player") { aimpoint.SetActive(false); }
                gameObject.SetActive(false);
            }
        }
    }
}
