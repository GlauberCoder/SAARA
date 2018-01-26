import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-number-input',
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.scss']
})
export class NumberInputComponent implements OnInit {

  @Input() placeHolder: string;
  @Input() addon: string;
  @Input() type: string;
  @Input() name: string;
  @Input() value: number;
  @Input() appModel: number;
  @Output() appModelChange: EventEmitter<any> = new EventEmitter();

  change(newValue) {
    this.appModel = newValue;
    this.appModelChange.emit(this.appModel);
  }

  constructor() {
  }

  ngOnInit() {

  }
}
