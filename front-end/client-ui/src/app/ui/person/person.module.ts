import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PersonContainerComponent} from './pages/person-container/person-container.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {CardModule} from 'primeng/card';
import {ButtonModule} from 'primeng/button';
import {TableModule} from 'primeng/table';
import {ToolbarModule} from 'primeng/toolbar';
import {DialogModule} from 'primeng/dialog';
import {InputTextModule} from 'primeng/inputtext';
import {DropdownModule} from 'primeng/dropdown';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {ConfirmationService, MessageService} from 'primeng/api';
import {ToastModule} from 'primeng/toast';
import {PanelModule} from 'primeng/panel';
import {MessageModule} from 'primeng/message';
import {MessagesModule} from 'primeng/messages';
import {InputMaskModule} from 'primeng/inputmask';

@NgModule({
  declarations: [
    PersonContainerComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    ButtonModule,
    TableModule,
    ToolbarModule,
    DialogModule,
    InputTextModule,
    DropdownModule,
    ConfirmDialogModule,
    ToastModule,
    PanelModule,
    MessageModule,
    MessagesModule,
    InputMaskModule
  ],
  providers: [
    ConfirmationService,
    MessageService
  ]
})
export class PersonModule {
}
