using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class weapon_chacker : MonoBehaviour {

    public GameObject Item;
    public Transform StartLocation;
    public AudioClip rocket_s;
    public GameObject aimpoint;

    private bool ready=false;
    private string big_button_data;
    private GameObject InstanceItem;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // 以gameobject所在位置为起点，创建一条向前发射的射线  
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int layerMask = 1 << 11;//定义射线需要忽略的图层，11为图层的编号，可在设置里查看
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask))
        {
            // 如果射线与平面碰撞，打印碰撞物体信息  
            Debug.Log("碰撞对象: " + hit.collider.name);
            // 在场景视图中绘制射线  
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }

        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on"||Input.GetButtonDown("Fire1") ) { ready = true;}
        if (transform.root.tag == "Player") { aimpoint.SetActive(ready); }

        if (Input.GetButtonUp("Fire1") || transform.parent.root.tag == "com" || (big_button_data == "off" && ready))
        {
            ready = false;
            InstanceItem = Instantiate(Item, StartLocation.position, StartLocation.rotation) as GameObject;

            if (hit.collider == null) { InstanceItem.name = transform.parent.root.name + "_null"; }
            else if (hit.collider.tag == "com"|| hit.collider.tag == "Player") { InstanceItem.name = transform.parent.root.name + "_" + hit.collider.name; }
            else { InstanceItem.name = transform.parent.root.name + "_position_"+hit.point.x+"_" + hit.point.y + "_" + hit.point.z; }

            AudioSource.PlayClipAtPoint(rocket_s, StartLocation.position, 1);
            if (transform.root.tag == "Player") { aimpoint.SetActive(false); }
            gameObject.SetActive(false);
        }
    }
}
