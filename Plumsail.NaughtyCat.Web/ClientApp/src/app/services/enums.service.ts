import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BaseApiUrl } from "../models/urlConstants";
import { Observable } from "rxjs";
import { EnumItemDto } from "../models/enum-item-dto";
import { EnumItemContainerDto } from "../models/enumitem-container-dto";

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

  getRabbitRelated(): Observable<EnumItemContainerDto[]> {
    return this.httpClient.get<EnumItemContainerDto[]>(
      BaseApiUrl + "enums/rabbit-related"
    );
  }
}
