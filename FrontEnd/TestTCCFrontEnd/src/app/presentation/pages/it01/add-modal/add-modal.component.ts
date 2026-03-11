import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonUseCase } from '../../../../core/use-cases/person.use-case';
import { PersonResponse } from '../../../../core/models/person.model';

@Component({
  selector: 'app-add-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-modal.component.html',
  styleUrl: './add-modal.component.css',
})
export class AddModalComponent {
  @Output() onSave = new EventEmitter<PersonResponse>();
  @Output() onCancel = new EventEmitter<void>();

  form: FormGroup;
  calculatedAge = 0;
  isSubmitting = false;

  constructor(private fb: FormBuilder, private personUseCase: PersonUseCase) {
    this.form = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      address: ['', Validators.required],
    });
  }

  onBirthDateChange(): void {
    const birthDate = this.form.get('birthDate')?.value;
    this.calculatedAge = this.calculateAge(birthDate);
  }

  calculateAge(birthDateStr: string): number {
    if (!birthDateStr) return 0;
    const birthYear = new Date(birthDateStr).getFullYear();
    const currentYear = new Date().getFullYear();
    return currentYear - birthYear;
  }
  save(): void {
    if (this.form.invalid || this.isSubmitting) return;
    this.isSubmitting = true;

    const { firstName, lastName, birthDate, address } = this.form.value;

    this.personUseCase.createPerson({ firstName, lastName, birthDate, address }).subscribe({
      next: (person) => {
        this.isSubmitting = false;
        this.onSave.emit(person);
      },
      error: (err) => {
        this.isSubmitting = false;
        console.error('Failed to create person', err);
      },
    });
  }

  cancel(): void {
    this.onCancel.emit();
  }
}
