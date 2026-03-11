export interface SerialCodeResponse {
  id: number;
  code: string;
  codeRaw: string;
  createdAt: string;
}

export interface SerialCodeRequest {
  code: string;
}
