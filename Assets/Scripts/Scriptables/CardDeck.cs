using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData;

[CreateAssetMenu(fileName ="new cardDeck",menuName ="cardDeck")]
public class CardDeck : ScriptableObject
{
    public List<card> cardDeck = new List<card>();
    public void InitializeDeck()
    {
        card temp = new card();
      cardDeck.Clear();
        for (int j = 1; j < 5; j++)
        {
            for (int i = 1; i < 14; i++)
            {
                temp = new card();
                if (i < 11 && i > 1)
                    temp.cardPoints = i;
                else if (i == 1)
                    temp.cardPoints = 11;
                else
                    temp.cardPoints = 10;
                temp.cardType = (typesOfCard)System.Enum.Parse(typeof(typesOfCard), j.ToString());
                temp.cardValue = i;
              cardDeck.Add(temp);
            }
        }
        Debug.Log("card Initialization ++"+cardDeck.Count);
    }
}
