using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public Camera currCamera;
	public GameObject player;
	// Use this for initialization
	float orthographicSize,aspectRatio,cameraHeight,cameraWidth,OffsetZ,playerX,playerY;
	public float Xmax,Xmin;
	void Start () {
		orthographicSize = currCamera.orthographicSize; 
		aspectRatio = Screen.width * 1.0f / Screen.height;
		cameraHeight = orthographicSize * 2;
		cameraWidth = cameraHeight * aspectRatio;
		OffsetZ = transform.position.z;
		transform.Translate(new Vector3((transform.position.x-player.transform.position.x+(cameraWidth/8)),(player.transform.position.y - transform.position.y + cameraHeight/6),0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerX = player.transform.position.x;
		playerY = player.transform.position.y;
		if((transform.position.x - playerX)>cameraWidth/8){
			transform.Translate(new Vector3(playerX + cameraWidth/8-transform.position.x,0,0));
		}else if((playerX - transform.position.x)>cameraWidth/8){
			transform.Translate(new Vector3(playerX - cameraWidth/8 - transform.position.x,0,0));
		}
		if((transform.position.y - playerY)>cameraHeight/6){
			transform.Translate(new Vector3(0,playerY - transform.position.y + cameraHeight/6,0));
		}
	}
}
