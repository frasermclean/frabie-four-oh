import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateInvite } from 'src/app/models/create-invite';

@Component({
  selector: 'app-invite-create',
  templateUrl: './invite-create.component.html',
  styleUrls: ['./invite-create.component.scss'],
})
export class InviteCreateComponent implements OnInit {
  formGroup: FormGroup;
  @Output() inviteData = new EventEmitter<CreateInvite>();

  constructor() {
    this.formGroup = new FormGroup({
      name: new FormControl(null, [Validators.required]),
      email: new FormControl(null, [Validators.required, Validators.email]),
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    this.inviteData.emit(this.formGroup.value);
  }

  onReset() {
    this.formGroup.reset();
    this.formGroup.markAsUntouched();
  }
}
