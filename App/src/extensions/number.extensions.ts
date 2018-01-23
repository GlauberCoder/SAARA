interface Number{
  proportionOn(value: number, decimalPlaces?: number): number;
  percentageOn(value: number, decimalPlaces?: number): number;
  overPercentage(): number;
  toPercent(): number;
  round(value: number): number;
  isNumber();
}

Number.prototype.proportionOn = function (value:number, decimalPlaces?: number) {
  let result = this / value;
  return decimalPlaces ? result.round(decimalPlaces) : result;
};

Number.prototype.percentageOn = function (value:number, decimalPlaces?: number) {
  let result = this.proportionOn(value) * 100;
  return decimalPlaces ? result.round(decimalPlaces) : result;
};

Number.prototype.round = function (value: number) {
  // return parseFloat(this.toFixed(value));
  return Math.round(this * Math.pow(10, value)) / Math.pow(10, value);
};

Number.prototype.toPercent = function () {
  return this / 100;
};

Number.prototype.overPercentage = function () {
  return Math.abs((this - 1) * 100).round(2);
};
