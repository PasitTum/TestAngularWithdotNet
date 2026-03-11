export interface CommentResponse {
  id: number;
  postId: number;
  username: string;
  commentText: string;
  createdAt: string;
}

export interface PostResponse {
  id: number;
  username: string;
  imageBase64: string | null;
  content: string | null;
  createdAt: string;
  comments: CommentResponse[];
}
