import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductCodeRepository } from '../repositories/product-code.repository';
import { ProductCodeRequest, ProductCodeResponse } from '../models/product-code.model';

@Injectable({ providedIn: 'root' })
export class ProductCodeUseCase {
  constructor(private repo: ProductCodeRepository) {}

  getAll(): Observable<ProductCodeResponse[]> {
    return this.repo.getAll();
  }

  add(request: ProductCodeRequest): Observable<ProductCodeResponse> {
    return this.repo.add(request);
  }

  delete(id: number): Observable<{ message: string }> {
    return this.repo.delete(id);
  }
}
