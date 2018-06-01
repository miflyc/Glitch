using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	public int atkPower;
	List<GameObject> enemies;		//创建一个空数组

	// Use this for initialization
	void Start () {

	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		enemies = new List<GameObject>();		//初始化一个空数组
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerStay2D(Collider2D other)									//这里有严重的问题，只有对方在Trigger中移动才能触发TriggerStay
	{																		//我使用了怪物被攻击会后退的设定，不知道ok不ok
		if(other.tag == "Enemy" & !enemies.Contains(other.gameObject)){		//这个问题已经解决了，需要把怪物rigidbody那里改成“Continous”
			enemies.Add(other.gameObject);									//要把所有的可以被攻击到的敌人加入数组
			//Debug.Log("Enemy found");
		}
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		//Debug.Log("AtkField disabled");
		if(enemies.Count>0){
			foreach (GameObject i in enemies)
			{
				Monster mon = i.GetComponent<Monster>();
				mon.Attacked(atkPower);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
