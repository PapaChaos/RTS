using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Locale
{
    public List<string> en;
    public List<string> no;
    public List<string> de;
    public List<string> zh_hans;
    public List<string> rus;

    //string json = JsonUtility.ToJson(myObject);
    //myObject = JsonUtility.fromJson<MyClass>(json);

    public static Locale CreateFromJson(string jsonString)
	{
        return JsonUtility.FromJson<Locale>(jsonString);
	}

}
