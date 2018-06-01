using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ui_finish : MonoBehaviour {

    public GameObject UI_finish_data;
    public GameObject layer_1;
    public GameObject layer_2;
    public GameObject layer_3;
    public GameObject layer_4;
    public Text layer_5;
    public float duration;

    private data_manager data;
    private int color_per_frame;
    private Color32 color = new Color32(255, 255, 255, 0);
    private float timer;

	// Use this for initialization
	void Start () {
        color.a = 0;
        data = GameObject.Find("managers").GetComponent<data_manager>();
        switch (data._rank)
        {
            case 1:
                layer_5.text = "第一名！";
            break;
            case 2:
                layer_5.text = "第二名！";
                break;
            case 3:
                layer_5.text = "第三名！";
                break;
            default:
                layer_5.text = "失败！";
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer < duration)
        {
            color_per_frame += 6;//每帧增加6透明度
            color_per_frame = Mathf.Min(color_per_frame, 255);
        }
        else
        {
            color_per_frame -= 6;//每帧增加6透明度
            color_per_frame = Mathf.Max(color_per_frame, 0);
        }

        //byte[] a = System.BitConverter.GetBytes(color_per_frame);
        color.a = (byte)color_per_frame;
        layer_1.GetComponent<Image>().color = color;
        layer_2.GetComponent<Image>().color = color;
        layer_3.GetComponent<Image>().color = color;
        layer_4.GetComponent<Image>().color = color;
        layer_5.color = color;

        if (color_per_frame == 0)
        {
            game_manager.gm.gamestate = game_manager.game_state.finish_data;
            gameObject.SetActive(false); timer = 0;
            UI_finish_data.SetActive(true);
        }
    }
}
