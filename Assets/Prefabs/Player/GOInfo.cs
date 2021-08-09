using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GOInfo : MonoBehaviour
{
	//References and variables
    public Faction faction;
    public Owner owner;
    public Stats stats;
	NavMeshAgent NAVAgent;
	public List<Material> facMaterial;

	public bool attackable = true;

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
		updateMaterials();
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
		updateStats();
		updateMaterials();
	}
	public void updateStats() //This is to make sure all necessary components change acording to stat updates.
	{
		NAVAgent.speed = movementspeed;
		//other stuff?
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
	void Update()
	{
		updateMaterials();
	}
	public void updateMaterials()
	{
		Renderer renderer = GetComponent<Renderer>();
		if (renderer)
		{
			Material[] mats = new Material[renderer.materials.Length];
			for (int i = 0; i < renderer.materials.Length; i++)
			{

				if (owner != Owner.player)
				{
					Material facmat = new Material(facMaterial[(int)faction]);
					mats[i] = facmat;
				}

				if (owner == Owner.player)
				{
					Material facmat = new Material(facMaterial[4]);
					mats[i] = facmat;
				}
			}
			renderer.materials = mats;
		}
	}


}
