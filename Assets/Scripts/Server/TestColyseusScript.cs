using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestColyseusScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject messgageTxt;
    public GameObject iPMessageTxt;
    public void JoinServer()
    {
        ColyseusClient.Instance.ConnectToServer();
        messgageTxt.GetComponent<TextMeshProUGUI>().text = "Connected To server";
    }
    public void GetRooms()
    {
        ColyseusClient.Instance.GetAvailableRooms();
    }
    public void JOCRoom() { ColyseusClient.Instance.JoinOrCreateRoom();
        messgageTxt.GetComponent<TextMeshProUGUI>().text = "Connected To Room";
    }

    public void TestRoom()
    {
        // ColyseusClient.Instance.ConnectToServer();
        // ColyseusClient.Instance.JoinOrCreateRoom();
       messgageTxt.GetComponent<TextMeshProUGUI>().text = "My Id=:" + ColyseusClient.Instance.room.SessionId + " & Connected to room " + ColyseusClient.Instance.room.Id;
    }
    public void SendMSG()
    {
        ColyseusClient.Instance.room.Send("TestMessage", iPMessageTxt.GetComponent<TMP_InputField>().text);
    
        messgageTxt.GetComponent<TextMeshProUGUI>().text = "Message Sent To server";
    }
}
