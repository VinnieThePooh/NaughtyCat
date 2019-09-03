import { Component, OnInit, EventEmitter, Output, Input } from "@angular/core";
import { RabbitListModelFilter } from "src/app/models/rabbit-listmodel-filter";
import { EnumItemDto } from "src/app/models/enum-item-dto";
import { MatSelectChange } from "@angular/material";
import { FormBuilder, NgModelGroup, FormGroup } from "@angular/forms";

@Component({
  selector: "ncat-rabbit-filter",
  templateUrl: "./rabbit-filter.component.html",
  styleUrls: ["./rabbit-filter.component.css"]
})
export class RabbitFilterComponent implements OnInit {
  @Input() delicacyEnums: EnumItemDto[];
  @Input() priorityEnums: EnumItemDto[];

  delicacyData: EnumItemDto[];
  priorityData: EnumItemDto[];
  filterForm: FormGroup;

  @Output() filterApplied: EventEmitter<
    RabbitListModelFilter
  > = new EventEmitter();

  constructor(private builder: FormBuilder) {}

  ngOnInit() {
    this.delicacyData = [];
    this.delicacyData.push({
      value: 0,
      description: "No delicacy",
      selected: true
    });

    this.delicacyEnums.forEach(item => this.delicacyData.push(item));

    this.priorityData = [];
    this.priorityData.push({
      value: 0,
      description: "No priority",
      selected: true
    });

    this.priorityEnums.forEach(item => this.priorityData.push(item));
    this.filterForm = this.builder.group({
      name: [""],
      color: [""],
      delicacy: null,
      priority: null,
      age: null,
      createDateFrom: null,
      createDateTo: null,
      updateDateFrom: null,
      updateDateTo: null
    });
  }

  filterData(filter: RabbitListModelFilter): RabbitListModelFilter {
    const f = filter;

    let isEmpty = true;
    Object.keys(f).forEach(k => {
      if (!!f[k]) isEmpty = false;
    });

    if (isEmpty) return null;

    f.delicacy = !!f.delicacy ? f.delicacy : null;
    f.priority = !!f.priority ? f.priority : null;

    let fname = f.name && f.name.trim();
    f.name = !!fname ? fname : null;

    return f;
  }

  delicacyChange(event: MatSelectChange) {
    console.log(event.value);
  }

  applyFilter(event: Event) {
    event.preventDefault();
    var filter = JSON.stringify(this.filterForm.value) as RabbitListModelFilter;
    var clearFilter = this.filterData(filter);
    this.filterApplied.emit(clearFilter);
  }
}
