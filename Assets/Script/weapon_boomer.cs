using UnityEngine;
using System.Collections;

public class weapon_boomer : MonoBehaviour
{
    public float ThrustForce = 5000f;
    public Rigidbody Item;
    public Transform StartLocation;
    public int set_item_num=1;
    public GameObject aimpoint;

    private bool ready = false;
    private string big_button_data;
    private Rigidbody InstanceItem;
    private float timer=0;
    public int item_num;

    private int shoot_num = 0;
    private bool shooting=false;
    void Start()
    {
        item_num = set_item_num;
        if (StartLocation == null) { StartLocation = transform; }
    }

    void Update()
    {
        if (item_num <= 0)//先判定是否剩余子弹
        {
            timer += Time.deltaTime;
            if (timer >= 0.3f)
            {
                item_num = set_item_num;
                if (transform.root.tag == "Player") { aimpoint.SetActive(false); }
                shooting = false;
                shoot_num = 0;
                gameObject.SetActive(false);
            }
        }

        shoot();

        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on" || Input.GetButtonDown("Fire1")) { ready = true; }
        if (transform.root.tag == "Player") { aimpoint.SetActive(ready); }

        if (Input.GetButtonUp("Fire1") || transform.parent.root.tag == "com" || (big_button_data == "off" && ready))
        {
            if ( item_num <= 0){ return; }
            shooting = true;
            ready = false;
        }
    }

    void OnDisable()
    {
        ready = false;
        if (transform.root.tag == "Player") { aimpoint.SetActive(ready); }
    }

    void shoot()
    {
        if (shooting&&item_num>0)
        {
            shoot_num++;
        }
        if (shoot_num % 2 == 0 && shooting && item_num > 0)
        {
            item_num -= 1;
            InstanceItem = Instantiate(Item, StartLocation.position, StartLocation.rotation) as Rigidbody;
            InstanceItem.name = transform.parent.root.name + "_";
            InstanceItem.AddForce(StartLocation.forward * ThrustForce);
        }
    }
}