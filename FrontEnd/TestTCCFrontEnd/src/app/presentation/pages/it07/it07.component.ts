import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SerialCodeResponse } from '../../../core/models/serial-code.model';
import { SerialCodeUseCase } from '../../../core/use-cases/serial-code.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';
import { QrCodeComponent } from '../../shared/qr-code/qr-code.component';

@Component({
  selector: 'app-it07',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent, QrCodeComponent],
  templateUrl: './it07.component.html',
  styleUrl: './it07.component.css',
})
export class It07Component implements OnInit {
  inputCode = '';
  errorMessage = '';
  isAdding = false;

  serials: SerialCodeResponse[] = [];

  confirmDeleteId: number | null = null;
  confirmDeleteCode = '';

  qrItem: SerialCodeResponse | null = null;

  constructor(private serialCodeUseCase: SerialCodeUseCase) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.serialCodeUseCase.getAll().subscribe({
      next: (data) => (this.serials = data),
      error: () => {},
    });
  }

  onInputChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    let raw = input.value.replace(/[^A-Z0-9]/gi, '').toUpperCase().slice(0, 30);
    let formatted = raw.match(/.{1,5}/g)?.join('-') ?? raw;
    this.inputCode = formatted;
    input.value = formatted;
  }

  private readonly CODE_REGEX = /^[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}-[A-Z0-9]{5}$/;

  add(): void {
    this.errorMessage = '';
    if (!this.inputCode) {
      this.errorMessage = 'กรุณากรอกรหัสสินค้า';
      return;
    }
    if (!this.CODE_REGEX.test(this.inputCode)) {
      this.errorMessage = 'รหัสสินค้าต้องเป็น Format XXXXX-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX (A-Z, 0-9 เท่านั้น)';
      return;
    }

    this.isAdding = true;
    this.serialCodeUseCase.add({ code: this.inputCode }).subscribe({
      next: (item) => {
        this.isAdding = false;
        this.serials = [...this.serials, item];
        this.inputCode = '';
      },
      error: (err) => {
        this.isAdding = false;
        this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด';
      },
    });
  }

  openQr(item: SerialCodeResponse): void {
    this.qrItem = item;
  }

  closeQr(): void {
    this.qrItem = null;
  }

  openConfirmDelete(item: SerialCodeResponse): void {
    this.confirmDeleteId = item.id;
    this.confirmDeleteCode = item.code;
  }

  cancelDelete(): void {
    this.confirmDeleteId = null;
    this.confirmDeleteCode = '';
  }

  confirmDelete(): void {
    if (this.confirmDeleteId === null) return;
    const id = this.confirmDeleteId;
    this.cancelDelete();
    this.serialCodeUseCase.delete(id).subscribe({
      next: () => (this.serials = this.serials.filter((s) => s.id !== id)),
      error: () => {},
    });
  }
}
