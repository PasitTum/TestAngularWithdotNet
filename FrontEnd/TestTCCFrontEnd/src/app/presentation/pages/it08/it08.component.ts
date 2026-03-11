import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PostResponse, CommentResponse } from '../../../core/models/post.model';
import { PostUseCase } from '../../../core/use-cases/post.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';

const POST_ID = 2;
const COMMENTER = 'Blend 285';

@Component({
  selector: 'app-it08',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent],
  templateUrl: './it08.component.html',
  styleUrl: './it08.component.css',
})
export class It08Component implements OnInit {
  post: PostResponse | null = null;
  commentInput = '';

  constructor(private postUseCase: PostUseCase) {}

  ngOnInit(): void {
    this.postUseCase.getPost(POST_ID).subscribe({
      next: (data) => (this.post = data),
      error: () => {},
    });
  }

  getInitial(username: string): string {
    return username.charAt(0).toUpperCase();
  }

  onCommentKeydown(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      event.preventDefault();
      this.submitComment();
    }
  }

  submitComment(): void {
    const text = this.commentInput.trim();
    if (!text || !this.post) return;

    this.postUseCase.addComment(POST_ID, COMMENTER, text).subscribe({
      next: (comment) => {
        this.post!.comments = [...this.post!.comments, comment];
        this.commentInput = '';
      },
      error: () => {},
    });
  }
}
