using UnityEngine;
using System.Collections;

public class road_check : MonoBehaviour {

    void OnTriggerEnter(Collider Trigger)
    {
        if (Trigger.tag == "Player"|| Trigger.tag == "com")
        {
            Trigger.GetComponent<road_manager>().finish_one_loop();
        }
    }
}
