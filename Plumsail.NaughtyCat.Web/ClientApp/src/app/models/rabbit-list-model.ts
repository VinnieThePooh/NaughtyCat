import { RabbitListModelFilter } from "./rabbit-listmodel-filter";

export interface RabbitListModel {
  pageNumber?: number;
  pageSize?: number;
  filter?: RabbitListModelFilter;
}
