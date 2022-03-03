

export enum Role {
  Operator = 1,
  Maintenance,
  Commission,
  Admin,
}

export interface User {
  id: string;
  userName: string;
  name: string;
  role: Role;
  token: string;
}
