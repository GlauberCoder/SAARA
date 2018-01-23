import { SymbolDomain } from "./symbol.domain";
import {ExchangerDomain} from "./exchanger.domain";

export class ExchangerSymbolDomain{


  constructor(
    public exchanger?: ExchangerDomain,
    public symbol?: SymbolDomain
  ){}

}
