import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  logError(error: any) {
    console.log("ERROR: ", error);
  }
}
