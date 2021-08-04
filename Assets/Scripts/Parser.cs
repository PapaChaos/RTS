using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Parser : MonoBehaviour
{
    [SerializeField]
    TextAsset TextParseTarget;
    public List<Stats> unitsStats = new List<Stats>();
    public List<string> unitName;
    public List<float> maxHealth, armor, damage, range, attackSpeed, movementSpeed;

    string[] rows;

    void Awake()
    {
        unitName.Clear();
        maxHealth.Clear();
        armor.Clear();
        damage.Clear();
        range.Clear();
        TextAsset TextParseTarget = Resources.Load<TextAsset>("Spreadsheets/UnitBaseStats");

        rows = TextParseTarget.text.Split(new char[] { '\n' });


        for (int i = 1; i < rows.Length; i++)
        {
            string[] col = rows[i].Split(new char[] { ',' });

            //work on later. Need to change editorgui for lists.
            /*Stats unitStat = new Stats();
            unitStat.name = col[0];
            unitsStats.Add(unitStat)*/

            float hp = 1;
            float ar = 0;
            float dmg = 0;
            float rng = 0;
            float ats = 1;
            float ms = 3;

            string errorName = col[0];
            bool parse = true;

            parse = float.TryParse(col[1], out hp);
            if (parse)
                parse = float.TryParse(col[2], out ar);
            if (parse)
                parse = float.TryParse(col[3], out dmg);
            if (parse)
                parse = float.TryParse(col[4], out rng);
            if (parse)
                parse = float.TryParse(col[5], out ats);
            if (parse)
                parse = float.TryParse(col[6], out ms);

            if (parse)
            {
                unitName.Add(col[0]);
                maxHealth.Add(hp);
                armor.Add(ar);
                damage.Add(dmg);
                range.Add(rng);
                attackSpeed.Add(ats);
                movementSpeed.Add(ms);
            }
            else
                Debug.LogError($"Parse failed! Error in columns on row {i+1}/{errorName}!"); //Error on line 4 is on purpose for testing.
        }
    }
}
