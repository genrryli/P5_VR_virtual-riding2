using UnityEngine;
using System.Collections;

public class adder : MonoBehaviour {

    public enum add { chacker,boomer,defencer,propellper,crusher,attracter,silder,random }
    public add add_stuff;
    public AudioClip collid_shound;
    public GameObject UI_get_waepon;

    private bool right_weapon_actived;
    private bool left_weapon_actived;
    private string weapon_last_name;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider col)
    {
        int stuff = (int)add_stuff;
        if (add_stuff == add.random) { stuff = Random.Range(0, 7); }

        if (col.gameObject.tag == "com" || col.gameObject.tag == "Player")
        {
            //判定Player上的Weapon是否有打开
            foreach (Transform child in col.transform.FindChild("weapons"))
            {
                if (child.gameObject.activeSelf)
                {
                    if (child.tag == "right_weapon") { right_weapon_actived = true; }
                    if (child.tag == "left_weapon") { left_weapon_actived = true; }
                }
            }

            //若全部打开则不传送数据，若右边Weapon已经打开则传送打开左边的Weapon，其他情况均打开右边的Weapon
            if (right_weapon_actived && left_weapon_actived) { return; }
            else if (right_weapon_actived) { weapon_last_name = " (1)"; }
            else { weapon_last_name = ""; }

            //执行数据传送
            add_weapon(col.transform.name, stuff, weapon_last_name);
            AudioSource.PlayClipAtPoint(collid_shound, transform.position);

            //销毁adder
            //Destroy(gameObject, 2f);

            //重置
            right_weapon_actived = false;
            left_weapon_actived = false;
        }
    }

    public void add_weapon(string name, int stuff, string weapon_num)//执行添加Weapon的函数
    {
        GameObject com = GameObject.Find(name);
        switch (stuff)
        {
            case 0:
                com.transform.FindChild("weapons").FindChild("chacker" + weapon_num).gameObject.SetActive(true);
                break;
            case 1:
                com.transform.FindChild("weapons").FindChild("boomer" + weapon_num).gameObject.SetActive(true);
                break;
            case 2:
                com.transform.FindChild("weapons").FindChild("defencer" + weapon_num).gameObject.SetActive(true);
                break;
            case 3:
                com.transform.FindChild("weapons").FindChild("propellper" + weapon_num).gameObject.SetActive(true);
                break;
            case 4:
                com.transform.FindChild("weapons").FindChild("crusher" + weapon_num).gameObject.SetActive(true);
                break;
            case 5:
                com.transform.FindChild("weapons").FindChild("attracter" + weapon_num).gameObject.SetActive(true);
                break;
            case 6:
                com.transform.FindChild("weapons").FindChild("silder" + weapon_num).gameObject.SetActive(true);
                break;
        }
        if (com.tag == "Player")
        {
            UI_get_waepon.SetActive(true);
            UI_get_waepon.GetComponent<get_weapon>().change_icon(stuff);
        }
    }
}
