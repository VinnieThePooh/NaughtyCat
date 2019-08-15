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

  getRabbits(listModel: RabbitListModel = null): Observable<Rabbit[]> {
    let params = new HttpParams();
    params.append("listModel", listModel && JSON.stringify(listModel));

    return this.httpClient.get<Rabbit[]>(BaseApiUrl + "rabbits/", {
      headers: Headers.JsonContentType,
      params: params
    });
  }

  addNewRabbit(): Observable<any> {
    return null;
  }
}
