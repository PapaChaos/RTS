using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IGUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] menus;

	int menuIndex = 0;
	public Player player;

	[SerializeField]
	Slider BGMSlider, SFXSlider;

	private void Awake()
	{
		ChangeMenu(menuIndex);

		BGMSlider.value = player.BGMVolume;
		SFXSlider.value = player.SFXVolume;
	}

	private void Update()
	{
		if (menuIndex == 1)
		{
			player.BGMVolume = BGMSlider.value;
			player.SFXVolume = SFXSlider.value;
		}
	}
	public void SaveSettings()
	{
		player.BGMVolume = BGMSlider.value;
		player.SFXVolume = SFXSlider.value;
		SaveData.SavePlayer(player);
		ChangeMenu(0);
	}

	public void ChangeMenu(int index)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if (i != index)
			{
				menus[i].SetActive(false);
			}
			else
			{
				menus[i].SetActive(true);
				menuIndex = i;
			}
		}
	}
}
