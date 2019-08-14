import { Component, OnInit } from "@angular/core";
import { AccountService } from "src/app/services/account.service";
import { Router } from "@angular/router";

@Component({
  selector: "ncat-top",
  templateUrl: "./top.component.html",
  styleUrls: ["./top.component.css"]
})
export class TopComponent implements OnInit {
  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit() {}

  get greetingName(): string {
    let name =
      this.accountService.isAuthenticated &&
      this.accountService.userData &&
      this.accountService.userData.name;
    return (name || "Quest") + "!";
  }

  get isAuthenticated(): Boolean {
    return this.accountService.isAuthenticated;
  }

  login() {
    this.router.navigate(["login"]);
  }

  logout() {
    // console.log("Clicked logout");
    this.accountService.logout().subscribe(r => {
      r && this.router.navigate(["home"]);
    });
  }
}
