using UnityEngine;
using System.Collections;

public class smooth_aim : MonoBehaviour {

    public Transform center;
    public GameObject[] layer;
    public float[] duration;
    public float[] distance;
    public GameObject cam;

    void Start () {
    }

	void Update () {
        for(int i = 0; i < layer.Length; i++)
        {
            layer[i].transform.LookAt(cam.transform);
            Vector3 direction = center.position - layer[i].transform.position;
            layer[i].transform.Translate(direction / duration[i], Space.World);
            layer[i].transform.Translate(-Vector3.forward * distance[i]);
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < layer.Length; i++)
        {
            layer[i].transform.LookAt(cam.transform);
            layer[i].transform.position = center.position;
            layer[i].SetActive(true);
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < layer.Length; i++)
        {
            layer[i].SetActive(false);
        }
    }
}
