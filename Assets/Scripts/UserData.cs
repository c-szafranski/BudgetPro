using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[Serializable]
public class UserData : MonoBehaviour {

    public string username;
    public string password;
    public double balance;
    public double goalBalance;
    public string goalDate;
    public bool hasGoal;
    public double incomeRate;
    public int payPeriod;
    public int hoursPerPayPeriod;


    public UserData(string userN, string pass, double bal, double goalVal, string date)
    {
        this.username = userN;
        this.password = pass;
        this.balance = bal;
        this.goalBalance = goalVal;
        this.goalDate = date;
        if (date.Equals(""))
        {
            this.hasGoal = false;
        }
    }
    public UserData(string userN, string pass, double bal,double incomer,int hoursPP,int PP, double goalVal, string date)
    {
        this.username = userN;
        this.password = pass;
        this.balance = bal;
        this.goalBalance = goalVal;
        this.goalDate = date;
        this.payPeriod = PP;
        this.hoursPerPayPeriod = hoursPP;
        this.incomeRate = incomer;
        if (date.Equals(""))
        {
            this.hasGoal = false;
        }
    }

}
