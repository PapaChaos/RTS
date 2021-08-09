using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_FactionInfo : MonoBehaviour
{
    [SerializeField]
    Text metalText, metalGainText;

    public FactionResources factionResources;

    [SerializeField]
    List<FactionResources> otherPlayers;

    [SerializeField]
    Text losewin;

    public string win = "You Win!";
    public string lose = "You Lose!";

    GOInfo goinfo;
	private void Awake()
	{
        goinfo = GetComponent<GOInfo>();
        FactionResources[] fra = (FactionResources[])FindObjectsOfType(typeof(FactionResources));
        foreach(FactionResources fr in fra)
		{
            if(fr.GetComponent<GOInfo>().faction == GetComponent<GOInfo>().faction)
			{
                factionResources = fr;
			}
			else
			{
                otherPlayers.Add(fr);
			}
		}

	}
	// Update is called once per frame
	void Update()
    {
        metalText.text = "Metal:"+factionResources.metal.ToString();
        metalGainText.text = "Metal Gain: +"+factionResources.metalGain.ToString();

        if(!factionResources)
		{
            losewin.text = lose;
        }
        foreach(FactionResources otherplayer in otherPlayers)
		{
            if (!otherplayer)
                otherPlayers.Remove(otherplayer);
		}
        if(otherPlayers.Count == 0)
		{
            losewin.text = win;
		}
    }
}
