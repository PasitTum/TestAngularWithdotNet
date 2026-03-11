import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { QueueResponse } from '../../../../core/models/queue.model';
import { PageLayoutComponent } from '../../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [CommonModule, PageLayoutComponent],
  templateUrl: './ticket.component.html',
  styleUrl: './ticket.component.css',
})
export class TicketComponent implements OnInit {
  queue: QueueResponse | null = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const nav = this.router.getCurrentNavigation();
    const state = nav?.extras?.state ?? history.state;
    this.queue = state?.['queue'] ?? null;
    if (!this.queue) {
      this.router.navigate(['/it05']);
    }
  }

  goBack(): void {
    this.router.navigate(['/it05']);
  }
}
