import { Component, OnInit } from "@angular/core";
import { RabbitService } from "src/app/services/rabbit.service";
import { MatTableDataSource } from "@angular/material/table";
import { Observable } from "rxjs";
import { Rabbit } from "src/app/models/rabbit";

@Component({
  selector: "ncat-rabbit-listview",
  templateUrl: "./rabbit-listview.component.html",
  styleUrls: ["./rabbit-listview.component.css"]
})
export class RabbitListviewComponent implements OnInit {
  rabbitsDataSource: Observable<Rabbit[]>;
  columnsToDisplay: Array<string> = [
    "name",
    "age",
    "color",
    "createDate",
    "updateDate"
  ];

  constructor(private rabbitService: RabbitService) {}

  ngOnInit() {
    this.rabbitsDataSource = this.rabbitService.getRabbits();
  }
}
