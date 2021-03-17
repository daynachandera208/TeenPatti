using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

[CreateAssetMenu(fileName ="new remainigCardDeck",menuName ="remainingCardDeck")]
public class RemainingDeck : ScriptableObject
{
    public int tmp;
    int temp;
    public CardDeck allCards;
    public List<card> remainingDeck = new List<card>();
    public void ReloadDeck()
    {
       
        remainingDeck = new List<card>();
        foreach (card c in allCards.cardDeck) {
            remainingDeck.Add(c);
        }
        tmp = 0;
        
    }
    public card GetRandomCard(bool winProbability =true, int currentPoints =0, int oponentPoints=0) {

        
        card c = new card();
        temp = 0;
       /*  if (tmp < 5)
         {
             c = remainingDeck[10];
                 tmp++;
            return c;
         }
         */
        //return GetCard(2, 6);
        if (currentPoints == 0 && oponentPoints == 0)
        {
            c = remainingDeck[Random.Range(0, remainingDeck.Count)];
            remainingDeck.Remove(c);
            return c;
        }
        if (winProbability)
        {
            if (currentPoints == 0 && oponentPoints == 0)
            {
                c = remainingDeck[Random.Range(0, remainingDeck.Count)];
                remainingDeck.Remove(c);
                return c;
            }
            else if (currentPoints < oponentPoints)
            {
                int tmp = (oponentPoints - currentPoints) + 1;
                if (tmp == 11)
                    return GetCard(11, 11);
                else if (tmp < 10)
                {
                    if (currentPoints + 10 <= 21)
                        return GetCard(tmp, 11);
                    else
                        if ((21 - currentPoints) > tmp)
                        return GetCard(tmp, (21 - currentPoints));
                    else
                        return GetCard(tmp,tmp);

                }
                else
                    return GetCard(tmp, tmp);
            }
            else
            {
                if (currentPoints + 10 <= 21)
                {
                    return GetCard(2, 10);
                }
                else
                    return GetCard(2, (21 - currentPoints));

            }
        }
        else
        {
            if (currentPoints > 11)
            { //on hit sending burst card
                return GetCard(10, 10);
            }
            else {
                if(16-currentPoints<11)
                return GetCard(2, (16-currentPoints));
                else
                    return GetCard(2, 10);
            }
          /*  if (currentPoints > oponentPoints)
            {
                if (currentPoints <= 11)
                {//dispencing second card
                    return GetCard(2, 6);
                }
                else
                { //on hit sending burst card
                    return GetCard(10,10);
                }
            }
            else if (currentPoints < oponentPoints)
            {
                if (currentPoints + 10 < 17)
                    return GetCard(2, 10);
                else
                    return GetCard(2,(16-currentPoints));
            }
            else
            {
                return GetCard(2, 6);
            }
            */
        }
      //      c = remainingDeck[Random.Range(0, remainingDeck.Count)];
       // remainingDeck.Remove(c);//}
       // return c;
    }
    card GetCard(int min,int max)
    {
        
        List<card> cards = new List<card>();
        int point = (int)Random.RandomRange(min, max);
       
        Debug.Log("Card "+point);
        while (cards.Count == 0)
        {
            foreach (card c in remainingDeck)
            {
                if (c.cardPoints == point)
                {
                    cards.Add(c);
                }
            }

            if (cards.Count == 0 && temp < 3)
            {
                temp++;
                int oldpoint = point;
                
                point = (int)Random.RandomRange(min, max);
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
           /* else if (temp > 3)
            {
               card c = remainingDeck[Random.Range(0, remainingDeck.Count)];
                remainingDeck.Remove(c);
                return c;
            }*/
        }
       
        card cardToRemove = cards[Random.RandomRange(0, cards.Count)];
        remainingDeck.Remove(cardToRemove);
        return cardToRemove;
    }
}
