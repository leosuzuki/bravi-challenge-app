import {IPersonRepository} from '../../../domain/person/repositories/iperson.repository';
import {PersonResult} from '../../../domain/person/models/results/person.result';
import {CreatePersonInput} from '../../../domain/person/commands/inputs/create-person.input';
import {UpdatePersonInput} from '../../../domain/person/commands/inputs/update-person.input';
import {HttpClient} from '@angular/common/http';
import {map, Observable} from 'rxjs';
import {environment} from '../../../../environments/environment';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PersonRepository extends IPersonRepository {

  constructor(
    private readonly http: HttpClient
  ) {
    super();
  }

  getPersons(): Observable<PersonResult[]> {
    return this.http.get<PersonResult[]>(`${environment.apiUrl}/person`).pipe(map((res: PersonResult[]) => {
      return res;
    }));
  }

  getPerson(id: number): Observable<PersonResult> {
    return this.http.get<PersonResult>(`${environment.apiUrl}/person/${id}`).pipe(map((res: PersonResult) => {
      return res;
    }));
  }

  createPerson(person: CreatePersonInput): Observable<any> {
    return this.http.post<any>(`${environment.apiUrl}/person`, person).pipe(map((res: any) => {
      return res;
    }));
  }

  updatePerson(id: number, person: UpdatePersonInput): Observable<any> {
    return this.http.put<any>(`${environment.apiUrl}/person/${id}`, person).pipe(map((res: any) => {
      return res;
    }));
  }

  deletePerson(id: number): Observable<any> {
    return this.http.delete<any>(`${environment.apiUrl}/person/${id}`).pipe(map((res: any) => {
      return res;
    }));
  }
}
