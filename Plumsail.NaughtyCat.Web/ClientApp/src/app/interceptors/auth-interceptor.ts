import { HttpInterceptor } from "@angular/common/http";
import { HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { tokenGetter } from "../helpers/auth-helpers";
import { AuthService } from "../services/auth.service";
import { Injectable } from "@angular/core";

@Injectable({ providedIn: "root" })
export class AuthInterceptor implements HttpInterceptor {
  constructor(authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    var token = tokenGetter();

    // todo: refactor token getting
    if (token) {
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
    return next.handle(req);
  }
}
