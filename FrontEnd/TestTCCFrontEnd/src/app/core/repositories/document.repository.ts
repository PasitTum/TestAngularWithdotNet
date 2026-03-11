import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DocumentResponse, ApprovalRequest, DocumentRequest } from '../../core/models/document.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class DocumentRepository {
  private readonly base = `${environment.apiUrl}/documents`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<DocumentResponse[]> {
    return this.http.get<DocumentResponse[]>(this.base);
  }

  approve(request: ApprovalRequest): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.base}/approve`, request);
  }

  reject(request: ApprovalRequest): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.base}/reject`, request);
  }
}
