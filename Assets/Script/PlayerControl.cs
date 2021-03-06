﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	
	public float baseSpeed,attackRange;
	float speed;
	public Rigidbody2D rb;
	public GameObject gameController,crasherCollider;
	public Gamemode gm;
	float t1,t2;		//用来判断双击的时间，其中t2用来显示当前的时间，t1用来保存第一次敲击的时间
	public enum playerStates{		//角色状态机
		jump,
		falling,
		landing,
		running
	}
	public playerStates currState = playerStates.falling;
	public float jumpForce = 100.0f;
	delegate void helper();		//用来提供函数借口的委托函数
	public SpriteRenderer white,black;
	public Crasher crasher;
	


	// Use this for initialization
	void Start (){
		rb = GetComponent<Rigidbody2D>();
		speed = baseSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		ShiftMode();
	}

	void FixedUpdate()
	{
		Move();
		DoubleClick(KeyCode.F);
		Run();
		Jump();
		AtkCondition2(attackRange,1);
	}

	void LateUpdate () {
		
	}

	void changeMesh(){				//更换角色外形
		
	}

	void rua(){						//向前冲刺并发起攻击
		crasher.enabled = true;
		
	}

	void Jump(){					//跳跃函数
		if(currState!=playerStates.falling){	
			if(Input.GetKeyDown(KeyCode.W)){
			currState = playerStates.jump;
			}
		}
	}

	void Move(){
		float moveHorizontal = Input.GetAxis("Horizontal")*speed;
		moveHorizontal *= Time.deltaTime;
		transform.Translate(new Vector3(moveHorizontal,0,0));
	}


	bool DoubleClick(KeyCode keyCode){
		if(Input.GetKeyDown(keyCode)){
            t2 = Time.realtimeSinceStartup;
            if(t2 - t1 < 0.2){
                print("double click");
				t1 = t2;
				return true;
            }
            t1 = t2;
			return false;
        }
		return false;
	}

	void Run(){
		if(currState == playerStates.landing){
			if(DoubleClick(KeyCode.D)||DoubleClick(KeyCode.A)){
				speed = baseSpeed*2;
				currState = playerStates.running;
				Debug.Log(currState);
			}
		}else if(currState == playerStates.running){
			if(Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.A)){
				currState = playerStates.landing;
				speed = baseSpeed;
			}
		}
	}

	void ShiftMode(){
		if(gm.gameMode == true){
			black.enabled = false;
			white.enabled = true;
		}else{
			black.enabled = true;
			white.enabled = false;
		}
	}

	private void AtkCondition2(float _range,float _angle)  
	{  
		if(Input.GetAxis("Fire1")!=0)
		{// 球形射线检测周围怪物，不用循环所有怪物类列表，无法获取“Enemy”类  
		Collider[] colliderArr = Physics.OverlapSphere(transform.position, _range, LayerMask.GetMask("Enemy"));  
		for (int i = 0; i < colliderArr.Length; i++)  
		{  
			Vector3 v3 = colliderArr[i].gameObject.transform.position - transform.position;  
			float angle = Vector3.Angle(v3, transform.forward);  
			if (angle < _angle)  
			{  
				// 距离和角度条件都满足了  
			}  
		}  
		transform.Translate(new Vector3(3.0f,0.0f,0.0f));}
	}  
}
