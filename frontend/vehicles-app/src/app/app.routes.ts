import { Routes } from '@angular/router';
import { VehicleFormComponent } from './features/vehicles/pages/vehicles-form/vehicles-form.component';
import { VehicleListComponent } from './features/vehicles/pages/vehicles-list/vehicles-list.component';
import { VehicleDetailsComponent } from './features/vehicles/pages/vehicles-detail/vehicles-detail.component';

export const routes: Routes = [
  { path: '', component: VehicleListComponent },
  { path: 'vehicles/create', component: VehicleFormComponent },
  { path: 'vehicles/edit/:id', component: VehicleFormComponent },
  { path: 'vehicles/details/:id', component: VehicleDetailsComponent },
  { path: '**', redirectTo: '' },
];
