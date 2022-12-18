import {ContactTypeEnum} from '../../models/enums/contact-type.enum';

export interface UpdatePersonInput {
  name: string;
  contacts: {
    contactType: ContactTypeEnum;
    description: string;
  }[];
}
