import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { QueueUseCase } from '../../../core/use-cases/queue.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-it05',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent],
  templateUrl: './it05.component.html',
  styleUrl: './it05.component.css',
})
export class It05Component implements OnInit {
  isMaxReached = false;
  isLoading = false;
  errorMessage = '';

  constructor(private queueUseCase: QueueUseCase, private router: Router) {}

  ngOnInit(): void {
    this.queueUseCase.getCurrent().subscribe({
      next: (res) => (this.isMaxReached = res.isMaxReached),
      error: () => {},
    });
  }

  takeTicket(): void {
    if (this.isLoading) return; // ป้องกันกดซ้ำ
    this.isLoading = true;
    this.errorMessage = '';

    this.queueUseCase.takeTicket().subscribe({
      next: (res) => {
        this.isLoading = false;
        this.router.navigate(['/it05/ticket'], { state: { queue: res } });
      },
      error: (err) => {
        this.isLoading = false;
        this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด';
      },
    });
  }

  goToReset(): void {
    this.router.navigate(['/it05/reset']);
  }
}
