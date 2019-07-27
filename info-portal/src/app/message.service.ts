import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  message = '';

  add(message: string) {
    console.log(message);
    this.message = message;
  }
}
