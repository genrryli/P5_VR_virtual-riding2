using UnityEngine;
using System.Collections;
using System.Reflection;

public class weapon_chacker_child : MonoBehaviour {

    public float hit_target_force;
    public GameObject boom;

    private float timer = 0;
    private Vector3 target_position;
    private string[] name_list;
    private string shooter_name;
    private string target_name;

	// Use this for initialization
	void Start () {
        //从gameobject的name上获取信息。分别是shooter名，target名
        name_list = gameObject.name.Split('_');
        shooter_name = name_list[0];
        target_name = name_list[1];
    }
	
	// Update is called once per frame
	void Update () {
        //当target名为position时，获取target坐标
        Vector3 position_name =new Vector3(0,0,0);
        if (target_name == "position") { position_name = new Vector3(float.Parse(name_list[2]), float.Parse(name_list[3]), float.Parse(name_list[4])); }

        //根据信息定义需要攻击的target的position
        if (target_name != "position" && target_name != "null") { target_position = GameObject.Find(target_name).transform.position; }
        else if (target_name == "position") { target_position = position_name; }

        //gameobject先在空中飞行0.5秒，再瞄准target。瞄准前速度为20，瞄准后为40，速度会随时间递增
        timer += Time.deltaTime;
        if (timer <= 0.5) { transform.Translate(0, 0, 20 * Time.deltaTime); }
        else
        {
            gameObject.GetComponent<hover>().enabled = true;//打开在空中飞行的悬浮效果
            if (target_position== new Vector3(0, 0, 0)) { transform.Translate(0, 0, 10 * Time.deltaTime*timer); return; }//当target为原始值时，则不追目标
            transform.LookAt(target_position);//当target为原始值时，则追目标
            transform.Translate(0, 0, 40 * Time.deltaTime*timer );            
            gameObject.GetComponent<hover>().enabled = false;
        }
        
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.transform.root.name == shooter_name) { return; }//防止误中shooter
        if (target.gameObject.tag == "com"|| target.gameObject.tag == "Player")
        {
            Rigidbody ri = target.gameObject.GetComponent<Rigidbody>();
            ri.AddForce(transform.forward * -hit_target_force);
        }
        GameObject InstanceItem = Instantiate(boom, transform.position, transform.rotation) as GameObject;
        Destroy(gameObject);
    }
}
