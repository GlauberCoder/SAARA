import { Symbol } from './symbol';
import {Exchanger} from './exchanger';

export class ExchangerSymbol {

  constructor(
    public exchanger?: Exchanger,
    public symbol?: Symbol
  ) {}

}
