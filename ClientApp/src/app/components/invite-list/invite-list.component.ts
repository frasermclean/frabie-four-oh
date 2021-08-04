import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { CreateInvite } from 'src/app/models/create-invite';
import { Invite, InviteStatus } from 'src/app/models/invite';
import { InviteService } from 'src/app/services/invite.service';

@Component({
  selector: 'app-invite-list',
  templateUrl: './invite-list.component.html',
  styleUrls: ['./invite-list.component.scss'],
})
export class InviteListComponent implements OnInit, OnDestroy {
  dataSource = new MatTableDataSource<Invite>();
  private subscription: Subscription;
  displayedColumns: string[] = [
    'name',
    'email',
    'inviteStatus',
    'deleteInvite',
  ];

  constructor(private inviteService: InviteService) {
    this.subscription = this.inviteService.data$.subscribe((invites) => {
      this.dataSource.data = invites;
    });
  }

  ngOnInit(): void {
    this.inviteService.getInvites();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onInviteData(data: CreateInvite) {
    this.inviteService.createInvite(data);
  }

  onDelete(invite: Invite) {
    this.inviteService.deleteInvite(invite.id);
  }

  getInviteStatus(invite: Invite) {
    switch (invite.inviteStatus) {
      case InviteStatus.Attending:
        return 'Attending';
      case InviteStatus.AwaitingResponse:
        return 'Awaiting Response';
      case InviteStatus.NotAttending:
        return 'Not Attending';
      default:
        return 'Unknown';
    }
  }
}
