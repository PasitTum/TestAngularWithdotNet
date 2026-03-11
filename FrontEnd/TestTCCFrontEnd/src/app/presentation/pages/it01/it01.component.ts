import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PersonResponse } from '../../../core/models/person.model';
import { PersonUseCase } from '../../../core/use-cases/person.use-case';
import { AddModalComponent } from './add-modal/add-modal.component';
import { ViewModalComponent } from './view-modal/view-modal.component';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-it01',
  standalone: true,
  imports: [CommonModule, AddModalComponent, ViewModalComponent, PageLayoutComponent],
  templateUrl: './it01.component.html',
  styleUrl: './it01.component.css',
})
export class It01Component implements OnInit {
  persons: PersonResponse[] = [];
  showAddModal = false;
  showViewModal = false;
  selectedPerson: PersonResponse | null = null;

  constructor(private personUseCase: PersonUseCase) {}

  ngOnInit(): void {
    this.loadPersons();
  }

  loadPersons(): void {
    this.personUseCase.getPersonList().subscribe({
      next: (data) => (this.persons = data),
      error: (err) => console.error('Failed to load persons', err),
    });
  }

  openAddModal(): void {
    this.showAddModal = true;
  }

  closeAddModal(): void {
    this.showAddModal = false;
  }

  onPersonAdded(person: PersonResponse): void {
    this.persons = [...this.persons, person];
    this.showAddModal = false;
  }

  openViewModal(person: PersonResponse): void {
    this.selectedPerson = person;
    this.showViewModal = true;
  }

  closeViewModal(): void {
    this.showViewModal = false;
    this.selectedPerson = null;
  }
}
