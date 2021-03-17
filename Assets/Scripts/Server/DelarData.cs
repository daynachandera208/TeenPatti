// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class DelarData : Schema {
	[Type(0, "uint8")]
	public uint points = default(uint);

	[Type(1, "array", typeof(ArraySchema<PlayCard>))]
	public ArraySchema<PlayCard> cards = new ArraySchema<PlayCard>();
}

