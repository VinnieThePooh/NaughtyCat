import { Component, OnInit, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Rabbit } from "src/app/models/rabbit";
import { NgForm } from "@angular/forms";
import { EnumsService } from "src/app/services/enums.service";
import { EnumItemDto } from "src/app/models/enum-item-dto";
import { RabbitEditViewModel } from "src/app/models/rabbit-edit-view-model";
import { RabbitService } from "src/app/services/rabbit.service";

@Component({
  selector: "ncat-rabbit-edit",
  templateUrl: "./rabbit-edit.component.html",
  styleUrls: ["./rabbit-edit.component.css"]
})
export class RabbitEditComponent {
  constructor(
    private dialogRef: MatDialogRef<RabbitEditComponent>,
    private rabbitsService: RabbitService,
    @Inject(MAT_DIALOG_DATA) private rabbitViewModel: RabbitEditViewModel
  ) {
    this.delicacyEnums = rabbitViewModel.delicacyEnums;
    this.priorityEnums = rabbitViewModel.priorityEnums;
    this.rabbit = rabbitViewModel.rabbit;
    this.rabbit.delicacy = rabbitViewModel.delicacyEnums[0].value;
    this.rabbit.priority = rabbitViewModel.priorityEnums[0].value;
  }

  public rabbit: Rabbit;
  public delicacyEnums: EnumItemDto[] = [];
  public priorityEnums: EnumItemDto[] = [];

  cancelClick(): void {
    this.dialogRef.close(null);
  }

  okClick(form: NgForm): void {
    this.rabbitsService.addNewRabbit(this.rabbit).subscribe(
      r => {
        this.rabbit.id = r;
        this.dialogRef.close(this.rabbit);
      },
      error => {
        throw error;
      }
    );
  }
}
