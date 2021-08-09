using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FactionResources : MonoBehaviour
{
    public int metal, metalGain, oil, oilGain, metalCost;
    public List<Stats> squad;
    public int squadCost;
    public List<Spawners> spawners;
    float resourceTickTime = 2f;
    List<ResourceNode> resourceNodes;

    // Start is called before the first frame update
    void Start()
    {
        metal = 50;
        oil = 0;
        metalGain = 5;
        oilGain = 0;
        metalCost = 100;
        StartCoroutine(ResourceTick());

        ResourceNode[] rn = (ResourceNode[])FindObjectsOfType(typeof(ResourceNode));
        resourceNodes = rn.ToList();

    }
    IEnumerator ResourceTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(resourceTickTime);
            metal += metalGain;
            oil += oilGain;
            SpawnUnits();
        }
    }

    void SpawnUnits()
	{
        squadCost = 0;
        if (spawners.Count > 0)
        {

            foreach (Stats unit in squad)
            {
                squadCost += unit.cost;

            }

            if (metal >= squadCost)
            {
                metal -= squadCost;

                foreach (Spawners spawner in spawners)
                {
                    if (spawner)
                    {
                        spawner.squad = squad;
                        StartCoroutine(spawner.SpawnSquad());
                    }
                }

            }
        }
	}
	private void Update()
	{
        ResourceUpdate();
	}
	public void ResourceUpdate()
	{
        metalGain = 5;
        oilGain = 0;
        foreach(ResourceNode rn in resourceNodes)
		{
            if (rn.GetComponent<GOInfo>().faction == GetComponent<GOInfo>().faction)
            {
                metalGain += rn.metalGain;
                oilGain += rn.oilGain;
            }
		}
	}
}
