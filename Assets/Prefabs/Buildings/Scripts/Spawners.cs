using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField]
    GameObject unit;

    [SerializeField]
    string spawnLocationTag;

    [SerializeField]
    Parser statlist;

    [SerializeField]
    GameObject spawningLocation;

    [SerializeField]
    GameObject[] wayPoints;

    GOInfo thisinfo;
    // Start is called before the first frame update
    void Start()
    {
        spawningLocation = GameObject.FindGameObjectWithTag(spawnLocationTag);
        thisinfo = GetComponent<GOInfo>();
        StartCoroutine(spawnTimer());
    }

    IEnumerator spawnTimer()
    {
        while (true) 
        { 
            yield return new WaitForSeconds(5f);
            int randomIntfortesting = Random.Range(0, 3);
            SpawnUnit(spawningLocation.transform.position, spawningLocation.transform.rotation, randomIntfortesting);
        }
    }

    void SpawnUnit(Vector3 location, Quaternion rotation, int unitType)
	{
        GameObject refUnit = Instantiate(unit, location, rotation);
        Stats unitStats = refUnit.GetComponent<Stats>();
        unitStats.unitName = statlist.unitName[unitType];
        unitStats.maxHealth = statlist.maxHealth[unitType];
        unitStats.curHealth = unitStats.maxHealth;
        unitStats.armor = statlist.armor[unitType];
        unitStats.damage = statlist.damage[unitType];
        unitStats.range = statlist.range[unitType];
        unitStats.attackspeed = statlist.attackSpeed[unitType];
        unitStats.movementspeed = statlist.movementSpeed[unitType];
        unitStats.updateStats();
        refUnit.name = unitStats.name;

        UnitController unitcontroller = refUnit.GetComponent<UnitController>();
        unitcontroller.navTargets = wayPoints.ToList();
        //unitcontroller.faction = faction;

        GOInfo baseInfo = refUnit.GetComponent<GOInfo>();
        baseInfo.faction = thisinfo.faction;
        unitcontroller.setInfo();

    }
}
