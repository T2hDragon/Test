import axios from 'axios'
import { IMessage } from "@/types/IMessage";
import { IQueryParams } from "@/types/IQueryParams";
import { IFetchResponse } from "@/types/IFetchResponse";
import store from "@/store";

export class BaseService<TEntity> {
    jwt = store.getters.jwt;

    constructor(protected apiEndpointUrl: string) {
        this.apiEndpointUrl = apiEndpointUrl;
    }

    private authHeaders = this.jwt !== undefined
        ? {
            Authorization: 'Bearer ' + this.jwt
        }
        : {

        };

    async getAll(): Promise<IFetchResponse<TEntity[]>> {
        const url = this.apiEndpointUrl;

        try {
            const response = await axios.get(url, { headers: this.authHeaders });
            if (response.status >= 200 && response.status < 300) {
                const TEntityList = (await response.data) as TEntity[];
                return {
                    statusCode: response.status,
                    data: TEntityList,
                };
            }
            const data = (await response.data) as IMessage;
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

    async get(id: string): Promise<IFetchResponse<TEntity>> {
        let url = this.apiEndpointUrl;
        url = url + '/' + id;

        try {
            const response = await axios.get(url, { headers: this.authHeaders });

            if (response.status >= 200 && response.status < 300) {
                const TEntity = (await response.data) as TEntity;
                return {
                    statusCode: response.status,
                    data: TEntity,
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

    async update(id: string, queryParams?: IQueryParams): Promise<IFetchResponse<TEntity>> {
        let url = this.apiEndpointUrl;
        url = url + '/' + id;

        try {
            const body = queryParams;
            const bodyStr = JSON.stringify(body);
            const response = await axios.put(
                url,
                bodyStr,
                {
                    headers: {
                        Authorization: this.authHeaders.Authorization,
                        'Content-Type': 'application/json'
                    }
                }
            );
            console.log(response);

            if (response.status >= 200 && response.status < 300) {
                const TEntity = (await response.data) as TEntity;
                return {
                    statusCode: response.status,
                    data: TEntity,
                };
            }

            const data = (await response.data) as IMessage;

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

    async delete(id: string): Promise<IFetchResponse<TEntity>> {
        let url = this.apiEndpointUrl;
        url = url + '/' + id;

        try {
            const response = await axios.delete(
                url,
                {
                    headers: {
                        Authorization: this.authHeaders.Authorization,
                        'Content-Type': 'application/json'
                    }
                }
            );

            if (response.status >= 200 && response.status < 300) {
                const TEntity = (await response.data) as TEntity;
                return {
                    statusCode: response.status,
                    data: TEntity,
                };
            }
            const data = (await response.data) as IMessage;
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

    async create(queryParams?: IQueryParams,): Promise<IFetchResponse<TEntity>> {
        let url = this.apiEndpointUrl;
        url = url + '/';

        try {
            const body = queryParams;
            const bodyStr = JSON.stringify(body);
            console.log(bodyStr);
            const response = await axios.post(
                url,
                bodyStr,
                {
                    headers: {
                        Authorization: this.authHeaders.Authorization,
                        'Content-Type': 'application/json'
                    }
                }
            );
            console.log(response);
            if (response.status >= 200 && response.status < 300) {
                return {
                    statusCode: response.status,
                };
            }
            const data = (await response.data) as IMessage;
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
