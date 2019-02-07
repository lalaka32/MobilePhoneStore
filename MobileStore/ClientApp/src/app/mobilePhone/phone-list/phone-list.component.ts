import { Component, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MobilePhoneService } from '../../shared/services/MobilePhoneService';
import { MobilePhoneViewModel } from '../../shared/viewModels/MobilePhoneViewModel';
import { MobilePhone } from '../../shared/entities/MobilePhone';

@Component({
  selector: 'phone-list',
  templateUrl: './phone-list.component.html'
})
export class PhoneListComponent {

  @Input() public phones: MobilePhoneViewModel[];

  @Output() onChanged = new EventEmitter();

  constructor(private phoneService: MobilePhoneService) {
  }
  addToUser(phone: MobilePhone) {
    console.log("list");
    this.phoneService.addToUser(phone).subscribe(data => { this.onChanged.emit(); });

  }

  deleteFromUser(phoneId: number) {
    console.log("list");
    this.phoneService.deleteFromUser(phoneId).subscribe(data => { this.onChanged.emit(); });
  }
}
