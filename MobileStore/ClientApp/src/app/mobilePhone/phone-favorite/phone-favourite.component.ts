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
    this.phoneService.getUserFavourite().subscribe((catalogData: MobilePhone[]) => {
      this.viewModelPhones = this.mapMobilePhoneToViewModel(catalogData);
    });
  }

  onChanged() {
    this.load();
  }

  mapMobilePhoneToViewModel(list: MobilePhone[]) {
    let viewModels = new Array<MobilePhoneViewModel>();
    for (var i = 0; i < list.length; i++) {
      let viewModel = new MobilePhoneViewModel(
        list[i]
      );
      viewModel.isFavourite = true;
      viewModels.push(viewModel);
    }
    return viewModels;
  }
}
