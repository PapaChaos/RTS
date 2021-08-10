using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressLocationParticleScript : MonoBehaviour
{
    public GameObject particle;
	public float timeItTakesToDeactivate = 0.5f;

	float deactivateTimer;
	bool deactivate;



	private void Awake()
	{
		deactivate = false;
		deactivateTimer = 0f;
		particle.SetActive(false);
	}
	public void StartParticle(Vector3 location)
	{
		deactivate = false;
		deactivateTimer = 0f;
		particle.SetActive(true);
		particle.transform.position = location;

	}
	private void Update()
	{
		deactivateTimer += Time.deltaTime;
		if (deactivateTimer >= timeItTakesToDeactivate)
			DeactivateParticle();
	}

	public void StopParticle()
	{

		deactivate = true;

	}
	public void DeactivateParticle()
	{
		particle.SetActive(false);
	}
}
