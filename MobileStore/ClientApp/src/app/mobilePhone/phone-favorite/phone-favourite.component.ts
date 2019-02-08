import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MobilePhoneService } from '../../shared/services/MobilePhoneService';
import { MobilePhone } from '../../shared/entities/MobilePhone';
import { MobilePhoneViewModel } from '../../shared/viewModels/MobilePhoneViewModel';

@Component({
  selector: 'phone-favorite',
  templateUrl: './phone-favourite.component.html'
})
export class PhoneFavoriteListComponent implements OnInit {

  public viewModelPhones: MobilePhoneViewModel[];

  constructor(private phoneService: MobilePhoneService) {

  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.phoneService.Catalog().subscribe((catalogData: MobilePhoneViewModel[]) => {
      this.viewModelPhones = catalogData.filter(x => x.isFavourite == true);
    });
  }

  onChanged() {
    this.load();
  }
}
