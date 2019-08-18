import { Component, ViewChild, OnInit } from "@angular/core";
import { RabbitService } from "src/app/services/rabbit.service";
import { Rabbit } from "src/app/models/rabbit";
import { PageEvent, MatPaginator, MatDialog } from "@angular/material";
import { RabbitListModel } from "src/app/models/rabbit-list-model";
import { RabbitEditComponent } from "../rabbit-edit/rabbit-edit.component";

@Component({
  selector: "ncat-rabbit-listview",
  templateUrl: "./rabbit-listview.component.html",
  styleUrls: ["./rabbit-listview.component.css"]
})
export class RabbitListviewComponent implements OnInit {
  rabbitsDataSource: Rabbit[];
  totalRecordsCount: number;
  pageSize: number;
  isDataLoading: boolean = true;
  isPaging: boolean = false;
  pageIndex: number;

  columnsToDisplay: Array<string> = [
    // "id",
    "name",
    "age",
    "color",
    "createDate",
    "updateDate"
  ];

  constructor(
    private rabbitService: RabbitService,
    private dialog: MatDialog
  ) {}

  private paginator: MatPaginator;

  @ViewChild(MatPaginator, { static: false }) set content(
    content: MatPaginator
  ) {
    if (content) {
      this.paginator = content;
      this.updatePaginator(this.paginator);
    }
  }

  get hasRabbits() {
    return this.rabbitsDataSource && this.rabbitsDataSource.length;
  }

  ngOnInit(): void {
    // console.log("ngOnInit event hook called");
    this.rabbitService.getRabbits().subscribe(
      r => {
        this.isDataLoading = false;

        this.rabbitsDataSource = r.pageData;
        this.pageSize = r.pageSize;
        this.totalRecordsCount = r.totalRecordsCount;
        this.pageIndex = r.pageNumber - 1;
      },
      e => {
        this.isDataLoading = false;
        throw e;
      }
    );
  }

  updatePaginator(p) {
    setTimeout(() => {
      p.pageSize = this.pageSize;
      p.pageIndex = this.pageIndex;
      p.length = this.totalRecordsCount;
    });
  }

  addNewRabbit() {
    const dialogRef = this.dialog.open(RabbitEditComponent, {
      width: "300px",
      data: {} as Rabbit
    });
    dialogRef.afterClosed().subscribe(r => {
      console.log("Dialog was closed");
    });
  }

  formatDate(date?: string): string {
    var parsedDate = Date.parse(date);
    return !Number.isNaN(parsedDate)
      ? new Date(parsedDate).toLocaleDateString("ru-RU")
      : "N/A";
  }

  getPageData(pageEvent: PageEvent): void {
    let listModel: RabbitListModel = {};
    listModel.pageNumber = pageEvent.pageIndex + 1;
    listModel.pageSize = pageEvent.pageSize;

    this.isPaging = true;
    this.rabbitService.getRabbits(listModel).subscribe(
      r => {
        this.isPaging = false;
        this.rabbitsDataSource = r.pageData;
        this.pageSize = r.pageSize;
        this.totalRecordsCount = r.totalRecordsCount;
        this.pageIndex = r.pageNumber - 1;
      },
      e => {
        this.isPaging = false;
        throw e;
      }
    );
  }
}
