import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ExamRepository } from '../repositories/exam.repository';
import { ExamQuestion, ExamResult, ExamSubmitRequest } from '../models/exam.model';

@Injectable({ providedIn: 'root' })
export class ExamUseCase {
  constructor(private repo: ExamRepository) {}

  getQuestions(): Observable<ExamQuestion[]> {
    return this.repo.getQuestions();
  }

  submit(request: ExamSubmitRequest): Observable<ExamResult> {
    return this.repo.submit(request);
  }
}
