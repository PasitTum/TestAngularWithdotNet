import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';
import { PageLayoutComponent } from '../../../shared/page-layout/page-layout.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, PageLayoutComponent],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent {
  username = '';
  password = '';
  confirmPassword = '';
  errorMessage = '';
  isLoading = false;

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    if (!this.username || !this.password || !this.confirmPassword) {
      this.errorMessage = 'กรุณากรอกข้อมูลให้ครบถ้วน';
      return;
    }

    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Password และ Confirm Password ไม่ตรงกัน';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';

    this.authService
      .register({ username: this.username, password: this.password, confirmPassword: this.confirmPassword })
      .subscribe({
        next: () => {
          this.isLoading = false;
          this.router.navigate(['/it02/login']);
        },
        error: (err) => {
          this.isLoading = false;
          this.errorMessage = err?.error?.message || 'เกิดข้อผิดพลาด กรุณาลองใหม่อีกครั้ง';
        },
      });
  }
}
