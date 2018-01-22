// import { async } from '@angular/core/testing';
// import { MovimentDomain} from "../domain/moviment.domain";
// import { Operation, Transaction} from "../domain/operation.domain";
// import '../extensions/number.extensions';
// import {TypesOfTransaction} from "../enum/variables.enum";
// import {ExchangerSymbolDomain} from "../domain/exchanger-symbol.domain";
// import {ExchangerDomain} from "../domain/exchanger.domain";
//
//
// describe('Check if sellPrice is returning the correct value ', () => {
//
//   it('should return correct price(58230.45) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () =>{
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.sellPrice()).toBe(58230.45);
//   }));
//
//   it('should return correct price(100) to sell the currency with =>  purchasedPrice:100, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () =>{
//     const calc = new MovimentDomain(100, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.sellPrice()).toBe(104.02);
//   }));
//
//   it('should return correct price(58240.85) to sell the currency with =>  purchasedPrice:55990, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () =>{
//     const calc = new MovimentDomain(55990, 1200, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.sellPrice()).toBe(58240.85);
//   }));
//
//
// });
//
// describe('Check if nominalGain is returning the correct value', () => {
//
//   it('should return correct nominalValue(4.02%) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.nominalGain()).toBe(4.02);
//   }));
//
// });
//
// describe('Check if CheckPrice % is returning the correct value', () => {
//
//   it('should return to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55990, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.getExpectedFinalPercentage(55000)).toBe(1.77);
//   }));
//
// });
//
// describe('Check if grossGain is returning the correct value', () => {
//
//   it('should return correct grossGainValue(104.2) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.grossGainValue()).toBe(104.02);
//   }));
//
// });
//
// describe('Check if sellValueWithoutTaxes is returning the correct value', () => {
//
//   it('should return correct sellValueWithoutTaxes(3%) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.sellValueWithoutTaxes()).toBe(103);
//   }));
//
// });
//
// describe('Check if realGain is returning the correct value', () => {
//
//   it('should return correct realValue(3%) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.realGainInPercentage()).toBe(3);
//   }));
//
// });
//
// describe('Check if expectedProfitValue is returning the correct value', () => {
//
//   it('should return correct expectedProfitValue(103) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.expectedProfitValue()).toBe(103);
//   }));
//
// });
//
// describe('Check if expectedProfitValue is returning the correct value', () => {
//
//   it('should return correct expectedProfitValue(103) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.finalProfit()).toBe(3);
//   }));
//
// });
//
// describe('Check if finalExpectedValueRealValueErrors is returning the correct value', () => {
//
//   it('should return correct finalExpectedValueRealValueErrors(0) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
//     const calc = new MovimentDomain(55980, 100, 3, new ExchangerSymbolDomain(new ExchangerDomain(0.5, 0.5)));
//     expect(calc.finalExpectedValueRealValueErrors()).toBe(0);
//   }));
//
// });
// //
// describe('Check if AVERAGE SPENT is returning the correct value', () => {
//
//   it('should return correct Average Spent', async( () => {
//     const calc = new Operation();
//     calc.Transactions = [
//       new Transaction(new MovimentDomain(55800,10), 10, TypesOfTransaction.buy),
//       new Transaction(new MovimentDomain(55900,10), 10, TypesOfTransaction.buy),
//     ];
//     expect(calc.averageSpent()).toBe(55850);
//   }));
//
// });
//
// describe('Check if AVERAGE SPENT is returning the correct value', () => {
//
//   it('should return correct Average Spent', async( () => {
//     const calc = new Operation();
//     calc.Transactions= [
//       new Transaction(new MovimentDomain(55800,10), 10, TypesOfTransaction.buy),
//       new Transaction(new MovimentDomain(55900,20), 10, TypesOfTransaction.buy),
//     ];
//     expect(calc.averageSpent()).toBe(55866.67);
//   }));
//
// });
//
// describe('Check if AVERAGE SPENT is returning the correct value', () => {
//
//   it('should return correct Average Spent', async( () => {
//     const calc = new Operation();
//     calc.Transactions= [
//       new Transaction(new MovimentDomain(55800,10), 10, TypesOfTransaction.stop),
//       new Transaction(new MovimentDomain(55900,20), 10, TypesOfTransaction.stop),
//     ];
//     expect(calc.averageSpent()).toBe(55866.67);
//   }));
//
// });
//
// describe('Check if Buy part of transactions is correct is returning the correct value', () => {
//
//   it('should return correct Average Spent', async( () => {
//     const calc = new Operation();
//     calc.Transactions= [
//       new Transaction(new MovimentDomain(55800,20), 70, TypesOfTransaction.buy),
//       new Transaction(new MovimentDomain(55900,20), 100, TypesOfTransaction.buy),
//
//     ];
//     expect(calc.checkTransactions(calc.Transactions, 100, 0)).toEqual({amountBought: 0.001791152916434237 , amountMoney: 0, profit: 0});
//   }));
//
// });
//
// describe('Check if Buy part of transactions is correct is returning the correct value', () => {
//
//   it('should return correct Average Spent', async( () => {
//     const calc = new Operation();
//     calc.Transactions= [
//       new Transaction(new MovimentDomain(55800,100,70), 70, TypesOfTransaction.buy),
//       new Transaction(new MovimentDomain(55900,100,70), 100, TypesOfTransaction.buy),
//       new Transaction(new MovimentDomain(55700,100,70), 30, TypesOfTransaction.stop),
//       new Transaction(new MovimentDomain(55300,100,70), 100, TypesOfTransaction.stop),
//     ];
//     expect(calc.checkTransactions(calc.Transactions, 100, 0)).toEqual({amountBought: 0 , amountMoney: 99.26569462878541, profit : 0.9926569462878541 });
//   }));
//
// });
//
//
//
//
//

import {CalculatorDomain} from "../domain/calculator.domain";
import '../extensions/number.extensions';
import {async} from "q";
import {ExchangerDomain} from "../domain/exchanger.domain";
import {TypesOfTransaction} from "../enum/variables.enum";
import {OperationDomain} from "../domain/operation.domain";
import {MovimentDomain} from "../domain/moviment.domain";

// describe('Check calculator calculations with tax 0% and short: ', () => {
//   it('should return correct calculus', async( () => {
//     const calc = new CalculatorDomain();
//     calc.exchanger = new ExchangerDomain(0, 0);
//     calc.amount = 0.086;
//     calc.entryPrice = 100;
//     calc.exitValue = 10.32;
//     calc.exitPrice = 120;
//     calc.entryValue = 8.6;
//     calc.type = TypesOfTransaction.short;
//
//     expect(calc.calculateAmount()).toBe(0.086);
//     expect(calc.calculateEntryValue()).toBe(8.6);
//     expect(calc.calculateExitValue()).toBe(10.32);
//     expect(calc.calculateExitPL()).toBe(-1.72);
//     expect(calc.calculateExitPLPercentage()).toBe(-20);
//     // expect(calc.calculateOnReceiveExitPrice()).toBe(10.32);
//
//   }));
//
// });

describe('Check calculator calculations with tax 0% and long', () => {
  let calc = new CalculatorDomain();
  beforeEach(() => {
    calc.exchanger = new ExchangerDomain(0, 0);
    calc.amount = 0.086;
    calc.entryPrice = 100;
    calc.exitValue = 10.32;
    calc.exitPrice = 120;
    calc.entryValue = 8.6;
    calc.type = TypesOfTransaction.short;
  });

  it('should return correct Amount', async( () => {
    expect(calc.calculateAmount()).toBe(0.086);
  }));
  it('should return correct Entry Value', async( () => {
    expect(calc.calculateEntryValue()).toBe(8.6);
  }));
  it('should return correct Exit Price', async( () => {
    expect(calc.calculateExitPrice()).toBe(120);
  }));
  it('should return correct Exit Value', async( () => {
    expect(calc.calculateExitValue()).toBe(10.32);
  }));
  it('should return correct Calculate Exit PL', async( () => {
    expect(calc.calculateExitPL()).toBe(-1.72);
  }));
  it('should return correct Calculate PL', async( () => {
    expect(calc.calculateExitPLPercentage()).toBe(-20);
  }));

});


describe('Check calculator calculations with tax 0% and long', () => {
  let calc = new CalculatorDomain();
  beforeEach(() => {
    calc.exchanger = new ExchangerDomain(0, 0);
    calc.amount = 0.086;
    calc.entryPrice = 100;
    calc.exitPrice = 1162.7;
    calc.exitValue = 100;
    calc.entryValue = 8.6;
    calc.type = TypesOfTransaction.long;
  });

  it('should return correct Amount', async( () => {
    expect(calc.calculateAmount()).toBe(0.086);
  }));
  it('should return correct Entry Value', async( () => {
    expect(calc.calculateEntryValue()).toBe(8.6);
  }));
  it('should return correct Exit Price', async( () => {
    expect(calc.calculateExitPrice()).toBe(1162.79);
  }));
  it('should return correct Exit Value', async( () => {
    expect(calc.calculateExitValue()).toBe(99.99);
  }));
  it('should return correct Calculate Exit PL', async( () => {
    expect(calc.calculateExitPL()).toBe(91.4);
  }));
  it('should return correct Calculate PL', async( () => {
    expect(calc.calculateExitPLPercentage()).toBe(1062.70);
  }));

});

describe('Check calculator calculations with tax 0.5% and long', () => {
  let calc = new CalculatorDomain();
  beforeEach(() => {
    calc.exchanger = new ExchangerDomain(0.5, 0.5);
    calc.entryPrice = 100;
    calc.amount = 0.086;
    calc.entryValue = 8.6;
    calc.exitValue = 10.42;
    calc.exitPrice = 121.11;
    calc.type = TypesOfTransaction.long;
  });

  it('should return correct Amount', async( () => {
    expect(calc.calculateAmount()).toBe(0.08557);
  }));
  it('should return correct Entry Value', async( () => {
    expect(calc.calculateEntryValue()).toBe(8.64);
  }));
  it('should return correct Exit Price', async( () => {
    expect(calc.calculateExitPrice()).toBe(121.16);
  }));
  it('should return correct Exit Value', async( () => {
    expect(calc.calculateExitValue()).toBe(10.42);
  }));
  it('should return correct Calculate Exit PL', async( () => {
    expect(calc.calculateExitPL()).toBe(1.81);
  }));
  it('should return correct Calculate PL', async( () => {
    expect(calc.calculateExitPLPercentage()).toBe(21.11);
  }));

});

describe('Check Operations Calculations', () => {
  let operations = new OperationDomain();
  beforeEach(() => {
    operations.moviments = [
      new MovimentDomain(100, 8.6, 0.5, TypesOfTransaction.long),
      new MovimentDomain(100, 8.6, 0.5, TypesOfTransaction.short),
      new MovimentDomain(100, 8.6, 0.5, TypesOfTransaction.long),
      new MovimentDomain(100, 8.6, 0.5, TypesOfTransaction.short),
      new MovimentDomain(100, 8.6, 0.5, TypesOfTransaction.long),
      new MovimentDomain(100, 8.6, 0.5, TypesOfTransaction.short),
    ]
  });

  it('should return correct Value Spent on taxes', async( () => {
    expect(operations.getAmountSpentOnTaxes()).toBe(0.258 );
  }));
  it('should return correct Of Calculate profit', async( () => {
    expect(operations.calculateProfit()).toBe(0);
  }));
  it('should return correct Of Investiment', async( () => {
    expect(operations.calculateInvestment()).toBe(25.8);
  }));
  it('should return correct Of Amount spent on taxes', async( () => {
    expect(operations.getPercentAmountSpentOnTaxes()).toBe(0.01);
  }));

});


