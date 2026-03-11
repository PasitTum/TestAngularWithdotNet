import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { QueueRepository } from '../repositories/queue.repository';
import { QueueResponse } from '../models/queue.model';

@Injectable({ providedIn: 'root' })
export class QueueUseCase {
  constructor(private repo: QueueRepository) {}

  getCurrent(): Observable<QueueResponse> {
    return this.repo.getCurrent();
  }

  takeTicket(): Observable<QueueResponse> {
    return this.repo.takeTicket();
  }

  reset(): Observable<QueueResponse> {
    return this.repo.reset();
  }
}
