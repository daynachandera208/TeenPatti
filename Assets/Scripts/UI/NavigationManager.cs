using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
   
    public void ExitGame()
    {

        Application.LoadLevel("TablesAvailable");

    }
    public void LeaveMatch() {
        /* try
         {
             ColyseusClient.Instance.LeaveRoom(true);
         }
         catch(Exception e) {
             print("locha de " + e);
         }
         ColyseusClient.Instance.isForTableOnly = true;*/
        PlayerPrefs.SetFloat("chips", GameData.Instance.myChips);
        Application.LoadLevel("Tables");

    }
    public void ExitTablesPage()
    {
        Application.LoadLevel("Menu");
    }
    
    public void LoadServerTest() {
        Application.LoadLevel("TestServer");
    }
    public void PlayOnline() {
        
       // ColyseusClient.Instance.ConnectToServer();
        print("////"+ PlayerPrefs.GetString("room_ID")+"++++"+ PlayerPrefs.GetString("Session_ID"));
       /* if (PlayerPrefs.GetString("room_ID") == "" && PlayerPrefs.GetString("Session_ID") == "")
        {*/

            Application.LoadLevel("Tables");
      /*  }
        else
            StartCoroutine(reconnectGame());*/
    }
   /* public IEnumerator reconnectGame() {
        Application.LoadLevel("Tables");
        yield return new WaitForSeconds(3f);
       

    }*/
}
