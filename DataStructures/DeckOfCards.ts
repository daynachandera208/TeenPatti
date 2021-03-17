import { type,Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";
import {PlayCard,CardsType} from "./PlayCard"
  export class DeckOfCards extends Schema{
  @type([PlayCard]) cardsOfDeck:ArraySchema<PlayCard>;
  @type('int32') deckLength:number;
  @type('int32') counter:number;


  public CopyCard(cD:ArraySchema<PlayCard>){
    this.deckLength=52;  
    console.log("-- copycard- --"+cD.length);
  //  hghlgjk yaha pe locha he use solve karna hen :>#!)/?
this.counter=0;
    this.cardsOfDeck=new ArraySchema<PlayCard>();
    this.cardsOfDeck.clear();
    //this.cardsOfDeck=cD;
    cD.forEach(e=>{
     //   console.log(e);
        this.cardsOfDeck.push(e);
    });
  }

  public  SuffleDeck()
  {
    console.log("-- suffledeck-");
    this.deckLength=52;
    this.cardsOfDeck=new ArraySchema<PlayCard>();
    this.cardsOfDeck.clear();
    
    for( const i in CardsType)
    { 
        if(isNaN(Number(CardsType[i])))
        {
            for(let j:number=1;j<14;j++)
            {
                let c:PlayCard;
                c=new PlayCard();
                c.cardType=CardsType[CardsType[i]];
                c.cardValue=j;
                if(j>1 )
                c.cardPoint=j;
                else if(j==1)
                c.cardPoint=14;
                this.cardsOfDeck.push(c);
            }
        }
    }  
   
  }
  
  public GetRandomCard(winProbability:Boolean=true,currentPoints:number=0,opponentPoints:number=0)
  {
    let c:PlayCard;
    c=new PlayCard();
    
    if(currentPoints==0 && opponentPoints==0){
        let index:number;
       index= Math.floor(Math.random() * (this.deckLength-1  + 1));
        c=this.cardsOfDeck.at(index);
        this.RemoveCard(index);
        return c;
    }
    if(winProbability){
        if (currentPoints == 0 && opponentPoints == 0)
            {
                let index:number;
       index= Math.floor(Math.random() * (this.deckLength-1 - 0 + 1)) + 0;
        c=this.cardsOfDeck.at(index);
        this.RemoveCard(index);
        return c;
            }
            
            else if (currentPoints < opponentPoints)
            {
                let tmp:number = (opponentPoints - currentPoints) + 1;
                if (tmp == 11)
                    return this.GetCard(11, 11);
                else if (tmp < 10)
                {
                    if (currentPoints + 10 <= 21)
                        return this.GetCard(tmp, 11);
                    else
                        if ((21 - currentPoints) > tmp)
                        return this.GetCard(tmp, (21 - currentPoints));
                    else
                        return this.GetCard(tmp,tmp);

                }
                else
                    return this.GetCard(tmp, tmp);
            }
            else
            {
                if (currentPoints + 10 <= 21)
                {
                    return this.GetCard(2, 10);
                }
                else
                    return this.GetCard(2, (21 - currentPoints));

            }
    }
    else
    {
        if (currentPoints > 11)
        { //on hit sending burst card
            return this.GetCard(10, 10);
        }
        else {
            if(16-currentPoints<11)
            return this.GetCard(2, (16-currentPoints));
            else
                return this.GetCard(2, 10);
        }
   // }
    }
  }
  public GetCard(min:number,max:number)
  {
   // console.log("ahiyathi call jjay 6");


    let cards:ArraySchema<PlayCard> =new ArraySchema<PlayCard>();
    let point:number = Math.floor(Math.random() * (max - min + 1)) + min;
   let cardsCount:number=0;
   console.log("call from inside");

    //console.log("Card "+point);
    while (cardsCount == 0)
    {
       this.cardsOfDeck.forEach ( c =>
        {
            if (c.cardPoint == point)
            {
                cards.push(c);
                cardsCount++;
            }
        });

        if (cardsCount == 0 )
        {
            console.log("locho");
            
            let oldpoint:number = point;
            
            point = Math.floor(Math.random() * (max - min + 1)) + min;
            if (point == oldpoint) {
                if (point <= max && point > min)
                    point--;
                else
                    point++;
                if (min == max)
                {
                    if (point < 10)
                        point++;
                    else
                        point--;
                }
            }
        }
    }

    let index:number= 0;
    index=Math.floor(Math.random() * ((cardsCount-1) - 0 + 1)) + 0;
   // index= this.cardsOfDeck.indexOf(cards[index]);
   let ind:number=0;
   ind=this.cardsOfDeck.indexOf(cards.at(index));
    let c1:PlayCard;
    c1=new PlayCard();
    c1=this.cardsOfDeck.at(ind); 
    this.RemoveCard(ind);
    //console.log(c1+" "+index +"  "+cardsCount+" "+ind);
    return c1;
}
  RemoveCard(index:number){
      this.cardsOfDeck.deleteAt(index);
      this.deckLength--;
  }
  



}
