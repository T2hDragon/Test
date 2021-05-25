import {IRouteViewModel} from 'aurelia';

export class ContacttypeDetails implements IRouteViewModel {
    private data: string = '';

    load(parameters){
        console.log(parameters);
        this.data = JSON.stringify(parameters);
    }
}