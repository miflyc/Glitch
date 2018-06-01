using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Botton : MonoBehaviour {
    public Button button1;
    public Button button2;
    public Image answer1;
    public Image answer2;
	// Use this for initialization
	void Start () {
        button1.onClick.AddListener(OnClick1);
        button2.onClick.AddListener(OnClick2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick1()
    {
        answer1.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnClick2()
    {
        answer2.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
  
}
