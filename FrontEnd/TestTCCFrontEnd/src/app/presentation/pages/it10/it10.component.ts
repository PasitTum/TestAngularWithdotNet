import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';
import { ExamAnswerResult, ExamQuestion, ExamSubmitRequest } from '../../../core/models/exam.model';
import { ExamUseCase } from '../../../core/use-cases/exam.use-case';

type ChoiceKey = 'A' | 'B' | 'C' | 'D';

@Component({
  selector: 'app-it10',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent],
  templateUrl: './it10.component.html',
  styleUrl: './it10.component.css',
})
export class It10Component implements OnInit {
  questions: ExamQuestion[] = [];
  examineeName = '';
  selectedAnswers: Record<number, ChoiceKey | undefined> = {};
  result: { examineeName: string; scoreText: string; results: ExamAnswerResult[] } | null = null;
  isSubmitting = false;
  errorMessage = '';

  readonly choiceKeys: ChoiceKey[] = ['A', 'B', 'C', 'D'];

  constructor(private examUseCase: ExamUseCase) {}

  ngOnInit(): void {
    this.loadQuestions();
  }

  loadQuestions(): void {
    this.examUseCase.getQuestions().subscribe({
      next: (questions) => {
        this.questions = questions;
        this.selectedAnswers = {};
      },
      error: () => {
        this.errorMessage = 'ไม่สามารถโหลดข้อสอบได้';
      },
    });
  }

  getChoiceText(question: ExamQuestion | ExamAnswerResult, choice: ChoiceKey): string {
    const map = {
      A: question.choiceA,
      B: question.choiceB,
      C: question.choiceC,
      D: question.choiceD,
    };

    return map[choice];
  }

  submit(): void {
    const name = this.examineeName.trim();
    if (!name) {
      this.errorMessage = 'กรุณากรอกชื่อ-สกุล';
      return;
    }

    const unanswered = this.questions.some((q) => !this.selectedAnswers[q.id]);
    if (unanswered) {
      this.errorMessage = 'กรุณาตอบคำถามให้ครบทุกข้อ';
      return;
    }

    const request: ExamSubmitRequest = {
      examineeName: name,
      answers: this.questions.map((question) => ({
        questionId: question.id,
        selectedChoice: this.selectedAnswers[question.id]!,
      })),
    };

    this.isSubmitting = true;
    this.errorMessage = '';

    this.examUseCase.submit(request).subscribe({
      next: (result) => {
        this.result = {
          examineeName: result.examineeName,
          scoreText: result.scoreText,
          results: result.results,
        };
        this.isSubmitting = false;
      },
      error: (error) => {
        this.isSubmitting = false;
        this.errorMessage = error?.error?.message ?? 'ไม่สามารถส่งข้อสอบได้';
      },
    });
  }

  restart(): void {
    this.examineeName = '';
    this.selectedAnswers = {};
    this.result = null;
    this.errorMessage = '';
  }

  trackByQuestionId(_: number, question: ExamQuestion | ExamAnswerResult): number {
    return 'id' in question ? question.id : question.questionNo;
  }
}
