using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateListener : MonoBehaviour {

	public GameObject player;
	PlayerControl playerControl;
	PlayerControl.playerStates currState;
	// Use this for initialization
	void Start () {
		playerControl = player.GetComponent<PlayerControl>();
		currState = playerControl.currState;
	}
	
	// Update is called once per frame
	void Update () {
		currState = playerControl.currState;
		jump();
	}

	void OnTriggerEnter2D(Collider2D collidedObject)
	{
		if(collidedObject.tag == "Floor"){
			changeState(PlayerControl.playerStates.landing);
		}
	}

	void OnTriggerExit2D(Collider2D collidedObject)
	{
		if(collidedObject.tag == "Floor"){
			changeState(PlayerControl.playerStates.falling);
		}
	}
	public void changeState(PlayerControl.playerStates newState){
		if(currState == newState){
			return;
		}else{
			playerControl.currState = newState;
			currState = playerControl.currState;
			return;
		}
	}

	void jump(){
		if(currState == PlayerControl.playerStates.jump){
			playerControl.rb.AddForce(new Vector2(0,playerControl.jumpForce));
		}
	}
}
