using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
[System.Serializable]
public struct card
{
    public GameData.typesOfCard cardType;
    public int cardValue;
    public int cardPoints;
}
public struct Table
{
    public long minBet;
    public long maxBet;
    public string tableName;
    public long interval;

}
public class GameData : MonoBehaviour
{
     public TextAsset tableText;
   
    public GameTable[] onlineTables;
 
    private static long myChip;
    public bool isListining;
    public long myChips
    {
        get { return myChip; }
        set
        {
            GameData.myChip = value;
            if (value < 0) GameData.myChip = 0;
        }
        
    }
    private static long MyMoney;
    public long myMoney
    {
        get { return MyMoney; }
        set
        {
            GameData.MyMoney = value;
            if (value < 0) GameData.MyMoney = 0;
        }

    }

    public enum typesOfCard
    {
        Spades = 1,
        Clubs = 2,
        Hearts = 3,
        Diamonds = 4
    }

    public static GameData Instance;
    // Start is called before the first frame update
    void Start()
    {

        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetString("Name", "Tommy");
        PlayerPrefs.SetString("UserId", "123456789");
       // print("------------1234+"+ PlayerPrefs.GetFloat("chips"));
      //  if (PlayerPrefs.GetFloat("chips")==0)
        {
            PlayerPrefs.SetFloat("chips", 4000000);// 0000f);
            PlayerPrefs.SetFloat("cash", 500);
        }
     PlayerPrefs.SetString("room_ID","");
            PlayerPrefs.SetString("Session_ID", "");
        myChip = (long)PlayerPrefs.GetFloat("chips");
        MyMoney = (long)PlayerPrefs.GetFloat("cash");
        /* System.Random rnd = new System.Random();
         GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text = ""+remainingDeck.Count;
         int index = rnd.Next(Instance.remainingDeck.Count);
        card c= remainingDeck[index];
         GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text += " Got card " + c.cardValue + " of type " + c.cardType + " having card point " + c.cardPoints;
         remainingDeck.Remove(c);
         foreach (card cd in remainingDeck)
         {
             print("Card No"+cd.cardValue +"of type " + cd.cardType + " having card point " + cd.cardPoints);
         }*/
        if (myChip < 1000)
        {
            GameObject.Find("CashTitle").GetComponent<TextMeshProUGUI>().text += MyMoney.ToString();
        }
        else if (MyMoney >= 1000 && MyMoney < 1000000)
        {
            GameObject.Find("CashTitle").GetComponent<TextMeshProUGUI>().text += (MyMoney / 1000f).ToString() + "K";
        }
        else if (MyMoney >= 1000000 && MyMoney < 1000000000)
        {
            GameObject.Find("CashTitle").GetComponent<TextMeshProUGUI>().text += (MyMoney / 1000000f).ToString() + "M";

        }
        else
        {
            GameObject.Find("CashTitle").GetComponent<TextMeshProUGUI>().text += (MyMoney / 1000000000f).ToString() + "B";
        }
        if (myChip < 1000)
        {
            GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text += myChip.ToString();
        }
        else if (myChip >= 1000 && myChip < 1000000)
        {
            GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text += (myChip / 1000f).ToString() + "K";
        }
        else if (myChip >= 1000000 && myChip < 1000000000)
        {
            GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text += (myChip / 1000000f).ToString() + "M";
          
        }
        else
        {
            GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text += (myChip / 1000000000f).ToString() + "B";
        }
       // GameObject.Find("ChipsTitle").GetComponent<TextMeshProUGUI>().text += "" + myChip;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
   
     
    public string GetChipsString()
    {
        long amt = this.myChips;
        string str = "";
        if (amt < 1000) { str = amt.ToString(); }
        else if (amt >= 1000 && amt < 1000000)
        {

            str += (amt / 1000f).ToString() + "K";
        }
        else if (amt >= 1000000 && amt < 1000000000)
        {
            str += (amt / 1000000f).ToString() + "M";
        }
        else
        {
            str += (amt / 1000000000f).ToString() + "B";
        }
        return str;
    }
    public void SaveOnlineGameData(long money=0,long chips=0,string sessionId="",string roomId="",bool clear=false)
    {
        if(money!=0)
        this.myChips = chips;
        if(chips!=0)
        this.myMoney = money;
        PlayerPrefs.SetFloat("chips",this.myChips);
        PlayerPrefs.SetFloat("cash", this.myMoney);
        if(sessionId!="")
        PlayerPrefs.SetString("Session_ID",sessionId);
        if(roomId!="")
        PlayerPrefs.SetString("room_ID",roomId);
        if (clear==true) {
            PlayerPrefs.SetString("room_ID","");
            PlayerPrefs.SetString("Session_ID", "");

        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("chips", GameData.Instance.myChips);
    }
    public string GetAmtStr(long amt)
    {
        string str = "";
        if (amt < 1000) { str = amt.ToString(); }
        else if (amt >= 1000 && amt < 1000000)
        {

            str += (amt / 1000f).ToString() + "K";
        }
        else if (amt >= 1000000 && amt < 1000000000)
        {
            str += (amt / 1000000f).ToString() + "M";
        }
        else
        {
            str += (amt / 1000000000f).ToString() + "B";
        }
        return str;
    }
}
