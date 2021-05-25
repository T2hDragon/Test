import { ContacttypeService } from './../services/contacttype-service';
import { IContactType } from "../domain/IContactType";

export class ContacttypesView {
    private data: IContactType[] =  [];

    constructor(private contacttypeService: ContacttypeService){

    }
    
    async attached() {
        console.log("ContacttypesView attached");
        this.data = await this.contacttypeService.getAll();
    }
}