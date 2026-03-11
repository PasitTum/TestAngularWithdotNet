import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { QueueResponse } from '../../core/models/queue.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class QueueRepository {
  private readonly base = `${environment.apiUrl}/queue`;

  constructor(private http: HttpClient) {}

  getCurrent(): Observable<QueueResponse> {
    return this.http.get<QueueResponse>(this.base);
  }

  takeTicket(): Observable<QueueResponse> {
    return this.http.post<QueueResponse>(`${this.base}/take`, {});
  }

  reset(): Observable<QueueResponse> {
    return this.http.post<QueueResponse>(`${this.base}/reset`, {});
  }
}
