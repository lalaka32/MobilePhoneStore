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
    this.phoneService.MarkAsFavourite(phone.id).subscribe(data => { this.load(); });
  }

  deleteFromUser(phoneId: number) {
    this.phoneService.DeleteFromFavourite(phoneId).subscribe(data => { this.load(); });
  }

  load() {
    if (this.id)
      this.phoneService.GetPhoneDTO(this.id)
        .subscribe((phoneData: MobilePhoneViewModel) => {
          this.phoneModel = phoneData;
          if (this.phoneModel != null) this.loaded = true;
        });
  }
  
}
