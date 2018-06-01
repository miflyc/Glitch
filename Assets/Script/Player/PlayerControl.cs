using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	GameControl gameControl;
	public float jumpSpeed,moveSpeed;
	float nowJumpSpeed,grav;
	Rigidbody2D rb;
	bool jump;
	public enum PlayerState{
		Idle,
		Run,
		Jump,
		Land,
		WalkOnStairs,
		UnderAttack
	}
	public enum PlayerFace{		//玩家朝向，至于我为什么用枚举类型，因为在面板上长得好看，而且可以限制范围
		Right = 0,			//我很想知道怎么把枚举类型的值作为整数使用
		Left = 1
	}
	public PlayerFace currFace = PlayerFace.Left;
	// int face = 1;
	public PlayerState currState = PlayerState.Land;
	GameObject stair,top,buttom,inn,outt;
	float stairWidth,stairHeight;
	PlayerStep playerStep;
	Animator innAnim,outtAnim;
	bool gameMode;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();		//希望通过这个来获取重力
		grav = rb.gravityScale*10;				//生硬的重力计算，我猜这里面一个单位的重力加速度应该是10
		playerStep = transform.FindChild("Step").gameObject.GetComponent<PlayerStep>();
		gameControl = FindObjectOfType<GameControl>();
		inn = transform.FindChild("Inn").gameObject;
		outt = transform.FindChild("Outt").gameObject;
		innAnim = inn.GetComponent<Animator>();
		outtAnim = outt.GetComponent<Animator>();
		gameMode = gameControl.gameMode;
	}

	void FixedUpdate()
	{
		if(gameMode!=gameControl.gameMode){
			gameMode = gameControl.gameMode;
		}
		if(Input.GetKey(KeyCode.A))
			ChangeFace(PlayerFace.Left);
		else if(Input.GetKey(KeyCode.D))
			ChangeFace(PlayerFace.Right);
		if(currFace == PlayerFace.Right){					//里世界采用切换动画的方法
			if(gameMode)
				innAnim.SetBool("PlayerFacingRight",true);
		}
		else{
			if(gameMode)
				innAnim.SetBool("PlayerFacingRight",false);
		}
		Switch();		//目前不论任何时候都可以切换表里世界
		if(currState == PlayerState.WalkOnStairs){		//只有玩家不在楼梯上的时候才能进行常规移动
			WalkOnStairs();
		}else{
			Jump();		//跳跃脚本，每帧执行一次
			Move();
		}
	}

	void Move(){
		if((Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)) & currState == PlayerState.Idle)		//这里的动作切换会有问题，Horizontal会延迟不等于0
			ChangePlayerState(PlayerState.Run);														//所以我换成了用按键A或者D
		else if(!(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)) & currState == PlayerState.Run)
			ChangePlayerState(PlayerState.Idle);
		if(currState == PlayerState.Run){
			if(gameMode)
				innAnim.SetBool("Running",true);
			else
				outtAnim.SetBool("Running",true);
		}else if(currState == PlayerState.Idle){
			if(gameMode)
				innAnim.SetBool("Running",false);
			else
				outtAnim.SetBool("Running",false);
		}
		float moveHorizontal = Input.GetAxis("Horizontal")*moveSpeed;								//平凡的移动脚本
		moveHorizontal *= Time.deltaTime;
		transform.Translate(new Vector3(moveHorizontal,0,0));
	}

	void Jump(){
		if(Input.GetKeyDown(KeyCode.W)){
			if(!jump){
				nowJumpSpeed = jumpSpeed;
				jump = true;							//Jump这个变量是用来控制上升运动的，顺便防止二段跳，落地之后再关掉，原理很奇怪
				if(gameMode)
					innAnim.SetBool("Jump",true);
				else
					outtAnim.SetBool("Jump",true);
				ChangePlayerState(PlayerState.Jump);	//把玩家的当前运动状态改为了跳跃中
				return;
			}
		}
		if(jump){
			transform.Translate(new Vector3(0,Time.deltaTime*jumpSpeed,0));
			nowJumpSpeed-=grav*Time.deltaTime;				//我直接生硬的做了个减法
			if(nowJumpSpeed<=0){							//如果玩家用的是rigidbody，如何判定现在是向上还是向下运动
				//切换玩家状态机状态至开始下落
				if(currState == PlayerState.Idle){			//我想把落地判定放在另一个脚本上
					jump = false;
					if(gameMode){
						innAnim.SetBool("Jump",false);
						innAnim.SetBool("Running",false);
					}else{
						outtAnim.SetBool("Jump",false);
						outtAnim.SetBool("Running",false);
					}
					return;									//加了两行return，解决了变成Idle之后马上变回去的问题
				}
				ChangePlayerState(PlayerState.Land);
				return;
			}
		}
	}

	public void ChangePlayerState(PlayerState toState){		//这个函数是通用的改变玩家当前状态的函数
		if(currState != toState){
			currState = toState;
		}
	}

	//void OnTriggerEnter2D(Collider2D other)
	//{
		//如果玩家在下落状态下碰到地面，则改回站立状态。
		//这个功能在step里面实现了！
	//}


	
	// Update is called once per frame
	void Update () {
		
	}

	void WalkOnStairs(){				//我认为这个优雅的脚本是没有问题的，但是他现在的确存在问题(已解决)
		if(stair != playerStep.stair){	//问题就是如果楼梯横跨在地板上，这个移动会被地板拦住（用取消碰撞盒的方法）
			stair = playerStep.stair;	//第二个问题就是会受到重力的影响（用取消重力的方法）
			if(stair!=null){
				top = stair.transform.FindChild("Top").FindChild("SpawnPosition").gameObject; 
				buttom = stair.transform.FindChild("Buttom").FindChild("SpawnPosition").gameObject;
				stairWidth = top.transform.position.x-buttom.transform.position.x;
				stairHeight = top.transform.position.y-buttom.transform.position.y;
			}
		}
		if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)){
			if(gameMode)
				innAnim.SetBool("Running",true);
			else
				outtAnim.SetBool("Running",true);
		}
		else if(!(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))){
			if(gameMode)
				innAnim.SetBool("Running",false);
			else
				outtAnim.SetBool("Running",false);
		}
		float move = Input.GetAxis("Horizontal")*moveSpeed;
		float hori = move*Time.deltaTime;
		float vert = hori*stairHeight/stairWidth;
		transform.Translate(new Vector3(hori,vert,0));
	}

	void Switch(){			//切换表里世界的脚本
		if(gameControl.gameMode){
			if(!inn.activeSelf)
				inn.SetActive(true);
			if(outt.activeSelf)
				outt.SetActive(false);
		}else{
			if(!outt.activeSelf)
				outt.SetActive(true);
			if(inn.activeSelf)
				inn.SetActive(false);
		}
	}

	void ChangeFace(PlayerFace toFace){				//改变玩家的朝向
		if(currFace != toFace){
			currFace = toFace;
			outt.transform.Rotate(new Vector3(0,180,0));		//表世界采用旋转角色模型的方法
		}
	}
}