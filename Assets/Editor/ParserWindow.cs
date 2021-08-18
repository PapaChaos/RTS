using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ParserWindow : EditorWindow
{
    string[] ParsingType = { "TextFile", "JSON" };

    [SerializeField]
    TextAsset TextParseTarget;

    string parseFileTargets = "UnitBaseStats";
    string spreadsheetFolderPath = "Spreadsheets/";
    string JSONFolderPath = "JSON/";

    string[] rows;

    int menuIndex = 0;

    [MenuItem("Window/Parser")]
	public static void ShowWindow()
	{
		GetWindow<ParserWindow>("Parser");
	}

    void OnGUI()
    {
        GUILayout.Label("Parser", EditorStyles.boldLabel);

        menuIndex = EditorGUILayout.Popup(menuIndex, ParsingType);

        if (menuIndex == 0) 
        {
            parseFileTargets = EditorGUILayout.TextField("UnitBaseStats");
            if (GUILayout.Button("Parse Text File"))
            {
                Parse();
            }
        }
        if(menuIndex == 1)
		{
            parseFileTargets = EditorGUILayout.TextField("Locale");
            if (GUILayout.Button("Parse JSON"))
            {
                Parse();
            }
        }
	}

    void Parse()
    {
            TextAsset TextParseTarget = Resources.Load<TextAsset>(spreadsheetFolderPath+parseFileTargets);

            rows = TextParseTarget.text.Split(new char[] {'\n'});


            for (int i = 1; i < rows.Length; i++)
            {
                string[] col = rows[i].Split(new char[] {','});

                float hp = 1;
                float ar = 0;
                float dmg = 0;
                float rng = 0;
                float ats = 1;
                float ms = 3;
                float uc = 0;

                string errorName = col[0];

                //Ternary Operator looks smoother and cleaner imo.
                bool parse = true ==
                float.TryParse(col[2], out hp) &&
                float.TryParse(col[3], out ar) &&
                float.TryParse(col[4], out dmg) &&
                float.TryParse(col[5], out rng) &&
                float.TryParse(col[6], out ats) &&
                float.TryParse(col[7], out ms) &&
                float.TryParse(col[8], out uc)
                ;

                if (parse)
                {
                    Stats stat = ScriptableObject.CreateInstance<Stats>();
                    stat.name = (col[0]);
                    stat.maxHealth = (int)hp;
                    stat.armor = (int)ar;
                    stat.damage = (int)dmg;
                    stat.range = rng;
                    stat.attackspeed = ats;
                    stat.movementspeed = ms;
                    stat.cost = (int)uc;
                    stat.generateAsset(stat);

                }
                else
                    Debug.LogError($"Parse failed! Error in columns on row {i + 1}/{errorName}!"); //Error on line 4 is on purpose for testing.
            }
    }
}
