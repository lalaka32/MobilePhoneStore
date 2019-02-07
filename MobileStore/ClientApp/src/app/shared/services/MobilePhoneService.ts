import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MobilePhone } from '../entities/MobilePhone';

@Injectable()
export class MobilePhoneService {

  private url = '/api/MobilePhone';

  constructor(private http: HttpClient) {
  }

  getCatalog() {
    return this.http.get(this.url + "/Catalog");
  }

  getPhone(id: number) {
    return this.http.get(this.url + "/" + id);
  }

  getUserFavourite() {
    return this.http.get(this.url + "/UserFavourite");
  }

  isFavorite(id: number) {
    return this.http.get(this.url + "/IsFavourite/" + id);
  }

  addToUser(phoneModel: MobilePhone) {
    let id = phoneModel.id;
    return this.http.get<any>(this.url + '/AddPhoneToUser/' + id);
  }

  deleteFromUser(phoneId: number) {
    return this.http.delete(this.url + '/DeletePhoneUser/' + phoneId);
  }
}
