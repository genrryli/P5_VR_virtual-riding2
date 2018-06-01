using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_manager_playing : MonoBehaviour {
    [Header("数据")]
    public data_manager data;

    [Header("UI_Torso")]
    public Text rank;
    public Text all_rank;
    public GameObject speed;
    public GameObject distance;

    [Header("UI_finish")]
    public GameObject UI_finish;

    [Header("UI_wrong_way")]
    public GameObject UI_wrong_way;
    public road_manager road_date;

    //public Slider distance_percentage;
    //public GameObject message_panel;
    //public Text message;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        rank.text = "" + data._rank;
        all_rank.text = "/" + data._all_rank;
        //time_cost.text = ((int)(data._time_cost / 60)).ToString("D2") + ":" + ((int)(data._time_cost % 60)).ToString("D2") + ":" + ((int)((data._time_cost - (int)data._time_cost) * 100)).ToString("D2");
        speed.GetComponent<Image>().fillAmount = data._speed / data._max_speed * 0.75f;
        distance.GetComponent<Image>().fillAmount =  data._road_distance/data._all_loop_distance * 0.75f;

        if (game_manager.gm.gamestate == game_manager.game_state.finish)
        {
            UI_finish.SetActive(true);
        }

        if (game_manager.gm.gamestate == game_manager.game_state.playing)
        {
            UI_wrong_way.SetActive(road_date._wrong_way);
        }
    }

    //void close_message_display()
    //{
    //    message_panel.SetActive(false);
    //    message.text = null;
    //}

    //void message_wrong_way(bool x)
    //{
    //    if (x && is_wrong_way)
    //    {
    //        message_panel.SetActive(x);
    //        message.text = "WRONG WAY";
    //        is_wrong_way = false;
    //    }
    //    if (!x && !is_wrong_way)
    //    {
    //        close_message_display();
    //        is_wrong_way = true;
    //    }
    //}

    //public void message_on(bool x, string m)
    //{
    //    message_panel.SetActive(x);
    //    message.text = m;
    //    message_panel.GetComponent<Animation>().enabled = !x;
    //    message.GetComponent<Animation>().enabled = !x;
    //}
}
