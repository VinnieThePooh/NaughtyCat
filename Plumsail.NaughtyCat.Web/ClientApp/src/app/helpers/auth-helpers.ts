import { JwtModule } from "@auth0/angular-jwt";

import { authJwtConst } from "../models/storageConstants";

export function tokenGetter() {
  return localStorage.getItem(authJwtConst);
}
