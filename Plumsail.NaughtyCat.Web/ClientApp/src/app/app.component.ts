import { Component } from "@angular/core";
import { setTheme } from "ngx-bootstrap/utils";
// import { MatGridList } from "@angular/material/grid-list";

@Component({
  selector: "ncat-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent {
  title = "ClientApp";

  constructor() {
    setTheme("bs4");
  }
}
