import { MobilePhone } from '../entities/MobilePhone';
export class MobilePhoneViewModel {
  constructor(
    public phone?: MobilePhone,
    public isFavourite?: boolean,
  ){ }
}
