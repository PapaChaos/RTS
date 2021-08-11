using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField]
    //Material[] material;
    public List<GameObject> navTargets;
    GameObject curNavTarget;
    //GameObject[] enemyTargets;
    [SerializeField]
    GameObject curCombatTarget;
    BaseStats stats;

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

    public Vector3 targetLocation; //pre set location for testing
    void Start()
    {
        targetLocation = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();

        stats = GetComponent<BaseStats>();
        
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
            else if(stats.owner == BaseInfo.Owner.player)
			{
                moveToLocation(targetLocation);

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
    
    void CheckForEnemies()
	{
        curCombatTarget = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.range);
        foreach(Collider hitCollider in hitColliders)
		{
            if(hitCollider.GetComponent<BaseStats>() != null)
			{

                if(hitCollider.GetComponent<BaseStats>().faction != stats.faction && hitCollider.GetComponent<BaseStats>().maxHealth > 0)
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
                target.GetComponent<BaseStats>().takeDamage(stats.damage);
                if (debugTesting)
                {
                    Debug.DrawLine(transform.position, target.transform.position, Color.red, 5f);
                }
            }
        }
    }
}
