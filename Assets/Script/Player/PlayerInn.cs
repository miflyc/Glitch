using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInn : MonoBehaviour {

	GameObject attackField1,flashField,player;
	int currFace,flashDir;
	Animator playerAnim;
	public float flashTime = 1.0f,flashAcc = 60.0f,prepareTime = 0.0f;
	float flashTimeCount = 0.0f,nowSpeed = 0.0f;
	bool flash=false;

	// Use this for initialization
	void Start () {
		attackField1 = transform.FindChild("AtkField").gameObject;
		flashField = transform.FindChild("FlashField").gameObject;
		player = GameObject.FindGameObjectWithTag("Player");
		playerAnim = player.transform.FindChild("Inn").gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	IEnumerator Attack1(){
		attackField1.SetActive(true);
		//Debug.Log("Attack Field On");
		yield return new WaitForSeconds(0.1f);	
		attackField1.SetActive(false);
		//Debug.Log("Attack Field Off");
	}

	// IEnumerator Flash(){			//这里重写，写成玩家一点点移动穿刺的脚本
	// 	flashField.SetActive(true);
	// 	playerAnim.SetBool("Flash",true);
	// 	yield return new WaitForSeconds(1);
	// 	flashField.SetActive(false);
	// 	playerAnim.SetBool("Flash",false);
	// 	player.transform.Translate(new Vector3(Mathf.Pow(-1,currFace)*flashRange,0,0));
	// }

	void Flash(){
		if(flashTimeCount>=flashTime){		//将所有修改过的属性还原
			flash = false;
			playerAnim.SetBool("Flash",false);
			Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
			rb.gravityScale = 1;
			BoxCollider2D bc = player.GetComponent<BoxCollider2D>();
			bc.enabled = true;
			flashField.SetActive(false);
		}
		if(flashTimeCount > prepareTime)
			nowSpeed+=flashAcc*Time.deltaTime;
		flashTimeCount+=Time.deltaTime;
		player.transform.Translate(new Vector3(Mathf.Pow(-1,flashDir)*nowSpeed*Time.deltaTime,0,0));
	}

	void ChangeDir(){
		if(player.GetComponent<PlayerControl>().currFace == PlayerControl.PlayerFace.Right){
			currFace = 0;
			attackField1.transform.eulerAngles = new Vector3(0,180,0);
			flashField.transform.eulerAngles = new Vector3(0,180,0);
		}
		else{
			currFace = 1;
			attackField1.transform.eulerAngles = new Vector3(0,0,0);
			flashField.transform.eulerAngles = new Vector3(0,0,0);
		}
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		ChangeDir();
		if(flash)
			Flash();		//如果闪现处于开启状态，则运行闪现
		else if(Input.GetKeyDown(KeyCode.K)){		//初始化一些闪现属性
			flashTimeCount = 0.0f;					//将闪现时间重置为0
			flash = true;							//开始闪现功能，关闭闪现写在了flash函数里面
			flashDir = currFace;					//记录闪现方向
			nowSpeed = 0.0f;						//初始速度置为0
			playerAnim.SetBool("Flash",true);		//将闪现动画开关打开
			Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
			rb.gravityScale = 0;					//关闭角色重力
			BoxCollider2D bc = player.GetComponent<BoxCollider2D>();
			bc.enabled = false;						//关闭角色碰撞盒
			flashField.SetActive(true);				//开启闪现攻击方块区域
		}
		if(Input.GetKeyDown(KeyCode.J))
			StartCoroutine(Attack1());
	}
}
