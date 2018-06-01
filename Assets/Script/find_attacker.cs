using UnityEngine;
using System.Collections;

public class find_attacker : MonoBehaviour
{

    public GameObject UI_under_attack;

    void OnTriggerEnter(Collider Trigger)
    {
        if (Trigger.tag == "attack" || Trigger.tag == "effect")
        {
            UI_under_attack.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "attack" || target.gameObject.tag == "effect")
        {
            UI_under_attack.SetActive(true);
        }
    }
}