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

	void OnCollisionEnter2D(Collision2D visitor)
	{
		if(visitor.gameObject.tag == "PlayerField"){
			Debug.Log("Warning");
			Attack();
		}
	}

	IEnumerator Prepare(int times){
		for(int i = times;i>0;i--){
			Debug.Log(i);
			yield return new WaitForSeconds(1);
		}
	}

	void Attack(){
		StartCoroutine(Prepare(countDown));
		Debug.Log("Attack");
	}
}
