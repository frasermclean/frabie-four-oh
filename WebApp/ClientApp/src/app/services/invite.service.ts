import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Invite } from '../models/invite';

@Injectable({
  providedIn: 'root',
})
export class InviteService {
  private readonly baseUrl = `${environment.apiUrl}/invite`;

  constructor(private httpClient: HttpClient) {}

  getInvites() {
    return this.httpClient.get<Invite[]>(this.baseUrl)
  }
}
