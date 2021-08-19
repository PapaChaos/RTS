using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
	[SerializeField]
	string trainingScenePath;

	[SerializeField]
	Player player;

	public int menuIndex = 0;

	public GameObject[] menus; 

	[SerializeField]
	InputField nameField;

	[SerializeField]
	Slider BGMVSlider, SFXVSlider;

	[SerializeField]
	InterstitialAd interstitialAd;

	[SerializeField]
	BannerAd bannerAd;

	public void StartGame()
	{
		if (!player.firstTimePlay)
		{
			bannerAd.HideBannerAd();
			interstitialAd.LoadAd();
		}
		else
		{
			player.firstTimePlay = false;
			SaveData.SavePlayer(player);
			startGameScene();
		}
	}
	public void Training() 
	{
		if (!player.firstTimePlay)
		{
			bannerAd.HideBannerAd();
			if (Application.isEditor)
				trainingScene();
			else
			interstitialAd.LoadAd();
		}

		else
		{
			player.firstTimePlay = false;
			SaveData.SavePlayer(player);
			trainingScene();
		}

	}
	private void Awake()
	{
		LoadPlayer();
		SFXVSlider.value = player.SFXVolume;
		BGMVSlider.value = player.BGMVolume;
	}

	public void SavePlayer()
	{
		SaveData.SavePlayer(player);
	}
	public void LoadPlayer()
	{
		string path = Application.persistentDataPath + "/player.dat";
		if (File.Exists(path))
		{
			PlayerData data = SaveData.LoadPlayer();

				player.firstTimePlay = data.firstTimePlay;
				player.BGMVolume = data.BGMVolume;
				player.SFXVolume = data.SFXVolume;
				player.PlayerNick = data.PlayerNick;
			changeMenu(0);

		}
		else
		{
			changeMenu(2);
		}

	}

	public void changeMenu(int index)
	{
		for(int i = 0; i < menus.Length; i++)
		{
			if(i != index)
			{
				menus[i].SetActive(false);
			}
			else
			{
				menus[i].SetActive(true);
			}
		}
	}

	public void setName()
	{
		if (nameField.text.Length > 2)
		{
			player.PlayerNick = nameField.text;
			SavePlayer();
			changeMenu(0);
		}

	}

	public void SaveSettings()
	{
		player.BGMVolume = BGMVSlider.value;
		player.SFXVolume = SFXVSlider.value;
		SavePlayer();

		changeMenu(0);
	}

	public void setBGMValue()
	{
		player.BGMVolume = BGMVSlider.value;
	}

	public void setSFXValue()
	{
		player.SFXVolume = SFXVSlider.value;

	}

	public void trainingScene()
	{
		SceneManager.LoadScene(trainingScenePath, LoadSceneMode.Single);
	}
	void startGameScene()
	{
		//change this later when we have finished the tutorial and added an actual game mode.
		SceneManager.LoadScene(trainingScenePath, LoadSceneMode.Single);
	}
}
