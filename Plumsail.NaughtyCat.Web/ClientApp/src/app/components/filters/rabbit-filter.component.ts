import { Component, OnInit, EventEmitter, Output, Input } from "@angular/core";
import { RabbitListModelFilter } from "src/app/models/rabbit-listmodel-filter";
import { EnumItemDto } from "src/app/models/enum-item-dto";

@Component({
  selector: "ncat-rabbit-filter",
  templateUrl: "./rabbit-filter.component.html",
  styleUrls: ["./rabbit-filter.component.css"]
})
export class RabbitFilterComponent implements OnInit {
  public filter: RabbitListModelFilter;

  @Input() delicacyEnums: EnumItemDto[];
  @Input() priorityEnums: EnumItemDto[];

  delicacyEnumsGet: EnumItemDto[];
  priorityEnumsGet: EnumItemDto[];

  @Output() filterApplied: EventEmitter<
    RabbitListModelFilter
  > = new EventEmitter();

  constructor() {}

  ngOnInit() {
    this.filter = {
      name: null,
      color: null,
      age: null,
      delicacy: null,
      priority: null,
      createDateFrom: null,
      createDateTo: null,
      updateDateFrom: null,
      updateDateTo: null
    };

    this.delicacyEnumsGet = [];
    this.delicacyEnumsGet.push({
      value: 0,
      description: "No delicacy",
      selected: true
    });

    this.delicacyEnums.forEach(item => this.delicacyEnumsGet.push(item));

    this.priorityEnumsGet = [];
    this.priorityEnumsGet.push({
      value: 0,
      description: "No priority",
      selected: true
    });

    this.priorityEnums.forEach(item => this.priorityEnumsGet.push(item));
  }

  get filterData(): RabbitListModelFilter {
    const f = this.filter;
    const name = f.name && f.name.trim();
    const color = f.color && f.color.trim();

    let isEmpty = true;
    Object.keys(this.filter).forEach(k => {
      if (!!this.filter[k]) isEmpty = false;
    });

    if (isEmpty) return null;

    this.filter.delicacy = !!this.filter.delicacy ? this.filter.delicacy : null;
    this.filter.priority = !!this.filter.priority ? this.filter.priority : null;

    let fname = this.filter.name && this.filter.name.trim();
    this.filter.name = !!fname ? fname : null;

    return this.filter;
  }

  applyFilter() {
    this.filterApplied.emit(this.filterData);
  }
}
