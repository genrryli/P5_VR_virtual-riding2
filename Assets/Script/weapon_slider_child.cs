using UnityEngine;
using System.Collections;
using System.Reflection;
using FluffyUnderware.Curvy;//

public class weapon_slider_child : MonoBehaviour {

    public GameObject slider_chlid_child;
    private CurvySpline Spline; // spline轨迹对象  
    public float speed;

    private Vector3 target_position;
    private Quaternion target_rotation;
    private Vector3 original_position;
    private string shooter_name;
    private string target_name;
    private float dis;
    private float time;
    private float dTime;
    private Vector3 v;
    private Vector3 Gravity;
    private Vector3 currentAngle;

    // Use this for initialization
    void Start () {
        original_position = transform.position;
        Spline = GameObject.Find("Curvy Spline1").GetComponent<CurvySpline>();

        //从gameobject的name上获取信息。分别是shooter名，target名和target坐标
        string[] name_list = gameObject.name.Split('_');
        shooter_name = name_list[0];
        target_name = name_list[1];

        //根据信息定义需要攻击的target的position
        if (target_name == "position") { target_position = new Vector3(float.Parse(name_list[2]), float.Parse(name_list[3]), float.Parse(name_list[4])); }

        //找出spline上的target的rotation
        Vector3 targetPos = Spline.transform.InverseTransformPoint(target_position);
        float nearest_TF = Spline.GetNearestPointTF(targetPos);
        target_rotation = Spline.GetOrientationFast(nearest_TF);

        //定义抛物线的初速度和重力加速度
        dis = Vector3.Distance(original_position, target_position);
        time = Mathf.Min( dis / speed,2f);
        v = new Vector3((target_position.x - original_position.x) / time, (target_position.y - original_position.y) / time - 0.5f * (-10.0f) * time, (target_position.z - original_position.z) / time);
        Gravity = Vector3.zero;
    }

    void FixedUpdate()
    {
        // v=gt
        Gravity.y = -10.0f * (dTime += Time.fixedDeltaTime);

        //模拟位移
        transform.position += (v + Gravity) * Time.fixedDeltaTime;

        // 弧度转度：Mathf.Rad2Deg
        currentAngle.x = -Mathf.Atan((v.y + Gravity.y) / v.z) * Mathf.Rad2Deg;

        // 设置当前角度
        transform.eulerAngles = currentAngle;
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.name == shooter_name) { return; }//防止误中shooter
        if (target.gameObject.tag == "road")
        {
            GameObject InstanceItem = Instantiate(slider_chlid_child, target_position, target_rotation) as GameObject;////
            InstanceItem.name = shooter_name + "_";
            Destroy(gameObject);
        }
    }
}
