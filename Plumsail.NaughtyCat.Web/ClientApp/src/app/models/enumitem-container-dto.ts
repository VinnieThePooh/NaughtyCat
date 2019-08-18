import { EnumItemDto } from "./enum-item-dto";

export interface EnumItemContainerDto {
  enumName: string;
  items: EnumItemDto[];
}
