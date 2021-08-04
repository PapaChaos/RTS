using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    Material[] material;
    public List<GameObject> navTargets;
    GameObject curNavTarget;
    GameObject curCombatTarget;

    [SerializeField]
    float targetNTDist;

    [SerializeField]
    enum Task
	{
        Moving,
        Combat,
        Idling,
	}
    Task task;

    Vector3 targetLocation = new Vector3(827f, 10f, 823f); //pre set location for testing
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();


        if (navTargets.Count < 1)
            moveToLocation(navTargets[0].transform.position);
    }
	void Update()
	{
        //if(task == Task.Moving)
        if (navTargets.Count > 0) {
            targetNTDist = Vector3.Distance(navTargets[0].transform.position, transform.position);
            if (targetNTDist < 5)
            {
                navTargets.Remove(navTargets[0]);
            }
            else
            {
                curNavTarget = navTargets[0];//debug testing mostly
                moveToLocation(navTargets[0].transform.position);
            }
        }

    }

	bool moveToLocation(Vector3 loc)
	{
        agent.SetDestination(loc);
        return agent.SetDestination(loc);
	}

    public void setInfo()
    {
        GOInfo goinfo = GetComponent<GOInfo>();
        if(goinfo.faction == GOInfo.Faction.green)
		{
            GetComponent<Renderer>().material = material[0];
        }
        if (goinfo.faction == GOInfo.Faction.red)
        {
            GetComponent<Renderer>().material = material[1];
        }
	}

}
