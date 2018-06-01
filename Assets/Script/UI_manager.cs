using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_manager : MonoBehaviour {

    private GameObject player;
    public GameObject start_page;
    public GameObject single_mode;

	// Use this for initialization
	void Start () {
        if (game_manager.gm.gamestate == game_manager.game_state.start)
        {
            player = GameObject.FindGameObjectWithTag("bike").gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void play_jungle_day()
    {
        SceneManager.LoadScene("jungle_day");
		GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<motion_data> ().close_port ();
    }

    public void play_jungle_night()
    {
        SceneManager.LoadScene("jungle_night");
		GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<motion_data> ().close_port ();
    }

    public void go_to_single_mode()
    {
        start_page.SetActive(false);
        player.SetActive(false);
        single_mode.SetActive(true);        
    }

    public void go_to_start_page()
    {
        single_mode.SetActive(false);
        start_page.SetActive(true);
        player.SetActive(true);   
    }

    public void play_again()
    {
        Scene scene = SceneManager.GetActiveScene();
        string name_ = scene.name;
        SceneManager.LoadScene(name_);
		GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<motion_data> ().close_port ();
    }

    public void back_to_menue()
    {
        SceneManager.LoadScene("start");
		GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<motion_data> ().close_port ();
    }

}
