// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class GameTable : Schema {
	[Type(0, "uint64")]
	public ulong startingBet = default(ulong);

	[Type(1, "uint64")]
	public ulong maxBetLimit = default(ulong);

	[Type(2, "uint64")]
	public ulong maxPotLimit = default(ulong);

	[Type(3, "uint8")]
	public uint handsLimit = default(uint);

	[Type(4, "string")]
	public string tableName = default(string);

	[Type(5, "uint32")]
	public uint entryFees = default(uint);
}

