export interface RabbitListModelFilter {
  name?: string;
  age?: string;
  color?: string;
  delicacy?: number | null;
  priority?: number | null;
  createDateFrom?: Date | null;
  createDateTo?: Date | null;
  updateDateFrom?: Date | null;
  updateDateTo?: Date | null;
}
