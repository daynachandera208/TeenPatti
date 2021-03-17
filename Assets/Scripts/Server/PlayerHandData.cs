// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class PlayerHandData : Schema {
	[Type(0, "uint8")]
	public uint points = default(uint);

	[Type(1, "uint64")]
	public ulong bet = default(ulong);

	[Type(2, "array", typeof(ArraySchema<PlayCard>))]
	public ArraySchema<PlayCard> cards = new ArraySchema<PlayCard>();

	[Type(3, "boolean")]
	public bool isDouble = default(bool);

	[Type(4, "boolean")]
	public bool isInsured = default(bool);

	[Type(5, "boolean")]
	public bool isSurrender = default(bool);

	[Type(6, "boolean")]
	public bool isSplited = default(bool);

	[Type(7, "uint8")]
	public uint splitno = default(uint);
}

