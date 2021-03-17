using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class OnlineGamePlayScript : MonoBehaviour
{
    List<int> winProbability;
    int currentSitInTable;
    int totalRounds;
     int CurrentRound;
    public CardDeck allCards;
    public RemainingDeck remainingCards;
   public long currentBet;
    long previousBet;
   // long chipsInBet;
    //DelarGameData DelarHandsDetails;
   // PlayerGameData PlayersDetails;
    bool flagForSplitDoubleControl;
  
    
    Player[] PlayersinGame;
   

    int splitTurn;
    int TurnOfSplitNo;

    public GameObject playerBetPrefeb;
    public GameObject tableBetPot;
    public GameObject chipTray;
    public GameObject[] PlayerCards;
    public GameObject PricePoolPanel;
    public GameObject bgBlur;
    public GameObject bgWin;
    public GameObject bgLoss;
    public GameObject btnBet;
    public GameObject btnPack;
    public GameObject btnShow;
    public GameObject btnDoubleBet;
    public GameObject btnSee;
    public GameObject btnNormalBet;
    public GameObject cardPrefeb;
    public GameObject chipsStack;
    public GameObject sideShowRequestDialog;
    public GameObject WinningStatusText;
    public Player[] ReconnectedPlayers;
    int currentTableIndex;
   

    bool MyCurrentHandWinProbability;
    public GameTable table;
    public static OnlineGamePlayScript Instance;
    public void SaveReconenctionData(int points) {
        {
            print(" cards count -------------------- "+ColyseusClient.Instance.room.State.remainingDeck.cardsOfDeck.Count);
        }


      
    }
   
    
    public IEnumerator ShowHandResult(int sitNo,int splitNo,int resType)
    {
        yield return new WaitForSeconds(0f);
    }
   
    
    
    public void GetNextTurn(int sitNo, int splitNo)
    {
    }




    


    public void RevealCards() {
        
    }


    public void DistributeUnSplitCard(int sitNo,PlayCard card)
    {
            GameObject gm = Instantiate(this.cardPrefeb, PlayerCards[sitNo - 1].transform);
            if (PlayerCards[sitNo - 1].transform.GetChildCount() >=2)
            {
                foreach (Transform t in PlayerCards[sitNo - 1].transform)
                {

                    t.gameObject.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 3.5f));
                    t.gameObject.GetComponent<RectTransform>().anchoredPosition -= new Vector2(17.5f, 0f);
                }
                GameObject lastCard = PlayerCards[sitNo - 1].transform.GetChild(PlayerCards[sitNo - 1].transform.GetChildCount() - 2).gameObject;
                gm.GetComponent<RectTransform>().anchoredPosition = lastCard.GetComponent<RectTransform>().anchoredPosition + new Vector2(35f, 0f);
                gm.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, (float)(lastCard.GetComponent<RectTransform>().eulerAngles.z - 7)));
               
            }
    }

    /*public void Temp()
    {
        ColyseusClient.Instance.LocalCopyOFState.playersInGame.ForEach(element => {
            print("----" + element.SitNo + "------------------------<->---" + element.PlayerName + "from invoke out of handler--" + ColyseusClient.Instance.LocalCopyOFState.playersInGame.Count);
        });
    }*/
    public void PlacePlayerInSit(int sitno,Player p)
    {
      /*  print("call to sit  .....");
        ColyseusClient.Instance.LocalCopyOFState.playersInGame.ForEach(element => {
            print("----" + element.SitNo + "------------------------<->---" + element.PlayerName + "from out of handler--" + ColyseusClient.Instance.LocalCopyOFState.playersInGame.Count);
        });*/
       // Invoke("Temp",0.2f);
        GameObject playerTab = GameObject.Find("Player" + sitno + "Profile");
        playerTab.transform.GetChild(0).gameObject.SetActive(true);
        playerTab.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = p.PlayerName;
        playerTab.transform.GetChild(1).gameObject.SetActive(true);
        playerTab.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetAmtStr((long)p.myChips);
        playerTab.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
        playerTab.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
        if (p.sessionId==ColyseusClient.Instance.room.SessionId)
        {
            currentSitInTable = sitno;
            for (int i = 1; i < 4; i++) {
                if (i != sitno)
                {
                    if (GameObject.Find("Player" + i + "Profile").transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.active)
                    {
                        GameObject.Find("Player" + i + "Profile").transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
              }
        }
       

    }
    public void InitializeNewHand(int handNo)
    {
       
      


    }
   
    public void StartNewHand(StartNewHand data)
    {
        print(" -"+data+" --"+data.player1);
        //start timing ring animation
        PlayersinGame[0] = new Player();
       /* PlayersinGame[0].bet = (long)data.player1.bet;
        PlayersinGame[0].points=(int)data.player1.points;
        PlayersinGame[0].cards= new List<card>();
        PlayersinGame[0].isDouble= data.player1.isDouble;
        PlayersinGame[0].splitcount=0;
        PlayersinGame[0].splitno=(int)data.player1.splitno;
        PlayersinGame[0].isInsured= data.player1.isInsured;
        PlayersinGame[0].playerPosition=1;
        PlayersinGame[0].isSurrender= data.player1.isSurrender;
        PlayersinGame[0].currentSplitTurn = 0;*/


        PlayersinGame[1] = new Player();
       /* PlayersinGame[1].bet = (long)data.player2.bet;
        PlayersinGame[1].points = (int)data.player2.points;
        PlayersinGame[1].cards = new List<card>();
        PlayersinGame[1].isDouble = data.player2.isDouble;
        PlayersinGame[1].splitcount = 0;
        PlayersinGame[1].splitno = (int)data.player2.splitno;
        PlayersinGame[1].isInsured = data.player2.isInsured;
        PlayersinGame[1].playerPosition = 1;
        PlayersinGame[1].isSurrender = data.player2.isSurrender;
        PlayersinGame[1].currentSplitTurn = 0;*/


        PlayersinGame[2] = new Player();
       /* PlayersinGame[2].bet = (long)data.player2.bet;
        PlayersinGame[2].points = (int)data.player2.points;
        PlayersinGame[2].cards = new List<card>();
        PlayersinGame[2].isDouble = data.player2.isDouble;
        PlayersinGame[2].splitcount = 0;
        PlayersinGame[2].splitno = (int)data.player2.splitno;
        PlayersinGame[2].isInsured = data.player2.isInsured;
        PlayersinGame[2].playerPosition = 1;
        PlayersinGame[2].isSurrender = data.player2.isSurrender;
        PlayersinGame[2].currentSplitTurn = 0;*/







    }

    void Start()
    {
        MyCurrentHandWinProbability = true;
        if (!Instance)
            Instance = this;

        allCards.InitializeDeck();
        splitTurn = 0;
        totalRounds = 0;
        currentTableIndex = PlayerPrefs.GetInt("currentTable");
        //print("--"+currentTableIndex);
        table = GameData.Instance.onlineTables[currentTableIndex];
        GameData.Instance.myMoney -= table.entryFees;
        GameData.Instance.SaveOnlineGameData();
//        GameObject.Find("TableName").GetComponent<TextMeshProUGUI>().text = table.tableName;
 //       GameObject.Find("MaxBet").GetComponent<TextMeshProUGUI>().text = GetAmmountString((long)table.maxPotLimit);
 //       GameObject.Find("MinBet").GetComponent<TextMeshProUGUI>().text = GetAmmountString((long)table.startingBet);
        this.currentBet = (long)table.startingBet;
        this.previousBet = (long)table.startingBet;
        winProbability = new List<int>();
        ResetProbability();
    


        PlayerCards = new GameObject[3];
        PlayerCards[0] = GameObject.Find("Player1Cards");
        PlayerCards[1] = GameObject.Find("Player2Cards");
        PlayerCards[2] = GameObject.Find("Player3Cards");


     


 

        PlayersinGame = new Player[3];
        if (ColyseusClient.Instance.LocalCopyOFState.playersInGame.Count != 0) {

            ColyseusClient.Instance.LocalCopyOFState.playersInGame.ForEach(p =>
            {
               
                int sitno = (int)p.SitNo;
                if (sitno != 0 || p==null)
                {
                    GameObject playerTab = GameObject.Find("Player" + sitno + "Profile");
                    playerTab.transform.GetChild(0).gameObject.SetActive(true);
                    playerTab.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = p.PlayerName;
                    playerTab.transform.GetChild(1).gameObject.SetActive(true);
                    playerTab.transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = GameData.Instance.GetAmtStr((long)p.myChips);
                    playerTab.transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
                    playerTab.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
                }
            });
        }

        if (ColyseusClient.Instance.LocalCopyOFState.isPlayingGame == true)
        {
          //logic to load previous scene Shift this to awake() 
          
        }
        
    }

  
    private void ResetProbability()
    {
        for (int i = 1; i < 6; i++)
            winProbability.Add(i);
    }
    public  void JoinTable(int sit)
    {

         ColyseusClient.Instance.room.Send("SitInChair", sit);
        
    }
    
    void SetTotalIncrsedChipsint(int  player, long betamt)
    {
        print(" Sit no for set reduced chips "+player);


    GameObject.Find("Player" + player + "Profile").transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GetAmmountString(GetAmmountNo(GameObject.Find("Player" + player + "Profile").transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text)+betamt);

    }
void SetTotalReducedChips(int player, double betamt)
    {
        print(" Sit no for set reduced chips "+player);


        GameObject.Find("Player" + player + "Profile").transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GetAmmountString(GetAmmountNo(GameObject.Find("Player" + player + "Profile").transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text)-betamt);

    }
   


   


    
    


    
   
    public void ResetTable()
    {
        
    }
    void SetTotalChips()
    {
        GameObject.Find("Player" + currentSitInTable + "Profile").transform.GetChild(1).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GetAmmountString(GameData.Instance.myChips);

    }
    public double GetAmmountNo(string s) {
        double amt = 0;
        print(s);
        if (s.Contains("K"))
            amt = (double)(double.Parse(s.Replace("K", "")) * 1000f);
        else if (s.Contains("M"))
            amt = (double)(double.Parse(s.Replace("M", "")) * 1000000f);
        else if (s.Contains("B"))
            amt = (double)(double.Parse(s.Replace("B", "")) * 1000000000f);
        else
            amt = double.Parse(s);
        return amt;
    }
    public string GetAmmountString(double amt)
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
   
   

    public IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(0.1f);
    }



    public IEnumerator GetStartingBet() {
        //yield return new WaitForSeconds(5f);
        tableBetPot.active=true;
        tableBetPot.transform.GetChild(1).GetComponentInParent<TextMeshProUGUI>().text = "0";
        int playerInGameCount=0;
        List<GameObject> playerBetObjects = new List<GameObject>();
     //   print("-**********--------"+ColyseusClient.Instance.LocalCopyOFState.playersInGame.Count);
        ColyseusClient.Instance.LocalCopyOFState.playersInGame.ForEach(element=> {
            print(element.isInGame);
            if (element.isInGame) {
                GameObject gm1 = GameObject.Find("Player" + element.SitNo + "Profile");
                GameObject gm = GameObject.Instantiate(playerBetPrefeb,GameObject.Find("Canvas").transform);
             //   print("-**********-------- Sit No:" + (int)element.SitNo);
                gm.GetComponent<Animator>().SetInteger("PlayerNo", (int)element.SitNo);
                gm.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text= this.GetAmmountString(ColyseusClient.Instance.LocalCopyOFState.currentTable.startingBet);
                gm.transform.parent = GameObject.Find("Canvas").transform;
                playerBetObjects.Add(gm);
                playerInGameCount++;
                if (element.sessionId == ColyseusClient.Instance.room.SessionId)
                {
                    GameData.Instance.myChips = GameData.Instance.myChips -(long) ColyseusClient.Instance.LocalCopyOFState.currentTable.startingBet;
                }
                SetTotalReducedChips((int)element.SitNo, (long)ColyseusClient.Instance.LocalCopyOFState.currentTable.startingBet);
                gm1.transform.GetChild(4).gameObject.SetActive(true);
                gm1.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.LocalCopyOFState.currentTable.startingBet); 

            }
        });
        yield return new WaitForSeconds(1.8f);
        tableBetPot.transform.GetChild(1).GetComponentInParent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.tablePotAmmount);//(double)(ColyseusClient.Instance.LocalCopyOFState.currentTable.startingBet *(ulong)playerInGameCount));
        playerBetObjects.ForEach(element=> {
            GameObject.Destroy(element);
        });

    }
    public void StartTurnOfPlayer(int sitNo)
    {
        if (ColyseusClient.Instance.room.State.playersInGame[currentSitInTable-1].isBlind==true && ColyseusClient.Instance.room.State.playersInGame[currentSitInTable - 1].dosePacked==false)
        {
            btnSee.SetActive(true);
        }
       /* {//turning of previous players timing ring and if it is our turn than hide controls
            GameObject pTab = GameObject.Find("Player" + sitNo + "Profile");//player profile
            pTab.transform.GetChild(2).gameObject.SetActive(true);
            if (sitNo == currentSitInTable)
            {
                btnBet.SetActive(false);
                btnSee.SetActive(false);
                btnDoubleBet.SetActive(false);
                btnNormalBet.SetActive(false);
                btnPack.SetActive(false);
                btnShow.SetActive(false);
            }
        }*/
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");
        playerTab.transform.GetChild(2).gameObject.SetActive(true);
        if (sitNo == currentSitInTable) {

            if (ColyseusClient.Instance.room.State.playersInGame[sitNo - 1].isBlind) {
                //player is blind
                if (ColyseusClient.Instance.room.State.currentBlindBetAmmount <= (ulong)GameData.Instance.myChips)
                {
                    btnBet.SetActive(true);
                    btnBet.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "BLIND";
                    btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = ColyseusClient.Instance.room.State.currentBlindBetAmmount.ToString();
                }
                else
                {
                    //logic for pack
                }
                if ((ColyseusClient.Instance.room.State.currentBlindBetAmmount * 2) <= (ulong)GameData.Instance.myChips )
                {
                    if( ColyseusClient.Instance.room.State.currentTable.maxBetLimit > ColyseusClient.Instance.room.State.currentBlindBetAmmount)
                         btnDoubleBet.SetActive(true);
                    
                }
            }
            else {
                //player has seen cards
                if (ColyseusClient.Instance.room.State.currentBetAmmount <= (ulong)GameData.Instance.myChips)
                {
                    btnBet.SetActive(true);
                    btnBet.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "CHAAL";
                    btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = ColyseusClient.Instance.room.State.currentBetAmmount.ToString();
                }
                else
                {
                    //logic for pack
                }
                if ((ColyseusClient.Instance.room.State.currentBetAmmount*2) <= (ulong)GameData.Instance.myChips)
                {
                    if (ColyseusClient.Instance.room.State.currentTable.maxBetLimit > ColyseusClient.Instance.room.State.currentBetAmmount)
                        btnDoubleBet.SetActive(true);
                }
            }
            //  btnNormalBet.SetActive(true);
          
            if (ColyseusClient.Instance.room.State.totalPlayersInGame != 2)
            {
                if (ColyseusClient.Instance.room.State.lastBetBySit != 0 && ColyseusClient.Instance.room.State.playersInGame[ColyseusClient.Instance.room.State.lastBetBySit - 1].isBlind == false)
                {
                    btnShow.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "SIDE SHOW";
                    btnShow.SetActive(true);
                }
            }
            else
            {
                btnShow.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "SHOW";
                btnShow.SetActive(true);
            }
            btnPack.SetActive(true);
        }
    }

    public void PackRequest()
    {
       
        print("pack req.-->");
        ColyseusClient.Instance.room.Send("GameControl","pack");
    }
    public void BetRequest()
    {


        if (btnNormalBet.active)
        {
            print("Double Bet req.-->");
            ColyseusClient.Instance.room.Send("GameControl", "doubleBet");

        }
        else if (btnDoubleBet.active)
        {
            print("Bet req.-->");
            ColyseusClient.Instance.room.Send("GameControl", "bet");

        }
        else
        {
            ColyseusClient.Instance.room.Send("GameControl", "bet");
        }

    }

    public void SeeRequest()
    {
        

        ColyseusClient.Instance.room.Send("GameControl", "see");

    }
    public void ShowRequest()
    {
        ColyseusClient.Instance.room.Send("GameControl", "show");

    }
    public void SetDoubleBet()
    {
        if (ColyseusClient.Instance.room.State.playersInGame[currentSitInTable - 1].isBlind)
        {
            btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (ColyseusClient.Instance.room.State.currentBlindBetAmmount * 2).ToString();
        }
        else
        {
            btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (ColyseusClient.Instance.room.State.currentBetAmmount * 2).ToString();
        }
        btnDoubleBet.SetActive(false);
        btnNormalBet.SetActive(true);

    }
    public void SetNormalBet()
    {
        if (ColyseusClient.Instance.room.State.playersInGame[currentSitInTable - 1].isBlind)
        {
            btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (ColyseusClient.Instance.room.State.currentBlindBetAmmount).ToString();
        }
        else
        {
            btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = (ColyseusClient.Instance.room.State.currentBetAmmount ).ToString();
        }
        btnDoubleBet.SetActive(true);
        btnNormalBet.SetActive(false);
    }

    public void Pack(int sitNo)
    {
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");
        playerTab.transform.GetChild(5).gameObject.SetActive(true);
        playerTab.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Packed";
        playerTab.transform.GetChild(2).gameObject.SetActive(false);
     //   print("Check See Button::::::"+currentSitInTable+"-"+sitNo);
        playerTab.transform.GetChild(4).gameObject.SetActive(false);

        if (sitNo == currentSitInTable) {
          //  print("Check See Button::::::");
            btnBet.SetActive(false);
            btnSee.SetActive(false);
            btnDoubleBet.SetActive(false);
            btnNormalBet.SetActive(false);
            btnPack.SetActive(false);
            btnShow.SetActive(false);
        }

        foreach (Transform t in PlayerCards[sitNo - 1].transform) { GameObject.Destroy(t.gameObject); }
    }
    public void See(int sitNo)
    {
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile"); 
        playerTab.transform.GetChild(5).gameObject.SetActive(true);
        playerTab.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Seen";
        if (sitNo == currentSitInTable)
        {
            btnSee.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                PlayCard currentCard = ColyseusClient.Instance.room.State.playersInGame[currentSitInTable - 1].myCards[i];
                string cardPath = "Cards/" + currentCard.cardType.ToString() + "/" + currentCard.cardValue.ToString();
                print(cardPath);
                PlayerCards[currentSitInTable - 1].transform.GetChild(i).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardPath);

            }
        }
        btnBet.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "CHAAL";
        btnBet.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = GetAmmountString(ColyseusClient.Instance.room.State.currentBetAmmount);

    }
    public IEnumerator Bet(int sitNo, int multiplayer)
    {
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");
        playerTab.transform.GetChild(5).gameObject.SetActive(true);
        playerTab.transform.GetChild(2).gameObject.SetActive(false);

        double betAmmount = 0;
        if (ColyseusClient.Instance.room.State.playersInGame[sitNo - 1].isBlind)
        {
            betAmmount = (ColyseusClient.Instance.room.State.currentBlindBetAmmount);// * (ulong)multiplayer);
            playerTab.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Blind";
        }
        else
        {
            betAmmount = (ColyseusClient.Instance.room.State.currentBetAmmount);// * (ulong)multiplayer);
            playerTab.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Chaal";
        }
        playerTab.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(betAmmount);

        SetTotalReducedChips(sitNo,betAmmount);
        if (sitNo == currentSitInTable)
        {
            GameData.Instance.myChips -= (long)betAmmount;
            btnBet.SetActive(false);
            btnSee.SetActive(false);
            btnDoubleBet.SetActive(false);
            btnNormalBet.SetActive(false);
            btnPack.SetActive(false);
            btnShow.SetActive(false);
        }
        GameObject gm = GameObject.Instantiate(playerBetPrefeb, GameObject.Find("Canvas").transform);
        gm.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(betAmmount);
        gm.transform.parent = GameObject.Find("Canvas").transform;
        gm.GetComponent<Animator>().SetInteger("PlayerNo", sitNo);
        yield return new WaitForSeconds(1.8f);
        tableBetPot.transform.GetChild(1).GetComponentInParent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.tablePotAmmount);

    }

    public void SideShowRequest(int sitNo)
    {//logic for side show req.
        int previousSit=sitNo-1;
        if (previousSit == 0)
            previousSit = 3;

        if (currentSitInTable == previousSit)
        {//logic for opening side show dialog ;
            this.sideShowRequestDialog.SetActive(true);
        }
        if (currentSitInTable == sitNo)
        {//logic for hiding buttons
            btnBet.SetActive(false);
            btnSee.SetActive(false);
            btnDoubleBet.SetActive(false);
            btnNormalBet.SetActive(false);
            btnPack.SetActive(false);
            btnShow.SetActive(false);
        }
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");
        playerTab.transform.GetChild(4).gameObject.SetActive(true);
        playerTab.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S. Show";
        playerTab.transform.GetChild(7).gameObject.SetActive(true);
        playerTab = GameObject.Find("Player" + previousSit + "Profile");
       // playerTab.transform.GetChild(4).gameObject.SetActive(true);
       // playerTab.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S. Show";
        playerTab.transform.GetChild(7).gameObject.SetActive(true);

    }
    public void AcceptSideShow()
    {
        ColyseusClient.Instance.room.Send("SideShowAccepted");
    }
    public void RejectSideShow()
    {
        ColyseusClient.Instance.room.Send("SideShowRejected");

    }
    public void SideShowResult(int sitNo,bool doesWin)
    {
        int previousSit = sitNo - 1;
        if (previousSit == 0)
            previousSit = 3;

        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");
        if (currentSitInTable == previousSit)
        {//logic for opening side show dialog ;
            this.sideShowRequestDialog.SetActive(false);
        }
        if (currentSitInTable == sitNo)
        {//logic for hiding buttons
           /* btnBet.SetActive(false);
            btnSee.SetActive(false);
            btnDoubleBet.SetActive(false);
            btnNormalBet.SetActive(false);
            btnPack.SetActive(false);
            btnShow.SetActive(false);*/
        }
        playerTab.transform.GetChild(7).gameObject.SetActive(false);
        playerTab = GameObject.Find("Player" + previousSit + "Profile");
        // playerTab.transform.GetChild(4).gameObject.SetActive(true);
        // playerTab.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "S. Show";
        playerTab.transform.GetChild(7).gameObject.SetActive(false);

    }


    public void ShowWin(int sitNo,string message)
    {
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");
        this.WinningStatusText.GetComponent<TextMeshProUGUI>().text = "" + ColyseusClient.Instance.room.State.playersInGame[sitNo - 1].PlayerName + " Won By " + message;
         playerTab.transform.GetChild(4).gameObject.SetActive(false);
        playerTab.transform.GetChild(6).gameObject.SetActive(true);
        playerTab.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.playersInGame[sitNo-1].myChips) ;
        RevileCards(sitNo);
    }
    public void ShowOnlyWin(int sitNo)
    {
        GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");

        playerTab.transform.GetChild(4).gameObject.SetActive(false);
        playerTab.transform.GetChild(6).gameObject.SetActive(true);
        playerTab.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.playersInGame[sitNo - 1].myChips);

    }
    public void ShowTweenWin(int sitNo1,int sitNo2,string message)
    {
        string temp = "";
        GameObject playerTab = GameObject.Find("Player" + sitNo1 + "Profile");

        playerTab.transform.GetChild(4).gameObject.SetActive(false);
        playerTab.transform.GetChild(6).gameObject.SetActive(true);
        playerTab.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.playersInGame[sitNo1 - 1].myChips);
        temp += ColyseusClient.Instance.room.State.playersInGame[sitNo1 - 1].PlayerName + ",";
        playerTab = GameObject.Find("Player" + sitNo2 + "Profile");
        RevileCards(sitNo1);
        RevileCards(sitNo2);

        playerTab.transform.GetChild(4).gameObject.SetActive(false);
        playerTab.transform.GetChild(6).gameObject.SetActive(true);
        playerTab.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.playersInGame[sitNo2 - 1].myChips);
        temp += ColyseusClient.Instance.room.State.playersInGame[sitNo2 - 1].PlayerName + ",";

        this.WinningStatusText.GetComponent<TextMeshProUGUI>().text = "" + temp + " Won By " + message + " Tie";

    }
    public void ShowAllWin(string message)
    {
        string temp="";
        for (int i = 0; i < 3; i++)
        {
            int sitNo=0;
            if (ColyseusClient.Instance.room.State.playersInGame[i].isInGame==true && ColyseusClient.Instance.room.State.playersInGame[i].dosePacked==false)
            {
                sitNo = i + 1;
                RevileCards(sitNo);
                GameObject playerTab = GameObject.Find("Player" + sitNo + "Profile");

                temp += ColyseusClient.Instance.room.State.playersInGame[i].PlayerName + ",";
                playerTab.transform.GetChild(4).gameObject.SetActive(false);
                playerTab.transform.GetChild(6).gameObject.SetActive(true);
                playerTab.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = this.GetAmmountString(ColyseusClient.Instance.room.State.playersInGame[sitNo - 1].myChips);
            }
        }
        this.WinningStatusText.GetComponent<TextMeshProUGUI>().text = "" + temp + " Won By " + message+" Tie";
    }

    public void RevileCards(int sitNo)
    {

            for (int i = 0; i < 3; i++)
            {
                PlayCard currentCard = ColyseusClient.Instance.room.State.playersInGame[sitNo - 1].myCards[i];
                string cardPath = "Cards/" + currentCard.cardType.ToString() + "/" + currentCard.cardValue.ToString();
                print(cardPath);
                PlayerCards[sitNo - 1].transform.GetChild(i).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(cardPath);

            }

        
    }

}