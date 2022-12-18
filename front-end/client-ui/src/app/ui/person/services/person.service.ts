import {Injectable} from '@angular/core';
import {IPersonRepository} from '../../../domain/person/repositories/iperson.repository';
import {PersonRepository} from '../../../data/person/repositories/person.repository';
import {Observable} from 'rxjs';
import {PersonResult} from '../../../domain/person/models/results/person.result';
import {CreatePersonInput} from '../../../domain/person/commands/inputs/create-person.input';
import {UpdatePersonInput} from '../../../domain/person/commands/inputs/update-person.input';

@Injectable({
  providedIn: 'root'
})
export class PersonService implements IPersonRepository {

  constructor(
    private readonly repos: PersonRepository
  ) {
  }

  getPersons(): Observable<PersonResult[]> {
    return this.repos.getPersons();
  }

  getPerson(id: number): Observable<PersonResult> {
    return this.repos.getPerson(id);
  }

  createPerson(person: CreatePersonInput): Observable<any> {
    return this.repos.createPerson(person);
  }

  updatePerson(id: number, person: UpdatePersonInput): Observable<any> {
    return this.repos.updatePerson(id, person);
  }

  deletePerson(id: number): Observable<any> {
    return this.repos.deletePerson(id);
  }
}
