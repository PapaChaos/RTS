using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	string trainingScenePath;


	public void StartGame()
	{

	}
	public void Training() 
	{
		SceneManager.LoadScene(trainingScenePath, LoadSceneMode.Single);
	}
	public void Settings()
	{

	}
}
