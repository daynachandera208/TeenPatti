import { type,Schema, MapSchema, ArraySchema, Context } from "@colyseus/schema";

export class GameControlMsg extends Schema {

   @type('string') command:string;
   @type('int32') sitNo: number;
    }