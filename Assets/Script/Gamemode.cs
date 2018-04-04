using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour {

	public bool gameMode = false;
	MobileGlitchCameraShader glitchShader;

	public int objectCount;
	public GameObject[] npcs;
	// Use this for initialization
	void Start () {
		gameMode = false;
		glitchShader = GetComponent<MobileGlitchCameraShader>();
		glitchShader.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C)){
			ChangeGameMode();
		}
	}

	public void ChangeGameMode(){
		gameMode=!gameMode;
		StartCoroutine(GlitchEffect());
		foreach(GameObject o in npcs){
			if(o){
				GameObject inn = o.transform.FindChild("inn").gameObject;
				GameObject outt = o.transform.FindChild("outt").gameObject;
				if(inn & outt){
					if(gameMode == true){
						outt.SetActive(false);
						inn.SetActive(true);
					}else{
						outt.SetActive(true);
						inn.SetActive(false);
					}
				}
			}
		}
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
