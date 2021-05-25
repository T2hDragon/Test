import { HttpClient } from "aurelia";
import { AccountService } from "../../services/account-service";
import { AppState } from "../../state/app-state";
import { IJwt } from "../../types/IJwt";
import { IRouter } from "aurelia-direct-router";

export class IdentityRegister {
  //AccountService

    private service: AccountService =
        new AccountService("https://autokool.azurewebsites.net/api/v1/Account/register", this.httpClient);

        private email: string = "foobar@ttu.ee";
        private password: string = "DucksG0Quack!";
        private firstname: string = "foo";
        private lastname: string = "bar";
        private username: string = "foobar";
        private errorMessage: string = null;

    constructor(
        @IRouter private router: IRouter,
        private state: AppState,
        protected httpClient: HttpClient) {

    }

    async loginClicked(event: Event) {
        event.preventDefault();
        event.stopPropagation();

        let response = await this.service.register(this.email, this.username, this.password, this.firstname, this.lastname);

        if (response.statusCode == 200 && response.data ) {
            this.state.token = (response.data as IJwt).token;
            this.state.firstname = (response.data as IJwt).firstname;
            this.state.lastname = (response.data as IJwt).lastname;
            this.state.username = (response.data as IJwt).lastname;

            await this.router.load('/home-index');
        } else {
            this.errorMessage = response.errorMessage
        }
        

    }

}
