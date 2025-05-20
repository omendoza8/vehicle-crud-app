import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { VehicleService } from '../../../../core/services/vehicle.service';
import { Vehicle } from '../../../../core/models/vehicle.model';
import { MatCard, MatCardContent, MatCardTitle } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { NotificationService } from '../../../../core/services/notification.service';


@Component({
  selector: 'app-vehicles-list',
  standalone: true,
  imports: [
      CommonModule,
      MatTableModule,
      MatButtonModule,
      RouterModule,
      MatCardContent,
      MatIconModule,
      MatCardContent,
      MatCardTitle,
      MatCard
          ],
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.css'],
})
export class VehicleListComponent {
  private vehicleService = inject(VehicleService);
  private notification = inject(NotificationService);
  vehicles: Vehicle[] = [];
  displayedColumns = ['id', 'brand', 'model', 'year', 'plate', 'actions'];

  constructor() {
    this.vehicleService.getAll().subscribe(response => this.vehicles = response.data);
  }

  delete(id: number) {
    if (confirm('Are you sure you want to delete this vehicle?')) {
      this.vehicleService.delete(id).subscribe(() => {
        this.vehicles = this.vehicles.filter(v => v.id !== id);
        this.notification.success('Vehicle deleted successfully!');
      });
    }
  }
}
