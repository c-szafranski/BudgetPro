using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Day : MonoBehaviour {
    string month;
    string day;
    public string date;
    string year;
    string dateFull;
	void Start()
    {
        checkIfGoal();
    }
    public void checkIfGoal()
    {
        for (int i = 0; i < StaticValues.goal.Count; i++)
        {
            
            if (StaticValues.goal[i].date.Equals(date))
            {
                this.GetComponent<Button>().GetComponent<Image>().color = new Color(1f, 100 / 255f, 100 / 255f);
                GetComponent<Button>().GetComponentInChildren<Text>().text = GetComponent<Button>().GetComponentInChildren<Text>().text + " Goal";
                
                break;

            }
            
        }
       // return isGoal;
    }
}
