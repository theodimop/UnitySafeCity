using UnityEngine;
using System.Collections;

public class settings : MonoBehaviour {

    public float swD2, shD2;

    private static settings instance;
    public static settings Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
        swD2 = Screen.width / 2;
        shD2 = Screen.height / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
