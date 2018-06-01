using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_finish_data : MonoBehaviour {

    public data_manager data;
    public Text max_speed;
    public Text avg_speed;
    public GameObject rank_all;
    public GameObject rank;
    public Text all_user_list;
    public Text all_time_list;
    public Text best_time;
    public Text score;
    public GameObject score_circle;

    public
    // Use this for initialization
    void Start()
    {
        if (data == null) { data = GameObject.Find("managers").GetComponent<data_manager>(); }

        //最高速度
        max_speed.text = data._max_speed.ToString("#0.00");//保留2位小数

        //平均速度
        avg_speed.text = (data._move_distance / data._time_cost/3).ToString("#0.00");//3为bike_con脚本的riding_scale

        //参数者的个数
        rank_all.GetComponent<Image>().fillAmount = ((float)data._all_rank) / 6.0f;

        //玩家的排名
        float x = rank.transform.localPosition.x;
        float y = rank.transform.localPosition.y + 38 * (data._rank - 1);
        float z = rank.transform.localPosition.z;
        rank.transform.localPosition = new Vector3(x, y, z);
        
        //用户名和对应时间的排序
        string[] user_list = new string[data._all_rank];
        string[] time_lsit = new string[data._all_rank];
        all_user_list.text = "";
        all_time_list.text = "";
        for (int i=0;i<user_list.Length; i++)
        {
            if (i + 1 == data._rank)
            {
                user_list[i] = "player"+"\n";
                time_lsit[i]= ((int)(data._time_cost / 60)).ToString("D2") + ":" + ((int)(data._time_cost % 60)).ToString("D2") + ":" + ((int)((data._time_cost - (int)data._time_cost) * 100)).ToString("D2")+"\n";
            }
            else
            {
                user_list[i] = "com"+ "\n";
                time_lsit[i] = "null" + "\n";
            }
            all_user_list.text += user_list[i];
            all_time_list.text += time_lsit[i];
        } 

        //最快时间
        best_time.text= ((int)(data._best_time_cost / 60)).ToString("D2") + ":" + ((int)(data._best_time_cost % 60)).ToString("D2") + ":" + ((int)((data._best_time_cost - (int)data._best_time_cost) * 100)).ToString("D2");

        //评分=排名*50%+平均速度/最高速度*50%
        float _score = data._all_rank / data._rank * 0.5f + data._move_distance / data._time_cost / data._max_speed*0.5f;
        score.text = _score.ToString("#0.0");//保留一位小数
        score_circle.GetComponent<Image>().fillAmount = _score/10;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
