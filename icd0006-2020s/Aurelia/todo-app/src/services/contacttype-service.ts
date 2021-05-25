import { IContactType } from './../domain/IContactType';
import { HttpClient, inject } from "aurelia";

@inject()
export class ContacttypeService {

    constructor(private httpClient: HttpClient) {

    }

    async getAll(): Promise<IContactType[]> {
        const response = await this.httpClient
            .get("https://localhost:5001/api/contacttypes/", { cache: "no-store" });
        console.log(response);
        if (response.ok) {
            const data = (await response.json()) as IContactType[];
            return data;
        }
        return [];
    }

    getAllPromiseStyle(): Promise<IContactType[]> {
        return this.httpClient
            .get("https://localhost:5001/api/contacttypes/", { cache: "no-store" })
            .then(response => {
                console.log(response);
                return response.json();
            })
            .then(data => {
                console.log(data);
                return data;
            })
            .catch(error => []);
    }
}