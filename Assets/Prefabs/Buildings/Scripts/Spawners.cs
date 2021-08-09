using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Spawners : MonoBehaviour
{
    [SerializeField]
    GameObject unit;

    [SerializeField]
    GameObject spawningLocation;

    [SerializeField]
    bool playerSpawner = false;
    
    public GameObject playerUnit;

    [SerializeField]
    GameObject[] wayPoints;

    public List<Stats> squad;

    float spawnTimer;
    GOInfo thisinfo;
    // Start is called before the first frame update
    void Start()
    {
        thisinfo = GetComponent<GOInfo>();
    }

    public IEnumerator SpawnSquad()
	{
        if(!playerSpawner)
        for(int u = 0; u < squad.Count; u++ )
		{
            SpawnUnit(spawningLocation.transform.position, spawningLocation.transform.rotation, squad[u]);
            yield return new WaitForSeconds(0.5f);
        }
	}
	private void Update()
	{
		if (!playerUnit && playerSpawner)
		{
            spawnPlayerUnit();
		}
	}
	public void spawnPlayerUnit()
	{
        SpawnUnit(spawningLocation.transform.position, spawningLocation.transform.rotation, squad[0]);
	}

    void SpawnUnit(Vector3 location, Quaternion rotation, Stats unitType)
	{
        GameObject refUnit = Instantiate(unit, location, rotation);

        GOInfo baseInfo = refUnit.GetComponent<GOInfo>();

        baseInfo.stats = unitType;
        baseInfo.faction = thisinfo.faction;

        baseInfo.StartingStats();
        if (!playerUnit && playerSpawner)
        {
            baseInfo.owner = GOInfo.Owner.player;
            //NavMeshAgent agent = refUnit.GetComponent<NavMeshAgent>();
            //agent.
            playerUnit = refUnit;
        }

        UnitController unitcontroller = refUnit.GetComponent<UnitController>();
        unitcontroller.setInfo();
        unitcontroller.navTargets = wayPoints.ToList();


    }
}
