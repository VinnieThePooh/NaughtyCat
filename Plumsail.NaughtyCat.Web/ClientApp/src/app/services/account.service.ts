import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { BaseApiUrl } from "../models/urlConstants";
import {
  authJwtConst as AuthJwtConst,
  userDataConst as UserDataConst
} from "../models/storageConstants";
import { Observable, BehaviorSubject, Subject } from "rxjs";
import { LoginResult } from "../models/login-result";
import { LoginComponent } from "../components/login/login.component";
import { UserData } from "../models/user-data";

@Injectable({
  providedIn: "root"
})
export class AccountService {
  constructor(private httpClient: HttpClient) {}

  login(email: string, password: string): Observable<LoginResult> {
    let subject: Subject<LoginResult> = new Subject();

    var res = this.httpClient
      .post(
        BaseApiUrl + "/account/login",
        {
          email: email,
          password: password
        },
        { headers: new HttpHeaders({ "Content-Type": "application/json" }) }
      )
      .subscribe(
        r => {
          var result = r as LoginResult;

          if (result.succeeded) {
            // cause of we dont do unnecessary data round-trip
            result.userData.email = email;
            localStorage.setItem(AuthJwtConst, result.token);
            localStorage.setItem(
              UserDataConst,
              JSON.stringify(result.userData)
            );
          }
          subject.next(result);
        },
        er => {
          subject.error(er);
        }
      );

    return subject.asObservable();
  }

  logout(): Observable<Boolean> {
    let subject: BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);

    if (!this.isAuthenticated) {
      subject.next(false);
      return subject.asObservable();
    }

    localStorage.removeItem(AuthJwtConst);
    subject.next(true);

    return subject.asObservable();
  }

  get isAuthenticated(): Boolean {
    //todo:  add expiration date checking
    var token = localStorage.getItem(AuthJwtConst);
    return !!token;
  }

  register(): Observable<Boolean> {
    let subject: BehaviorSubject<Boolean> = new BehaviorSubject<Boolean>(false);

    return subject.asObservable();
  }

  get userData(): UserData {
    var data = localStorage.getItem(UserDataConst);
    return JSON.parse(data);
  }
}
