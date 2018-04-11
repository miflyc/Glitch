using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {
	//普通的地面作战怪物
	public int countDown;
	public int hp = 10;
	public float dashSpeed,accleration,dashTime,atkRange;
	public GameObject player;
	public enum EnemyStates{
		Idle,
		Attacking,
		Preparing,
		Dead,
	}
	private EnemyStates currState;
	float nowSpeed,timeCount,rangeCount;
	float targetOnYourRight;

	// Use this for initialization
	void Start () {
		currState = EnemyStates.Idle;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(hp<=0){
			currState = EnemyStates.Dead;
			Die();
		}else if(currState == EnemyStates.Attacking){
			Dash(targetOnYourRight,nowSpeed);
		}
	}

	void OnTriggerEnter2D(Collider2D Visitor)
	{
		if(Visitor.gameObject.tag == "PlayerField"){
			if(currState!=EnemyStates.Attacking){
				Debug.Log("Warning");		//播放准备动画
				Vector3 targetPoint = player.transform.position;
				targetOnYourRight = 1;
				if(targetPoint.x>transform.position.x)
					targetOnYourRight = 0;
				StartCoroutine(Attack(countDown));
			}
		}
	}

	
	IEnumerator Attack(int times){
		if(times>0){
			currState = EnemyStates.Preparing;
			Debug.Log(times);
			yield return new WaitForSeconds(1);
			StartCoroutine(Attack(times-1));
		}else{
			Debug.Log("Attack");			//修改为怪物的攻击方式
			nowSpeed = 0.0f;
			timeCount = 0.0f;
			rangeCount = 0.0f;
			currState = EnemyStates.Attacking;
		}
	}
	
	void Die(){
		Destroy(gameObject);
	}

	void Dash(float targetOnYourRight,float Speed){
		Debug.Log("Dash");
		if(timeCount>=dashTime||rangeCount>=atkRange){
			currState = EnemyStates.Idle;
			return;
		}else if(Speed<dashSpeed){
			transform.Translate(new Vector3(Speed*Time.deltaTime*Mathf.Pow(-1,targetOnYourRight),0,0));
		}else{
			transform.Translate(new Vector3(dashSpeed*Time.deltaTime*Mathf.Pow(-1,targetOnYourRight),0,0));
		}
		nowSpeed+=accleration*Time.deltaTime;
		timeCount+=Time.deltaTime;
		rangeCount+= Speed*Time.deltaTime;
	}
}
