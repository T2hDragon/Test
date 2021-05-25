import { IRouter } from "@aurelia/router";
import { AppState } from "./state/app-state";

export class MyApp {
  constructor(
    @IRouter private router: IRouter,
    private state: AppState) {
  }

  attached() {
    console.log(this.state);
  }


  async logOut(){
    this.state.token = null;
    this.state.firstname = null;
    this.state.lastname = null;

    await this.router.load('/home-index');
  }
}
