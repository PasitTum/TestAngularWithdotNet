import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonRepository } from '../repositories/person.repository';
import { PersonRequest, PersonResponse } from '../models/person.model';

@Injectable({ providedIn: 'root' })
export class PersonUseCase {
  constructor(private repository: PersonRepository) {}

  getPersonList(): Observable<PersonResponse[]> {
    return this.repository.getAll();
  }

  getPersonById(id: number): Observable<PersonResponse> {
    return this.repository.getById(id);
  }

  createPerson(request: PersonRequest): Observable<PersonResponse> {
    return this.repository.create(request);
  }

}
