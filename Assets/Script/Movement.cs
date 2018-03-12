using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed;
	public float speedDown = 0.3f;
	float th;
	float tv;
	float horspeed;
	float verspeed;
	public GameObject gameController;
	public Gamemode gm;
	


	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		shiftMode();
		Move();
	}

	void changeMesh(){
		
	}

	void rua(){

	}

	void Move(){
		if(Input.GetButton("Horizontal")){
			th = 1;
		}
		if(Input.GetButton("Vertical")){
			tv = 1;
		}
		horspeed =Input.GetAxis("Horizontal")*speed;
		verspeed = Input.GetAxis("Vertical")*speed;
		float 左右 = horspeed;
		float 上下 = verspeed;
		左右 *= Time.deltaTime;
		上下 *= Time.deltaTime;
		if(th>0){
			th-= speedDown;
		}
		else{
			th = 0;
		}
		if(tv>0){
			tv-= speedDown;
		}
		else{
			tv = 0;
		}
		transform.Translate(左右,上下,0);
	}

	void shiftMode(){
		if(Input.GetKeyDown(KeyCode.Y)){
			gm.ChangeGameMode();
		}
	}
}
