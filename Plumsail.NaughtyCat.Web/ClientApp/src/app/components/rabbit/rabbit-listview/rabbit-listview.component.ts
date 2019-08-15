import { Component, OnInit } from "@angular/core";
import { RabbitService } from "src/app/services/rabbit.service";
import { MatTableDataSource } from "@angular/material/table";

@Component({
  selector: "ncat-rabbit-listview",
  templateUrl: "./rabbit-listview.component.html",
  styleUrls: ["./rabbit-listview.component.css"]
})
export class RabbitListviewComponent implements OnInit {
  rabbitsDataSource: MatTableDataSource;
  columnsToDisplay: Array<string> = [
    "name",
    "age",
    "color",
    "createDate",
    "updateDate"
  ];

  constructor(private rabbitService: RabbitService) {}

  ngOnInit() {
    this.rabbitService.getRabbits().subscribe(
      r => {
        this.rabbitsDataSource = r;
      },
      error => {}
    );
  }
}
