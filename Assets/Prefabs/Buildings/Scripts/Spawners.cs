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

    public List<Stats> playerUnits;
    public List<Stats> squad;

    float spawnTimer;
    BaseStats thisinfo;
    // Start is called before the first frame update
    void Start()
    {
        thisinfo = GetComponent<BaseStats>();
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
        SpawnUnit(spawningLocation.transform.position, spawningLocation.transform.rotation, playerUnits[0]);
	}

    void SpawnUnit(Vector3 location, Quaternion rotation, Stats unitType)
	{
        GameObject refUnit = Instantiate(unit, location, rotation);

        BaseStats baseInfo = refUnit.GetComponent<BaseStats>();

        baseInfo.stats = unitType;
        baseInfo.faction = thisinfo.faction;

        baseInfo.StartingStats();
        if (!playerUnit && playerSpawner)
        {
            baseInfo.owner = BaseInfo.Owner.player;
            playerUnit = refUnit;
        }

        UnitController unitcontroller = refUnit.GetComponent<UnitController>();
        baseInfo.updateMaterials();
        unitcontroller.navTargets = wayPoints.ToList();
    }
}
