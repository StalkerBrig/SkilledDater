using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq; // for Cast()
using UnityEditor.Experimental.GraphView;

public class UnityTools : MonoBehaviour
{

    [MenuItem("Tools/FCO/Compute Characters")]
    static void ComputeCharacters()
    {
        Debug.Log("ComputeCharacters: start");
        TextAsset asset = Resources.Load<TextAsset>("PassiveSkills/passiveSkillsStats"); // no extension
        string[] lines = asset.text.Split('\n'); // line separator, i.e. newline
        lines = lines.Skip(1).Take(lines.Length - 1 - 1).ToArray(); // remove header and last empty line
        foreach (string line in lines)
        {
            string[] cols = line.Split('\t'); // column separator, i.e. tabulation
            Debug.Log("col0: " + cols[0]); // will display first column for each line
        }


        SkillStatsSO[] skills = Resources.LoadAll("PassiveSkillsSOs", typeof(SkillStatsSO)).Cast<SkillStatsSO>().ToArray(); // load all "Skill" Scriptable Objects
        foreach (SkillStatsSO skill in skills)
        {
            //skill.name = id; // update any field
            //skill.slots = new List<string> { "a", "b" }; // even List
            EditorUtility.SetDirty(skill); // flag SO as dirty
        }
        Debug.Log("ComputeCharacters: saving db");
        AssetDatabase.SaveAssets(); // save assets to disk
        AssetDatabase.Refresh(); // refresh editor
        Debug.Log("ComputeCharacters: end");

    }

}


