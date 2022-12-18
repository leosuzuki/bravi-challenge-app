import {ContactTypeEnum} from '../../models/enums/contact-type.enum';

export interface CreatePersonInput {
  name: string;
  contacts: {
    contactType: ContactTypeEnum;
    description: string;
  }[];
}
