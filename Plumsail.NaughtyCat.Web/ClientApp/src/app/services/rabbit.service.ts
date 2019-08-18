import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { Rabbit } from "../models/rabbit";
import { BaseApiUrl } from "../models/urlConstants";
import { RabbitListModel } from "../models/rabbit-list-model";
import Headers from "../helpers/headers";

@Injectable({
  providedIn: "root"
})
export class RabbitService {
  constructor(private httpClient: HttpClient) {}

  getRabbits(listModel: RabbitListModel = null): Observable<any> {
    var jsonModel = listModel && JSON.stringify(listModel);
    let params = new HttpParams().set("listModel", jsonModel);

    return this.httpClient.get<any>(BaseApiUrl + "rabbits/", {
      headers: Headers.JsonContentType,
      params: params
    });
  }

  addNewRabbit(rabbit: Rabbit): Observable<number> {
    return this.httpClient.post<number>(BaseApiUrl + "rabbits/", rabbit);
  }
}
