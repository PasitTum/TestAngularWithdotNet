import { Injectable } from '@angular/core';
import { Observable } from 'rxjs'; 
import { DocumentResponse, ApprovalRequest, DocumentRequest } from '../models/document.model';
import { DocumentRepository } from '../repositories/document.repository';

@Injectable({
  providedIn: 'root',
})
export class DocumentUseCase {
  constructor(private repo: DocumentRepository) {}

  getDocumentList(): Observable<DocumentResponse[]> {
    return this.repo.getAll();
  }

  approveDocuments(request: ApprovalRequest): Observable<{ message: string }> {
    return this.repo.approve(request);
  }

  rejectDocuments(request: ApprovalRequest): Observable<{ message: string }> {
    return this.repo.reject(request);
  }
}
