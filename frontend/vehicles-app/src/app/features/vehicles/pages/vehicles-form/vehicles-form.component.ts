import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { VehicleService } from '../../../../core/services/vehicle.service';
import { NotificationService } from '../../../../core/services/notification.service';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule
  ],
  templateUrl: './vehicles-form.component.html',
})
export class VehicleFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private vehicleService = inject(VehicleService);
  private notification = inject(NotificationService);

  form!: FormGroup;
  vehicleId?: number;
  isEditMode = false;

  ngOnInit(): void {
    this.form = this.fb.group({
      brand: ['', Validators.required],
      model: ['', Validators.required],
      year: [null, [Validators.required, Validators.min(1900)]],
      licensePlateNumber: ['', Validators.required],
    });

    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.vehicleId = +idParam;
      this.isEditMode = true;
      this.vehicleService.getById(this.vehicleId).subscribe(response => {
        const vehicle = response.data;
        const patchData = {
          ...vehicle,
          year: vehicle.year ? new Date(vehicle.year).getFullYear() : null
        };
        this.form.patchValue(patchData);
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const data = this.form.value;
    if (data.year) {
      data.year = new Date(Number(data.year), 0, 1);
    }

    if (this.isEditMode && this.vehicleId) {
      // Obtener el valor original del plate
      this.vehicleService.getById(this.vehicleId).subscribe(response => {
        const originalPlate = response.data.licensePlateNumber;
        if (originalPlate !== data.licensePlateNumber) {
          // Si el plate cambió, validar que no exista
          this.vehicleService.getByPlate(data.licensePlateNumber)
            .pipe(
              catchError(err => {
                if (err.status === 404) return of(null);
                throw err;
              })
            )
            .subscribe(existingVehicle => {
              if (existingVehicle) {
                this.notification.error('A vehicle with this plate already exists!');
              } else {
                this.vehicleService.update({ id: this.vehicleId, ...data }).subscribe(() => {
                  this.notification.success('Vehicle updated successfully!');
                  this.router.navigateByUrl('/');
                });
              }
            });
        } else {
          // Si el plate no cambió, solo actualiza
          this.vehicleService.update({ id: this.vehicleId, ...data }).subscribe(() => {
            this.notification.success('Vehicle updated successfully!');
            this.router.navigateByUrl('/');
          });
        }
      });
    } else {
      this.vehicleService.getByPlate(data.licensePlateNumber)
        .pipe(
          catchError(err => {
            if (err.status === 404) return of(null);
            throw err;
          })
        )
        .subscribe(existingVehicle => {
          if (existingVehicle) {
            this.notification.error('A vehicle with this plate already exists!');
          } else {
            this.vehicleService.create(data).subscribe(() => {
              this.notification.success('Vehicle created successfully!');
              this.router.navigateByUrl('/');
            });
          }
        });
    }
  }
}
