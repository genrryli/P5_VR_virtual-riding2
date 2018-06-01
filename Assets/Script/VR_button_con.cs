using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class VR_button_con : MonoBehaviour {

    [SerializeField]
    private VRInteractiveItem m_InteractiveItem;

    public Color original_color;
    public Color hover_color;
    public Color click_color;

    private  bool is_hover;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A)) { HandleClick(); }
	}

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        //m_InteractiveItem.OnUp += HandleUp;
        //m_InteractiveItem.OnDown += HandleDown;
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
        Debug.Log("---POINT CLICK---");
    }

}
