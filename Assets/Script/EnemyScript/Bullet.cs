using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	public int attackPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D crasher)
	{
		if(crasher.tag == "player"){
			PlayerControl playerControl = crasher.gameObject.GetComponent<PlayerControl>();
			playerControl.HealthPoint-=attackPoint;
		}
	}
}
