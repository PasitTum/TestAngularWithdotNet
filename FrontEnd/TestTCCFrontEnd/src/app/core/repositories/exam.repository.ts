import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ExamQuestion, ExamResult, ExamSubmitRequest } from '../../core/models/exam.model';

@Injectable({ providedIn: 'root' })
export class ExamRepository {
  private readonly base = `${environment.apiUrl}/exam`;

  constructor(private http: HttpClient) {}

  getQuestions(): Observable<ExamQuestion[]> {
    return this.http.get<ExamQuestion[]>(`${this.base}/questions`);
  }

  submit(request: ExamSubmitRequest): Observable<ExamResult> {
    return this.http.post<ExamResult>(`${this.base}/submit`, request);
  }
}
