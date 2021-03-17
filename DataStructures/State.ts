import { type,Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";
import {GameTable} from "./GameTable"
import {PlayCard} from "./PlayCard"
import {Player} from "./Player"
//import {DelarData} from "./DelarData"
import {DeckOfCards} from "./DeckOfCards"

export class State extends Schema {

    @type(GameTable)currentTable:GameTable;
    @type('uint8') tableIndex:number;
    @type(DeckOfCards)deck:DeckOfCards;
    @type(DeckOfCards)remainingDeck:DeckOfCards;
    @type('int32') currentHandNo:number;
    @type([Player])playersInGame:ArraySchema<Player>=new ArraySchema<Player>();
    @type('int8') CurrentHand:number;
    @type('boolean') BotWinProbability:boolean;
    @type('int32') currentTurnOfPlayer:number;
    @type('int32') playercounter:number;
    @type('boolean') isPlayingGame:boolean;
    @type('uint64') tablePotAmmount:number;
    @type('uint64') currentBetAmmount:number;
    @type('uint64') currentBlindBetAmmount:number;
    @type('int32') lastBetBySit:number;
    @type('int32') totalPlayersInGame:number;
    @type('int32') emptySitNo:number;

}