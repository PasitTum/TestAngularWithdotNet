export interface MemberRequest {
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  profileBase64: string;
  birthDay: string; // format: dd/MM/yyyy
  occupationId: number;
  sex: number; // 0 = Male, 1 = Female
}

export interface MemberResponse {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  profileBase64?: string;
  birthDay: string;
  occupationId: number;
  occupationName: string;
  sex: string;
  createdAt: string;
}

export interface OccupationResponse {
  id: number;
  name: string;
}

export interface CreateMemberResponse {
  message: string;
  id: number;
  data: MemberResponse;
}
