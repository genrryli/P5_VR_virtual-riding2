using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public  class game_manager : MonoBehaviour {

    static public game_manager gm;
    public enum game_state { start, ready, playing, finish,warm_up,finish_data}
    public game_state gamestate;
    public GameObject start_time_count;
    public data_manager data;

    private int rank;
    private float g_dis;
    // Use this for initialization
    void Start () {
        if (gm == null) { gm = GetComponent<game_manager>(); }
    }

    // Update is called once per frame
    void Update () {
        if (gm.gamestate == game_state.ready) { start_running(); }
        if (gm.gamestate == game_state.playing) { read_check_point(); }
    }

    void read_check_point()
    {
        if (data._loop_finished>=data.max_loop) { gm.gamestate = game_state.finish; }
    }

    void start_running()
    {
        if (start_time_count == null) { return; }
        if (start_time_count.activeSelf == false&&gm.gamestate==game_state.ready) { gm.gamestate = game_state.playing; }
    }
   
}
