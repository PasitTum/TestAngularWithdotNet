import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MemberRequest, CreateMemberResponse, OccupationResponse } from '../../core/models/member.model';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class MemberRepository {
  private readonly base = `${environment.apiUrl}/members`;

  constructor(private http: HttpClient) {}

  create(request: MemberRequest): Observable<CreateMemberResponse> {
    return this.http.post<CreateMemberResponse>(this.base, request);
  }

  getOccupations(): Observable<OccupationResponse[]> {
    return this.http.get<OccupationResponse[]>(`${this.base}/occupations`);
  }
}
