import { type,Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";
import {GameTable} from "../DataStructures/GameTable"
import {PlayCard} from "../DataStructures/PlayCard"
import {Player} from "../DataStructures/Player"
import {DeckOfCards} from "../DataStructures/DeckOfCards"
export class WinLogic extends Schema {

 @type('int32') winnerSitNo:number;
 @type ('string') WinningMessage:string;
    public sortCards(cards:ArraySchema<PlayCard> )
    {
      let a:PlayCard,b:PlayCard,c:PlayCard,highCard:PlayCard,midCard:PlayCard,lowCard:PlayCard;
      a=cards.pop();
      b=cards.pop();
      c=cards.pop();
      if(a.cardPoint>b.cardPoint && a.cardPoint>c.cardPoint )
      {//a is highest
        highCard=a;
        if(b.cardPoint>c.cardPoint)
        {//b is mid c is low
            midCard=b;
            lowCard=c;
        }
        else
        {//c is mid b is low
            midCard=c;
            lowCard=b;
        }
      }
      else if(b.cardPoint>c.cardPoint)
      {//b is highest
        highCard=b;
        if(a.cardPoint>c.cardPoint)
        {//a is mid c is low
            midCard=a;
            lowCard=c;
        }
        else
        {//c is mid a is low
            midCard=c;
            lowCard=a;
        }
      }
      else
      {//c is highest
        highCard=c;
        if(a.cardPoint>b.cardPoint)
        {//a is mid b is low
            midCard=a;
            lowCard=b;
        }
        else
        {//b is mid a is low
            midCard=b;
            lowCard=a;
        }
      }
    
     // highCard=(a.cardPoint>b.cardPoint)?(a.cardPoint>c.cardPoint?a:(b.cardPoint>c.cardPoint)?b:c):(b.cardPoint>c.cardPoint)?b:c;//getting highest pointed card
      cards.clear();
      cards.push(highCard);
      cards.push(midCard);
      cards.push(lowCard);
      
      
    }
    
    
    public CompareForTrail(player1:Player,player2:Player)
    {
        let flagForPlayer1:boolean=false,flagForPlayer2:boolean=false;
        if(player1.myCards.at(0).cardPoint==player1.myCards.at(1).cardPoint && player1.myCards.at(0).cardPoint==player1.myCards.at(2).cardPoint){flagForPlayer1=true;}//check for cards1 trail
        if(player2.myCards.at(0).cardPoint==player2.myCards.at(1).cardPoint && player2.myCards.at(0).cardPoint==player2.myCards.at(2).cardPoint){flagForPlayer2=true;}//check for cards1 trail
        if(flagForPlayer1==true && flagForPlayer2==true)
        {
            if(player1.myCards.at(0).cardPoint>player2.myCards.at(0).cardPoint)
            {//player1 have high trail
                this.winnerSitNo=player1.SitNo;
                this.WinningMessage="High Trail";
                return player1.SitNo;
            }
            else 
            {//player2 have high trail
                this.winnerSitNo=player1.SitNo;
                this.WinningMessage="High Trail";
                return player1.SitNo;
            }
            
        }
        else if(flagForPlayer1==true)
        {//player1 have trail
            this.winnerSitNo=player1.SitNo;
            this.WinningMessage="Trail";
            return player1.SitNo;
        }
        else if(flagForPlayer2==true)
        {//player2 have trail
            this.winnerSitNo=player2.SitNo;
            this.WinningMessage="Trail";
            return player2.SitNo;
        }
        else{return 0;}//no one have trail
    }
    
    public IsColor(cards:ArraySchema<PlayCard>)
    {//checking for color used in pure sequence and color rules
        if(cards.at(0).cardType==cards.at(1).cardType && cards.at(0).cardType==cards.at(2).cardType){return true;}
        else {return false;}
    }
    
    
    public IsSequence(cards:ArraySchema<PlayCard>)
    {
        if((cards.at(0).cardPoint==cards.at(1).cardPoint+1) && (cards.at(0).cardPoint==cards.at(2).cardPoint+2))
        {
            return true;
        }
        else if(((cards.at(0).cardPoint==14) && (cards.at(1).cardPoint==3)&& (cards.at(2).cardPoint==2)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /*public IsPureSequence(cards:ArraySchema<PlayCard>)
    { 
        if(this.IsColor(cards) && this.IsSequence(cards)){return true;}
        else{return false;}
    }*/
    
    public ComparePureSequenceorSequence(player1:Player,player2:Player)
    {//check for sequence or pure sequence
        if(this.IsSequence(player1.myCards) && this.IsSequence(player2.myCards)) 
        {//both are in sequence compare further for pure sequence and high sequence
            if(this.IsColor(player1.myCards) && this.IsColor(player2.myCards))
            {//both are in pure sequence compare further for high sequence
                let res:number= this.CompareHIghCard(player1.myCards,player2.myCards);
                switch(res)
                {
                    case 1:
                                this.winnerSitNo=player1.SitNo;
                                this.WinningMessage="High Pure Sequence"
                                return player1.SitNo;
                    case 2:
                                this.winnerSitNo=player2.SitNo;
                                this.WinningMessage="High Pure Sequence"
                                return player2.SitNo;
                    case 3:
                                this.WinningMessage="Pure Sequence"
                                this.winnerSitNo=4;
                                this.winnerSitNo=4;
                                return 4;
                                break;
                }
                
            }
            else if(this.IsColor(player1.myCards)==true && this.IsColor(player2.myCards)==false)
            {//cards1 is pure seq. hance 1 wins
                this.winnerSitNo=player1.SitNo;
                this.WinningMessage="Pure Sequence"
                return player1.SitNo;
            }
            else if(this.IsColor(player1.myCards)==false && this.IsColor(player2.myCards)==true)
            {//cards1 is pure seq. hance 1 wins
                            this.winnerSitNo=player2.SitNo;
                            this.WinningMessage="Pure Sequence"
                            return player2.SitNo;
            }
            else
            {//compare for high sequence
              
                let res:number= this.CompareHIghCard(player1.myCards,player2.myCards);
                switch(res)
                {
                    case 1:
                                this.winnerSitNo=player1.SitNo;
                                this.WinningMessage="High Sequence"
                                return player1.SitNo;
                    case 2:
                                this.winnerSitNo=player2.SitNo;
                                this.WinningMessage="High Sequence"
                                return player2.SitNo;
                    case 3:
                                this.WinningMessage="Sequence"
                                this.winnerSitNo=4;
                                return 4;
                                break;
                }
            }
        }
        else if(this.IsSequence(player1.myCards)==true && this.IsSequence(player2.myCards)==false)
        {//cards1 is higher hance 1 wins
                            this.winnerSitNo=player1.SitNo;
                            this.WinningMessage="Sequence"
                            return player1.SitNo;
        }
        else if(this.IsSequence(player1.myCards)==false && this.IsSequence(player2.myCards)==true)
        {//cards2 is higher hance 2 wins
                            this.winnerSitNo=player2.SitNo;
                            this.WinningMessage="Sequence"
                            return player2.SitNo;
        }
        else
        {//both don't have sequence  or pure sequence
            return 0; 
        }
    
    }
    public CompareHIghCard(cards1:ArraySchema<PlayCard>,cards2:ArraySchema<PlayCard>)
    {
        if(cards1.at(0).cardPoint>cards2.at(0).cardPoint)
        {//cards1 is high of player 1
            return 1;
        }
        else if(cards1.at(0).cardPoint<cards2.at(0).cardPoint)
        {//card1 is high of player 2
            return 2;
        }
        else
        {// both are same chack for secod cards
            if(cards1.at(1).cardPoint>cards2.at(1).cardPoint)
            {//cards2 is high of player 1
                return 1;
            }
            else if(cards1.at(1).cardPoint<cards2.at(1).cardPoint)
            {//card2 is high of player 2
                return 2;
            }
            else
            {// both are same chack for Third cards
                if(cards1.at(2).cardPoint>cards2.at(2).cardPoint)
                {//cards3 is high of player 1
                    return 1;
                }
                else if(cards1.at(2).cardPoint<cards2.at(2).cardPoint)
                {//card3 is high of player 1
                    return 2;
                }
                else
                {// all are same tie
                    return 3;
                }
            }
        }
    }
    
    public CompareColor(player1:Player,player2:Player)
    {
        if(this.IsColor(player1.myCards)&& this.IsColor(player2.myCards))
        {//both are color check for high cards
            let res:number= this.CompareHIghCard(player1.myCards,player2.myCards);
            switch(res)
            {
                case 1:
                            this.winnerSitNo=player1.SitNo;
                            this.WinningMessage="High Color"
                            return player1.SitNo;
                case 2:
                            this.winnerSitNo=player2.SitNo;
                            this.WinningMessage="High Color"
                            return player2.SitNo;
                case 3:
                            this.WinningMessage="Color"
                            this.winnerSitNo=4;
                            return 4;
                            break;
            }
        }
        else if(this.IsColor(player1.myCards)==true && this.IsColor(player2.myCards)==false)
        {//cards1 is color hance wins
                this.winnerSitNo=player1.SitNo;
                this.WinningMessage="Color"
                return player1.SitNo;
        }
        else if(this.IsColor(player1.myCards)==false&& this.IsColor(player2.myCards)==true)
        {//cards2 is color hance wins
                this.winnerSitNo=player2.SitNo;
                this.WinningMessage="Color"
                return player2.SitNo;
        }
        else
        {// both don't have color
            return 0;
        }
    }
    public IsPair(cards:ArraySchema<PlayCard>)
    {
        if(cards.at(0).cardPoint==cards.at(1) || cards.at(1).cardPoint==cards.at(2))// || cards.at(0).cardPoint==cards.at(2))
        {return true;}
        else{return false;}
    }
    public ComparePairHighCards(cards1:ArraySchema<PlayCard>,cards2:ArraySchema<PlayCard>)
    {
        let pairCardPoint1:number,pairCardPoint2:number,otherCardPoint1:number,otherCardPoint2:number;
        if(cards1.at(0).cardPoint==cards1.at(1).cardPoint)
        {
            pairCardPoint1=cards1.at(0).cardPoint;
            otherCardPoint1=cards1.at(2).cardPoint;
        }
        else
        {
            pairCardPoint1=cards1.at(2).cardPoint;
            otherCardPoint1=cards1.at(0).cardPoint;
        }
        if(cards2.at(0).cardPoint==cards2.at(1).cardPoint)
        {
            pairCardPoint2=cards2.at(0).cardPoint;
            otherCardPoint2=cards2.at(2).cardPoint;
        }
        else
        {
            pairCardPoint2=cards2.at(2).cardPoint;
            otherCardPoint2=cards2.at(0).cardPoint;
        }
        if(pairCardPoint1>pairCardPoint2)
        {
            return 1;
        }
        else if(pairCardPoint1<pairCardPoint2)
        {
            return 2;
        }
        else
        {
            if(otherCardPoint1>otherCardPoint2)
            {
                return 1;
            }
            else if(otherCardPoint1<otherCardPoint2)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
    public ComparePair(player1:Player,player2:Player)
    {
        if(this.IsPair(player1.myCards) && this.IsPair(player2.myCards))
        {

            let res:number=this.ComparePairHighCards(player1.myCards, player2.myCards);
            //this.CompareHIghCard(player1.myCards,player2.myCards);
            
            switch(res)
            {
                case 1:
                            this.winnerSitNo=player1.SitNo;
                            this.WinningMessage="High Pair"
                            return player1.SitNo;
                case 2:
                            this.winnerSitNo=player2.SitNo;
                            this.WinningMessage="High Pair"
                            return player2.SitNo;
                case 3:
                            this.WinningMessage="Pair"
                            this.winnerSitNo=4;
                            return 4;
                            break;
            }

        }
        else if(this.IsPair(player1.myCards)==true && this.IsPair(player2.myCards)==false)
        {
            this.winnerSitNo=player1.SitNo;
            this.WinningMessage="Pair"
            return player1.SitNo;
        }
        else if(this.IsPair(player1.myCards)==false && this.IsPair(player2.myCards)==true)
        {
            this.winnerSitNo=player2.SitNo;
            this.WinningMessage="Pair"
            return player2.SitNo;
        }
        else
        {
            return 0;
        }

    }
    public GetHighCardResult(player1:Player,player2:Player)
    {
        let res:number=this.CompareHIghCard(player1.myCards,player2.myCards);
        switch(res)
        {
            case 1:
                        this.winnerSitNo=player1.SitNo;
                        this.WinningMessage="High Card"
                        return player1.SitNo;
            case 2:
                        this.winnerSitNo=player2.SitNo;
                        this.WinningMessage="High card"
                        return player2.SitNo;
            case 3:
                        this.WinningMessage="high card"
                        this.winnerSitNo=4;
                        return 4;
                        break;
        }

    }
    public GetWinningPlayerFrom2Players(player1:Player,player2:Player)
    {
        //returns sit no 1,2,3 for winner 
        //returns 4 if tie .

        let winner:number=0;
        winner=this.CompareForTrail(player1,player2);//check for trail
        
        if(winner==0)
        {//check for sequence/pure sequence
            this.sortCards(player1.myCards);
            this.sortCards(player2.myCards);
            winner=this.ComparePureSequenceorSequence(player1,player2);
            if(winner==0)
            {//check for color
                winner=this.CompareColor(player1,player2);
                if(winner==0)
                {//check for Pair
                        winner=this.ComparePair(player1,player2);   
                        if(winner==0)
                        {//check for Highcard
                                winner=this.GetHighCardResult(player1,player2);   
                                return this.winnerSitNo;//high card
                        }
                        else{//pair
                            return this.winnerSitNo;
                        }
                }
                else{//color
                    return this.winnerSitNo;
                }
            }
            else{//seq./pure seq.
                return this.winnerSitNo;
            }

        }
        else{//trail
            return this.winnerSitNo;
        }
    }
    
}