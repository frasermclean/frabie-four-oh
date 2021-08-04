import { DataSource } from '@angular/cdk/collections';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of, Subject } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CreateInvite } from '../models/create-invite';
import { Invite } from '../models/invite';

@Injectable({
  providedIn: 'root',
})
export class InviteService {
  private readonly baseUrl = `${environment.apiUrl}/invite`;
  private readonly dataSubject = new Subject<Invite[]>();
  private cache: Invite[] = [];
  public data$ = this.dataSubject.asObservable();

  constructor(private httpClient: HttpClient) {}

  getInvites() {
    this.httpClient
      .get<Invite[]>(this.baseUrl)
      .pipe(
        tap((value) => {
          this.cache = value;
          this.dataSubject.next(this.cache);
        }),
        catchError(() => {
          return of(undefined);
        })
      )
      .subscribe();
  }

  createInvite(data: CreateInvite) {
    this.httpClient
      .post<Invite>(this.baseUrl, data)
      .pipe(
        tap((value) => {
          this.cache.push(value);
          this.dataSubject.next(this.cache);
        })
      )
      .subscribe();
  }

  deleteInvite(id: string) {
    const url = `${this.baseUrl}/${id}`;
    this.httpClient.delete(url).subscribe(() => {
      this.cache = this.cache.filter((invite) => invite.id !== id);
      this.dataSubject.next(this.cache);
    });
  }
}
