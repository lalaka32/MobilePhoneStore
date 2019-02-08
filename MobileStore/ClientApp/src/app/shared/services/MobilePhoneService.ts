import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { MobilePhone } from '../entities/MobilePhone';
import { RequestOptions } from '@angular/http';

@Injectable()
export class MobilePhoneService {

  private url = '/api/MobilePhone';

  constructor(private http: HttpClient) {
  }

  Catalog() {
    return this.http.get(this.url + "/Catalog");
  }

  GetPhoneDTO(id: number) {
    return this.http.get(this.url + "/" + id);
  }

  MarkAsFavourite(phoneId: number) {
    return this.http.get(this.url + '/MarkAsFavourite/' + phoneId);
  }

  DeleteFromFavourite(phoneId: number) {
    return this.http.delete(this.url + '/DeleteFromFavourite/' + phoneId);
  }
}
