import { BaseService } from '../../services/base-service';
import { HttpClient } from "aurelia";
import { ITitle } from '../../domain/ITitle';
import { AppState } from "../../state/app-state";
import { IRouter } from "aurelia-direct-router";

export class TitlesIndex {
    private titleService: BaseService<ITitle> = 
        new BaseService<ITitle>("https://autokool.azurewebsites.net/api/v1/Titles", this.httpClient, this.state.token);

    
    private titles: ITitle[] = [];

    constructor(protected httpClient: HttpClient, private state: AppState){

    }

    async attached() {

        let response = await this.titleService.getAll();
        if (response.data) {
            this.titles = response.data;
        } 
    }
}