import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MobilePhoneService } from '../../shared/services/MobilePhoneService';
import { MobilePhone } from '../../shared/entities/MobilePhone';
import { MobilePhoneViewModel } from '../../shared/viewModels/MobilePhoneViewModel';

@Component({
  selector: 'phone-catalog',
  templateUrl: './phone-catalog.component.html'
})
export class PhoneCatalogComponent implements OnInit {

  public viewModelPhones: MobilePhoneViewModel[];

  constructor(private phoneService: MobilePhoneService) {

  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.phoneService.getCatalog().subscribe((catalogData: MobilePhone[]) => {
      this.viewModelPhones = this.mapMobilePhoneToViewModel(catalogData);
    });
  }

  onChanged() {
    this.load();
  }

  mapMobilePhoneToViewModel(list: MobilePhone[]) {
    let viewModels = new Array<MobilePhoneViewModel>();
    for (var i = 0; i < list.length; i++) {
      let viewModel = new MobilePhoneViewModel(list[i]);
      this.phoneService.isFavorite(list[i].id).subscribe((isFavourite: boolean) => {
        viewModel.isFavourite = isFavourite;
      });
      viewModels.push(viewModel);
    }
    return viewModels;
  }
}
