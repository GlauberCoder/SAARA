import { async } from '@angular/core/testing';
import { OperationModel } from '../models/operations.model';
import '../extensions/number.extensions';

describe('Check if sellPrice is returning the correct value ', () => {

  it('should return correct price(58230.45) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () =>{
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.sellPrice()).toBe(58230.45);
  }));

  it('should return correct price(100) to sell the currency with =>  purchasedPrice:100, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () =>{
    const calc = new OperationModel(100, 100, 3, 0.5, 0.5);
    expect(calc.sellPrice()).toBe(104.02);
  }));

  it('should return correct price(58240.85) to sell the currency with =>  purchasedPrice:55990, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () =>{
    const calc = new OperationModel(55990, 1200, 3, 0.5, 0.5);
    expect(calc.sellPrice()).toBe(58240.85);
  }));


});

describe('Check if nominalGain is returning the correct value', () => {

  it('should return correct nominalValue(4.02%) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.nominalGain()).toBe(4.02);
  }));

});

describe('Check if grossGain is returning the correct value', () => {

  it('should return correct grossGainValue(104.2) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.grossGainValue()).toBe(104.02);
  }));

});

describe('Check if sellValueWithoutTaxes is returning the correct value', () => {

  it('should return correct sellValueWithoutTaxes(3%) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.sellValueWithoutTaxes()).toBe(103);
  }));

});

describe('Check if realGain is returning the correct value', () => {

  it('should return correct realValue(3%) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.realGainInPercentage()).toBe(3);
  }));

});

describe('Check if expectedProfitValue is returning the correct value', () => {

  it('should return correct expectedProfitValue(103) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.expectedProfitValue()).toBe(103);
  }));

});

describe('Check if expectedProfitValue is returning the correct value', () => {

  it('should return correct expectedProfitValue(103) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.finalProfit()).toBe(3);
  }));

});

describe('Check if finalExpectedValueRealValueErrors is returning the correct value', () => {

  it('should return correct finalExpectedValueRealValueErrors(0) to sell the currency with =>  purchasedPrice:55980, expectedProfit: 3%, purchaseTax: 0.5%, sellTax: 0.5%', async( () => {
    const calc = new OperationModel(55980, 100, 3, 0.5, 0.5);
    expect(calc.finalExpectedValueRealValueErrors()).toBe(0);
  }));

});
