import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { QueueUseCase } from '../../../../core/use-cases/queue.use-case';
import { PageLayoutComponent } from '../../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-reset',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent],
  templateUrl: './reset.component.html',
  styleUrl: './reset.component.css',
})
export class ResetComponent implements OnInit {
  currentQueueNumber = '';
  isLoading = false;
  isReset = false;

  constructor(private queueUseCase: QueueUseCase, private router: Router) {}

  ngOnInit(): void {
    this.queueUseCase.getCurrent().subscribe({
      next: (res) => (this.currentQueueNumber = res.queueNumber),
      error: () => {},
    });
  }

  resetQueue(): void {
    if (this.isLoading) return;
    this.isLoading = true;
    this.queueUseCase.reset().subscribe({
      next: (res) => {
        this.isLoading = false;
        this.currentQueueNumber = res.queueNumber; // "00"
        this.isReset = true;
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  goBack(): void {
    this.router.navigate(['/it05']);
  }
}
