// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class Player : Schema {
	[Type(0, "string")]
	public string sessionId = default(string);

	[Type(1, "string")]
	public string gameId = default(string);

	[Type(2, "string")]
	public string PlayerName = default(string);

	[Type(3, "uint8")]
	public uint SitNo = default(uint);

	[Type(4, "uint64")]
	public ulong lastbet = default(ulong);

	[Type(5, "uint64")]
	public ulong myChips = default(ulong);

	[Type(6, "uint64")]
	public ulong myMoney = default(ulong);

	[Type(7, "int32")]
	public int blindCounts = default(int);

	[Type(8, "int32")]
	public int slidShowCounts = default(int);

	[Type(9, "array", typeof(ArraySchema<PlayCard>))]
	public ArraySchema<PlayCard> myCards = new ArraySchema<PlayCard>();

	[Type(10, "boolean")]
	public bool isBot = default(bool);

	[Type(11, "boolean")]
	public bool isBlind = default(bool);

	[Type(12, "boolean")]
	public bool isConnected = default(bool);

	[Type(13, "boolean")]
	public bool isRemoved = default(bool);

	[Type(14, "boolean")]
	public bool dosePacked = default(bool);

	[Type(15, "boolean")]
	public bool isInGame = default(bool);
}

