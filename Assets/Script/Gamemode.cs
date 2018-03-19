using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour {

	public bool gameMode = false;
	public GameObject inside;

	// Use this for initialization
	void Start () {
		gameMode = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameMode){
			if(inside.activeInHierarchy == false){
				inside.SetActive(true);
			}
		}else{
			if(inside.activeInHierarchy == true){
				inside.SetActive(false);
			}
		}
		if(Input.GetKeyDown(KeyCode.C)){
			ChangeGameMode();
		}
	}

	public void ChangeGameMode(){
		if(gameMode){
			gameMode = false;
		}else{
			gameMode = true;
		}
	}
}
