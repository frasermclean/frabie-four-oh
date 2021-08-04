export interface Invite {
  id: string;
  name: string;
  email: string;
  inviteStatus: InviteStatus;
}

export enum InviteStatus {
  AwaitingResponse,
  Attending,
  NotAttending,
}
