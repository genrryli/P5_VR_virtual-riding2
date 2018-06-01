using UnityEngine;
using System.Collections;

public class weapon_propellper : MonoBehaviour
{
    public AudioClip speed_;
    public GameObject speed;
    public float duration=1;
    public float force=200000;
    public hover _hover;

    private Rigidbody rigid;
    private bool add;
    private float timer;
    private Vector3 original_posi;
    // Use this for initialization
    void Start()
    {
        rigid = transform.parent.root.gameObject.GetComponent<Rigidbody>();
        speed.SetActive(false);
        original_posi = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        string big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on"|| Input.GetButtonUp("Fire1") || transform.parent.root.tag == "com") { add_speed(); }

        timer += Time.deltaTime;
        if (add && timer <= duration)
        {
            rigid.AddForce(rigid.transform.forward * force);
            speed.SetActive(true);
            if (timer < duration / 2) { _hover.hoverHeight = 1.6f; }
            else { _hover.hoverHeight = 1f; }
        }
        if (add && timer > duration)
        {
            add = false;speed.SetActive(false);
            _hover.hoverHeight = 1;
            gameObject.transform.localPosition = original_posi;
            gameObject.SetActive(false);
        }
    }

    void add_speed()
    {
        if (add) { return; }
        AudioSource.PlayClipAtPoint(speed_, transform.position, 1000);
        add = true;
        timer = 0;
    }

}