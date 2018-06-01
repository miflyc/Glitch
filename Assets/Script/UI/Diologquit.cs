using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diologquit : MonoBehaviour {
    public bool answered;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        answered = true;
        if (Input.GetKeyDown(KeyCode.Mouse0))
            gameObject.SetActive(false);
	}
}
