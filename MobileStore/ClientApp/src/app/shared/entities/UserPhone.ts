import { User } from './User';
import { MobilePhone } from './MobilePhone';

export class UserPhone {
  constructor(
    public userId?: number,
    public user?: User,
    public phoneId?: number,
    public phone?: MobilePhone,
  ) { }
}
