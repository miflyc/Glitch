using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public Camera currCamera;
	public GameObject player;
	// Use this for initialization
	float orthographicSize,aspectRatio,cameraHeight,cameraWidth,OffsetZ,playerX,playerY;
	public float Xmax,Xmin,Ymax,Ymin;
	void Start () {
		orthographicSize = currCamera.orthographicSize; 
		aspectRatio = Screen.width * 1.0f / Screen.height;
		cameraHeight = orthographicSize * 2;
		cameraWidth = cameraHeight * aspectRatio;
		Xmax = Xmax-cameraWidth/2;
		Xmin = Xmin+cameraWidth/2;
		Ymax = Ymax - cameraHeight/2;
		Ymin = Ymin + cameraHeight/2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerX = player.transform.position.x;
		playerY = player.transform.position.y;
		if(transform.position.x>Xmax){
			transform.Translate(new Vector3(Xmax - transform.position.x,0,0));
		}else if(transform.position.x<Xmin){
			transform.Translate(new Vector3(Xmin - transform.position.x,0,0));
		}else if((transform.position.x - playerX)>cameraWidth/8 & playerX>Xmin-cameraWidth/8){
			transform.Translate(new Vector3(playerX + cameraWidth/8-transform.position.x,0,0));
		}else if((playerX - transform.position.x)>cameraWidth/8 &playerX<Xmax+cameraWidth/8){
			transform.Translate(new Vector3(playerX - cameraWidth/8 - transform.position.x,0,0));
		}

		if(transform.position.y<Ymin ){
			transform.Translate(new Vector3(0,Ymin - transform.position.y,0));
		}else if(transform.position.y>Ymax){
			transform.Translate(new Vector3(0,Ymax - transform.position.y,0));
		}else if((transform.position.y - playerY)>cameraHeight/4 &playerY>Ymin-cameraHeight/4){
			transform.Translate(new Vector3(0,playerY - transform.position.y + cameraHeight/4,0));
		}else if((playerY - transform.position.y)>cameraHeight/4 &playerY<Ymax+cameraHeight/4){
			transform.Translate(new Vector3(0,playerY - transform.position.y - cameraHeight/4,0));
		}
	}

	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
