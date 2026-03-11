import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExamQuestionRepository } from '../repositories/exam-question.repository';
import { ExamQuestionResponse, ExamQuestionRequest } from '../models/exam-question.model';

@Injectable({ providedIn: 'root' })
export class ExamQuestionUseCase {
  constructor(private repo: ExamQuestionRepository) {}

  getAll(): Observable<ExamQuestionResponse[]> {
    return this.repo.getAll();
  }

  add(request: ExamQuestionRequest): Observable<ExamQuestionResponse> {
    return this.repo.add(request);
  }

  delete(id: number): Observable<void> {
    return this.repo.delete(id);
  }
}
