using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    [SerializeField]
    GameObject unit;

    [SerializeField]
    List<Stats> stat;

    [SerializeField]
    string spawnLocationTag;

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
            int randomIntfortesting = Random.Range(0, stat.Count);
            SpawnUnit(spawningLocation.transform.position, spawningLocation.transform.rotation, randomIntfortesting);
        }
    }

    void SpawnUnit(Vector3 location, Quaternion rotation, int unitType)
	{
        GameObject refUnit = Instantiate(unit, location, rotation);

        GOInfo baseInfo = refUnit.GetComponent<GOInfo>();
        baseInfo.stats = stat[unitType];
        baseInfo.faction = thisinfo.faction;
        baseInfo.StartingStats();

        UnitController unitcontroller = refUnit.GetComponent<UnitController>();
        unitcontroller.setInfo();
        unitcontroller.navTargets = wayPoints.ToList();

    }
}
