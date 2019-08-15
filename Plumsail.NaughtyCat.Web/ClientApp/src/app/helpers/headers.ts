import { HttpHeaders } from "@angular/common/http";

export class Headers {
  static JsonContentType: HttpHeaders = new HttpHeaders({
    "Content-Type": "application/json"
  });
}

export default Headers;
