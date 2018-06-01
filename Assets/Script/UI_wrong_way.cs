using UnityEngine;
using System.Collections;

public class UI_wrong_way : MonoBehaviour {

    public road_manager data;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = data._backward_direction.transform.rotation;
        transform.position = data._backward_direction.transform.position;
	}
}
