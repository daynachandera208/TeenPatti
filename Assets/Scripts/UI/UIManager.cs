using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static GameObject LoadingScreen;
    public static GameObject LoadingBar;
    public static GameObject LoadingPrecentageTxt;
    public static int loadingEndPoint;
    public static void ActiveBtns()
    {
        LoadingScreen.SetActive(false);
        print("ActiveBtns -"+ PlayerPrefs.GetString("room_ID"));
        if (PlayerPrefs.GetString("room_ID") == "")
        {
            GameObject.Find("BtnTable3").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            GameObject.Find("BtnTable2").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            GameObject.Find("BtnTable1").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        }
        else {

            ColyseusClient.Instance.ReconnectRoom();
        }
    }
     void Start()
    {
        LoadingScreen = GameObject.Find("LoadingScreen");
        LoadingBar = GameObject.Find("LoadingBar");
        LoadingPrecentageTxt = GameObject.Find("LoadingPercantage");
        loadingEndPoint = 1;
        print("---"+loadingEndPoint+" lbar"+LoadingBar);
        
        GameObject.Find("UaerName").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("Name");
        GameObject.Find("UserId").GetComponent<TextMeshProUGUI>().text = "id: " + PlayerPrefs.GetString("UserId");
        GameObject.Find("MyChipsTxt").GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetChipsString();
        GameObject.Find("MyMoneyTxt").GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetAmtStr(GameData.Instance.myMoney);

        /*if (PlayerPrefs.GetInt("isoffline") == 1)
        {
            loadingEndPoint = 100;
            StartCoroutine(GetLoading());
            GameData.Instance.LoadTables();
       //   Invoke( "GameData.Instance.LoadTables()",2f);
        }
        else*/
        {
            StartCoroutine( ColyseusClient.Instance.LoadServerTables());
        }
    }
 /*   public void StartTable1()
    {
        int index = 0;
        if (GameData.Instance.myChips >= GameData.Instance.tables[index].minBet)
        {
            PlayerPrefs.SetInt("currentTable", index);
            // print("Current LevelDetails=" +PlayerPrefs.GetInt("currentTable").ToString());

            Application.LoadLevel("GameTableOfline");
        }
    }

   
    public void StartTabl2()
    {
        int index = 1;
        if (GameData.Instance.myChips >= GameData.Instance.tables[index].minBet)
        {
            PlayerPrefs.SetInt("currentTable", index);
            // print("Current LevelDetails=" +PlayerPrefs.GetInt("currentTable").ToString());

            Application.LoadLevel("GameTableOfline");
        }
    }*/
    public void StartTable(int index)
    {
       
             if (GameData.Instance.myChips >=(long) GameData.Instance.onlineTables[index].startingBet)
             {
            GameObject.Find("BtnTable3").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("BtnTable2").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
            GameObject.Find("BtnTable1").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;

            PlayerPrefs.SetInt("currentTable", index);
                ColyseusClient.Instance.isForGame = true;
                GameData.Instance.isListining = true;
                StartCoroutine(ColyseusClient.Instance.JoinTable(index+1));
             }
        
    }
    public static IEnumerator GetLoading() {
        while (LoadingBar.GetComponent<Slider>().value < loadingEndPoint)
        {
            LoadingBar.GetComponent<Slider>().value++;

            LoadingPrecentageTxt.GetComponent<TextMeshProUGUI>().text = LoadingBar.GetComponent<Slider>().value.ToString()+"%";
            yield return new WaitForEndOfFrame();
        }
        if (PlayerPrefs.GetInt("isoffline") == 1)
        {
            GameObject.Find("LoadingScreen").SetActive(false);
            GameObject.Find("BtnTable3").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            GameObject.Find("BtnTable2").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
            GameObject.Find("BtnTable1").transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
