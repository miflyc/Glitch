using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

	public bool gameMode = false;
	public GameObject[] inn,outt;
	public float switchColdDown = 1.0f;
	float coldDownTime = 0.0f;


	// Use this for initialization
	void Start () {
		SwitchGame(gameMode);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
			Pause();
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(coldDownTime>0)
			coldDownTime -= Time.deltaTime;
		else{
			if(Input.GetKeyDown(KeyCode.C)){
				coldDownTime = switchColdDown;
				SwitchGame(!gameMode);
			}
		}
	}

	public void SwitchGame(bool toMode){
		gameMode = toMode;
		StartCoroutine(Glitch());
		if(gameMode){
			foreach(GameObject i in inn)
				i.SetActive(true);
			foreach(GameObject o in outt)
				o.SetActive(false);
		}else{
			foreach(GameObject i in inn)
				i.SetActive(false);
			foreach(GameObject o in outt)
				o.SetActive(true);
		}
	}

	public void Pause(){
		if(Time.timeScale!=0)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	IEnumerator Glitch(){
		GlitchCameraShader glitch = GetComponent<GlitchCameraShader>();
		glitch.enabled = true;
		yield return new WaitForSeconds(1);
		glitch.enabled = false;
	}

	public void GameOver(){		//游戏结束
		
	}
}
