using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;
using UnityEngine.SceneManagement;

public class VR_button_con_mode : MonoBehaviour {

    [SerializeField]
    private VRInteractiveItem m_InteractiveItem;

    public Color original_color;
    public Color hover_color;
    public Color click_color;

    private string big_button_data;
    private bool is_hover;
       
    void Start()
    {
    }

    void Update()
    {
         big_button_data = GameObject.Find("player").GetComponent<motion_data>().big_button;
         if (big_button_data=="on") { HandleClick(); }
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
        Debug.Log("---POINT OUT---");
    }

    private void HandleOver()//视焦hover时触发
    {
        is_hover = true;
        gameObject.GetComponent<Image>().color = hover_color;
        Debug.Log("---POINT IN---");
    }

    private void HandleClick()//点击fire1时触发
    {
        if (!is_hover) { return; }
        gameObject.GetComponent<Image>().color = click_color;
        SceneManager.LoadScene("1_MODE_SINGLE");
        Debug.Log("---POINT CLICK---");
    }
}
