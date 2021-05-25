import { HttpClient } from "@aurelia/fetch-client";
import { inject } from "@aurelia/kernel";
import { IJoke } from "../domain/IJoke";

@inject()
export class JokesService{

    constructor(private httpClient: HttpClient){

    }

    async getJoke(category: string): Promise<IJoke>{
        const response = await this.httpClient
            .get("https://api.chucknorris.io/jokes/random?category=" + category, {cache: "no-store"});
        if (response.ok){
            const data = (await response.json()) as IJoke;
            return data;
        }
    }

    async getJokes(category: string, amount: number): Promise<IJoke[]>{
        var jokes: IJoke[] = [];
        var index: number;
        for (index = 0; index < amount; index++) {
          jokes.push(await this.getJoke(category))
        }
        return jokes;
    }
}