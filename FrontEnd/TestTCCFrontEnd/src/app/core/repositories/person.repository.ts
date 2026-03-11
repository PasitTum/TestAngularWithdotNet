import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonRequest, PersonResponse } from '../../core/models/person.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PersonRepository {
  private readonly base = `${environment.apiUrl}/persons`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<PersonResponse[]> {
    return this.http.get<PersonResponse[]>(this.base);
  }

  getById(id: number): Observable<PersonResponse> {
    return this.http.get<PersonResponse>(`${this.base}/${id}`);
  }

  create(request: PersonRequest): Observable<PersonResponse> {
    return this.http.post<PersonResponse>(this.base, request);
  }
}
