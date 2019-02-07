import { UserPhone } from './UserPhone';

export class MobilePhone {
  constructor(
    public id?: number,
    public brand?: string,
    public modelName?: string,
    public price?: number,
    public userPhones?: UserPhone[] 
  ){ }
}
