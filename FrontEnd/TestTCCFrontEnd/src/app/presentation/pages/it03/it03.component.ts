import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentResponse } from '../../../core/models/document.model';
import { DocumentUseCase } from '../../../core/use-cases/document.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';
import { ApproveModalComponent } from './approve-modal/approve-modal.component';

@Component({
  selector: 'app-it03',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent, ApproveModalComponent],
  templateUrl: './it03.component.html',
  styleUrl: './it03.component.css',
})
export class It03Component implements OnInit {
  documents: DocumentResponse[] = [];
  selectedIds = new Set<number>();
  selectedDocuments: DocumentResponse[] = [];
  showApproveModal = false;
  modalMode: 'approve' | 'reject' = 'approve';

  constructor(private documentUseCase: DocumentUseCase) {}

  ngOnInit(): void {
    this.loadDocuments();
  }

  loadDocuments(): void {
    this.documentUseCase.getDocumentList().subscribe({
      next: (data) => {
        this.documents = data;
        this.selectedIds.clear();
        this.selectedDocuments = [];
      },
      error: (err) => console.error('Failed to load documents', err),
    });
  }

  toggleSelect(id: number): void {
    const doc = this.documents.find((item) => item.id === id);
    if (!doc || !this.isPending(doc)) {
      return;
    }

    if (this.selectedIds.has(id)) {
      this.selectedIds.delete(id);
    } else {
      this.selectedIds.add(id);
    }
  }

  openActionModal(mode: 'approve' | 'reject'): void {
    const docs = this.documents.filter((item) => this.selectedIds.has(item.id));
    if (docs.length === 0) {
      alert('กรุณาเลือกรายการที่รออนุมัติอย่างน้อย 1 รายการ');
      return;
    }

    if (docs.some((doc) => !this.isPending(doc))) {
      alert('รายการที่เลือกมีสถานะที่ไม่สามารถทำรายการซ้ำได้');
      return;
    }

    this.modalMode = mode;
    this.selectedDocuments = docs;
    this.showApproveModal = true;
  }

  closeApproveModal(): void {
    this.showApproveModal = false;
    this.selectedDocuments = [];
  }

  onApproveModalSaved(): void {
    this.showApproveModal = false;
    this.selectedDocuments = [];
    this.loadDocuments();
  }

  isPending(doc: DocumentResponse): boolean {
    return doc.statusCode === 0;
  }

  isSelected(docId: number): boolean {
    return this.selectedIds.has(docId);
  }

  getStatusClass(statusCode: number): string {
    if (statusCode === 1) return 'text-success fw-bold';
    if (statusCode === 2) return 'text-danger fw-bold';
    return 'text-warning fw-bold';
  }
}
