import { Component, ViewChild, OnInit } from "@angular/core";
import { RabbitService } from "src/app/services/rabbit.service";
import { Rabbit } from "src/app/models/rabbit";
import {
  PageEvent,
  MatPaginator,
  MatDialog,
  MatTableDataSource,
  MatSnackBar
} from "@angular/material";
import { RabbitListModel } from "src/app/models/rabbit-list-model";
import { RabbitEditComponent } from "../rabbit-edit/rabbit-edit.component";
import { EnumsService } from "src/app/services/enums.service";
import { EnumItemDto } from "src/app/models/enum-item-dto";
import { RabbitEditViewModel } from "src/app/models/rabbit-edit-view-model";
import { Directionality } from "@angular/cdk/bidi";
import { RabbitListModelFilter } from "src/app/models/rabbit-listmodel-filter";
import { RabbitFilterComponent } from "../../filters/rabbit-filter.component";

@Component({
  selector: "ncat-rabbit-listview",
  templateUrl: "./rabbit-listview.component.html",
  styleUrls: ["./rabbit-listview.component.css"]
})
export class RabbitListviewComponent implements OnInit {
  rabbitsDataSource: MatTableDataSource<Rabbit>;
  totalRecordsCount: number;
  pageSize: number;
  isDataLoading: boolean = true;
  isPaging: boolean = false;
  pageIndex: number;
  filter: RabbitListModelFilter;

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
    private enumsService: EnumsService,
    private dialog: MatDialog,
    private matSnackBar: MatSnackBar
  ) {}

  private delicacyEnums: EnumItemDto[] = [];
  private priorityEnums: EnumItemDto[] = [];
  private paginator: MatPaginator;

  @ViewChild(MatPaginator, { static: false }) set content(
    content: MatPaginator
  ) {
    if (content) {
      this.paginator = content;
      this.updatePaginator(this.paginator);
    }
  }

  @ViewChild(RabbitFilterComponent, { static: false })
  filterComponent: RabbitFilterComponent;

  get hasRabbits() {
    return this.rabbitsDataSource && this.rabbitsDataSource.data.length;
  }

  ngOnInit(): void {
    this.isDataLoading = true;

    this.enumsService.getRabbitRelated().subscribe(
      r => {
        r.forEach(r => {
          if (r.enumName === "DelicacyEnum") {
            this.delicacyEnums = r.items;
          }

          if (r.enumName === "PriorityEnum") {
            this.priorityEnums = r.items;
          }
        });
      },
      e => {
        throw e;
      }
    );

    this.rabbitService.getRabbits().subscribe(
      r => {
        this.isDataLoading = false;

        this.rabbitsDataSource = new MatTableDataSource(r.pageData);
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
    let viewModel: RabbitEditViewModel = {} as RabbitEditViewModel;
    viewModel.delicacyEnums = this.delicacyEnums;
    viewModel.priorityEnums = this.priorityEnums;
    viewModel.rabbit = {} as Rabbit;

    const dialogRef = this.dialog.open(RabbitEditComponent, {
      width: "300px",
      data: viewModel
    });

    dialogRef.afterClosed().subscribe(r => {
      if (r) {
        this.rabbitsDataSource.data.unshift(r as Rabbit);
        this.rabbitsDataSource = new MatTableDataSource(
          this.rabbitsDataSource.data
        );
        this.matSnackBar.open("New rabbit added successfully", null, {
          duration: 3000,
          horizontalPosition: "end",
          verticalPosition: "top",
          panelClass: "sn-bar-container"
        });
        return;
      }
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
    listModel.filter = this.filter;
    this.getRabbits(listModel);
  }

  onFilterApplied(filter: RabbitListModelFilter) {
    console.log("Filter appleid: ", JSON.stringify(filter));

    let listModel: RabbitListModel = {};
    listModel.pageSize = this.pageSize;
    listModel.pageNumber = 1;
    this.filter = listModel.filter = filter;
    this.getRabbits(listModel);
  }

  getRabbits(listModel: RabbitListModel) {
    this.isPaging = true;
    this.rabbitService.getRabbits(listModel).subscribe(
      r => {
        this.rabbitsDataSource = new MatTableDataSource(r.pageData);
        this.pageSize = r.pageSize;
        this.totalRecordsCount = r.totalRecordsCount;
        this.pageIndex = r.pageNumber - 1;
        this.isPaging = false;
      },
      e => {
        this.isPaging = false;
        throw e;
      }
    );
  }
}
