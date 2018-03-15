using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCListener : MonoBehaviour {

	public string words;
	bool talk = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.J) & talk){
			Debug.Log(words);
		}
	}

	void OnTriggerStay2D(Collider2D Visitor)
	{
		if(Visitor.gameObject.tag=="PlayerField"){
			if(talk!=true){
				talk = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D Visitor)
	{
		if(Visitor.gameObject.tag=="PlayerField"){
			talk = false;
		}
	}

}
