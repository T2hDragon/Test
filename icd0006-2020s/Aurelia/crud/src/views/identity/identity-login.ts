import { HttpClient } from "aurelia";
import { AccountService } from "../../services/account-service";
import { AppState } from "../../state/app-state";
import { IJwt } from "../../types/IJwt";
import { IRouter } from "aurelia-direct-router";

export class IdentityLogin {
  //AccountService

    private service: AccountService =
        new AccountService("https://autokool.azurewebsites.net/api/v1/Account/login", this.httpClient);

    private user: string = "admin";
    private password: string = "DucksG0Quack!";
    private errorMessage: string = null;

    constructor(
        @IRouter private router: IRouter,
        private state: AppState,
        protected httpClient: HttpClient) {

    }

    async loginClicked(event: Event) {
        event.preventDefault();
        event.stopPropagation();

        let response = await this.service.login(this.user, this.password);

        if (response.statusCode < 400 && response.data ) {
            this.state.token = (response.data as IJwt).token;
            this.state.firstname = (response.data as IJwt).firstname;
            this.state.lastname = (response.data as IJwt).lastname;

            await this.router.load('/home-index');
        } else {
            this.errorMessage = response.errorMessage
        }
        

    }

}
