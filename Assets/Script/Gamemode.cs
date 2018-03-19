using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour {

	public bool gameMode = false;
<<<<<<< HEAD
	public GameObject inside;
=======
	MobileGlitchCameraShader glitchShader;
>>>>>>> 17c8a21fa2bb4395d81f22baa463c595de777b53

	// Use this for initialization
	void Start () {
		gameMode = false;
<<<<<<< HEAD
=======
		glitchShader = GetComponent<MobileGlitchCameraShader>();
		glitchShader.enabled = false;
>>>>>>> 17c8a21fa2bb4395d81f22baa463c595de777b53
	}
	
	// Update is called once per frame
	void Update () {
<<<<<<< HEAD
		if(gameMode){
			if(inside.activeInHierarchy == false){
				inside.SetActive(true);
			}
		}else{
			if(inside.activeInHierarchy == true){
				inside.SetActive(false);
			}
		}
=======
>>>>>>> 17c8a21fa2bb4395d81f22baa463c595de777b53
		if(Input.GetKeyDown(KeyCode.C)){
			ChangeGameMode();
		}
	}

	public void ChangeGameMode(){
<<<<<<< HEAD
		if(gameMode){
			gameMode = false;
		}else{
			gameMode = true;
		}
	}
=======
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
>>>>>>> 17c8a21fa2bb4395d81f22baa463c595de777b53
}
