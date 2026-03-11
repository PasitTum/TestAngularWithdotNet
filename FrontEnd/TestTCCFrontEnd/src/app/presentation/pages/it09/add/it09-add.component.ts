import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ExamQuestionRequest } from '../../../../core/models/exam-question.model';
import { ExamQuestionUseCase } from '../../../../core/use-cases/exam-question.use-case';
import { PageLayoutComponent } from '../../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-it09-add',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent],
  templateUrl: './it09-add.component.html',
  styleUrl: './it09-add.component.css',
})
export class It09AddComponent {
  form: ExamQuestionRequest = {
    questionText: '',
    choiceA: '',
    choiceB: '',
    choiceC: '',
    choiceD: '',
    correctChoice: '',
  };

  errorMessage = '';
  isSaving = false;

  readonly choiceOptions = [
    { key: 'A', label: 'A' },
    { key: 'B', label: 'B' },
    { key: 'C', label: 'C' },
    { key: 'D', label: 'D' },
  ];

  constructor(private examQuestionUseCase: ExamQuestionUseCase, private router: Router) {}

  save(): void {
    this.errorMessage = '';

    if (!this.form.questionText.trim()) {
      this.errorMessage = 'กรุณากรอกคำถาม';
      return;
    }
    if (!this.form.choiceA.trim() || !this.form.choiceB.trim() || !this.form.choiceC.trim() || !this.form.choiceD.trim()) {
      this.errorMessage = 'กรุณากรอกคำตอบให้ครบทั้ง 4 ข้อ';
      return;
    }
    if (!this.form.correctChoice) {
      this.errorMessage = 'กรุณาเลือกเฉลย';
      return;
    }

    this.isSaving = true;
    this.examQuestionUseCase.add(this.form).subscribe({
      next: () => {
        this.isSaving = false;
        this.router.navigate(['/it09']);
      },
      error: (err) => {
        this.isSaving = false;
        this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด';
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/it09']);
  }
}
