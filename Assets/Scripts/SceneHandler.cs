using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Linq;


public class SceneHandler : MonoBehaviour {
    //public static SceneHandler handler;
    int currentScene;
    public GameObject calenderDay;
    public GameObject overviewPanel;
    public GameObject goalPanel;
    public GameObject enableGoalPanelButton;
    public GameObject enableOverviewPanelButton;
    public GameObject transactionIncome;
    public GameObject transactionExpense;
    public GameObject balanceField;
    public GameObject goalByDateField;
    public GameObject remainingBudget;
    public GameObject[] chartOne;
    public GameObject[] chartTwo;
    public GameObject[] chartThree;
    Color activeButton;
    Color inactiveButton;
    public GameObject titleText;
    string month ="";
    string day = "";
    string date = "";
    string year = "";
    string username;
    string password;
    double userBalance;
    double userGoalBalance;
    string userGoalDate;
   
    // Use this for initialization
    void Start () {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(Application.persistentDataPath + "/playerInfo.dat");
           // titleText = GameObject.FindWithTag("TitleBar");
            date = System.DateTime.Now.ToString("yyyy/MM/dd");
            month = parseDate(date);
            inactiveButton = new Color(0.01f, 70 / 255f, 160 / 255f, 255 / 255f);
            activeButton = new Color(0.01f, 120 / 255f, 255 / 255f, 255 / 255f);
            Debug.Log(date);
            //calenderDay = GameObject.FindWithTag("DayOnCalender");
            calenderBuilder();
            Goal g = new Goal();
            g.date = date;
            StaticValues.goal.Add(g);
            //overviewPanel = GameObject.FindWithTag("OverviewPanelTag");
           // goalPanel = GameObject.FindWithTag("GoalPanelTag");
            goalPanel.SetActive(false);
        titleText.GetComponent<Text>().text = "Set Goal panel to false";
           // enableGoalPanelButton = GameObject.FindWithTag("toggleGoalPanel");
            enableGoalPanelButton.GetComponent<Button>().GetComponent<Image>().color = inactiveButton;
           // enableOverviewPanelButton = GameObject.FindWithTag("toggleOverviewPanel");
            enableOverviewPanelButton.GetComponent<Button>().GetComponent<Image>().color = activeButton;
        UserData user = new UserData("C_szafranski", "Password", 2432.12, 27.5,40,14,5324.33, "2018/09/01");
        Budget budget = new Budget(.3, .1, .4, .1, .05, .05, user);
        balanceField.GetComponent<Text>().text = "$" + user.balance;
        goalByDateField.GetComponent<Text>().text = "$" + user.goalBalance;
        remainingBudget.GetComponent<Text>().text = "$" + budget.budgetValue;
        for(int i=0; i<chartOne.Length; i++)
        {
            List<double> list = budget.monthlyAllocations[0];
            float yscale1 = (float)(budget.monthlyAllocations[0][i] / budget.budgetValue);
            float yscale2 = (float)(budget.monthlyAllocations[1][i] / budget.budgetValue);
            float yscale3 = (float)(budget.monthlyAllocations[3][i] / budget.budgetValue);
            chartOne[i].GetComponent<Image>().transform.localScale = new Vector3(1, yscale1, 0);
            chartTwo[i].GetComponent<Image>().transform.localScale = new Vector3(1, yscale2, 0);
            chartThree[i].GetComponent<Image>().transform.localScale = new Vector3(1, yscale3, 0);

        }
    }
	public void setBudgetValues(Budget bud) 
    {
        
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void loadNextLevel()
    {
        currentScene += 1;
        SceneManager.LoadScene(currentScene);
    }
    public void enableGoalPanel()
    {
        //titleText.GetComponent<Text>().text = "Dissabling OverviewPanel";
        overviewPanel.SetActive(false);
        goalPanel.SetActive(true);
        enableGoalPanelButton.GetComponent<Button>().GetComponent<Image>().color = activeButton;
        enableOverviewPanelButton.GetComponent<Button>().GetComponent<Image>().color = inactiveButton;
    }
    public void enableOverviewPanel()
    {
        //titleText.GetComponent<Text>().text = "Enabling Overview Panel";
        goalPanel.SetActive(false);
        overviewPanel.SetActive(true);
        enableGoalPanelButton.GetComponent<Button>().GetComponent<Image>().color = inactiveButton;
        enableOverviewPanelButton.GetComponent<Button>().GetComponent<Image>().color = activeButton;
    }
    void calenderBuilder()
    {
        calenderDay.GetComponent<Button>().GetComponentInChildren<Text>().text = "" + 1;
        string dateAndTimeVar = System.DateTime.Now.ToString("yyyy/MM/dd"); //Get the current date to build an appropriete calender
        int numberOfDays = 0;
        if (month.Equals("January")|| month.Equals("March") || month.Equals("May") || month.Equals("July") || month.Equals("August") || month.Equals("October") || month.Equals("December"))
        {
            numberOfDays = 31;
        }
        if (month.Equals("February"))
        {
            numberOfDays = 28;
        }
        else
        {
            numberOfDays = 30;
        }
        for(int i = 0; i<numberOfDays - 1; i++)
        {
            GameObject ob = Instantiate(calenderDay, transform.position, Quaternion.identity) as GameObject;
            ob.transform.SetParent(GameObject.FindWithTag("CalenderPanel").transform);
            ob.transform.localScale = new Vector3(1, 1, 1);
            ob.transform.localPosition = Vector3.zero;
            Day dayScript = (Day)ob.GetComponent(typeof(Day));
            //Debug.Log("Date is = " + date + " Has Length= "+ date.Length);
            string daytemp = ""+ (i + 1);
            //Debug.Log("Day is " + daytemp);
            if (Int32.Parse(daytemp)< 9)
            {
                daytemp = "0"+daytemp;
            }
            dayScript.date=date.Substring(0,8)+ daytemp;
             //dayScript.checkIfGoal();
            Debug.Log("Day is = " + dayScript.date);
            ob.GetComponent<Button>().GetComponentInChildren<Text>().text = ""+(i+2);
            
        }
        //Destroy(calenderDay);


    }
    string parseDate(string date)
    {
        year = date.Substring(0, 3);//get year
        month = StaticValues.months[Int32.Parse(date.Substring(5, 2))-1]; //get month
        Debug.Log("Year= " + year + " Month = " + month);

        return month;
    }
    public void SaveUserInfo(string userN, string pass, double bal, double goalVal, string date)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        UserData pl = new UserData(userN, pass,bal, goalVal, date);
     
        bf.Serialize(file, pl);
        file.Close();

    }
    public void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            UserData data = (UserData)bf.Deserialize(file);
            file.Close();

            username = data.username;
            password = data.password;
        }
    }
    public void LoadTransactionData()
    {
        
        if (File.Exists(Application.persistentDataPath + "/transactionData.csv"))
        {
            Debug.Log("File Located");
            StreamReader file = new StreamReader(Application.persistentDataPath + "/transactionData.csv");
            while (!file.EndOfStream)
            {
                string input = file.ReadLine();

            }
            file.Close();
        }
    }
    
    [Serializable]
    class Transaction
    {
        public string rawTransaction;
        public int category;
        public double value;
        public string date;
    }
    [Serializable]
    class TransactionOverview
    {
        public double groceriesSpending;
        public double travelSpending;
        public double shopSpending;
        public double recSpending;
        public double houseSpending;
        public double miscSpending;
        public double income;

    }
    public void getTransactionData()
    {
        string address = "http://ec2-35-164-79-66.us-west-2.compute.amazonaws.com/userGet";
        WebRequest request = WebRequest.Create(address);
        request.Credentials = CredentialCache.DefaultCredentials;
        WebResponse response = request.GetResponse();
        Debug.Log(((HttpWebResponse)response).StatusDescription);
    }
}
