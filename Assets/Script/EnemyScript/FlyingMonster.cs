using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : MonsterScript {
	//空中作战的小怪物
	new public float hp = 20;
	EnemyStates currState;
	// Use this for initialization
	void Start () {
		currState = EnemyStates.Idle;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "PlayerField"){
			Attack(other.gameObject);
		}
	}
	
	void Attack(GameObject other){
		Vector3 targetPoint = other.transform.position;
		
	}

	void FlyingDash(){
		
	}
}
