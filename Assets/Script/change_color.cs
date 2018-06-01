using UnityEngine;
using System.Collections;

public class change_color : MonoBehaviour {

    public Material material;
    public Color start_color;
    public Color late_color;
    public float duration;


    private Color _color;
    private Color per_color;
    private float timer;
	// Use this for initialization
	void Start () {
        per_color = (late_color - start_color) / duration*Time.deltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        _color += per_color;
        material.color = start_color + _color;
        if (timer > duration)
        {
            material.color = start_color;
            timer = 0;
            _color = Vector4.zero;
        }
	}
}
