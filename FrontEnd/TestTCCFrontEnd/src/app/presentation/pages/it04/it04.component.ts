import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { OccupationResponse } from '../../../core/models/member.model';
import { MemberUseCase } from '../../../core/use-cases/member.use-case';
import { PageLayoutComponent } from '../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-it04',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent],
  templateUrl: './it04.component.html',
  styleUrl: './it04.component.css',
})
export class It04Component implements OnInit {
  firstName = '';
  lastName = '';
  email = '';
  phone = '';
  profileBase64 = '';
  profileFileName = '';
  birthDate = ''; // type="date" value yyyy-MM-dd
  occupationId = 0;
  sex = -1; // 0=Male, 1=Female

  occupations: OccupationResponse[] = [];
  errors: Record<string, string> = {};
  isLoading = false;
  successMessage = '';
  successId: number | null = null;

  constructor(private memberUseCase: MemberUseCase) {}

  ngOnInit(): void {
    this.memberUseCase.getOccupations().subscribe({
      next: (data) => (this.occupations = data),
    });
  }

  onFileChange(event: Event): void {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (!file) return;
    this.profileFileName = file.name;
    const reader = new FileReader();
    reader.onload = () => {
      const result = reader.result as string;
      // strip data:...;base64, prefix
      this.profileBase64 = result.split(',')[1] ?? result;
    };
    reader.readAsDataURL(file);
  }

  private formatBirthDay(dateStr: string): string {
    // yyyy-MM-dd → dd/MM/yyyy
    const [y, m, d] = dateStr.split('-');
    return `${d}/${m}/${y}`;
  }

  private validate(): boolean {
    this.errors = {};
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    const phoneRegex = /^0[0-9]{8,9}$/;

    if (!this.firstName.trim()) this.errors['firstName'] = 'กรุณากรอกชื่อ';
    if (!this.lastName.trim()) this.errors['lastName'] = 'กรุณากรอกนามสกุล';
    if (!this.email.trim()) {
      this.errors['email'] = 'กรุณากรอกอีเมล';
    } else if (!emailRegex.test(this.email)) {
      this.errors['email'] = 'รูปแบบอีเมลไม่ถูกต้อง';
    }
    if (!this.phone.trim()) {
      this.errors['phone'] = 'กรุณากรอกเบอร์โทรศัพท์';
    } else if (!phoneRegex.test(this.phone)) {
      this.errors['phone'] = 'รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง (ตัวอย่าง: 0812345678)';
    }
    if (!this.profileBase64) this.errors['profile'] = 'กรุณาเลือกรูปโปรไฟล์';
    if (!this.birthDate) this.errors['birthDay'] = 'กรุณากรอกวันเกิด';
    if (!this.occupationId || this.occupationId === 0) this.errors['occupation'] = 'กรุณาเลือกอาชีพ';
    if (this.sex === -1) this.errors['sex'] = 'กรุณาเลือกเพศ';

    return Object.keys(this.errors).length === 0;
  }

  save(): void {
    if (!this.validate()) return;

    this.isLoading = true;
    this.successMessage = '';

    this.memberUseCase
      .createMember({
        firstName: this.firstName.trim(),
        lastName: this.lastName.trim(),
        email: this.email.trim(),
        phone: this.phone.trim(),
        profileBase64: this.profileBase64,
        birthDay: this.formatBirthDay(this.birthDate),
        occupationId: this.occupationId,
        sex: this.sex,
      })
      .subscribe({
        next: (res) => {
          this.isLoading = false;
          this.successMessage = res.message;
          this.successId = res.id;
          this.clear();
        },
        error: (err) => {
          this.isLoading = false;
          this.errors['form'] = err?.error?.message || 'เกิดข้อผิดพลาด กรุณาลองใหม่';
        },
      });
  }

  clear(): void {
    this.firstName = '';
    this.lastName = '';
    this.email = '';
    this.phone = '';
    this.profileBase64 = '';
    this.profileFileName = '';
    this.birthDate = '';
    this.occupationId = 0;
    this.sex = -1;
    this.errors = {};
  }

  dismissSuccess(): void {
    this.successMessage = '';
    this.successId = null;
  }
}
