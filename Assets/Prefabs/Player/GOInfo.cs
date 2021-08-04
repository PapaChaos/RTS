using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOInfo : MonoBehaviour
{
    public enum Faction
	{
        unset,
        green,
        red,
        neutral
	};

    public enum Owner
	{
        npc,
        player,
        none
	}
    public Faction faction;
    public Owner owner;

}
