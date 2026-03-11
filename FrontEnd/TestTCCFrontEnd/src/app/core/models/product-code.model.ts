export interface ProductCodeRequest {
  code: string; // XXXX-XXXX-XXXX-XXXX
}

export interface ProductCodeResponse {
  id: number;
  code: string;    // with dashes
  codeRaw: string; // without dashes — for barcode
  createdAt: string;
}
