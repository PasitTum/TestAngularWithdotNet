import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductCodeResponse } from '../../../core/models/product-code.model';
import { ProductCodeUseCase } from '../../../core/use-cases/product-code.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';
import { BarcodeComponent } from '../../shared/barcode/barcode.component';

@Component({
  selector: 'app-it06',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent, BarcodeComponent],
  templateUrl: './it06.component.html',
  styleUrl: './it06.component.css',
})
export class It06Component implements OnInit {
  inputCode = '';
  errorMessage = '';
  isAdding = false;

  products: ProductCodeResponse[] = [];

  confirmDeleteId: number | null = null;
  confirmDeleteCode = '';

  constructor(private productCodeUseCase: ProductCodeUseCase) {}

  ngOnInit(): void {
    this.loadAll();
  }

  loadAll(): void {
    this.productCodeUseCase.getAll().subscribe({
      next: (data) => (this.products = data),
      error: () => {},
    });
  }

  onInputChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    // keep only A-Z, 0-9, uppercase
    let raw = input.value.replace(/[^A-Z0-9]/gi, '').toUpperCase().slice(0, 16);
    // insert dashes at 4, 8, 12
    let formatted = raw.match(/.{1,4}/g)?.join('-') ?? raw;
    this.inputCode = formatted;
    input.value = formatted;
  }

  private readonly CODE_REGEX = /^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$/;

  add(): void {
    this.errorMessage = '';
    if (!this.inputCode) {
      this.errorMessage = 'กรุณากรอกรหัสสินค้า';
      return;
    }
    if (!this.CODE_REGEX.test(this.inputCode)) {
      this.errorMessage = 'รหัสสินค้าต้องเป็น Format XXXX-XXXX-XXXX-XXXX (A-Z, 0-9 เท่านั้น)';
      return;
    }

    this.isAdding = true;
    this.productCodeUseCase.add({ code: this.inputCode }).subscribe({
      next: (item) => {
        this.isAdding = false;
        this.products = [...this.products, item];
        this.inputCode = '';
      },
      error: (err) => {
        this.isAdding = false;
        this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด';
      },
    });
  }

  openConfirmDelete(item: ProductCodeResponse): void {
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
    this.productCodeUseCase.delete(id).subscribe({
      next: () => (this.products = this.products.filter((p) => p.id !== id)),
      error: () => {},
    });
  }
}
