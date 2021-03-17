using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

using System.Threading;
using System.Threading.Tasks;

using Colyseus;
using Colyseus.Schema;

using GameDevWare.Serialization;
using System.Collections;
using UnityEngine.SceneManagement;
//client script for connection and mgmt of server connectivity
[Serializable]
class Metadata
{
	public string str;
	public int number;
}

[Serializable]
class CustomRoomAvailable : RoomAvailable
{
	public Metadata metadata;
}

class CustomData
{
	public int integer;
	public string str;
}

class TypeMessage
{
	public bool hello;
}

enum MessageType
{
	ONE = 0
};
class MessageByEnum
{
	public string str;
}

public class ColyseusClient : MonoBehaviour
{

	// UI Buttons are attached through Unity Inspector
	public bool isForGame;
	public bool isForTableOnly;
	//public Player[] PlayesrDetails;
	public int NoOfPlayersinGame;
	public State LocalCopyOFState;
	public string roomName = "Game";
	public bool isoffline = false;
	public static ColyseusClient Instance;
	public Client client;
	public Room<State> room;
	public EventHandler<State> OnInitialState;
	public Room<IndexedDictionary<string, object>> roomFossilDelta;
	public Room<object> roomNoneSerializer;
	public EventHandler<State> OnGameStateChangeMenuHandler;
	public EventHandler<State> OnGameStateChangeGameHandler;
	private bool initialStateReceived = false;
	public bool isGameQuit;
	// Use this for initialization
	void Start()
	{//initialization
		/* Demo UI */

		isForTableOnly = true;
		isGameQuit = false;
		isForGame = false;
		if (!Instance)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
		//PlayesrDetails = new Player[3];
	}
	public IEnumerator LoadServerTables()
	{
		UIManager.loadingEndPoint = 50;
		//yield return new WaitForSeconds(1f);
		StartCoroutine(UIManager.GetLoading());

		Instance.ConnectToServer();
		isForGame = false;
		Instance.JoinOrCreateRoom();
		yield return new WaitForSeconds(1f);
		if (room != null)
		{

			Instance.room.Send("GetTables");
			//print("Sent Message");
		}
		else
			print("Before connection call");
		yield return new WaitForSeconds(0.2f);
		Instance.LeaveRoom(true, false);
		//print("Exited room");
		yield return new WaitForSeconds(2f);

		//if (PlayerPrefs.GetString("room_ID")=="")
		{
			//print("ActiveBtns -" + PlayerPrefs.GetString("room_ID"));

			UIManager.loadingEndPoint = 100;
			StartCoroutine(UIManager.GetLoading());
			yield return new WaitForSeconds(1f);

			UIManager.ActiveBtns();


		}
		/*+else
		{
			isForGame = true;
			GameData.Instance.isListining = false;
			UIManager.loadingEndPoint = 100;
           StartCoroutine(UIManager.GetLoading());
		//yield return new WaitForSeconds(1f);

			this.ReconnectRoom();
		

		}*/
	}
	public IEnumerator JoinTable(int index)
	{
		Instance.ConnectToServer();
		Instance.JoinOrCreateRoom(index);
		isForTableOnly = false;
		yield return new WaitForSeconds(0.1f);
		/*	if (room != null)
			{
				Instance.room.Send("GetCurrentPlayers");
				//print("Sent Message");
			}
			else
				print("Before connection call for table");*/

	}

	public async void ConnectToServer()//method for connecting to server
	{
		/*
		 * Get Colyseus endpoint from InputField
		 */

		//string endpoint = "ws://floating-wave-93492.herokuapp.com";
		string endpoint = "ws://localhost:2567";

		Debug.Log("Connecting to " + endpoint);

		/*
		 * Connect into Colyeus Server
		 */
		client = ColyseusManager.Instance.CreateClient(endpoint);

		await client.Auth.Login();

		/*var friends = await client.Auth.GetFriends();

		// Update username
		client.Auth.Username = "Jake";
		await client.Auth.Save();*/
	}

	public async void CreateRoom()//for creation of room 
	{
		room = await client.Create<State>(roomName, new Dictionary<string, object>() { });
		//roomNoneSerializer = await client.Create("no_state", new Dictionary<string, object>() { });
		//roomFossilDelta = await client.Create<IndexedDictionary<string, object>>("fossildelta", new Dictionary<string, object>() { });
		print("----" + room);
		RegisterRoomHandlers();
	}

	public async void JoinOrCreateRoom(int index = 1)// for joining available room if not found any room than creates one mainly used 
	{
		roomName = "Table" + index;
		//			print(room);

		room = await client.JoinOrCreate<State>(roomName, new Dictionary<string, object>() { });
		RegisterRoomHandlers();
	}

	public async void JoinRoom()
	{
		room = await client.Join<State>(roomName, new Dictionary<string, object>() { });

		RegisterRoomHandlers();
	}

	public async void ReconnectRoom()
	{

		string roomId = PlayerPrefs.GetString("room_ID");
		string sessionId = PlayerPrefs.GetString("Session_ID");
		print(" from client" + roomId + " -" + sessionId);
		if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(roomId))
		{
			Debug.Log("Cannot Reconnect without having a roomId and sessionId");
			return;
		}
		try
		{
			Application.LoadLevel("OnlineGameTable");

			//	yield return new WaitForSeconds(1f);
			room = await client.Reconnect<State>(roomId, sessionId);
			print(room.Id);
			Debug.Log("Reconnected into room successfully.");

			RegisterRoomHandlers();
			OnlineGamePlayScript.Instance.SaveReconenctionData(1);
		}
		catch (Exception e)
		{
			print(" Table Shoud be visible");
			GameData.Instance.SaveOnlineGameData(clear: true);
			print("locha of " + e);
			PlayerPrefs.SetInt("isoffline", 0);
			Application.LoadLevel("Tables");


		}

	}
	void OnStateChangeHandler(State state, bool isFirstState)//main code where state changes comes and called to different changes
	{
		this.LocalCopyOFState = state;
		if (isFirstState)
		{
			StartCoroutine(LoadGame(state));
		}
		
	}
	public void RegisterRoomHandlers()//used to assign properites and assign handlers for different state changing events
	{



		room.State.TriggerAll();

		PlayerPrefs.SetString("roomId", room.Id);
		PlayerPrefs.SetString("sessionId", room.SessionId);
		PlayerPrefs.Save();

		room.OnLeave += (code) => Debug.Log("ROOM: ON LEAVE");
		room.OnError += (code, message) => Debug.LogError("ERROR, code =>" + code + ", message => " + message);
		room.OnStateChange += OnStateChangeHandler;

		room.OnMessage((Message message) =>
		{
			Debug.Log("Received Schema message:");
			Debug.Log(message.num + ", " + message.str);
		});

		room.OnMessage<MessageByEnum>((byte)MessageType.ONE, (message) =>
		{
			Debug.Log(">> Received message by enum/number => " + message.str);
		});

		room.OnMessage<TypeMessage>("type", (message) =>
		{
			Debug.Log("Received 'type' message!");
			Debug.Log(message);
		});

		room.OnMessage<TestMSGClass>("TestBrodcast", (message) =>
		{
			Debug.Log("Brodcast from server -=-=-=-=-=" + message);
		});
		room.OnMessage<TestMSGClass>("TestServerMessage", (message) =>
		{
			Debug.Log("Message from server -=-=-=-=-=" + message.msg);
		});
		room.OnMessage<TableDetails>("TableDetails", (tbls) =>
		{
			GameData.Instance.onlineTables = new GameTable[3];
			GameData.Instance.onlineTables = tbls.data;

			for (int i = 0; i < 3; i++)
			{
				GameData.Instance.onlineTables[i] = new GameTable();
				GameData.Instance.onlineTables[i] = tbls.data[i];

			}
			foreach (GameTable gt in GameData.Instance.onlineTables)
			{
				print(gt.tableName + " -- " + gt.entryFees);
			}
		});
		room.OnMessage<StartNewHand>("StartNewHand", (data) => {
			OnlineGamePlayScript.Instance.StartNewHand(data);
		});

		/*
		room.OnMessage<PlayersInGame>("PlayersInGame", (data)=> {
			this.NoOfPlayersinGame = data.playerCount;
			if (data.playerCount == 1)
			{
				print(data.player1Data.PlayerName);
				this.PlayesrDetails = new Player[1];
				this.PlayesrDetails[0] = data.player1Data;
			}
			else if (data.playerCount == 2)
			{
				this.PlayesrDetails = new Player[2];
				this.PlayesrDetails[0] = data.player1Data;
				this.PlayesrDetails[1] = data.player2Data;
			}

			print(data.playerCount + "  ---");
			if (!isForTableOnly)
			{
				Player p = new Player();
				p.PlayerName = PlayerPrefs.GetString("Name");
				p.gameId = PlayerPrefs.GetString("UserId");
				p.myChips = (ulong)GameData.Instance.myChips;
				p.sessionId = room.SessionId;
				p.lastbet = 0;
			//	p.handsWon = 0;
				p.isBot = false;
			//	p.totalChipsWon = 0;
			//	p.totalChipsInCurrentBet = 0;
				p.SitNo = 0;
//				p.willWin = -1;
				p.isConnected = true;
				p.isRemoved = false;
				p.myMoney = (ulong)GameData.Instance.myMoney;
			//	p.playerHandDetails = new ArraySchema<PlayerHandData>();
				//p.currentSplitTurn = 0;
				room.Send("MyIntro", p);
				if (isForGame)
				{
					print("Game Loaded");
					GameData.Instance.SaveOnlineGameData(roomId: this.room.Id, sessionId: this.room.SessionId);
					Application.LoadLevel("OnlineGameTable");
				}
			}
		});
		*/
		room.OnMessage<SaveLatestDataMsg>("SaveLatestData", (data) => {

			if (data.player.sessionId == room.SessionId && isForGame == true)
				GameData.Instance.SaveOnlineGameData((long)data.player.myMoney, (long)data.player.myChips, data.player.sessionId, this.room.Id);
		});
		room.OnMessage<StandMsg>("PlayerHandBJ", (data) => {
			StartCoroutine(OnlineGamePlayScript.Instance.ShowHandResult(data.sitno, data.splitNo, 1));

		});
		room.OnMessage<StandMsg>("PlayerHandWin", (data) => {
			StartCoroutine(OnlineGamePlayScript.Instance.ShowHandResult(data.sitno, data.splitNo, 2));
		});
		room.OnMessage<StandMsg>("PlayerHandPush", (data) => {
			StartCoroutine(OnlineGamePlayScript.Instance.ShowHandResult(data.sitno, data.splitNo, 3));
		});
		room.OnMessage<StandMsg>("PlayerHandLoss", (data) => {
			StartCoroutine(OnlineGamePlayScript.Instance.ShowHandResult(data.sitno, data.splitNo, 4));

		});

		room.OnMessage<DistributeMainCard>("DistributeMainCard", (data) => {
			//	print(data.sitno +" "+ data.points + " " + data.cType + " " + data.cValue);
			OnlineGamePlayScript.Instance.DistributeUnSplitCard(data.sitno, JsonUtility.FromJson<PlayCard>(data.card));
		});
		room.OnMessage<InitializeHandDetails>("InitializeNewHand", (data) => {
			OnlineGamePlayScript.Instance.InitializeNewHand(data.handNo);
		});
		room.OnMessage<PlayerSitted>("PlayerSitted", (data) => {

			Player p = new Player();
			p = JsonUtility.FromJson<Player>(data.player);
			print(" sitNo=" + data.sitno + " --  - " + p.PlayerName);
			if (!OnlineGamePlayScript.Instance)
			{
				SceneManager.LoadScene("OnlineGameTable");
			}
			OnlineGamePlayScript.Instance.PlacePlayerInSit(data.sitno, p);

		});
		room.OnMessage<SideShowReqMsg>("SideShowReq", (data) => {
			OnlineGamePlayScript.Instance.SideShowRequest(data.sitNo);
		});
		room.OnMessage<SideShowReqMsg>("SideShowWIn", (data) => {
			OnlineGamePlayScript.Instance.SideShowResult(data.sitNo,true);
		});
		room.OnMessage<SideShowReqMsg>("SideShowLoss", (data) => {
			OnlineGamePlayScript.Instance.SideShowResult(data.sitNo, false);
		});
		room.OnMessage<WinMessage>("WIN",(data)=> {
			OnlineGamePlayScript.Instance.ShowWin(data.sitNO, data.message);
		});
		room.OnMessage<TweenWinMessage>("TweenWIN", (data) => {
			OnlineGamePlayScript.Instance.ShowTweenWin(data.sitNo1,data.SitNo2, data.message);
		});
		room.OnMessage<AllWinMsg>("AllWIN", (data) => {
			OnlineGamePlayScript.Instance.ShowAllWin( data.message);

		});
		room.OnMessage<OnlyLeftWinMsg>("OnlyLeftWIN", (data)=> {
			OnlineGamePlayScript.Instance.ShowOnlyWin( data.sitNO);

		});
		room.OnMessage<GameControl>("GameControl", (data) => {
			print("GameControl---->" + data.command);
			if (data.command == "GetStartingBet")
			{
				if (CheckIamInGame())
				{
					StartCoroutine(OnlineGamePlayScript.Instance.GetStartingBet());
				}
			}
			else if (data.command == "PlayerSeenCards")
			{
				OnlineGamePlayScript.Instance.See(data.sitNo);

			}
			else if (data.command == "PlayerPacked")
			{
				OnlineGamePlayScript.Instance.Pack(data.sitNo);

			}
			else if (data.command == "PlayerBet")
			{
				StartCoroutine(OnlineGamePlayScript.Instance.Bet(data.sitNo,1));

			}
			else if (data.command == "PlayerDoubleBet")
			{
			StartCoroutine(	OnlineGamePlayScript.Instance.Bet(data.sitNo,2));

			}
		});

		room.OnMessage<ReconnectionData>("SaveImportantReconnectData", (data) => {
			OnlineGamePlayScript.Instance.SaveReconenctionData(data.delarPoint);
		});
		room.OnMessage<TestCodeMsg>("TestCode", (data) => {
		});

		room.OnMessage<StartGameTurn>("StartGameTurn", (data) => {
			print("Turn of Player :"+data.sitNo);
			//if (CheckIamInGame())
			{
				OnlineGamePlayScript.Instance.StartTurnOfPlayer(data.sitNo);
			}
		});
	}

	public void LeaveRoom(Boolean consented = false, bool flag = true)
	{
		if (consented == true && flag == true)
		{
			GameData.Instance.SaveOnlineGameData(clear: true);
		}
		room.Leave(consented);

	}

	public async void GetAvailableRooms()
	{
		var roomsAvailable = await client.GetAvailableRooms<CustomRoomAvailable>(roomName);

		Debug.Log("Available rooms (" + roomsAvailable.Length + ")");
		for (var i = 0; i < roomsAvailable.Length; i++)
		{
			Debug.Log("roomId: " + roomsAvailable[i].roomId);
			Debug.Log("maxClients: " + roomsAvailable[i].maxClients);
			Debug.Log("clients: " + roomsAvailable[i].clients);
			Debug.Log("metadata.str: " + roomsAvailable[i].metadata.str);
			Debug.Log("metadata.number: " + roomsAvailable[i].metadata.number);
		}
	}
	public IEnumerator LoadGame(State state)
	{

		if (!isForTableOnly)
		{

			Player p = new Player();
			p.PlayerName = PlayerPrefs.GetString("Name");
			p.gameId = PlayerPrefs.GetString("UserId");
			p.myChips = (ulong)GameData.Instance.myChips;
			p.sessionId = room.SessionId;
			p.lastbet = 0;
			//	p.handsWon = 0;
			p.isBot = false;
			//	p.totalChipsWon = 0;
			//	p.totalChipsInCurrentBet = 0;
			p.SitNo = 0;
			//				p.willWin = -1;
			p.isConnected = true;
			p.isRemoved = false;
			p.myMoney = (ulong)GameData.Instance.myMoney;
			p.isInGame = false;
			//	p.playerHandDetails = new ArraySchema<PlayerHandData>();
			//p.currentSplitTurn = 0;
			room.Send("MyIntro", p);

			yield return new WaitForSeconds(0.2f);

			print("Game Loaded");
			GameData.Instance.SaveOnlineGameData(roomId: this.room.Id, sessionId: this.room.SessionId);
			SceneManager.LoadScene("OnlineGameTable");

		}
	}
	public void SendMessage(String msg)//used to instruct server about scores level win loose etc.
	{
		if (room != null)
		{
			//room.Send("schema");
			room.Send("GameControl", msg);
		}
		else
		{
			Debug.Log("Room is not connected!");
		}
	}


	public bool CheckIamInGame()
	{
		bool flag = false;
		LocalCopyOFState.playersInGame.ForEach(elemet => {
			if (elemet.sessionId == room.SessionId && elemet.isInGame == true)
			{
				flag = true;
			}

		});
		return flag;

	}
}

class GameFinishedMsg
{
	public string msg;
	public int player;
}

class LoadLevelMsg
{
	public int lvl;
}
public class StartNewHand
{
	public PlayerHandData player1;
	public PlayerHandData player2;
	public PlayerHandData player3;
}
internal class TestMSGClass
{
	public string msg;
}

class PowerUpMessage
{
	public string type;
}
class TableDetails
{
	public GameTable[] data;
}
class PlayersInGame
{
	public int playerCount;
	public Player player1Data;
	public Player player2Data;
}
[System.Serializable]
class PlayerSitted
{
	public int sitno;
	public string player;
}
class InitializeHandDetails
{
	public int handNo;
}
class SetInitialBet
{
	public long bet;
	public int sitno;
}
class DistributeMainCard
{
	public int sitno;
	public string card;

}
class StartNewTurn
{
	public int turnOfPlayer;
	public int splitNo;
}
class HitMsg
{
	public int sitno;
	public int points;
	public int cType;
	public int cValue;
	public int splitNo;
}
class StandMsg
{
	public int sitno;
	public int splitNo;
}
class InsureMsg
{
	public int sitno;

}
class RevealDelarCardMSg
{
}
class PricePoolMsg
{
	public int winner;
	public int runner;
}

class SaveLatestDataMsg
{
	public Player player;
}
class ReconnectionPlayerDataMsg
{
	public Player player1;
	public Player player2;
	public Player player3;
	public int currentHandNo;
}
class ReconnectionPlayerHandDataMsg
{
	public int sitNo;
	public int SplitNo;
	public PlayerHandData handData;
}
class SaveCardMsg
{
	public int sitNo;
	public int splitNo;
	public int points;
	public int cType;
	public int cValue;
}
class SaveDelarCardMsg
{
	public int points;
	public int cType;
	public int cValue;
}
class ReconnectionData
{
	public int delarPoint;
}
class TestCodeMsg
{
	public State tc;
}
class GetInitialBet
{
	public long initialBet;
}
class GameControl
{
	public string command;
	public int sitNo;
}
class StartGameTurn
{
	public int sitNo;
}
class SideShowReqMsg
{
	public int sitNo;
}
class WinMessage
{
	public int sitNO;
	public String message;
}
class TweenWinMessage
{
	public int sitNo1;
	public int SitNo2;
	public String message;
}
class AllWinMsg
{
	public string message;
}
class OnlyLeftWinMsg
{
	public int sitNO;
}
