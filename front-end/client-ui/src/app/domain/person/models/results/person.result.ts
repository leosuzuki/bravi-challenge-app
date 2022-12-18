import {PersonContactResult} from './person-contact.result';

export interface PersonResult {
  id: number;
  name: string;
  contacts: PersonContactResult[];
}
