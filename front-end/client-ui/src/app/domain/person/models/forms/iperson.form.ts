import {ContactTypeEnum} from '../enums/contact-type.enum';

export interface IPersonForm {
  id: number;
  name: string;
  contacts: {
    contactType: ContactTypeEnum;
    description: string;
  }[];
}
