﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	
	public float baseSpeed,attackRange,gap;
	float speed;
	public int HealthPoint = 10;
	public Rigidbody2D rb;
	public GameObject gameController,crasherCollider;
	public Gamemode gm;
	float t1,t2;		//用来判断双击的时间，其中t2用来显示当前的时间，t1用来保存第一次敲击的时
	float triplet1,triplet2,triplet3;	//三段斩时间判定
	public enum playerStates{		//角色状态机
		jump,
		falling,
		landing,
		running
	}
	public enum AttackStyle{
		rua,
		triplet1,
		triplet2,
		triplet3,
	}
	public enum FacingDirection{
		Left,
		Right
	} 
	public FacingDirection currDir = FacingDirection.Left;
	public playerStates currState = playerStates.falling;
	public float jumpForce = 100.0f;
	delegate void helper();		//用来提供函数借口的委托函数
	public GameObject white,black;
	public AttackStyle nextStyle = AttackStyle.triplet1;
	


	// Use this for initialization
	void Start (){
		rb = GetComponent<Rigidbody2D>();
		speed = baseSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		ShiftMode();
		if(HealthPoint<=0){			//死亡
			Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		ChangeDir();
		Move();
		DoubleClick(KeyCode.F);
		Run();
		Jump();
		AtkCondition2(attackRange,1);
		Attack();
	}

	void LateUpdate () {
		
	}

	void ChangeDir(){				//更换角色方向
		if(Input.GetKey(KeyCode.A)){
			if(currDir == FacingDirection.Right){
				currDir = FacingDirection.Left;
				white.transform.Rotate(new Vector3(0,180,0));
				black.transform.Rotate(new Vector3(0,180,0));
				//此处应该转换动画或者把人物模型翻转
			}
		}
		if(Input.GetKey(KeyCode.D)){
			if(currDir == FacingDirection.Left){
				currDir = FacingDirection.Right;
				white.transform.Rotate(new Vector3(0,-180,0));
				black.transform.Rotate(new Vector3(0,-180,0));
			}
		}
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
			black.SetActive(false);
			white.SetActive(true);
		}else{
			black.SetActive(true);
			white.SetActive(false);
		}
	}

	private void AtkCondition2(float _range,float _angle)  
	{  
		// 球形射线检测周围怪物，不用循环所有怪物类列表，无法获取“Enemy”类  
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
	}

	private void Triplet(){
		float t = Time.realtimeSinceStartup;
		if(t - triplet2 <= gap & nextStyle == AttackStyle.triplet3){
			triplet3 = t;
			AtkCondition2(attackRange,1);
			nextStyle = AttackStyle.triplet1;
			//播放动画3
		}else if(t-triplet1<=gap & nextStyle == AttackStyle.triplet2){
			triplet2 = t;
			AtkCondition2(attackRange,1);
			nextStyle = AttackStyle.triplet3;
			//播放动画2
		}else{
			triplet1 = t;
			AtkCondition2(attackRange,1);
			nextStyle = AttackStyle.triplet2;
			//播放动画1
		}
	}

	private void Attack(){
		if(Input.GetButtonDown("Fire1")){
			transform.Translate(new Vector3(3.0f,0.0f,0.0f));
		}else if(Input.GetButtonDown("Fire2")){
			Triplet();
		}
	}

}
