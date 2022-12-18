import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {finalize, Subscription} from 'rxjs';
import {ContactTypeEnum} from 'src/app/domain/person/models/enums/contact-type.enum';
import {PersonResult} from 'src/app/domain/person/models/results/person.result';
import {PersonService} from '../../services/person.service';
import {ConfirmationService, MessageService} from 'primeng/api';

@Component({
  selector: 'app-person-container',
  templateUrl: './person-container.component.html',
  styleUrls: ['./person-container.component.scss']
})
export class PersonContainerComponent implements OnInit, OnDestroy {

  private getPersonsSubscription: Subscription | undefined;
  private getPersonSubscription: Subscription | undefined;
  private createPersonSubscription: Subscription | undefined;
  private updatePersonSubscription: Subscription | undefined;
  private deletePersonSubscription: Subscription | undefined;

  public persons: PersonResult[] = []
  public displayPersonDialog: boolean = false;
  public isLoading: boolean = false;
  public submitted: boolean = false;

  public personForm: FormGroup;
  public personContactForm: FormGroup[] = [];

  public contactTypeOptions: {
    label: string, value: number
  }[] = [];

  constructor(
    private readonly confirmationService: ConfirmationService,
    private readonly messageService: MessageService,
    private readonly personService: PersonService,
    private readonly fb: FormBuilder,
  ) {
    this.personForm = this.newPersonForm();
  }

  public newPersonForm(): FormGroup {
    return this.fb.group({
      id: [0],
      name: ['', [Validators.required]],
    });
  }

  public newPersonContactForm(): FormGroup {
    return this.fb.group({
      contactType: [null, [Validators.required]],
      description: ['', [Validators.required]],
    });
  }

  private getContactTypeOptions() {
    Object.entries(ContactTypeEnum).filter(([key]) => isNaN(parseInt(key))).forEach(f => {
      this.contactTypeOptions.push({
        label: f[0], value: parseInt(f[1].toString())
      })
    });
  }

  ngOnInit() {
    this.getContactTypeOptions();
    this.getPersons();
  }

  ngOnDestroy() {
    this.unSubscribe();
  }

  private unSubscribe() {
    if (this.getPersonsSubscription)
      this.getPersonsSubscription.unsubscribe();

    if (this.getPersonSubscription)
      this.getPersonSubscription.unsubscribe();

    if (this.createPersonSubscription)
      this.createPersonSubscription.unsubscribe();

    if (this.updatePersonSubscription)
      this.updatePersonSubscription.unsubscribe();

    if (this.deletePersonSubscription)
      this.deletePersonSubscription.unsubscribe();
  }

  private getPersons() {
    this.isLoading = true;
    this.getPersonsSubscription = this.personService.getPersons()
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe({
        next: (data: PersonResult[]) => {
          this.persons = data;
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Erro', detail: 'Ops. algo deu errado!'});
        }
      });
  }

  private getPerson(id: number) {
    this.isLoading = true;
    this.getPersonSubscription = this.personService.getPerson(id)
      .pipe(
        finalize(() => this.isLoading = false)
      )
      .subscribe({
        next: (data: PersonResult) => {
          this.personForm.patchValue({
            id: data.id,
            name: data.name
          });
          data.contacts.forEach(c => {
            const form = this.newPersonContactForm();

            form.patchValue({
              contactType: c.contactType,
              description: c.description,
            });

            this.personContactForm.push(form);
          });
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Erro', detail: 'Ops. algo deu errado!'});
        }
      });
  }

  public showConfirmDialog(id: number) {
    this.confirmationService.confirm({
      message: 'Tem certeza de que deseja executar esta ação?',
      header: 'Confirmação de Exclusão',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.deletePerson(id);
      },
      reject: () => {
      }
    });
  }

  public showPersonDialog(id?: number) {
    if (id) {
      this.getPerson(id)
    } else {
      this.personContactForm.push(
        this.newPersonContactForm()
      );
    }
    this.displayPersonDialog = true;
  }

  public hidePersonDialog() {
    this.displayPersonDialog = false;
  }

  public onHideDialog() {
    this.personForm.reset();
    this.personContactForm = [];
    this.submitted = false;
  }

  public savePerson() {
    this.submitted = true;

    if (this.personForm.invalid
      || this.hasInvalidPersonContactForm()) {
      return;
    }

    const {id} = this.personForm.getRawValue();
    id ? this.updatePerson(id) : this.createPerson();
  }

  private createPerson() {
    this.isLoading = true;
    this.createPersonSubscription = this.personService.createPerson({
      ...this.personForm.getRawValue(),
      contacts: this.personContactForm.map(pcf => {
        return pcf.getRawValue();
      })
    })
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.messageService.add({severity: 'success', summary: 'Sucesso', detail: 'Cadastro foi realizado!'});
          this.hidePersonDialog();
          this.getPersons();
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Erro', detail: 'Ops. algo deu errado!'});
        }
      });
  }

  private updatePerson(id: number) {
    this.isLoading = true;
    this.updatePersonSubscription = this.personService.updatePerson(id, {
      ...this.personForm.getRawValue(),
      contacts: this.personContactForm.map(pcf => {
        return pcf.getRawValue();
      })
    })
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.messageService.add({severity: 'success', summary: 'Sucesso', detail: 'Cadastro foi atualizado!'});
          this.hidePersonDialog();
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Erro', detail: 'Ops. algo deu errado!'});
        }
      });
  }

  private deletePerson(id: number) {
    this.isLoading = true;
    this.deletePersonSubscription = this.personService.deletePerson(id)
      .pipe(finalize(() => this.isLoading = false))
      .subscribe({
        next: () => {
          this.messageService.add({severity: 'success', summary: 'Sucesso', detail: 'Cadastro foi excluído!'});
          this.getPersons();
        },
        error: (e) => {
          this.messageService.add({severity: 'error', summary: 'Erro', detail: 'Ops. algo deu errado!'});
        }
      });
  }

  private hasInvalidPersonContactForm() {
    let invalid = false;
    for (let form of this.personContactForm) {
      if (form.invalid) {
        invalid = true;
      }
    }
    return invalid;
  }

  public addPersonContactForm() {
    if (this.hasInvalidPersonContactForm()) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Atenção',
        detail: 'Informe corretamente os campos de Contato!'
      });
      return;
    }

    const form = this.newPersonContactForm();
    this.personContactForm.push(form);
  }

  public removePersonContactForm(form: FormGroup) {
    this.personContactForm = this.personContactForm.filter(f => f !== form);
  }
}
