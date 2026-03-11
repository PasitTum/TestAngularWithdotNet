import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ExamQuestionResponse } from '../../../core/models/exam-question.model';
import { ExamQuestionUseCase } from '../../../core/use-cases/exam-question.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-it09',
  standalone: true,
  imports: [CommonModule, RouterLink, PageLayoutComponent],
  templateUrl: './it09.component.html',
  styleUrl: './it09.component.css',
})
export class It09Component implements OnInit {
  questions: ExamQuestionResponse[] = [];

  constructor(private examQuestionUseCase: ExamQuestionUseCase) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.examQuestionUseCase.getAll().subscribe({
      next: (data) => (this.questions = data),
      error: () => {},
    });
  }

  delete(id: number): void {
    this.examQuestionUseCase.delete(id).subscribe({
      next: () => this.loadAll(),
      error: () => {},
    });
  }

  choiceLabel(q: ExamQuestionResponse): string[] {
    return [q.choiceA, q.choiceB, q.choiceC, q.choiceD];
  }

  choiceKeys = ['A', 'B', 'C', 'D'];
}
