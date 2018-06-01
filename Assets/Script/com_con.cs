using UnityEngine;
using System.Collections;

public class com_con : MonoBehaviour {

    public bike_con bike;
    public road_manager road_data;
    public float speed_add;

    private Rigidbody ridg;
    private float timer;
    private float movement;
    private int player_rank;
    private float original_speed;
	// Use this for initialization
	void Start () {
        ridg = gameObject.GetComponent<Rigidbody>();
        original_speed = speed_add;
	}
	
	// Update is called once per frame
	void Update () {
        player_rank = GameObject.Find("managers").GetComponent<data_manager>()._rank;
        if (game_manager.gm.gamestate == game_manager.game_state.playing)
        {
            //控制车辆向前走
            bike.virtual_speed = speed_add;
            //控制车辆的方向
            gameObject.transform.rotation = road_data._forward_direction.transform.rotation;
            //检测车辆与道路中心的距离
            float out_way_dis = Vector3.Distance(transform.position, road_data._forward_direction.transform.position);
            //若距离大于8则向其添加相反的力
            if (out_way_dis > 8)
            {
                Vector3 force_direction =  road_data._forward_direction.transform.position - transform.position;
                ridg.AddForce(force_direction * out_way_dis * 40);
            }
            //若距离小于8则让车辆做小幅度的横向移动
            if (out_way_dis < 8)
            {
                transform.Translate(transform.right * movement);
            }
            if (timer < Random.Range(1,3)){timer = timer + Time.deltaTime;}
            else { timer = 0; movement = Random.Range(-0.15f, 0.15f); }

            catch_player();
        }
    }

    void catch_player()
    {
        if (player_rank <= 2) { speed_add = 5; }
        else if(player_rank>3){ speed_add = Random.Range(1, 3); }
        else { speed_add = original_speed; }
    }
}
