using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
	public bool firstTimePlay;
	public float BGMVolume;
	public float SFXVolume;
	public string PlayerNick;


	public PlayerData(Player player)
	{
		firstTimePlay = player.firstTimePlay;
		BGMVolume = player.BGMVolume;
		SFXVolume = player.SFXVolume;
		PlayerNick = player.PlayerNick;
	}
}
