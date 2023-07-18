import { HttpHeaders } from '@angular/common/http';

export const baseUrl: string="https://localhost:7162/";

export const myHeaders: HttpHeaders = new HttpHeaders({
    passwordKey: 'passwordKey123456789',
  });