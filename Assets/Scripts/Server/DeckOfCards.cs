// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class DeckOfCards : Schema {
	[Type(0, "array", typeof(ArraySchema<PlayCard>))]
	public ArraySchema<PlayCard> cardsOfDeck = new ArraySchema<PlayCard>();

	[Type(1, "int32")]
	public int deckLength = default(int);

	[Type(2, "int32")]
	public int counter = default(int);
}

