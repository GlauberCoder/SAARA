import { Injectable } from '@angular/core';
import { ExchangerDomain } from '../domain/exchanger.domain';
import {ExchangerSymbolDomain} from '../domain/exchanger-symbol.domain';
import {SymbolDomain} from '../domain/symbol.domain';


@Injectable()
export class ExchangerService{

  constructor(){
    console.log('exhanger service done');
  }


  getExchangers(){
    return [
      new ExchangerSymbolDomain(new ExchangerDomain(0, 0), new SymbolDomain('NOTAX')),
      new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5), new SymbolDomain('BTCUSD')),
      new ExchangerSymbolDomain(new ExchangerDomain(0.3, 0.3), new SymbolDomain('BTCBRL')),
    ];
  }

}
