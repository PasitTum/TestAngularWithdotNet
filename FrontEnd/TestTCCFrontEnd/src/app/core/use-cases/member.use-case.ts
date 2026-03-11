import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MemberRepository } from '../repositories/member.repository';
import { MemberRequest, CreateMemberResponse, OccupationResponse } from '../models/member.model';

const MOCK_OCCUPATIONS: OccupationResponse[] = [
  { id: 1, name: 'วิศวกร' },
  { id: 2, name: 'แพทย์' },
  { id: 3, name: 'ครู/อาจารย์' },
  { id: 4, name: 'นักธุรกิจ' },
  { id: 5, name: 'เกษตรกร' },
  { id: 6, name: 'รับราชการ' },
  { id: 7, name: 'นักเรียน/นักศึกษา' },
  { id: 8, name: 'อื่นๆ' },
];

@Injectable({ providedIn: 'root' })
export class MemberUseCase {
  constructor(private repo: MemberRepository) {}

  createMember(request: MemberRequest): Observable<CreateMemberResponse> {
    return this.repo.create(request);
  }

  getOccupations(): Observable<OccupationResponse[]> {
    return this.repo.getOccupations().pipe(
      catchError(() => of(MOCK_OCCUPATIONS))
    );
  }
}
