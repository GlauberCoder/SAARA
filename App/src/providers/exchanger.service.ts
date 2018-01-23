import { Injectable } from '@angular/core';
import { Exchanger } from '../domain/exchanger';
import {ExchangerSymbol} from '../domain/exchanger-symbol';
import {Symbol} from '../domain/symbol';


@Injectable()
export class ExchangerService{

  constructor(){
    console.log('exhanger service done');
  }


  getExchangers(){
    return [
      new ExchangerSymbol(new Exchanger(0, 0), new Symbol('NOTAX')),
      new ExchangerSymbol(new Exchanger(0.5, 0.5), new Symbol('BTCUSD')),
      new ExchangerSymbol(new Exchanger(0.3, 0.3), new Symbol('BTCBRL')),
    ];
  }

}
