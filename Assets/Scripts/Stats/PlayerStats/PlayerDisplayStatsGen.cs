using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class DisplayStatsClass
{
    public TMP_Text statText;
    public StatTypes statEnum;
}

public class PlayerDisplayStatsGen : MonoBehaviour
{
    [SerializeField] private PlayerCurrentStatsSO statSO;

    //[SerializeField] private DisplayStatsClass[] displayStatsList;
    private List<DisplayStatsClass> displayStatsList = new List<DisplayStatsClass>();

    private void Start()
    {

        if (statSO != null) { UpdateStats(); }

        TMP_Text[] displayTmpTexts = Helper.FindComponentsInChildrenWithTag<TMP_Text>(this.gameObject, "DisplayStat");

        foreach (var tmpTexts in displayTmpTexts) 
        {
            try
            {
                //"Text_Display_" is expected before the Enum Stat name, or else this won't automatically get it
                string tmpStatName = tmpTexts.name.Replace("Text_Display_", "");
                //Gets Enum value based on the Stat Name
                StatTypes enumStatName = (StatTypes)System.Enum.Parse(typeof(StatTypes), tmpStatName);

                DisplayStatsClass tmpDispStats = new DisplayStatsClass();
                tmpDispStats.statText = tmpTexts;
                tmpDispStats.statEnum = enumStatName;

                displayStatsList.Add(tmpDispStats);
            }
            catch (Exception ex)
            {
                Debug.Log($"Issue while getting Display Text; skipping this TMP_Text object: {tmpTexts.name}. \r\nError: {ex}");
            }
        }

        //foreach (GameObject displayStatTag in GameObject.FindGameObjectsWithTag("DisplayStat"))

    }

    public void Update()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        foreach (var displayStats in displayStatsList)
        {
            if (displayStats.statEnum == StatTypes.className)
            {
                string curClassName = statSO.GetClassName();

                displayStats.statText.text = curClassName;
            }
            else
            {
                displayStats.statText.text = ((int)statSO.GetCurrentStatValue(displayStats.statEnum)).ToString();
            }
        }

    }

}
