import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Equipment, Result } from '../models/equipment';

@Injectable({
  providedIn: 'root'
})
export class EquipmentService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/equipments/';
  }

  getEquipments(): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(this.myAppUrl + this.myApiUrl)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getEquipment(postId: number): Observable<Equipment> {
    return this.http.get<Equipment>(this.myAppUrl + this.myApiUrl + postId)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  saveEquipment(equipment): Observable<Equipment> {
    return this.http.post<Equipment>(this.myAppUrl + this.myApiUrl, JSON.stringify(equipment), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  updateEquipment(equipment): Observable<Equipment> {
    return this.http.put<Equipment>(this.myAppUrl + this.myApiUrl, JSON.stringify(equipment), this.httpOptions)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  deleteEquipment(id: number): Observable<Equipment> {
    return this.http.delete<Equipment>(this.myAppUrl + this.myApiUrl + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
