using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CreateAssetMenu(fileName = "new Stat", menuName = "Stat")]
#endif
public class Stats : ScriptableObject
{
	public enum typeOfObject
	{
		Unit,
		Building
	}

	public typeOfObject typeofobject;
	#if UNITY_EDITOR
	string unitPath = "Assets/Data/Stats/Units/";
	string buildingPath = "Assets/Data/Stats/Buildings/";
#endif

	public new string name;
	public Mesh mesh;
	public int maxHealth, armor, damage, cost;
	public float range, attackspeed, movementspeed;


	#if UNITY_EDITOR
	public void generateAsset(Stats asset)
	{
		string filePath = "/";
		if (typeofobject == typeOfObject.Unit)
			filePath = unitPath + name + ".asset";
		else if (typeofobject == typeOfObject.Building)
			filePath = buildingPath + name + ".asset";

		//must be a cleaner way to do this?
		AssetDatabase.DeleteAsset(filePath);
		AssetDatabase.CreateAsset(asset, filePath);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		//Maybe this is going to be bothering.
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
	#endif
}
