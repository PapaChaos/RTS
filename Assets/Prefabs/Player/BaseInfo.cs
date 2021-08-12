using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfo : MonoBehaviour
{
	public enum Faction
	{
		neutral,
		green,
		red,
	};

	public enum Owner
	{
		npc,
		player,
	}
	public Faction faction;
	public Owner owner;
	public List<Material> facMaterial;

	private void Start()
	{
		updateMaterials();
	}

	public void updateMaterials()
	{
		Renderer renderer = GetComponent<Renderer>();

		if (renderer)
		{
			if (renderer.materials.Length > 0)
			{
				Material[] mats = new Material[renderer.materials.Length];

				for (int i = 0; i < mats.Length; i++)
				{

					if (owner != Owner.player)
					{
						Material facmat = new Material(facMaterial[(int)faction]);
						if (i == 0)
							mats[i] = facmat;
						else
							mats[i] = renderer.materials[i];

					}

					if (owner == Owner.player)
					{
						Material facmat = new Material(facMaterial[3]);

						if (i == 0)
							mats[i] = facmat;
						else
							mats[i] = renderer.materials[i];

						//mats[i] = facmat;
					}
				}
				renderer.materials = mats;
			}
		}
	}

}
