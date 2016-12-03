using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Budget : MonoBehaviour {
    public double groceriesSpending;// index 0
    public double travelSpending;//1
    public double shopSpending;//2
    public double recSpending;//3
    public double houseSpending;//4
    public double miscSpending;//5

    int daysTillGoal;
    int monthsTillGoal;
    double incomeRate;
    int payPeriod; //in days
    bool isPossible;
    public double budgetValue;
    public List<List<double>> monthlyAllocations; //two dimentional array of allocations in dollars
    public List<List<double>> monthlySpending; //this list will keep track of how much spending we still can do per month

    public Budget(double gSpend, double tSpend, double sSpend, double rSpend, double hSpend, double mSpend, UserData user)//These are passed spending percentages
    {

        monthlyAllocations = new List<List<double>>();
        monthlySpending = new List<List<double>>();
        monthlyAllocations = BuildBudget( gSpend,  tSpend,  sSpend,  rSpend,  hSpend,  mSpend,  user);//temporary allocations
        if(monthlyAllocations.Count == 0)
        {
            isPossible = false;
            Debug.Log("Goal is Impossible");
        }
        //This next line will keep track of the origional percentages aloted;
        groceriesSpending = gSpend; travelSpending = tSpend; shopSpending = sSpend; recSpending = rSpend; houseSpending = hSpend; miscSpending = mSpend;
    }
    public List<List<double>> BuildBudget(double gSpend, double tSpend, double sSpend, double rSpend, double hSpend, double mSpend,UserData usr)
    {
        int days = daysToGoal(System.DateTime.Now.ToString("yyyy/MM/dd"), usr.goalDate);
        Debug.Log("Days To Goal = " + days);
        double gMax =  usr.balance + (usr.incomeRate*usr.hoursPerPayPeriod*(Math.Floor((double)days/usr.payPeriod)));//considering no expenses we would have this much money after the given period
        Debug.Log("Max budget = " + gMax);
        if (gMax < usr.goalBalance)
        {
            return monthlyAllocations; //return empty list
        }
        else
        {
            budgetValue = gMax - usr.goalBalance;
            int monthsgoal = monthsToGoal(System.DateTime.Now.ToString("yyyy/MM/dd"), usr.goalDate);//inproper algorithm here
            double gMaxPerMonth = gMax / monthsgoal;//to start we evenly divide the maxBudget by the number of months

            List<double> month = new List<double> { (gMaxPerMonth * gSpend), (gMaxPerMonth * tSpend), (gMaxPerMonth * sSpend), (gMaxPerMonth * rSpend), (gMaxPerMonth * hSpend), (gMaxPerMonth * mSpend) };
            List<double> spend = new List<double> { 0, 0, 0, 0, 0, 0 };
            for (int i =0; i< monthsgoal; i++)
            {
                monthlyAllocations.Add(month); //populate for all months
                monthlySpending.Add(spend); //populate empty spending list
            }
            return monthlyAllocations;
        }
           
        }
    // return monthlyAllocations; //return default allocations
    public int daysToGoal(string strDate, string goDate)
    {
        //yyyy/MM/dd
        DateTime start = Convert.ToDateTime(strDate);
        DateTime endDate = Convert.ToDateTime(goDate);
        int daysgoal = (endDate - start).Days;
        return daysgoal;
    }
    public int monthsToGoal(string startDate, string goalDate)
    {
        //yyyy/MM/dd
        DateTime start = Convert.ToDateTime(startDate);
        DateTime endDate = Convert.ToDateTime(goalDate);
        int monthstogoal = ((endDate.Year - start.Year) * 12) + endDate.Month - start.Month;
        return monthstogoal;
    }


    public double getTotal(List<List<double>> list)
    {
        double tot = 0;
        Debug.Log("List length = "+list.Count);
        for(int i =0; i < list.Count; i++)
        {
            List<double> listd = list[i];
            for(int j = 0; j<list[i].Count; j++)
            {
                Debug.Log("Value " + listd[j] +"total =" +tot);
                tot = tot + listd[j];
            }
        }
        return tot;
    }
}



