import { Component, OnInit, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Rabbit } from "src/app/models/rabbit";

@Component({
  selector: "ncat-rabbit-edit",
  templateUrl: "./rabbit-edit.component.html",
  styleUrls: ["./rabbit-edit.component.css"]
})
export class RabbitEditComponent implements OnInit {
  constructor(
    private dialogRef: MatDialogRef<RabbitEditComponent>,
    @Inject(MAT_DIALOG_DATA) public rabbit: Rabbit
  ) {}

  ngOnInit() {}

  cancelClick(): void {
    this.dialogRef.close(null);
  }

  okClick(event: Event): void {}
}
