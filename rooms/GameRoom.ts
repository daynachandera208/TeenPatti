import { Room, Client, generateId, Delayed } from "colyseus";
import { Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";
import { verifyToken, User, IUser } from "@colyseus/social";
import { DeckOfCards } from "../DataStructures/DeckOfCards";
import { GameTable } from "../DataStructures/GameTable";
import { PlayCard } from "../DataStructures/PlayCard";
import { Player } from "../DataStructures/Player";
import { State } from "../DataStructures/State";
import { WinLogic } from "../Win Logic/WinLogic";
import { createLessThan, flattenDiagnosticMessageText, idText, NumericLiteral } from "typescript";
const type = Context.create();
class MyMessage extends Schema {
  @type("string") message: string;
}
export class GameRoom extends Room {
 maxClients=3;
 FixedBotIndex:number=-1;
 //playercounter:number=0;
 sittingplayercounter:number=0;
 isGameFinished=false;
SetTable(){//override this to create new table
  this.state.currentTable=new GameTable();
  this.state.currentTable=new GameTable();
  this.state.currentTable.minBet=10;
  this.state.currentTable.maxBetLimit=500;

  this.state.currentTable.maxBet=5000;
  this.state.currentTable.handsLimit=3;
  this.state.currentTable.entryFees=5;
  this.state.currentTable.tableName="oops";
  console.log('Unwanted one - Table Created without child');
}
  onCreate (options: any) {
    console.log("Room created.", options);
    this.setState(new State());
    this.SetTable();
    this.state.playersInGame=new ArraySchema<Player>();
    this.clock.clear();
    this.clock.start();
    this.state.deck=new DeckOfCards();
    this.state.remainingDeck=new DeckOfCards();
    this.state.playercounter=0;
    this.state.emptySitNo=0;
    this.state.isPlayingGame=false;
    this.state.tablePotAmmount=0;
  this.state.deck.SuffleDeck();

  this.setMetadata({
      str: "hello",
      number: 10
    });

    this.setPatchRate(1000 / 20);
    this.setSimulationInterval((dt) => this.update(dt));

    this.onMessage(0, (client, message) => {
      client.send(0, message);
    });
    this.onMessage("SideShowAccepted",(client,data)=>{
     this.clock.clear();this.clock.start();
     this.clock.setTimeout(()=>{this.SideShow(this.state.currentTurnOfPlayer);},500); 
    });

    this.onMessage("SideShowRejected",(client,data)=>{
     this.clock.setTimeout(()=>{this.GetNextTurn();},500); 
    });
    this.onMessage("GameControl", (client,data) => {
      let sit:number=0;
      

      this.state.playersInGame.forEach(element => {
        if(element.sessionId==client.sessionId)
        {
          sit=element.SitNo;
        }
      });
      console.log("Cards Command By player "+sit+" of"+data);
      if(data=="pack"){
      console.log("Cards Pack Command By player "+sit);

          this.Pack(sit);
      }
      else if(data=="bet")
      {
        this.Bet(sit,1);
      }else if(data=="doubleBet")
      {
        this.Bet(sit,2);
        
      }else if(data=="see")
      {
        this.state.playersInGame.at(sit-1).isBlind=false;
        console.log("Cards Seen By player "+sit);
        this.broadcast("GameControl",{command:"PlayerSeenCards",sitNo:sit});
        
      }else if(data=="show")
      {
        let playerWithCard:number=0;
        this.state.playersInGame.forEach(element => {
          if(element.isInGame==true&&element.dosePacked==false)
          {
playerWithCard++;
          }
        });
        if(playerWithCard==2)
     {   
     // this.Bet(sit,1,true);
      this.clock.setTimeout(()=>{
       this.Show(false);
      },2000);
     }
     else
     {
        this.SideShowReq(sit);
     }
        
      }
    })
this.onMessage( "TestMessage",(client,msg)=>{
  console.log("Receved TestMessage from Client :"+client.sessionId +" of " +msg);
  client.send("TestServerMessage",{  msg:"YoHOOHOOHO"});
});
this.onMessage("MyIntro",(client,p)=>{
 // console.log("Intro");
  
//this.playercounter++;
//this.winprobabilitytarget++;
//console.log(this.playercounter-1+" --- Playercounter");
//console.log("p="+p.myMoney);
let player:Player;
player=new Player();
player.sessionId=p.sessionId;
//player.playerHandDetails=new ArraySchema<PlayerHandData>();
player.gameId=p.gameId;
//player.currentSplitTurn=p.currentSplitTurn;
player.PlayerName=p.PlayerName;
player.SitNo=p.SitNo;
//player.totalChipsInCurrentBet=p.totalChipsInCurrentBet;
player.lastbet=p.lastbet;
player.myChips=p.myChips;
player.myMoney=p.myMoney;
//player.handsWon=p.handsWon;
//player.totalChipsWon=p.totalChipsWon;
player.isBot=p.isBot;
//player.willWin=p.willWin;
player.myCards=new ArraySchema<PlayCard>();
player.isConnected=p.isConnected;
player.isInGame=false;
player.blindCounts=0;
player.slidShowCounts=0;
player.isRemoved=p.isRemoved;
if(this.state.isPlayingGame){
 
  this.state.playersInGame.at(this.state.emptySitNo-1).sessionId=p.sessionId;
  this.state.playersInGame.at(this.state.emptySitNo-1).gameId=p.gameId;
  this.state.playersInGame.at(this.state.emptySitNo-1).PlayerName=p.PlayerName;
  this.state.playersInGame.at(this.state.emptySitNo-1).SitNo=this.state.emptySitNo;
  this.state.playersInGame.at(this.state.emptySitNo-1).lastbet=p.lastbet;
  this.state.playersInGame.at(this.state.emptySitNo-1).myChips=p.myChips;
  this.state.playersInGame.at(this.state.emptySitNo-1).myMoney=p.myMoney;
  this.state.playersInGame.at(this.state.emptySitNo-1).isBot=p.isBot;
  this.state.playersInGame.at(this.state.emptySitNo-1).myCards=new ArraySchema<PlayCard>();
  this.state.playersInGame.at(this.state.emptySitNo-1).isConnected=p.isConnected;
  this.state.playersInGame.at(this.state.emptySitNo-1).isInGame=false;
  this.state.playersInGame.at(this.state.emptySitNo-1).blindCounts=0;
  this.state.playersInGame.at(this.state.emptySitNo-1).slidShowCounts=0;
  this.state.playersInGame.at(this.state.emptySitNo-1).isRemoved=p.isRemoved;
  this.clock.setTimeout(()=>{
  console.log("Third Persion Entry :: "+this.state.playersInGame.at(2).SitNo+"  !!! "+this.state.playersInGame.at(2).PlayerName);
  this.broadcast("PlayerSitted",{sitno:this.state.emptySitNo,player:JSON.stringify(this.state.playersInGame.at(2))});//this.state.playersInGame[index]});
  },2000);
  
}
else{

  
this.state.playersInGame.push(player );

}
if(this.sittingplayercounter==2){
//this.PlacePlayerOnEmptySit();
}else{
this.SetPlayerSittingTimeOut(this.state.playersInGame.indexOf(player));
}
//console.log("Player Enterd ===>"+player.isInGame);

});
this.onMessage("SitInChair",(client,sit)=>{
  console.log("request to sit -"+sit);
  this.state.playersInGame.forEach(element => {
    if(element.sessionId==client.sessionId){
    let flag:boolean=true;
    this.state.playersInGame.forEach(element1 => {
      if(element1.SitNo==sit){
      flag=false;}
    });
    if(flag){
      element.SitNo=sit;
      console.log("sit---"+sit+" -- "+element);
      
      this.sittingplayercounter++;
      
       
      console.log("Player ManualSit "+element+" "+element.SitNo+"  " +element.PlayerName+"+  +"+sit+
      " e"+ element.SitNo +" sitting players"+ this.sittingplayercounter);

      this.broadcast("PlayerSitted",{sitno:sit,player:JSON.stringify(element)});//this.state.playersInGame[index]});
      if(this.sittingplayercounter>1)
      {
        this.StartGame();
      }
    }
    }
  });
});
this.onMessage("SetMyCurrentWinProbability",(client,winprobability)=>{
  
  this.state.playersInGame.forEach(element => {

    if(element.sessionId==client.sessionId)
    {
      console.log("MY Win Probability--"+element.SitNo);

      if(winprobability)
      element.willWin=1;
      else
      element.willWin=0;
     // this.winprobabilitycamecounter++;
    }
  });
});




  this.onMessage("GetTables",(client)=>{
    let tables:GameTable[];
    tables=new Array(3);
    tables[0]=new GameTable();
    tables[0].startingBet=10;
    tables[0].maxPotLimit=5000;
    tables[0].handsLimit=3;
    tables[0].entryFees=5;
    tables[0].tableName="tbl1";
    tables[1]=new GameTable();
    tables[1].startingBet=10000;
    tables[1].maxPotLimit=5000000;
    tables[1].handsLimit=3;
    tables[1].entryFees=25;
    tables[1].tableName="tbl2";
    tables[2]=new GameTable();
    tables[2].startingBet=1000000;
    tables[2].maxPotLimit=500000000;
    tables[2].handsLimit=3;
    tables[2].entryFees=50;
    tables[2].tableName="tbl3";
    client.send("TableDetails",{data:tables});
  });

    this.onMessage("SplitRequest",(client,splitNo)=>{


    //  this.Split(this.state.currentTurnOfPlayer);
      
      });
  
    this.onMessage("*", (client, type, message) => {
      console.log(`received message "${type}" from ${client.sessionId}:`, message);
    });
  }

  async onAuth (client, options) {
  //  console.log("onAuth(), options!", options);
   // return await User.findById(verifyToken(options.token)._id);
 return true;
  }



  onJoin (client: Client, options: any, user: IUser) {
    console.log("client joined!", client.sessionId);
   // client.send("GiveIntro");
    /*if(this.state.playercounter==0)
  {
    this.SetBotTimeOut();
  }*/
    this.state.playercounter++;
    if(this.state.playercounter==3)
    {
      this.lock();
    }
}

  async onLeave (client: Client, consented: boolean) {
 
  }
  removePlayer(sessionId: string) {
   /* this.state.playersInGame.forEach(element => {
      if(element.sessionId==sessionId){
        console.log("element"+element.sessionId+" -- "+element.isRemoved);
        element.isRemoved=true;
      }
    });
    this.playercounter--;
    if(this.playercounter<2){
        let runnerupIndex:number=0;
        let winningPlayerIndex:number=-1;
        this.state.playersInGame.forEach(element => {
          if(element.isRemoved!=true && element.isConnected==true )
          {
              winningPlayerIndex=element.SitNo-1;
          }
        });
                  if(winningPlayerIndex!=-1){
          this.state.playersInGame.at(winningPlayerIndex).myMoney+=(this.state.currentTable.entryFees*3)/2;
          console.log("  winnwr /Index: "+winningPlayerIndex);
          this.broadcast("PricePool",{winner:winningPlayerIndex,runner:runnerupIndex});
                  }
                  
          this.clock.clear();
          this.clock.start();
                  
          this.clock.setTimeout(()=>{
            this.isGameFinished=true;
            this.disconnect();
          },2000);
    }*/
  }

    setPlayerConnected(sessionId: string, value: boolean) {
      this.state.playersInGame.forEach(element => {
        if(element.sessionId==sessionId)
        {
            element.isConnected=value;
        }
      });
  }
  update (dt?: number) {
    // console.log("num clients:", Object.keys(this.clients).length);
  }

  onDispose () {
    console.log("DemoRoom disposed.");
  }

  SetBotTimeOut(){
   
    this.clock.setTimeout(()=>{
      if(this.state.playercounter<3)
      {
        this.state.playercounter++;
        let p:Player=new Player();
        p.PlayerName = "Bot";
				p.gameId = "999777888";
				p.myChips = 4000000;
        p.sessionId = "123456789";
        p.myMoney=5000;
				p.lastbet = 0;
			//	p.handsWon = 0;
        p.isBot = true;
        p.isConnected=true;
        p.blindCounts=0;
        p.slidShowCounts=0;
        p.isRemoved=false;
				//p.totalChipsWon = 0;
				//p.totalChipsInCurrentBet = 0;
        p.SitNo = 0;
       // p.willWin=-1;
        //p.currentSplitTurn=0;
        console.log(this.state.playercounter-1+" --- -- ");//+this.state.playersInGame[this.playercounter-1]);
       // this.state.playersInGame[this.playercounter-1]=new Player();
        this.state.playersInGame.push(p);

        if(this.state.playersInGame.at(this.state.playercounter-1).SitNo==0){
          for(let i:number=1;i<4;i++){
            let flag:boolean;
            flag=true;
          
          this.state.playersInGame.forEach(element => {
            if(element.SitNo==i){
             flag=false;
            }
          });
          if(flag)
          {
            this.state.playersInGame[this.state.playercounter-1].SitNo=i;
        //console.log("sit forcefully---"+i);
        this.sittingplayercounter++;
        
let p:Player;
p=new Player();
p=this.state.playersInGame.at(this.state.playercounter-1);
console.log("Bot AutoSit "+p+" "+p.SitNo+"sitting players"+ this.sittingplayercounter);
        this.broadcast("PlayerSitted",{sitno:i,player:JSON.stringify(p)});
        if(this.sittingplayercounter>1)
        {
          this.StartGame();
        }
           break; 
          }
          }
        }
       
      }
    },10000);
  }
  SetPlayerSittingTimeOut(PlayerIndex:number){
    
    
    console.log(PlayerIndex+" -- "+ this.state.playersInGame.at(PlayerIndex).SitNo);

    this.clock.setTimeout(()=>{
      //console.log(PlayerIndex+" -- "+ this.state.playersInGame[PlayerIndex]);
      if(this.state.playersInGame.at(PlayerIndex).SitNo==0){
        for(let i:number=1;i<4;i++){
          let flag:boolean;
          flag=true;
        
        this.state.playersInGame.forEach(element => {
          if(element.SitNo==i){
           flag=false;
          }
        });
        if(flag)
        {
          this.state.playersInGame.at(PlayerIndex).SitNo=i;
      //console.log("sit forcefully---"+i);
      let p:Player=new Player();
      console.log(typeof(p));
      p=this.state.playersInGame[PlayerIndex];
      this.sittingplayercounter++;
      
       
      var player=new Player(p);
      console.log("Player AutoSit "+this.state.playersInGame[PlayerIndex]+" "+this.state.playersInGame[PlayerIndex].SitNo+" "+typeof(p)+" "+new Player(p)+"----"+player+"sitting players"+ this.sittingplayercounter);
      
      this.broadcast("PlayerSitted",{sitno:i,player:JSON.stringify(player)});
      if(this.sittingplayercounter>1)
      {
        this.StartGame();
      }
         break; 
        }
        }
      }

    },5000);
  }

  StartGame(){
    console.log(" Enterd in start game"+this.state.isPlayingGame+"  total "+this.state.playercounter+" sitted"+this.sittingplayercounter);
    if(this.state.playercounter>this.sittingplayercounter && this.state.playercounter!=2)//check this one please
    {
    console.log(" Enterd in start game for sitting");

      this.state.playersInGame.forEach(element => {
        if(element.SitNo==0){
          for(let i:number=1;i<4;i++){
            let flag:boolean;
            flag=true;
          
          this.state.playersInGame.forEach(element => {
            if(element.SitNo==i){
             flag=false;
            }
          });
          if(flag)
          {
            element.SitNo=i;
        //console.log("sit forcefully---"+i);
       
        this.sittingplayercounter++;
        
         
  
        console.log("Player AutoSit "+element+" "+element.SitNo+" sitting players"+ this.sittingplayercounter);
        
        this.broadcast("PlayerSitted",{sitno:i,player:JSON.stringify(element)});
        }
        }
      }
      });
    }
   if(this.sittingplayercounter==this.state.playercounter && this.state.isPlayingGame==false){
     /*this.broadcast("StartGamePreTimer");
     this.clock.clear();
     this.clock.setInterval(()=>{

     },400);*/
this.state.totalPlayersInGame=0;

     this.state.playersInGame.forEach(element => {
       if(element.sitno!=0)
       {
         element.isBlind=true;
         element.dosePacked=false;
         element.isInGame=true;
         this.state.totalPlayersInGame++;
       }
     });
     console.log("Start Your Game From Here");
     let player:Player;
player=new Player();
player.sessionId="";
player.gameId="";
player.PlayerName="";
player.SitNo=0;
player.lastbet=0;
player.myChips=0;
player.myMoney=0;
player.isBot=false;
player.myCards=new ArraySchema<PlayCard>();
player.isConnected=false;
player.isInGame=false;
player.isRemoved=false;
player.isBlind=true;
player.blindCounts=0;
player.slidShowCounts=0;
player.dosePacked=false;
this.state.playersInGame.push(player);
console.log(" 0000000777777889:"+player.SitNo+"  -------------------------"+this.state.playersInGame.length);
this.PlaceLastPlayerOnEmptySit();
   this.SortPlayers();
     this.clock.clear();
     this.clock.start();
     this.state.isPlayingGame=true;
     this.clock.setTimeout(()=>{ 
       console.log("Sending Command for initial bet");
       this.state.playersInGame.forEach(element => {
         if(element.isInGame)
         {
           element.myChips=element.myChips-this.state.currentTable.startingBet;
           this.state.tablePotAmmount+=this.state.currentTable.startingBet;
         }
       });
       this.state.lastBetBySit=0;
       this.state.currentBetAmmount=this.state.currentTable.startingBet*2;
       this.state.currentBlindBetAmmount=this.state.currentTable.startingBet;
       
       this.broadcast("GameControl",{command:"GetStartingBet",sitNo:0});
     },2000);
     
     
     this.clock.setTimeout(()=>{this.DistributeCards();},4000);
     
   }
  }
  public DistributeCards(){
    console.log("start card distribution logic from here");
  

    this.state.remainingDeck=new DeckOfCards();
   // console.log(this.state.deck.DeckOfCards.at(0));
    this.state.remainingDeck.CopyCard(this.state.deck.cardsOfDeck);
    this.clock.clear();
    this.clock.start();
    let cardsNo:number=0;

    this.clock.setInterval(()=>{
      let flag:boolean=true;
let exitFlag:boolean=true;
let isToIncreseCardNo:boolean=true;
this.state.playersInGame.forEach(element => {
  if(element.isInGame){
  if(element.myCards.length != cardsNo)
  {
    isToIncreseCardNo=false;
  }
}
});
if(isToIncreseCardNo){
  if(cardsNo==3){
  
    this.clock.clear();
    this.state.currentTurnOfPlayer=0;
    this.GetNextTurn();
    
    }
    else
  cardsNo++;
}


this.state.playersInGame.forEach(element => {
  //console.log("Player Is in Game??->"+element.isInGame); 
if(element.isInGame && flag  ){ 
  //console.log(" --------///- cardsNO="+cardsNo+" ---- element.count="+element.myCards.length);
  if(element.myCards.length==0 || element.myCards.length<cardsNo ){
 
      let c:PlayCard;
      c=new PlayCard();
     // console.log("call from index");
      c=this.state.remainingDeck.GetRandomCard();
    //  console.log("locha is here in second hand -"+this.state.playersInGame[i].playerHandDetails[0].cards);
     // console.log(" return type -- "+c.cardType);
      element.myCards.push(c);
      

          
           // let ct:number=c.cardType;
      this.broadcast("DistributeMainCard",{sitno:element.SitNo,card:JSON.stringify(c)});
      flag=false;
    
          }}
                  });


    },1000);
  }
public GetNextTurn(){
  console.log(" Get Inside Turn");
  this.state.lastBetBySit=this.state.currentTurnOfPlayer;
  this.state.currentTurnOfPlayer++;
  
  if(this.state.currentTurnOfPlayer>3){
    this.state.currentTurnOfPlayer=1;
  }
  
  {
    if(this.state.playersInGame.at(this.state.currentTurnOfPlayer-1).isInGame==true){
      console.log(" Turn Of player  "+this.state.currentTurnOfPlayer);
      if(this.state.playersInGame.at(this.state.currentTurnOfPlayer-1).blindCounts>=4)
      {
        this.state.playersInGame.at(this.state.currentTurnOfPlayer-1).isBlind=false;
        console.log("Cards Auto Seen By player "+this.state.currentTurnOfPlayer);
        this.broadcast("GameControl",{command:"PlayerSeenCards",sitNo:this.state.currentTurnOfPlayer});
        this.clock.clear();
        this.clock.start();
       this.broadcast("StartGameTurn",{sitNo:this.state.currentTurnOfPlayer});
      }
      
      this.clock.clear();
        this.clock.start();
        this.clock.setTimeout(()=>{
       this.broadcast("StartGameTurn",{sitNo:this.state.currentTurnOfPlayer});
  this.clock.clear();
  this.clock.start();
  this.clock.setTimeout(()=>{
    //logic for auto chaal/blind 
    console.log("le TimeOut ........");
    this.Pack(this.state.currentTurnOfPlayer);
  },9800);   },500); 
  }
    else{
      this.GetNextTurn();
    }
  }
}
public PlaceLastPlayerOnEmptySit(){ //Places last came player to Empty Sit
  
  let arrayIndex:number=0;
  let allocatedSits:Array<number> =new Array<number>();
  this.state.playersInGame.forEach(element => {
    
    if(element.SitNo!=0){
      allocatedSits[arrayIndex]=element.SitNo;
      arrayIndex++;
    }
  });
  if(allocatedSits[0]!=1 && allocatedSits[1]!=1)
  {
    this.state.emptySitNo=1;
  }
  else if(allocatedSits[0]!=2 && allocatedSits[1]!=2)
  {
this.state.emptySitNo=2;
  }
  else
  {
    this.state.emptySitNo=3;
  }
  this.state.playersInGame.at(2).SitNo=this.state.emptySitNo;
  //this.state.playersInGame.at(2).isInGame=false;
 
 // this.SortPlayers(false);
}

public SortPlayers(flag:boolean=true){

  this.lock();
  let tmp:ArraySchema<Player>=new ArraySchema<Player>();
  for(let i:number=1;i<4;i++){
this.state.playersInGame.forEach(element => {
if(element.SitNo==i){
 tmp.push(element);
 //console.log(" -/-/-/-/-/-/-/-/-/-/-/ :"+element.SitNo);
//console.log(" 0000000777777889:  -------------------------"+this.state.playersInGame.length);

}
});
  }
  this.state.playersInGame=new ArraySchema<Player>();
  tmp.forEach(element => {
   
    this.state.playersInGame.push(element);
  });
  if(flag){
  this.unlock();
  }
}




public Pack(sitNo:number)
{
this.state.playersInGame.at(sitNo-1).dosePacked=true;
let totalCardsHavingPlayers:number=0;
this.state.playersInGame.forEach(element => {
  if(element.isInGame==true && element.dosePacked==false)
  {
    totalCardsHavingPlayers++;
  }

  
});
console.log("Packing player1111"+totalCardsHavingPlayers);
if(totalCardsHavingPlayers<2)
  {
    this.broadcast("GameControl",{command:"PlayerPacked",sitNo:sitNo});
    this.Show(true);
  }
  else
  {
    this.clock.clear();
    this.clock.start();
    this.clock.setTimeout(()=>{
 console.log("Packing player2222");

       this.broadcast("GameControl",{command:"PlayerPacked",sitNo:sitNo});
       this.GetNextTurn();
    },1000);
  }
}

public Bet(sitNo:number,DoubleBetMultiplayer:number,isNotFrmoBet:boolean=false)
{
    if(this.state.playersInGame.at(sitNo-1).isBlind ){
      let betAmmount:number=this.state.currentBlindBetAmmount*DoubleBetMultiplayer;
      this.state.playersInGame.at(sitNo-1).myChips-=(betAmmount);
      this.state.tablePotAmmount+=(betAmmount);
      
    //  if(this.state.currentBlindBetAmmount<=betAmmount)
      {
        this.state.currentBlindBetAmmount=betAmmount;
        console.log(" blind bet *2::"+(this.state.currentBlindBetAmmount*2)+" Normal Bet"+this.state.currentBetAmmount);
        if((this.state.currentBlindBetAmmount*2)!=this.state.currentBetAmmount)
        {
          this.state.currentBetAmmount=(this.state.currentBlindBetAmmount*2);
      console.log(" blind bet *2::"+(this.state.currentBlindBetAmmount*2)+" Normal Bet"+this.state.currentBetAmmount);

        }
      }
      this.state.playersInGame.at(sitNo-1).blindCounts++;

    }
    else{
      let betAmmount:number=this.state.currentBetAmmount*DoubleBetMultiplayer;
      this.state.playersInGame.at(sitNo-1).myChips-=(betAmmount);
      this.state.tablePotAmmount+=(betAmmount);
      //if(this.state.currentBetAmmount<=betAmmount)
      {

        this.state.currentBetAmmount=betAmmount;
        console.log(" Normal bet /2::"+(this.state.currentBetAmmount/2)+" blind Bet"+this.state.currentBlindBetAmmount);

        if((this.state.currentBetAmmount/2)!=this.state.currentBlindBetAmmount)
        {
          this.state.currentBlindBetAmmount=(this.state.currentBetAmmount/2);
      console.log(" Normal bet /2::"+(this.state.currentBetAmmount/2)+" blind Bet"+this.state.currentBlindBetAmmount);

        }
      }

    }
       
    console.log(" Normal bet ::"+(this.state.currentBetAmmount)+" blind Bet::"+this.state.currentBlindBetAmmount);

    console.log("After Bet Pot Ammount is"+this.state.tablePotAmmount);
    this.clock.clear();
    this.clock.start();
    this.clock.setTimeout(()=>{
    if(DoubleBetMultiplayer==1){
     this.broadcast("GameControl",{command:"PlayerBet",sitNo:sitNo});

    }
    else{
      this.broadcast("GameControl",{command:"PlayerDoubleBet",sitNo:sitNo});

    }
    console.log("call to next turn from bet()");
    
  this.clock.clear();
  this.clock.start();
  if(this.state.tablePotAmmount< this.state.currentTable.maxPotLimit){
    if(!isNotFrmoBet) {
  this.clock.setTimeout(()=>{
    this.GetNextTurn();   },1000);
    }
  }
  else{
    this.clock.setTimeout(()=>{  this.Show(true);},1000);
  }
},500);
}
public Show(autoShow:boolean)
{
  let playerCount:number=0;
  let players:ArraySchema<Player>=new ArraySchema<Player>();
  this.state.playersInGame.forEach(element => {
    if(element.isInGame==true && element.dosePacked==false)
    {
      playerCount++;
      let tmp:Player=new Player();
      tmp=element;
      players.push(tmp);
    }
  });
  let winLogicObj:WinLogic=new WinLogic();
  console.log("-------<<<<<<<<Show>>>>>>>>----------"+playerCount);
  if(autoShow){
    if(playerCount==2)
    {
    
        let WinningSitNo=winLogicObj.GetWinningPlayerFrom2Players(players.at(0),players.at(1));
        console.log("WinningSitNo==="+WinningSitNo);

        if(WinningSitNo==4)
        {
        console.log("WinningSitNo==="+WinningSitNo+" Message==="+winLogicObj.WinningMessage);

          this.broadcast("AllWIN",{message:winLogicObj.WinningMessage});
        }
        console.log("WinningSitNo==="+WinningSitNo+" Message==="+winLogicObj.WinningMessage);

        this.broadcast("WIN",{sitNO:WinningSitNo,message:winLogicObj.WinningMessage});
    }
    else if(playerCount==1)
    {
        this.broadcast("OnlyLeftWIN",{sitNO:players.at(0).SitNo});
    }
    else
    {
        let winLogicObj:WinLogic=new WinLogic();
        let sortedResultPlayer:ArraySchema<Player>=new ArraySchema<Player>();
        while(players.length!=0)
        {//gting sortedarray schema
                let tmpPlayerIndex:number=0;
                players.forEach(element=>{
                  let tmp:number=winLogicObj.GetWinningPlayerFrom2Players(element,players.at(tmpPlayerIndex));
                  if(tmp==4 || tmp!=players.at(tmpPlayerIndex).SitNo)
                  {
                    tmpPlayerIndex=players.indexOf(element);
                  }
                });
                sortedResultPlayer.push(players.at(tmpPlayerIndex));
                players.splice(tmpPlayerIndex,1);
        }
        if(winLogicObj.GetWinningPlayerFrom2Players(sortedResultPlayer.at(0),sortedResultPlayer.at(1))==4)
        {
          if(winLogicObj.GetWinningPlayerFrom2Players(sortedResultPlayer.at(0),sortedResultPlayer.at(2))==4)
          {//three winners
            this.SetWonChipsToPlayer(sortedResultPlayer.at(0).SitNo,Math.round(this.state.tablePotAmmount/3));
            this.SetWonChipsToPlayer(sortedResultPlayer.at(1).SitNo,Math.round(this.state.tablePotAmmount/3));
            this.SetWonChipsToPlayer(sortedResultPlayer.at(2).SitNo,Math.round(this.state.tablePotAmmount/3));
          this.broadcast("AllWIN",{message:winLogicObj.WinningMessage});
          }
          else
          {//two winners
            this.SetWonChipsToPlayer(sortedResultPlayer.at(0).SitNo,(this.state.tablePotAmmount/2));
            this.SetWonChipsToPlayer(sortedResultPlayer.at(1).SitNo,(this.state.tablePotAmmount/2));

            this.broadcast("TweenWIN",{sitNo1:sortedResultPlayer.at(0).SitNo,SitNo2:sortedResultPlayer.at(1).SitNo,message:winLogicObj.WinningMessage});

          }
        }
        else
        {//first winner
          this.SetWonChipsToPlayer(sortedResultPlayer.at(0).SitNo,this.state.tablePotAmmount);
        this.broadcast("WIN",{sitNO:sortedResultPlayer.at(0).SitNo,message:winLogicObj.WinningMessage});

        }
      
    

    }
  }
  else
  {
          let WinningSitNo=winLogicObj.GetWinningPlayerFrom2Players(players.at(0),players.at(1));
          console.log("WinningSitNo==="+WinningSitNo);

          if(WinningSitNo==4)
          {
            WinningSitNo=this.state.currentTurnOfPlayer;
          }
          console.log("WinningSitNo==="+WinningSitNo+" Message==="+winLogicObj.WinningMessage);
          this.SetWonChipsToPlayer(WinningSitNo,this.state.tablePotAmmount);
          this.broadcast("WIN",{sitNO:WinningSitNo,message:winLogicObj.WinningMessage});   
  }
}
public SetWonChipsToPlayer(sitNo:number,Ammount:number)
{
  this.state.playersInGame.at(sitNo-1).myChips+=Ammount;
}
public SideShowReq(sitNo:number)
{
  if(this.state.playersInGame.at(sitNo-1).slidShowCounts<3)
  {
    this.state.playersInGame.at(sitNo-1).slidShowCounts++;
   // this.Bet(sitNo,1,true);
    this.broadcast("SideShowReq",{sitNo:sitNo});
  }
  else
  {
    this.SideShow(sitNo);
  }
}
public SideShow(sitNo:number)
{//actual logic for side show goes here 
    let result:number,previousSit:number=0;
    let winLogicObj:WinLogic=new WinLogic();
    previousSit=sitNo-1;
    if(sitNo-1 ==0 )
    {
      previousSit=3;
    }
    let p1:Player=new Player();
    let p2:Player=new Player();
    p1=this.state.playersInGame.at(sitNo-1);
    p2=this.state.playersInGame.at(previousSit-1);
    result=winLogicObj.GetWinningPlayerFrom2Players(p1,p2);
    if(result==4){//what to do??????
      result=sitNo;//gappu chh6 conform it before finalizing
    } 
    else if(result=p1.SitNo)
    {
      this.Pack(p2.SitNo);
      this.broadcast("SideShowWIn",{sitNo:sitNo});
    }
    else if(result=p2.SitNo)
    {
      this.Pack(sitNo);
      this.broadcast("SideShowLoss",{sitNo:sitNo});
    }   
}


}
