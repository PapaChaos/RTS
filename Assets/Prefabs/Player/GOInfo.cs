using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GOInfo : MonoBehaviour
{
	//References and variables
    public Faction faction;
    public Owner owner;
    public Stats stats;
	NavMeshAgent NAVAgent;

	//Stats
	public float range, attackspeed, movementspeed;
	public int curHealth, maxHealth, armor, damage;

	//Enums
	public enum Faction
	{
		unset,
		green,
		red,
		neutral
	};

	public enum Owner
	{
		npc,
		player,
		none
	}
	private void Awake()
	{
		NAVAgent = GetComponent<NavMeshAgent>();

	}


	public void StartingStats()
	{
		maxHealth = stats.maxHealth;
		curHealth = stats.maxHealth;
		armor = stats.armor;
		damage = stats.damage;
		range = stats.range;
		attackspeed = stats.attackspeed;
		movementspeed = stats.movementspeed;
		updateStats();
	}
	public void updateStats() //This is to make sure all necessary components change acording to stat updates.
	{
		NAVAgent.speed = movementspeed;
		//other stuff?
	}
	void takeDamage(int damage)
	{
		int dmg = damage - armor;
		if (dmg < 1)
		{
			dmg = 1;
		}
		curHealth -= dmg;
		if (curHealth <= 0)
		{
			Destroy(gameObject);
		}
	}




}
