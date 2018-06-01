using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	public int hp = 10,wanderSpeed = 1;
	bool underAttack = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	public void Die(){
		if(hp<=0){		//血量低于0就死掉，这个函数可以通用于所有小怪物
			Destroy(gameObject,2.0f);	
			anim.SetBool("Dead",true);	
		}
	}

	public void Attacked(int atkPower){
		hp-=atkPower;			//扣血（在函数上预留了扣血接口），这个函数也可以用在所有小怪物身上
		StartCoroutine(UnderAttack());		//播放挨揍特效
	}

	public IEnumerator UnderAttack(){		//一个通用的挨揍函数
		underAttack = true;
		anim.SetBool("UnderAttack",true);		//调动挨揍动画
		//Debug.Log("Under Attack");
		yield return new WaitForSeconds(1.0f);
		underAttack = false;
		anim.SetBool("UnderAttack",false);		//还得把它关掉
		//Debug.Log("Jiechu");
	}

	public void MoveBack(){		//此函数用于监听小怪挨揍，挨揍后会后退
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(player.transform.position.x>transform.position.x)
			transform.Translate(new Vector3(-wanderSpeed*Time.deltaTime,0,0));
		else
			transform.Translate(new Vector3(wanderSpeed*Time.deltaTime,0,0));
	}

	// void Wander(){		//用于测试 后期建议删掉
	// 	int rand = Random.Range(0,1);
	// 	transform.Translate(new Vector3(Mathf.Pow(-1,rand)*wanderSpeed*Time.deltaTime,0,0));		//随机一个方向走两步
	// }
	
	// Update is called once per frame
	void Update () {
		Die();
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(underAttack){
			MoveBack();
		}
	}
}
