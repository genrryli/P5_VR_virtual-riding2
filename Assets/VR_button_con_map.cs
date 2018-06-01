using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

public class VR_button_con_map : MonoBehaviour {

    [SerializeField]
    private VRInteractiveItem m_InteractiveItem;

    public Color original_color;
    public Color hover_color;
    public Color click_color;
    public string ani_name;
    public string scene_nanme = "MAP_PINK_DREAM";

    private Animation ani;
    private string big_button_data;
    private bool is_hover;
    void Start()
    {
        ani = gameObject.GetComponent<Animation>();
    }

    void Update()
    {
        big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
        if (big_button_data == "on") { HandleClick(); }
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
        gameObject.GetComponent<Image>().color = original_color;
        ani.PlayQueued("canvas_"+ani_name+"_out", QueueMode.CompleteOthers,PlayMode.StopSameLayer);
        Debug.Log("---POINT OUT---");
    }

    private void HandleOver()//视焦hover时触发
    {
        is_hover = true;
        gameObject.GetComponent<Image>().color = hover_color;
        ani.PlayQueued("canvas_"+ani_name+"_in",QueueMode.PlayNow,PlayMode.StopSameLayer);
        Debug.Log("---POINT IN---");
    }

    private void HandleClick()//点击fire1时触发
    {
        if (!is_hover) { return; }
        gameObject.GetComponent<Image>().color = click_color;

        SceneManager.LoadScene(scene_nanme);
        Debug.Log("---POINT CLICK---");
    }
}
