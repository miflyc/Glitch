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
	float t1,t2;		//用来判断双击的时间，其中t2用来显示当前的时间，t1用来保存第一次敲击的时间

	delegate void helper();
	


	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		//ShiftMode();
		Move();
		DoubleClick(KeyCode.F);
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
		float zuoyou = horspeed;
		float shangxia = verspeed;
		zuoyou *= Time.deltaTime;
		shangxia *= Time.deltaTime;
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
		transform.Translate(zuoyou,shangxia,0);
	}

	void ShiftMode(){
		if(Input.GetKeyDown(KeyCode.Y)){
			gm.ChangeGameMode();
		}
	}

	void DoubleClick(KeyCode keyCode){
		if(Input.GetKeyDown(keyCode)){    
            t2 = Time.realtimeSinceStartup;    
            if(t2 - t1 < 0.2){    
                print("double click");    
            }    
            t1 = t2;
        }else if(Input.GetKeyUp(keyCode)){
			
		}
	}
}
