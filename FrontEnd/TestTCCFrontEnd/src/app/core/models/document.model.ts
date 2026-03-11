export interface DocumentResponse {
  id: number;
  documentNo: string;
  title: string;
  requestedBy: string;
  requestedAt: string;
  status: string;
  statusCode: number;
  approvedBy?: string;
  approvedAt?: string;
  remark?: string;
}

export interface ApprovalRequest {
  documentIds: number[];
  remark: string;
  approvedBy: string;
}

export interface DocumentRequest {
  title: string;
  requestedBy: string;
}
