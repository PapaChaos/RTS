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
    //GameObject[] enemyTargets;
    [SerializeField]
    GameObject curCombatTarget;
    GOInfo stats;

    [SerializeField]
    float targetNTDist;

    public bool debugTesting = true; 

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

        stats = GetComponent<GOInfo>();
        
        if (navTargets.Count > 0)
            moveToLocation(navTargets[0].transform.position);
		else
		{
            task = Task.Idling;
		}
    }
	void Update()
	{
        CheckForEnemies();
        if (task == Task.Moving)
        {
            agent.isStopped = false;
            StopCoroutine(AttackEnemy(curCombatTarget));
            if (navTargets.Count > 0)
            {
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
        if(task == Task.Combat)
		{
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            StartCoroutine(AttackEnemy(curCombatTarget));
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
    
    void CheckForEnemies()
	{
        curCombatTarget = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.range);
        foreach(Collider hitCollider in hitColliders)
		{
            if(hitCollider.GetComponent<GOInfo>() != null)
			{

                if(hitCollider.GetComponent<GOInfo>().faction != stats.faction && hitCollider.GetComponent<GOInfo>().attackable)
				{
                    curCombatTarget = hitCollider.gameObject;
                }

            }
		}
        if (curCombatTarget)
            task = Task.Combat;
		else
            task = Task.Moving;
    }
    IEnumerator AttackEnemy(GameObject target)
    {
        while (true)
        {
            yield return new WaitForSeconds(stats.attackspeed);
            if (target)
            {
                target.GetComponent<GOInfo>().takeDamage(stats.damage);
                if (debugTesting)
                {
                    Debug.DrawLine(transform.position, target.transform.position, Color.red, 5f);
                }
            }
        }
    }
}
