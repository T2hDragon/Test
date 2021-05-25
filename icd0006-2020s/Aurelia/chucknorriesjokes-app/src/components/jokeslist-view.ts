import { bindable } from "@aurelia/runtime-html";
import { IJoke } from "../domain/IJoke";

export class jokeslistView{
    @bindable public title : string = "Default Title";
    @bindable public jokes : IJoke[] = [];
    
}