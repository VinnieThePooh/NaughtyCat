import { Component, OnInit } from "@angular/core";

@Component({
  selector: "ncat-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit {
  constructor() {}

  ngOnInit() {}

  onClicked() {
    window.alert("Button was clicked!");
  }
}
