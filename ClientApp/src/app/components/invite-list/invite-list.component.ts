import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { CreateInvite } from 'src/app/models/create-invite';
import { Invite } from 'src/app/models/invite';
import { InviteService } from 'src/app/services/invite.service';

@Component({
  selector: 'app-invite-list',
  templateUrl: './invite-list.component.html',
  styleUrls: ['./invite-list.component.scss'],
})
export class InviteListComponent implements OnInit {
  dataSource = new MatTableDataSource<Invite>();
  displayedColumns: string[] = [
    'name',
    'email',
    'inviteStatus',
    'deleteInvite',
  ];

  constructor(private inviteService: InviteService) {}

  ngOnInit(): void {
    this.inviteService.data$.subscribe((invites) => {
      this.dataSource.data = invites;
    });
    this.inviteService.getInvites();
  }

  onInviteData(data: CreateInvite) {
    this.inviteService.createInvite(data);
  }

  onDelete(invite: Invite) {
    this.inviteService.deleteInvite(invite.id);
  }
}
