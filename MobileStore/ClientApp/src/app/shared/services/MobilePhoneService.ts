import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MobilePhone } from '../entities/MobilePhone';
import { RequestOptions } from '@angular/http';

@Injectable()
export class MobilePhoneService {

  private url = '/api/MobilePhone';

  constructor(private http: HttpClient) {
  }

  getCatalog() {
    return this.http.get(this.url + "/Catalog");
  }

  getUserCatalog() {
    return this.http.get(this.url + "/UserCatalog");
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

  addToUser(phoneId: number) {
    return this.http.get(this.url + '/AddPhoneToUser/' + phoneId);
  }

  deleteFromUser(phoneId: number) {
    return this.http.delete(this.url + '/DeletePhoneUser/' + phoneId);
  }
}
