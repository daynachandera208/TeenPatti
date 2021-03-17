// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class State : Schema {
	[Type(0, "ref", typeof(GameTable))]
	public GameTable currentTable = new GameTable();

	[Type(1, "uint8")]
	public uint tableIndex = default(uint);

	[Type(2, "ref", typeof(DeckOfCards))]
	public DeckOfCards deck = new DeckOfCards();

	[Type(3, "ref", typeof(DeckOfCards))]
	public DeckOfCards remainingDeck = new DeckOfCards();

	[Type(4, "int32")]
	public int currentHandNo = default(int);

	[Type(5, "array", typeof(ArraySchema<Player>))]
	public ArraySchema<Player> playersInGame = new ArraySchema<Player>();

	[Type(6, "int8")]
	public int CurrentHand = default(int);

	[Type(7, "boolean")]
	public bool BotWinProbability = default(bool);

	[Type(8, "int32")]
	public int currentTurnOfPlayer = default(int);

	[Type(9, "int32")]
	public int playercounter = default(int);

	[Type(10, "boolean")]
	public bool isPlayingGame = default(bool);

	[Type(11, "uint64")]
	public ulong tablePotAmmount = default(ulong);

	[Type(12, "uint64")]
	public ulong currentBetAmmount = default(ulong);

	[Type(13, "uint64")]
	public ulong currentBlindBetAmmount = default(ulong);

	[Type(14, "int32")]
	public int lastBetBySit = default(int);

	[Type(15, "int32")]
	public int totalPlayersInGame = default(int);

	[Type(16, "int32")]
	public int emptySitNo = default(int);
}

