import {Component, Input, Output, OnInit, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss']
})
export class InputComponent implements OnInit {

  @Input() placeHolder: string;
  @Input() addon: string;
  @Input() type: string;
  @Input() name: string;
  @Input() value: string;
  @Input() appModel: string;
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
