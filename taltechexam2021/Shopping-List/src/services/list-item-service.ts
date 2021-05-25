import { HttpClient } from "aurelia";
import { IFetchResponse } from "../types/IFetchResponse";
import { IQueryParams } from "../types/IQueryParams";
import { IMessage } from "../types/IMessage";
import { IListItem } from "../domain/IListItem";

export class ListItemService {
    private baseUrl="https://taltech.akaver.com/api/v1/ListItems";
    private apiKeyParam="?apiKey=a473ed73-012f-4fda-b1f8-7066e70aa0c6";

    constructor(protected httpClient: HttpClient) {
    }

    private apiKeyParams = {

    }

    async getAll(filter?: boolean ): Promise<IFetchResponse<IListItem[]>> {
        let url = this.baseUrl ;
        var filterString = (filter == null? "" : "&completed=" + (filter? "true" : "false"));
        try {
            const response = await this.httpClient.fetch(url + this.apiKeyParam + filterString, { cache: "no-store" });
            if (response.ok) {
                const data = (await response.json()) as IListItem[];
                return {
                    statusCode: response.status,
                    data: data,
                };
            }

            const data = (await response.json()) as IMessage;

            return {
                statusCode: response.status,
                errorMessage: response.statusText + ' ' + data.messages.join(' '),
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason),
            };
        }

    }

    async get(id: string): Promise<IFetchResponse<IListItem>> {
        let url = this.baseUrl;
        url = url + '/' + id;
        try {
            const response = await this.httpClient.fetch(url + this.apiKeyParam, { cache: "no-store" });
    
            if (response.ok) {
                const data = (await response.json()) as IListItem;
                return {
                    statusCode: response.status,
                    data: data,
                };
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText,
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason),
            };
        }

    }

    async update(id: string, listItem:IListItem): Promise<IFetchResponse<IListItem>> {
        let url = this.baseUrl;
        url = url + '/' + id;

        try {
            let body = listItem;
            let bodyStr = JSON.stringify(body);
            const response = await this.httpClient.put(url + this.apiKeyParam, bodyStr , { cache: "no-store" })

            if (response.ok) {
                return {
                    statusCode: response.status,
                };
            }

            const data = (await response.json()) as IMessage;

            return {
                statusCode: response.status,
                errorMessage: response.statusText + ' ' + data.messages.join(' '),
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason),
            };
        }
    }

    async delete(id: string): Promise<IFetchResponse<IListItem>> {
        let url = this.baseUrl;
        url = url + '/' + id;
        
        try {

            const response = await this.httpClient.delete(url + this.apiKeyParam, '', { cache: "no-store" });

            if (response.ok) {
                const data = (await response.json()) as IListItem;
                return {
                    statusCode: response.status,
                    data: data,
                };
            }
            const data = (await response.json()) as IMessage;
            return {
                statusCode: response.status,
                errorMessage: response.statusText + ' ' + data.messages.join(' '),
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason),
            };
        }
    }

    async create( listItem:{description: string, completed: boolean}): Promise<IFetchResponse<IListItem>> {
        let url = this.baseUrl;
        url = url;

        
        try {
            let body = listItem;
            let bodyStr = JSON.stringify(body);
            const response = await this.httpClient.post(url + this.apiKeyParam, bodyStr, { cache: "no-store" });

            if (response.ok) {
                const data = (await response.json()) as IListItem;
                return {
                    statusCode: response.status,
                    data: data,
                };
            }
            const data = (await response.json()) as IMessage;
            return {
                statusCode: response.status,
                errorMessage: response.statusText + ' ' + data.messages.join(' '),
            };
        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason),
            };
        }
    }

}