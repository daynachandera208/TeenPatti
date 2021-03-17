// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.11
// 

using Colyseus.Schema;

public partial class PlayCard : Schema {
	[Type(0, "int32")]
	public int cardType = default(int);

	[Type(1, "int32")]
	public int cardValue = default(int);

	[Type(2, "int32")]
	public int cardPoint = default(int);
}

