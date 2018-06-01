using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStep : MonoBehaviour {

	public GameObject player;
	Rigidbody2D rb;
	PlayerControl playerControl;
	PlayerControl.PlayerState currState;
	bool enterStair;		//能否进♂入楼梯的开关
	float playerGrav;
	public GameObject stair;
	Transform stairTop,stairButtom,enter;
	float leftEdge,rightEdge;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		playerControl = player.GetComponent<PlayerControl>();
		rb = player.GetComponent<Rigidbody2D>();
		playerGrav = rb.gravityScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currState = playerControl.currState;
		if(Input.GetKeyDown(KeyCode.S)){			//按S键进入楼梯
			EnterStair();
		}
		ExitStair();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(currState == PlayerControl.PlayerState.Land){
			if(other.tag == "Floor"){
				//Debug.Log("2");		//到这里为止都正常执行了
				playerControl.ChangePlayerState(PlayerControl.PlayerState.Idle);	//为什么这个函数在这里无效（看下一行）
				//上一行的问题已经解决了，原因是在PlayerControl的脚本中，“将角色状态改为Land”的函数写在了“如果角色当前的状态是Idle”之前
				//导致每次只要一切换到Idle就会以迅雷不及掩耳之势切换回Land……
			}
		}
		if(other.tag == "Stair"){		//进入楼梯顶部or底部的判定
			//Debug.Log("3");
			enterStair = true;
			enter = other.transform.FindChild("SpawnPosition");
			stair = other.transform.parent.gameObject;
			Debug.Log(stair.name);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Stair"){		//离开楼梯顶部or底部的判定
			//Debug.Log("4");
			enterStair = false;
			stair = null;
		}
	}

	void EnterStair(){
		if(enterStair){
			//Debug.Log("5");
			player.transform.Translate(new Vector3(enter.position.x-player.transform.position.x,enter.position.y-player.transform.position.y,0));
			playerControl.ChangePlayerState(PlayerControl.PlayerState.WalkOnStairs);	//玩家进入楼梯的脚本，此脚本的触发在Update函数里
			stairTop = stair.transform.FindChild("Top");
			stairButtom = stair.transform.FindChild("Buttom");
			if(stairTop.position.x<stairButtom.position.x){
				leftEdge = stairTop.position.x;
				rightEdge = stairButtom.position.x;
			}else{
				leftEdge =stairButtom.position.x;
				rightEdge = stairTop.position.x;
			}
			BoxCollider2D collider = player.GetComponent<BoxCollider2D>();
			if(collider.enabled){
				collider.enabled = false;
			}
			rb.gravityScale = 0;
		}
	}

	void ExitStair(){
		if(currState == PlayerControl.PlayerState.WalkOnStairs){
			if(player.transform.position.x<leftEdge||player.transform.position.x>rightEdge){
				playerControl.ChangePlayerState(PlayerControl.PlayerState.Idle);
				BoxCollider2D collider = player.GetComponent<BoxCollider2D>();
				if(!collider.enabled){
					collider.enabled = true;
				}
				rb.gravityScale = playerGrav;
			}
		}
	}
}
