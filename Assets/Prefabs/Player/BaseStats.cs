using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseStats : BaseInfo
{
	public Stats stats;
	public float range, attackspeed, movementspeed;
	public int curHealth, maxHealth, armor, damage;
	NavMeshAgent NAVAgent;

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
		MeshFilter mesh = GetComponent<MeshFilter>();
		mesh.mesh = stats.mesh;
		updateMaterials();
		updateStats();
	}

	public void takeDamage(int damage)
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

	public void updateStats() //This is to make sure all necessary components change acording to stat updates.
	{
		NAVAgent.speed = movementspeed;
	}

}
