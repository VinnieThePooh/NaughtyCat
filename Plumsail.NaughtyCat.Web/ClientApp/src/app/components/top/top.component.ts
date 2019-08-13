import { Component, OnInit } from "@angular/core";
import { AccountService } from "src/app/services/account.service";

@Component({
  selector: "ncat-top",
  templateUrl: "./top.component.html",
  styleUrls: ["./top.component.css"]
})
export class TopComponent implements OnInit {
  constructor(private accountService: AccountService) {}

  ngOnInit() {}

  get greetingName(): string {
    let email =
      this.accountService.isAuthenticated &&
      this.accountService.userData &&
      this.accountService.userData.email;
    return (email || "Quest") + "!";
  }
}
