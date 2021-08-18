using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandling : MonoBehaviour
{
    AudioSource source;

    public Player player;

	public enum AudioType
	{
		SFX,
		BGM,
		Voice
	}
	public AudioType audioType;

	private void Awake()
	{
        source = GetComponent<AudioSource>();

	}

	private void Update() //temp to I fix a call system
	{
		if (audioType == AudioType.BGM)
			source.volume = player.BGMVolume;

		if (audioType == AudioType.SFX)
			source.volume = player.SFXVolume;
	}
}
