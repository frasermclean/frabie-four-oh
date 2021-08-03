import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Invite } from 'src/app/models/invite';
import { InviteService } from 'src/app/services/invite.service';

@Component({
  selector: 'app-invite-list',
  templateUrl: './invite-list.component.html',
  styleUrls: ['./invite-list.component.scss'],
})
export class InviteListComponent implements OnInit {
  invites$: Observable<Invite[]>;
  displayedColumns: string[] = ['name', 'email', 'inviteStatus'];

  constructor(private inviteService: InviteService) {
    this.invites$ = this.inviteService.getInvites();
  }

  ngOnInit(): void {}
}
