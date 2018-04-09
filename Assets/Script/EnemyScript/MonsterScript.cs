using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {
	//普通的地面作战怪物
	public int countDown;
	public int hp = 10;
	public float atkRange = 2.0f;
	public float dashSpeed = 0.3f,deltaSpeed = 0.05f;
	public GameObject player;
	float timeCount = 0,nowSpeed;
	public enum EnemyStates{
		Idle,
		Attacking,
		Dead,
	}
	private EnemyStates currState;
	bool targetOnYourRight;

	// Use this for initialization
	void Start () {
		currState = EnemyStates.Idle;
		nowSpeed = dashSpeed;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(hp<=0){
			currState = EnemyStates.Dead;
			Die();
		}else if(currState == EnemyStates.Attacking){
			timeCount+=Time.deltaTime;
			nowSpeed = Mathf.Lerp(0.0f,dashSpeed,Time.deltaTime);
			Dash(targetOnYourRight);
		}
	}

	void OnTriggerEnter2D(Collider2D Visitor)
	{
		if(Visitor.gameObject.tag == "PlayerField"){
			if(currState!=EnemyStates.Attacking){
				Debug.Log("Warning");		//播放准备动画
				timeCount = 0.0f;
				nowSpeed = 0.0f;
				Vector3 targetPoint = player.transform.position;
				targetOnYourRight = false;
				if(targetPoint.x>transform.position.x)
					targetOnYourRight = true;
				StartCoroutine(Attack(countDown));
			}
		}
	}

	
	IEnumerator Attack(int times){
		if(times>0){
			currState = EnemyStates.Attacking;
			Debug.Log(times);
			yield return new WaitForSeconds(1);
			StartCoroutine(Attack(times-1));
		}else{
			Debug.Log("Attack");			//修改为怪物的攻击方式
			currState = EnemyStates.Idle;
		}
	}
	
	void Die(){
		Destroy(gameObject);
	}

	void Dash(bool targetOnYourRight){
		Debug.Log("Dash");
		if(targetOnYourRight){
			transform.Translate(Vector3.Lerp(new Vector3(0,0,0),new Vector3(atkRange,0,0),Mathf.Clamp(timeCount*nowSpeed,0.0f,1.0f)));
		}else{
			transform.Translate(Vector3.Lerp(new Vector3(0,0,0),new Vector3(-atkRange,0,0),Mathf.Clamp(timeCount*nowSpeed,0.0f,1.0f)));
		}
	}
}
