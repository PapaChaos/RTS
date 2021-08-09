using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquadUnitButton : MonoBehaviour
{
    public Text unitName;

    public FactionResources fr;
    public Stats unit;

    void removeUnit()
	{
        fr.squad.Remove(unit);
	}
}
