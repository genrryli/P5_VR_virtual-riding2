using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

public class UI_torso_con : MonoBehaviour {

    [SerializeField]
    private VRInteractiveItem m_InteractiveItem;

    public GameObject[] layer;
    public float[] distance;
    public float original_distance=-1.8f;
    public float dutation = 4;

    private bool is_hover;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (is_hover)
        {
            for (int i = 0; i < layer.Length; i++)
            {
                float z = layer[i].transform.localPosition.z;
                float y = layer[i].transform.localPosition.y;
                float x = layer[i].transform.localPosition.x;
                float speed = (distance[i] - z) / dutation;
                z = z + speed;
                layer[i].transform.localPosition = new Vector3( x , y , z );
            }
        }
        else
        {
            for (int i = 0; i < layer.Length; i++)
            {
                float z = layer[i].transform.localPosition.z;
                float y = layer[i].transform.localPosition.y;
                float x = layer[i].transform.localPosition.x;
                float speed = (original_distance - z) / dutation;
                z = z + speed;
                layer[i].transform.localPosition = new Vector3(x, y, z);
            }
        }
    }

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
    }

    private void HandleOut()//视焦离开时触发
    {
        is_hover = false;
        Debug.Log("---POINT OUT---");
    }

    private void HandleOver()//视焦hover时触发
    {
        is_hover = true;
        Debug.Log("---POINT IN---");

    }

    private void HandleClick()//点击fire1时触发
    {
        if (!is_hover) { return; }
        Debug.Log("---POINT CLICK---");
    }
}
