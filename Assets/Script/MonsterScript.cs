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
			StartCoroutine(Attack(countDown));
		}
	}

	
	IEnumerator Attack(int times){
		if(times>0){
			Debug.Log(times);
			yield return new WaitForSeconds(1);
			StartCoroutine(Attack(times-1));
		}else{
			Debug.Log("Attack");
		}
	}

}
