import { UserData } from "./user-data";

export interface LoginResult {
  succeeded: boolean;
  token: string;
  userData: UserData;
}
