import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { NgForm, FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { LoginResult } from "src/app/models/login-result";

@Component({
  selector: "ncat-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {}

  login(form: NgForm) {
    // do i need it?
    if (!form.valid) {
      return;
    }

    // todo: shorten it
    var controls = form.control.controls;
    var email = controls["email"].value;
    var password = controls["password"].value;

    this.authService.login(email, password).subscribe(
      r => {
        if (r.succeeded) {
          const rUrl = this.authService.redirectUrl;

          if (rUrl) {
            this.router.navigate([rUrl]);
            this.authService.redirectUrl = null;
          } else {
            this.router.navigate(["rabbits"]);
          }
        } else {
          // incorrect login or password
          console.log("Incorrect email or password");
        }
      },
      error => {
        throw error;
      }
    );
  }
}
