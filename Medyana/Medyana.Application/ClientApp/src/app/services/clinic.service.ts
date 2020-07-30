import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Clinic, Result } from '../models/clinic';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {

  myAppUrl: string;
  myApiUrl: string;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
    this.myAppUrl = environment.appUrl;
    this.myApiUrl = 'api/clinics/';
}

getClinics(): Observable<Clinic[]> {
  return this.http.get<Clinic[]>(this.myAppUrl + this.myApiUrl)
  .pipe(
    retry(1),
    catchError(this.errorHandler)
  );
}

getClinic(postId: number): Observable<Clinic> {
  return this.http.get<Clinic>(this.myAppUrl + this.myApiUrl + postId)
  .pipe(
    retry(1),
    catchError(this.errorHandler)
  );
}

saveClinic(clinic): Observable<Clinic> {
  return this.http.post<Clinic>(this.myAppUrl + this.myApiUrl, JSON.stringify(clinic), this.httpOptions)
  .pipe(
    retry(1),
    catchError(this.errorHandler)
  );
}

updateClinic(clinic): Observable<Clinic> {
  return this.http.put<Clinic>(this.myAppUrl + this.myApiUrl, JSON.stringify(clinic), this.httpOptions)
  .pipe(
    retry(1),
    catchError(this.errorHandler)
  );
}

deleteClinic(id: number): Observable<Clinic> {
  return this.http.delete<Clinic>(this.myAppUrl + this.myApiUrl + id)
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
