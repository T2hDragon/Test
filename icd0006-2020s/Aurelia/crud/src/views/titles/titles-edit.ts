import { HttpClient, IRouteViewModel } from "aurelia";
import { ITitle } from "../../domain/ITitle";
import { BaseService } from "../../services/base-service";
import { AppState } from "../../state/app-state";
import { IRouter } from "aurelia-direct-router";

export class TitlesEdit implements IRouteViewModel  {

    private service: BaseService<ITitle> =
        new BaseService<ITitle>("https://autokool.azurewebsites.net/api/v1/Titles", this.httpClient, this.state.token);


    private data: ITitle;
    private errorMessage: string;
    constructor(@IRouter private router: IRouter,protected httpClient: HttpClient, private state: AppState) {

    }

    async attached() {
    }

    async load(parameters){

        let response = await this.service.get(parameters[0]);
        if (response.data) {
            this.data = response.data;
        }
        
    }

    async updateClicked(event: Event): Promise<void> {
        event.preventDefault();
        event.stopPropagation();

        let response = await this.service.update(this.data.id, this.data);
        if ((response.statusCode >= 200 || response.statusCode <= 300)) {
            await this.router.load('/titles-index');
        } else {
            this.errorMessage = response.errorMessage
        }
    }

    async deleteClicked(event: Event) : Promise<void> {
        event.preventDefault();
        event.stopPropagation();

        let response = await this.service.delete(this.data.id);
        if ((response.statusCode >= 200 || response.statusCode <= 300)) {
            await this.router.load('/titles-index');
        } else {
            this.errorMessage = response.errorMessage
        }
    }


}