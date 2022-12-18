import {PersonResult} from '../models/results/person.result';
import {CreatePersonInput} from '../commands/inputs/create-person.input';
import {UpdatePersonInput} from '../commands/inputs/update-person.input';
import {Observable} from 'rxjs';

export abstract class IPersonRepository {
  abstract getPersons(): Observable<PersonResult[]>;
  abstract getPerson(id: number): Observable<PersonResult>;
  abstract createPerson(person: CreatePersonInput): Observable<any>;
  abstract updatePerson(id: number, person: UpdatePersonInput): Observable<any>;
  abstract deletePerson(id: number): Observable<any>;
}
