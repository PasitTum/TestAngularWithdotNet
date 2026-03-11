import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ExamQuestionResponse, ExamQuestionRequest } from '../../core/models/exam-question.model';

@Injectable({ providedIn: 'root' })
export class ExamQuestionRepository {
  private readonly base = environment.apiUrl + '/examquestions';

  constructor(private http: HttpClient) {}

  getAll(): Observable<ExamQuestionResponse[]> {
    return this.http.get<ExamQuestionResponse[]>(this.base);
  }

  add(request: ExamQuestionRequest): Observable<ExamQuestionResponse> {
    return this.http.post<ExamQuestionResponse>(this.base, request);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}
