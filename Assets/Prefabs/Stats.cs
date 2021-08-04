using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{

	public float curHealth = 1, maxHealth = 1, armor = 1, damage = 0, range = 0, attackspeed = 1, movementspeed = 3;
    public string unitName;
	NavMeshAgent NAVAgent;

	private void Awake()
	{
		NAVAgent = GetComponent<NavMeshAgent>();
	}

	private void Update()
	{
		//takeDamage(0.01f); //damage test
	}
	void takeDamage(float damage)
	{
		float dmg = damage - armor;
		if(dmg < 0.01f)
		{
			dmg = 0.01f;
		}
		curHealth -= (dmg);
		if(curHealth <= 0)
		{
			Destroy(gameObject);
		}
	}
	public void updateStats() //This is to make sure all necessary components change acording to stat updates.
	{
		NAVAgent.speed = movementspeed;
		//other stuff?
	}
}
