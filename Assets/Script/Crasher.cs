using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crasher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D enemy)
	{
		if(enemy.gameObject.tag == "enemy"){
			Debug.Log("Beat you!");
		}
	}
	
}
