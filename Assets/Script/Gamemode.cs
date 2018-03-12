using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour {

	public bool gameMode = false;
	public GameObject inside;
	MobileGlitchCameraShader glitchShader;

	// Use this for initialization
	void Start () {
		gameMode = false;
		glitchShader = GetComponent<MobileGlitchCameraShader>();
		glitchShader.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameMode){
			if(inside.activeInHierarchy == false){
				inside.SetActive(true);
			}
		}else{
			if(inside.activeInHierarchy == true){
				inside.SetActive(false);
			}
		}
		if(Input.GetKeyDown(KeyCode.C)){
			ChangeGameMode();
		}else if(Input.GetKeyDown(KeyCode.R)){
			Glitch();
		}
	}

	public void ChangeGameMode(){
		gameMode=!gameMode;
		StartCoroutine(GlitchEffect());
	}

	public void Glitch(){
		glitchShader.enabled = !glitchShader.enabled;
	}

	IEnumerator GlitchEffect()
    {
		Glitch();
        yield return new WaitForSeconds(1);
		Glitch();
    }
}
