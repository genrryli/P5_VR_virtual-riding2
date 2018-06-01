using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;//
using FluffyUnderware.DevTools;

public class road_manager : MonoBehaviour {

    public CurvySpline Spline; // spline轨迹对象  
    public Transform target; //计算偏离的物体，比如赛车  
    public float forward_distance;//在target前的距离
    public float backward_distance;//在target后的距离

    private bool out_way;
    private GameObject forward_direction;
    private GameObject backward_direction;
    private bool wrong_way;
    private float one_loop_distance;//单圈的路程
    private float current_loop_distance;//在单圈内所走的路程
    private float road_distance;//所走的全部路程
    private int loop_finished=-1;

    // Use this for initialization
    void Start () {
        if (!target) { target = transform.parent.root.transform; }
        forward_direction = new GameObject();
        backward_direction = new GameObject();
        loop_finished = -1;
    }
	
	// Update is called once per frame
	void Update () {
        one_loop_distance = Spline.Length;
        if (Spline && Spline.IsInitialized && target) //保证spline和target存在  
        {
            // 将target的坐标转换到spline所在的本地坐标系  
            Vector3 targetPos = Spline.transform.InverseTransformPoint(target.position);
            // 获得target在spline上的最近点的TF值,获得target前段和后段距离的点的TF值,TF值即可以通过DistanceToTF()函数互换  
            float nearest_TF = Spline.GetNearestPointTF(targetPos);
            float forward_TF = Spline.DistanceToTF(Spline.TFToDistance(nearest_TF) + forward_distance);
            float backward_TF = Spline.DistanceToTF(Spline.TFToDistance(nearest_TF) - backward_distance);
            // 转换到世界坐标系的spline上最近点的坐标值  
            Vector3 nearest_position = Spline.transform.TransformPoint(Spline.Interpolate(nearest_TF));
            Vector3 forward_position = Spline.transform.TransformPoint(Spline.Interpolate(forward_TF));
            Vector3 backward_position = Spline.transform.TransformPoint(Spline.Interpolate(backward_TF));
            Quaternion nearest_rotation = Spline.GetOrientationFast(nearest_TF);
            Quaternion forward_rotation = Spline.GetOrientationFast(nearest_TF);
            Quaternion backward_rotation = Spline.GetOrientationFast(nearest_TF);

            //判定Target是够超出spline中心20米
            float out_way_distance = Vector3.Distance(nearest_position, target.position);
            if (out_way_distance > 20) { out_way = true; Debug.Log("out of the way"); }
            else { out_way = false; }
            if (out_way)//重置target位置与方向
            {
                target.position = nearest_position+new Vector3(0,1.5f,0);
                target.rotation = forward_direction.transform.rotation; 
            }

            //定义target前一段和后一段位置的gameobject的transform
            forward_direction.transform.rotation = forward_rotation;
            forward_direction.transform.position = forward_position;
            backward_direction.transform.rotation = backward_rotation;
            backward_direction.transform.position = backward_position;

            //判定target方向是否与Spline的方向一致
            float angle = target.eulerAngles.y- forward_direction.transform.eulerAngles.y;
            if ((angle>120&&angle<240)||(angle<-120&&angle>-240)) { wrong_way = true; Debug.Log("wrong way"); }
            else { wrong_way = false; }

            //计算target在spline所走的距离
            current_loop_distance = Spline.TFToDistance(nearest_TF, CurvyClamping.Loop);
            road_distance = current_loop_distance + loop_finished * one_loop_distance;
        }
    }

    public void finish_one_loop()
    {
       loop_finished += 1;
    }

    public bool _out_way { get { return out_way; } }
    public bool _wrong_way { get { return wrong_way; } }
    public float _one_loop_distance { get { return one_loop_distance; } }
    public float _current_loop_distance { get { return current_loop_distance; } }
    public float _road_distance { get { return road_distance; } }
    public GameObject _backward_direction { get { return backward_direction; } }
    public GameObject _forward_direction { get { return forward_direction; } }
    public int _loop_finished { get { return loop_finished; } }
}
