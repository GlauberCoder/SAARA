interface Number {
  reducePorcent(percentage: number, decimalPlaces?: number): number;
  reducePercentage(percentage: number, decimalPlaces?: number): number;
  increasePorcent(percentage: number, decimalPlaces?: number): number;
  increasePercentage(percentage: number, decimalPlaces?: number): number;
  proportionOn(value: number, decimalPlaces?: number): number;
  percentageOn(value: number, decimalPlaces?: number): number;
  overPercentage(): number;
  toPercent(): number;
  toPorcent(): number;
  round(value: number): number;
  isNumber();
}

Number.prototype.reducePorcent = function (percentage: number, decimalPlaces?: number) {
  return this.reducePercentage(this, percentage.toPercent(), decimalPlaces);
};

Number.prototype.increasePorcent = function (percentage: number, decimalPlaces?: number) {
  return this.increasePercentage(this, percentage.toPercent(), decimalPlaces);
};

Number.prototype.reducePercentage = function (percentage: number, decimalPlaces?: number) {
  const result =  this * (1 - percentage);
  return decimalPlaces ? result.round(decimalPlaces) : result;
};

Number.prototype.increasePercentage = function (percentage: number, decimalPlaces?: number) {
  const result =  this * (1 + percentage);
  return decimalPlaces ? result.round(decimalPlaces) : result;
};

Number.prototype.proportionOn = function (value: number, decimalPlaces?: number) {
  const result = this / value;
  return decimalPlaces ? result.round(decimalPlaces) : result;
};

Number.prototype.percentageOn = function (value: number, decimalPlaces?: number) {
  const result = this.proportionOn(value) * 100;
  return decimalPlaces ? result.round(decimalPlaces) : result;
};

Number.prototype.round = function (value: number) {
  return Math.round(this * Math.pow(10, value)) / Math.pow(10, value);
};

Number.prototype.toPercent = function () {
  return this / 100;
};

Number.prototype.toPorcent = function () {
  return this * 100;
};

Number.prototype.overPercentage = function () {
  return Math.abs((this - 1) * 100).round(2);
};
