using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class data_manager : MonoBehaviour
{
    [Header("data_source")]
    public GameObject player;

    [Header("setting")]
    public int max_loop=2;
    public float max_angle = 6;
    public float max_slide_angle = 3;

    private int rank = 1;
    private int all_rank;
    private float time_loger = 0;
    private float time_cost;
    private float best_time_cost;
    private float virtual_bike_speed;
    private float max_virtual_bike_speed;
    private float move_distance;
    private Vector3 start_location;
    private Vector3 late_location;
    private float all_loop_distance;//一共要走的全部路程
    private float road_distance;//所走的全部路程
    public int loop_finished;
    private float angle_forward;
    private float angle_right;
    private float bike_angle;

    // Use this for initialization
    void Start()
    {
        if (!player) { player = GameObject.FindWithTag("Player"); }
        start_location = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        read_rank_data();
        read_time_data();
        read_speed();
        read_move_distance();
        read_road_distance();
        read_current_loop();
        read_angle_data();
    }
    void read_rank_data()
    {
        all_rank = GameObject.FindGameObjectsWithTag("Player").Length + GameObject.FindGameObjectsWithTag("com").Length;
        rank = read_rank_data(player);
    }

    public int read_rank_data(GameObject target)//-------------------------------------------------------------------------------------------
    {
        int target_rank = 1;//初始化int

        //获取所有player和com的gameobject
        GameObject[] com_list = GameObject.FindGameObjectsWithTag("com");
        GameObject[] player_list = GameObject.FindGameObjectsWithTag("Player");

        //将所有gameobject的路程收进列表
        List<float> dis_list = new List<float>();
        foreach (GameObject each_com in com_list)
        {
            float each_com_dis = read_road_distance(each_com);//计算每个com的路程
            dis_list.Add(each_com_dis);//添加到列表
        }
        foreach (GameObject each_com in player_list)
        {
            float each_com_dis = read_road_distance(each_com);
            dis_list.Add(each_com_dis);
        }

        //计算target的路程
        float target_dis = read_road_distance(target);

        //将列表里的路程降序排列
        dis_list.Sort((x, y) => -x.CompareTo(y));

        //比较target的路程与列表里的路程，若与某一个相等，即target排名等于路程排名+1
        for (int i = 0; i < dis_list.Count; i++)
        {
            if (dis_list[i] == target_dis) {  target_rank = i + 1; }
        }
        return target_rank;
    }

    void read_time_data()//-------------------------------------------------------------------------------------------
    {
        time_loger += Time.deltaTime;
        if (game_manager.gm.gamestate == game_manager.game_state.playing)
        {
            time_cost = time_loger;
        }
        else { time_cost = time_cost; time_loger = 0; }

        if (game_manager.gm.gamestate == game_manager.game_state.finish)
        {
            if (PlayerPrefs.GetFloat("best_time_cost"+player.name) <= time_cost && PlayerPrefs.GetFloat("best_time_cost"+player.name)>0) { return; }
            PlayerPrefs.SetFloat("best_time_cost"+player.name, time_cost);
        }
        best_time_cost = PlayerPrefs.GetFloat("best_time_cost"+player.name);
    }

    void read_speed()//-----------------------------------------------------------------------------------------------
    {
        virtual_bike_speed = player.GetComponent<bike_con>().virtual_speed;
        if (PlayerPrefs.GetFloat("max_virtual_bike_speed"+player.name) > virtual_bike_speed) { return; }
        PlayerPrefs.SetFloat("max_virtual_bike_speed"+player.name, virtual_bike_speed);
        max_virtual_bike_speed = PlayerPrefs.GetFloat("max_virtual_bike_speed"+player.name);
    }

    void read_move_distance()//--------------------------------------------------------------------------------------------
    {
        late_location = player.transform.position;//更新位置
        float S = Vector3.Distance(start_location, late_location);//计算距离
        start_location = late_location;
        move_distance = move_distance + S;             
    }

    void read_road_distance()//----------------------------------------------------------------------------------------
    {
        road_distance = read_road_distance(player);
        all_loop_distance = player.GetComponent<road_manager>()._one_loop_distance*max_loop;
    }

    float read_road_distance(GameObject target)
    {
        float road_distance = 0;
        road_distance = target.GetComponent<road_manager>()._road_distance; 
        return road_distance;
    }

    void read_current_loop()//------------------------------------------------------------------------------------------------
    {       
        loop_finished= player.GetComponent<road_manager>()._loop_finished;
    }

    void read_angle_data()//-------------------------------------------------------------------------------------------车辆自身的侧倾
    {
        angle_forward = player.transform.eulerAngles.x; if (angle_forward > 180) { angle_forward = angle_forward - 360; }
        if (angle_forward > max_angle-1) { angle_forward = max_angle-1; }else if (angle_forward < -max_angle) { angle_forward = -max_angle; }

        if (angle_out() > max_slide_angle) { angle_right = max_slide_angle-1; }
        else if  (angle_out() < -max_slide_angle) { angle_right = -max_slide_angle; }
        else { angle_right = angle_out(); }

        //angle_right = player.transform.eulerAngles.z; if (angle_right > 180) { angle_right = angle_right - 360; }
        //if ((Mathf.Abs(angle_right) - Mathf.Abs(angle_out())) <= 0) { angle_right = angle_out(); }
    }

    public float angle_out()//------------------------------------------------------------------------------------------基于车把转角的侧倾
    {
        float head_angle = player.GetComponent<bike_con>().virtual_angle;
        if (head_angle <= 0.5 && head_angle >= -0.5) { bike_angle = 0; }
        else { bike_angle = head_angle / 3; }//线性侧倾
        //非线性侧倾
        //float speed = player.GetComponent<bike_con>().virtual_speed / 3;
        //else { bike_angle = Mathf.Atan(2 * speed * speed * Mathf.Tan(head_angle / 2 / 180 * 3.14f) / 10) / 3.14f * 180; }
        return bike_angle;
    }

    void read_heat()
    {

    }

    public int _rank{get { return rank; }}
    public int _all_rank { get { return all_rank; } }
    public float _time_cost { get { return time_cost; } }
    public float _best_time_cost { get { return best_time_cost; } }
    public float _speed { get { return virtual_bike_speed; } }
    public float _max_speed { get { return max_virtual_bike_speed; } }
    public float _road_distance { get { return road_distance; } }
    public float _move_distance { get { return move_distance; } }
    public float _all_loop_distance { get { return all_loop_distance; } }
    public int _max_loop { get { return max_loop; } }
    public int _loop_finished { get { return loop_finished; } }
    public float _angle_forward { get { return angle_forward; } }
    public float _angle_right { get { return angle_right; } }

}