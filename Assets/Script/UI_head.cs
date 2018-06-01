using UnityEngine;
using System.Collections;

public class UI_head : MonoBehaviour {

    public GameObject[] weapon_1;
    public GameObject[] waapon_2;
    public GameObject aimpoint_1;
    public GameObject aimpoint_2;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    for(int i = 1; i < weapon_1.Length; i++)
        {
            if (weapon_1[i].activeSelf == true) { aimpoint_1.SetActive(true);return; }
        }
        for (int i = 1; i < waapon_2.Length; i++)
        {
            if (waapon_2[i].activeSelf == true) { aimpoint_2.SetActive(true); return; }
        }
    }
}
