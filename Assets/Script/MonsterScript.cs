using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {

	public int countDown;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D Visitor)
	{
		if(Visitor.gameObject.tag == "PlayerField"){
			Debug.Log("Warning");
			Attack();
		}
	}

	
	IEnumerator Prepare(int times){
		Debug.Log(times);
		yield return new WaitForSeconds(1);
	}

	void Attack(){
		for(int i = countDown;i>0;i--){
			StartCoroutine(Prepare(i));
		}
		Debug.Log("Attack");
	}
}
