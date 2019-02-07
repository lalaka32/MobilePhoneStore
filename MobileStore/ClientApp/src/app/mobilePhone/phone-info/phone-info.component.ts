import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router'

import { MobilePhoneService } from '../../shared/services/MobilePhoneService';

import { MobilePhone } from '../../shared/entities/MobilePhone';
import { MobilePhoneViewModel } from '../../shared/viewModels/MobilePhoneViewModel';

@Component({
  selector: 'phone-info',
  templateUrl: './phone-info.component.html'
})
export class PhoneInfoComponent implements OnInit {

  phoneModel: MobilePhoneViewModel;

  id: number;

  loaded: boolean;

  constructor(private phoneService: MobilePhoneService, private router: Router, activeRoute: ActivatedRoute) {
    this.id = parseInt(activeRoute.snapshot.params["id"]);
  }

  ngOnInit(): void {
    this.load();
  }

  addToUser(phone: MobilePhone) {
    this.phoneService.addToUser(phone).subscribe(data => { this.load(); });
  }

  deleteFromUser(phoneId: number) {
    this.phoneService.deleteFromUser(phoneId).subscribe(data => { this.load(); });
  }

  load() {
    if (this.id)
      this.phoneService.getPhone(this.id)
        .subscribe((phoneData: MobilePhone) => {
          this.phoneModel = new MobilePhoneViewModel(phoneData);
          this.phoneService.isFavorite(this.id).subscribe((isFavourite: boolean) => {
            this.phoneModel.isFavourite = isFavourite;
          });
          if (this.phoneModel != null) this.loaded = true;
        });
  }
  
}
