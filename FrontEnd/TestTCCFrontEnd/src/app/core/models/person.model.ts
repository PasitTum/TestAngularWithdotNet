export interface PersonResponse {
  id: number;
  firstName: string;
  lastName: string;
  fullName: string;
  birthDate: string;
  age: number;
  address: string;
}

export interface PersonRequest {
  firstName: string;
  lastName: string;
  birthDate: string;
  address: string;
}
