import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SerialCodeResponse, SerialCodeRequest } from '../../core/models/serial-code.model';

@Injectable({ providedIn: 'root' })
export class SerialCodeRepository {
  private readonly base = environment.apiUrl + '/serialcodes';

  constructor(private http: HttpClient) {}

  getAll(): Observable<SerialCodeResponse[]> {
    return this.http.get<SerialCodeResponse[]>(this.base);
  }

  add(request: SerialCodeRequest): Observable<SerialCodeResponse> {
    return this.http.post<SerialCodeResponse>(this.base, request);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
