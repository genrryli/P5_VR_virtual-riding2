using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class count_start_time : MonoBehaviour {

    public Text time;
    public AudioClip c1;
    public AudioClip c2;
    public GameObject[] child;

    private int timer = 4;
    private Animator ani;
	// Use this for initialization
	void Awake () {
        InvokeRepeating("time_counter",0,1);
        ani = gameObject.GetComponent<Animator>();
        for (int  i = 0;i< child.Length; i++)
        {
            child[i].SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
        time.text = timer + "";
        if (timer == 0) { time.text = "GO!";  }
        if (timer < 0) { gameObject.SetActive(false); }
    }

    void time_counter()
    {
        if (gameObject.active)
        {
            timer--;
            if (timer > 0) { GetComponent<AudioSource>().PlayOneShot(c1); } else { GetComponent<AudioSource>().PlayOneShot(c2); }
            ani.SetTrigger("count");
        }
    }
}
