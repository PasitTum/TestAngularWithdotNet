import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostRepository } from '../repositories/post.repository';
import { PostResponse, CommentResponse } from '../models/post.model';

@Injectable({ providedIn: 'root' })
export class PostUseCase {
  constructor(private repo: PostRepository) {}

  getPost(postId: number): Observable<PostResponse> {
    return this.repo.getPost(postId);
  }

  addComment(postId: number, username: string, commentText: string): Observable<CommentResponse> {
    return this.repo.addComment(postId, username, commentText);
  }
}
