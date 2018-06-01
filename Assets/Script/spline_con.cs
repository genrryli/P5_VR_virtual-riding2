using UnityEngine;
using System.Collections;
using FluffyUnderware.Curvy;//
using FluffyUnderware.DevTools;

public class spline_con : MonoBehaviour {

    public GameObject[] point;
    public Vector3[] point_position;

    void Awake()
    {
        gameObject.GetComponent<CurvySpline>().Interpolation = CurvyInterpolation.Linear;
    }

    void Start () {
        gameObject.GetComponent<CurvySpline>().Interpolation = CurvyInterpolation.Linear;
        for(int i = 0; i < point.Length; i++)
        {
            point[i].transform.localPosition = point_position[i];
        }
    }
	
	void Update () {
	}

    void OnApplicationQuit()
    {
        for (int i = 0; i < point.Length; i++)
        {
            point[i].transform.localPosition = point_position[i];
        }
    }
}
