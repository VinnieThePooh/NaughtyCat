import { Rabbit } from "./rabbit";
import { EnumItemDto } from "./enum-item-dto";

export interface RabbitEditViewModel {
  rabbit: Rabbit;
  delicacyEnums: EnumItemDto[];
  priorityEnums: EnumItemDto[];
}
