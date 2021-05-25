import { HttpClient, IRouteViewModel } from "aurelia";
import { ITitle } from "../../domain/ITitle";
import { BaseService } from "../../services/base-service";
import { AppState } from "../../state/app-state";
import { IRouter } from "aurelia-direct-router";

export class TitlesDetails implements IRouteViewModel  {

    private service: BaseService<ITitle> =
        new BaseService<ITitle>("https://autokool.azurewebsites.net/api/v1/Titles", this.httpClient, this.state.token);


    private data: ITitle;

    constructor(protected httpClient: HttpClient, private state: AppState) {

    }

    async attached() {
    }

    async load(parameters){

        let response = await this.service.get(parameters[0]);
        if (response.data) {
            this.data = response.data;
        }
        
    }
}