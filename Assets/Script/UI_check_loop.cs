using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_check_loop : MonoBehaviour {

    public data_manager data;
    public Text loop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float loop_left = data._max_loop - data._loop_finished;
        if (loop_left == 1) { loop.text = " 完成！"; }
        else if(loop_left == 2) { loop.text = " 最后一圈！"; }
        else { float next_loop = data.loop_finished + 2; loop.text = "第" + next_loop + "圈"; }
	}
}
