using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class get_weapon : MonoBehaviour {

    public Sprite[] icon_list;
    public GameObject icon;
    public Text icon_name;
    public float duration;
    public GameObject[] layer;
    public float speed;

    private string[] icon_name_list;
    private float timer;
    private float color_per_frame;
    private Color32 color = new Color32(255, 255, 255, 0);
    private Vector3[] original_position;

    void Start()
    {
    }  
	void OnEnable()
    {
        original_position = new Vector3[layer.Length];
        for (int i = 0; i < layer.Length; i++)
        {
            original_position[i] = layer[i].transform.localPosition;
        }

        icon_name_list = new string[7] { "CHACKER", "BOOMER", "DEFENCER", "PROPELLPER", "CRUSHER", "ATTACKER", "SLIDER" };
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        //改变透明度
        if (timer < duration)
        {
            color_per_frame += 20;//每帧增加20透明度
            color_per_frame = Mathf.Min(color_per_frame, 255);
        }
        else
        {
            color_per_frame -= 10;//每帧增加-10透明度
            color_per_frame = Mathf.Max(color_per_frame, 0);
        }
        color.a = (byte)color_per_frame;
        for (int i = 0; i < layer.Length; i++)
        {
            layer[i].GetComponent<Image>().color = color;
            //位移
            int ii = i + 1;
            layer[i].transform.Translate(Vector3.back * speed/ii* Time.deltaTime, Space.Self);
        }
        icon_name.color = color;

        //还原
        if (color_per_frame == 0)
        {
            for (int i = 0; i < layer.Length; i++) { layer[i].transform.localPosition = original_position[i]; }
            gameObject.SetActive(false);
            timer = 0;
        }
    }

    public void change_icon(int name)
    {
        icon.GetComponent<Image>().sprite = icon_list[name];
        icon_name.text = "GET A " + icon_name_list[name];
    }

}
