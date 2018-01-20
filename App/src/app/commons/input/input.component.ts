import {Component, Input, Output, OnInit, EventEmitter} from '@angular/core';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent implements OnInit {

  @Input() placeHolder: string;
  @Input() addon: string;
  @Input() appModel: string;
  @Input() type: string;
  @Input() name: string;
  @Input() value: any;
  @Output() appModelChange: EventEmitter<any> = new EventEmitter();

  change(newValue){
    this.appModel = newValue;
    this.appModelChange.emit(this.appModel);
  }

  constructor() { }

  ngOnInit() {
  }

}
