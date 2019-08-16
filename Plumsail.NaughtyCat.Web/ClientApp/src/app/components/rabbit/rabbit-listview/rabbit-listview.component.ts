import { Component, OnInit } from "@angular/core";
import { RabbitService } from "src/app/services/rabbit.service";
import { MatTableDataSource } from "@angular/material/table";
import { Observable } from "rxjs";
import { Rabbit } from "src/app/models/rabbit";
import { PageEvent } from "@angular/material";

@Component({
  selector: "ncat-rabbit-listview",
  templateUrl: "./rabbit-listview.component.html",
  styleUrls: ["./rabbit-listview.component.css"]
})
export class RabbitListviewComponent implements OnInit {
  rabbitsDataSource: Rabbit[];
  totalRecordsCount: number;
  pageSize: number;
  isDataLoading: boolean;

  columnsToDisplay: Array<string> = [
    // "id",
    "name",
    "age",
    "color",
    "createDate",
    "updateDate"
  ];

  constructor(private rabbitService: RabbitService) {}

  get hasRabbits() {
    return this.rabbitsDataSource && this.rabbitsDataSource.length;
  }

  ngOnInit() {
    this.isDataLoading = true;
    this.rabbitService.getRabbits().subscribe(
      r => {
        this.rabbitsDataSource = r.pageData;
        this.pageSize = r.pageSize;
        this.totalRecordsCount = r.totalRecordsCount;
        this.isDataLoading = false;
      },
      e => {
        this.isDataLoading = false;
        throw e;
      }
    );
  }

  addNewRabbit() {
    console.log("Invited new rabbit");
  }

  formatDate(date?: string): string {
    var parsedDate = Date.parse(date);
    return !Number.isNaN(parsedDate)
      ? new Date(parsedDate).toLocaleDateString("ru-RU")
      : "N/A";
  }

  getPageData(pageEvent: PageEvent): void {
    var pNumber = pageEvent.pageIndex + 1;
  }
}
