import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Vehicle } from "../models/vehicle.model";
import { ApiResponse } from "../models/api-response.model";
import { environment } from "../../../environments/environment";


@Injectable({ providedIn: 'root' })
export class VehicleService {
    private http = inject(HttpClient);
    private baseUrl = `${environment.apiUrl}/vehicles`;

    getAll(): Observable<ApiResponse<Vehicle[]>> {
        return this.http.get<ApiResponse<Vehicle[]>>(this.baseUrl);
    }

    getById(id: number): Observable<ApiResponse<Vehicle>> {
        return this.http.get<ApiResponse<Vehicle>>(`${this.baseUrl}/${id}`);
    }

    create(vehicle: Vehicle): Observable<ApiResponse<Vehicle>> {
        return this.http.post<ApiResponse<Vehicle>>(this.baseUrl, vehicle);
    }

    update(vehicle: Vehicle): Observable<ApiResponse<Vehicle>> {
        return this.http.put<ApiResponse<Vehicle>>(`${this.baseUrl}/${vehicle.id}`, vehicle);
    }

    delete(id: number): Observable<ApiResponse<void>> {
        return this.http.delete<ApiResponse<void>>(`${this.baseUrl}/${id}`);
    }

    getByPlate(plate: string): Observable<ApiResponse<Vehicle>> {
        return this.http.get<ApiResponse<Vehicle>>(`${this.baseUrl}/plate/${encodeURIComponent(plate)}`);
    }
}