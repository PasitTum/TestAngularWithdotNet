import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DocumentResponse } from '../../../../core/models/document.model';
import { DocumentUseCase } from '../../../../core/use-cases/document.use-case';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-approve-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './approve-modal.component.html',
  styleUrl: './approve-modal.component.css',
})
export class ApproveModalComponent {
  @Input() documents: DocumentResponse[] = [];
  @Input() mode: 'approve' | 'reject' = 'approve';
  @Output() onSave = new EventEmitter<void>();
  @Output() onCancel = new EventEmitter<void>();

  remark = '';
  errorMessage = '';
  isLoading = false;

  constructor(private documentUseCase: DocumentUseCase, private authService: AuthService) {}

  get pageTitle(): string {
    return this.mode === 'approve' ? 'IT 03-2' : 'IT 03-3';
  }

  get modalTitle(): string {
    return this.mode === 'approve' ? 'ยืนยันการอนุมัติ' : 'ยืนยันการไม่อนุมัติ';
  }

  get actionLabel(): string {
    return this.mode === 'approve' ? 'อนุมัติ' : 'ไม่อนุมัติ';
  }

  private getRequest() {
    return {
      documentIds: this.documents.map((document) => document.id),
      remark: this.remark,
      approvedBy: this.authService.getUsername() ?? '',
    };
  }

  submit(): void {
    this.isLoading = true;
    this.errorMessage = '';

    const request$ =
      this.mode === 'approve'
        ? this.documentUseCase.approveDocuments(this.getRequest())
        : this.documentUseCase.rejectDocuments(this.getRequest());

    request$.subscribe({
      next: () => {
        this.isLoading = false;
        this.onSave.emit();
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด';
      },
    });
  }
}
