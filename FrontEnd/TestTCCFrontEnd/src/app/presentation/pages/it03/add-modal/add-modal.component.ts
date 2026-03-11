import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DocumentUseCase } from '../../../../core/use-cases/document.use-case';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-add-document-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-modal.component.html',
  styleUrl: './add-modal.component.css',
})
export class AddDocumentModalComponent {
  @Output() onSave = new EventEmitter<void>();
  @Output() onCancel = new EventEmitter<void>();

  title = '';
  errorMessage = '';
  isLoading = false;

  constructor(private documentUseCase: DocumentUseCase, private authService: AuthService) {}

  save(): void {
    if (!this.title.trim()) {
      this.errorMessage = 'กรุณากรอกรายละเอียด';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    const requestedBy = this.authService.getUsername() ?? '';
    this.documentUseCase.createDocument({ title: this.title.trim(), requestedBy }).subscribe({
      next: () => {
        this.isLoading = false;
        this.onSave.emit();
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด กรุณาลองใหม่';
      },
    });
  }
}
