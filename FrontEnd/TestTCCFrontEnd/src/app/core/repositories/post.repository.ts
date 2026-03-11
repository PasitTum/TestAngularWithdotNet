import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PostResponse, CommentResponse } from '../../core/models/post.model';

@Injectable({ providedIn: 'root' })
export class PostRepository {
  private readonly base = environment.apiUrl + '/posts';

  constructor(private http: HttpClient) {}

  getPost(postId: number): Observable<PostResponse> {
    return this.http.get<PostResponse>(`${this.base}/${postId}`);
  }

  addComment(postId: number, username: string, commentText: string): Observable<CommentResponse> {
    return this.http.post<CommentResponse>(`${this.base}/${postId}/comments`, { username, commentText });
  }
}
