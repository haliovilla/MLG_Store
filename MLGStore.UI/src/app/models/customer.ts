export interface CustomerDTO {
  id: number;
  name: string;
  surnames: string;
  address: string;
  username: string;
}

export interface CreateCustomerDTO {
  name: string;
  surnames: string;
  address: string;
  username: string;
  password: string;
}

export interface UpdateCustomerDTO {
  name: string;
  surnames: string;
  address: string;
}
