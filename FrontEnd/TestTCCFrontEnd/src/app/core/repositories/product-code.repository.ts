import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductCodeRequest, ProductCodeResponse } from '../../core/models/product-code.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProductCodeRepository {
  private readonly base = `${environment.apiUrl}/productcodes`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<ProductCodeResponse[]> {
    return this.http.get<ProductCodeResponse[]>(this.base);
  }

  add(request: ProductCodeRequest): Observable<ProductCodeResponse> {
    return this.http.post<ProductCodeResponse>(this.base, request);
  }

  delete(id: number): Observable<{ message: string }> {
    return this.http.delete<{ message: string }>(`${this.base}/${id}`);
  }
}
