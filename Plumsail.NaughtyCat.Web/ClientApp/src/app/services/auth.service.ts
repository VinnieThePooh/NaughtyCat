import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BaseApiUrl } from "../models/urlConstants";
import { authJwt as AuthJwt } from "../models/storageConstants";
import { Observable, BehaviorSubject } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  constructor(private httpClient: HttpClient) {}

  login(email: string, password: string): Observable<Boolean> {
    let subject: BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);

    var res = this.httpClient
      .post(
        BaseApiUrl + "/auth/login",
        {
          email: email,
          password: password
        },
        { headers: new HttpHeaders({ "Content-Type": "application/json" }) }
      )
      .subscribe(
        r => {
          let token = (<any>r).token;
          localStorage.setItem(AuthJwt, token);

          subject.next(true);
        },
        er => {
          subject.next(false);
          throw er;
        }
      );

    return subject.asObservable();
  }

  logout(): Observable<Boolean> {
    let subject: BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);

    if (!localStorage.getItem(AuthJwt)) {
      subject.next(false);
      return subject.asObservable();
    }

    localStorage.removeItem(AuthJwt);
    subject.next(true);

    return subject.asObservable();
  }

  get isAuthenticated(): Boolean {
    // add expiration date checking
    var token = localStorage.getItem(AuthJwt);
    return !!token;
  }
}
