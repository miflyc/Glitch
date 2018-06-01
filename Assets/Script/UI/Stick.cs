using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stick : MonoBehaviour {
    public int Diolognumber;
    public int Tipsnumber;
    public int answernumber1;
    public int answernumber2;
    public int buttonnumber1;
    public int buttonnumber2;
    public bool hasDiolog;
    private bool storage1;
    private bool storage2;
    private bool detected;
    public Image question; 
    public Image answer1;
    public Image answer2;
    public Button button1;
    public Button button2;
    private Sprite storageanswer1;
    private Sprite storageanswer2;

	// Use this for initialization
	void Start () {
        hasDiolog = false;
        detected = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D other) {

        

        if (other.tag == "Player" && this.tag == "NPC")
        {
            if (!answer1.GetComponent<Diologquit>().answered && !answer2.GetComponent<Diologquit>().answered && !hasDiolog)
            {

                Debug.Log("test");
                if (Diolognumber < other.GetComponent<DiologList>().diologs.Length)
                {
                    question.gameObject.SetActive(true);
                    question.sprite = other.GetComponent<DiologList>().diologs[Diolognumber];
                    answer1.sprite = other.GetComponent<DiologList>().answers[answernumber1];
                    answer2.sprite = other.GetComponent<DiologList>().answers[answernumber2];
                    button1.image.sprite = other.GetComponent<DiologList>().button[buttonnumber1];
                    button2.image.sprite = other.GetComponent<DiologList>().button[buttonnumber2];
                    storageanswer1 = other.GetComponent<DiologList>().answers[answernumber1];
                    storageanswer2 = other.GetComponent<DiologList>().answers[answernumber2];
                    
                }
                else
                    Debug.Log("Array erro");
            }

            else 
                RunTheStorage();
        }

        if (other.tag == "Player" && this.gameObject.tag == "Tips")
        {
            if (Tipsnumber < other.GetComponent<DiologList>().tips.Length)
            {
                other.GetComponent<DiologList>().tips[Tipsnumber].gameObject.SetActive(true);
            }
            else
                Debug.Log("Array erro");


        }

    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player" && this.tag == "NPC")
        {
            if (answer2.GetComponent<Diologquit>().answered || answer1.GetComponent<Diologquit>().answered)
            {
                hasDiolog = true;
                if (answer1.GetComponent<Diologquit>().answered)
                    storage1 = true;
                if (answer2.GetComponent<Diologquit>().answered)
                    storage2 = true;
            }
            answer1.GetComponent<Diologquit>().answered = false;
            answer2.GetComponent<Diologquit>().answered = false;
            if (Diolognumber < other.GetComponent<DiologList>().diologs.Length)
                question.gameObject.SetActive(false);   
            else
                Debug.Log("Array erro");
        }

        if (other.tag == "Player" && this.gameObject.tag == "Tips")
        {
            if (Tipsnumber < other.GetComponent<DiologList>().tips.Length)
                other.GetComponent<DiologList>().tips[Tipsnumber].gameObject.SetActive(false);
            else
                Debug.Log("Array erro");


        }

    }



       

    void RunTheStorage()
    {
        
        if (storage1)
        {
            answer1.gameObject.SetActive(true);
            answer1.sprite = storageanswer1;
        }

        if (storage2)
        {
            answer2.gameObject.SetActive(true);
            answer2.sprite = storageanswer2;
        }


    }
}
