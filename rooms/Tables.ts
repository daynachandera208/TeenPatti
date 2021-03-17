import { Room, Client, generateId } from "colyseus";
import { Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";
import { verifyToken, User, IUser } from "@colyseus/social";
import { DeckOfCards } from "../DataStructures/DeckOfCards";
//import { DelarData } from "../DataStructures/DelarData";
import { GameTable } from "../DataStructures/GameTable";
import { PlayCard } from "../DataStructures/PlayCard";
import { Player } from "../DataStructures/Player";
//import { PlayerHandData } from "../DataStructures/PlayerHandData";
import { State } from "../DataStructures/State";

import {GameRoom} from "./GameRoom";
export class Table1 extends GameRoom 
{ 
    SetTable(){
        this.state.currentTable=new GameTable();
        this.state.currentTable.startingBet=100;
        this.state.currentTable.maxPotLimit=50000;
        this.state.currentTable.maxBetLimit=3200;
        this.state.currentTable.handsLimit=5;
        this.state.currentTable.entryFees=5;
        this.state.currentTable.tableName="Table1";
        console.log('Table1 Created');
    }
}
export class Table2 extends GameRoom 
{ SetTable(){
    this.state.currentTable=new GameTable();
    this.state.currentTable.startingBet=200;
    this.state.currentTable.maxBetLimit=10000;

    this.state.currentTable.maxPotLimit=100000;
    this.state.currentTable.handsLimit=10;
    this.state.currentTable.entryFees=25;
    this.state.currentTable.tableName="Table2";
    console.log('Table2 Created');

    }
}
export class Table3 extends GameRoom 
{ 
    SetTable(){
        this.state.currentTable=new GameTable();
        this.state.currentTable.startingBet=300;
    this.state.currentTable.maxBetLimit=20000;

        this.state.currentTable.maxPotLimit=500000;
        this.state.currentTable.handsLimit=15;
        this.state.currentTable.entryFees=50;
        this.state.currentTable.tableName="Table3";
        console.log('Table3 Created');

    }
}
