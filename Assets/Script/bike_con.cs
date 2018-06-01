using UnityEngine;
using System.Collections;

public class bike_con : MonoBehaviour {

    public float turning_scale=0.008f;
    public float riding_scale=3;
    public GameObject bike_head;
    public GameObject head_direction;
    public motion_data data;
    public bool take_control;
    public Transform r_center;

    private float speed;
    private float angle;
    private float _reversal = 1;
    private bool io;

    // Use this for initialization
    void Start()
    {
        speed = 0;
        angle = 0;
        if (gameObject.tag == "com") { return; }
        io = data.open_io;
    }

    void FixedUpdate()
    {
        if (gameObject.tag == "com") { motion(); return; }
        //龙头固定角度偏转
        Vector3 angle_dif = new Vector3(0, angle, 0) - bike_head.transform.localEulerAngles;
        bike_head.transform.RotateAround(r_center.position, r_center.up, angle_dif.y);
        Vector3 angle_dif_2 = new Vector3(0, angle, 0) - head_direction.transform.localEulerAngles;
        head_direction.transform.Rotate(0, angle_dif.y, 0);

        take_control = Input.GetKey(KeyCode.C);//由电脑来接管的开关

        if (io&&!take_control) { angle = data.real_angle; speed = data.real_speed; motion(); }
        else { motion_from_computer(); }
    }

    void Update()
    {
    }

    void motion()//驱动虚拟自行车
    {
        if (!(game_manager.gm.gamestate == game_manager.game_state.playing || game_manager.gm.gamestate == game_manager.game_state.warm_up)){ return; }
        //车体前进，以及前进速度
        gameObject.transform.Translate(0,0,1 * speed * riding_scale / 50 * _reversal);

        //车体转向，及转向速度
        float rotate_speed = angle * turning_scale * speed;
        if (speed > 0) { transform.Rotate(Vector3.up * rotate_speed ); }
    }

    void motion_from_computer()//使用电脑控制
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        speed = 5 * y;
        angle = 60 * x;
        motion();
    }

    public float virtual_speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float reversal
    {
        get { return _reversal; }
        set { _reversal = value; }
    }

    public float virtual_angle
    {
        get { return angle; }
        set { angle = value; }
    }

}
