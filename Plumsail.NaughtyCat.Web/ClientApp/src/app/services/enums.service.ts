import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseApiUrl } from "../models/urlConstants";
import { Observable } from "rxjs";
import { EnumItemDto } from "../models/enum-item-dto";

@Injectable({
  providedIn: "root"
})
export class EnumsService {
  constructor(private httpClient: HttpClient) {}

  getDelicacies(): Observable<EnumItemDto[]> {
    return this.httpClient.get<EnumItemDto[]>(BaseApiUrl + "enums/delicacies");
  }

  getPriorities(): Observable<EnumItemDto[]> {
    return this.httpClient.get<EnumItemDto[]>(BaseApiUrl + "enums/priorities");
  }
}
