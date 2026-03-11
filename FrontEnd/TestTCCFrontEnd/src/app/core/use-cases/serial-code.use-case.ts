import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SerialCodeRepository } from '../repositories/serial-code.repository';
import { SerialCodeResponse, SerialCodeRequest } from '../models/serial-code.model';

@Injectable({ providedIn: 'root' })
export class SerialCodeUseCase {
  constructor(private repo: SerialCodeRepository) {}

  getAll(): Observable<SerialCodeResponse[]> {
    return this.repo.getAll();
  }

  add(request: SerialCodeRequest): Observable<SerialCodeResponse> {
    return this.repo.add(request);
  }

  delete(id: number): Observable<void> {
    return this.repo.delete(id);
  }
}
