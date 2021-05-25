import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessages } from '../types/IMessages';
import { IQueryParams } from "../types/IQueryParams";


export abstract class BaseService {
    protected static axios = Axios.create({
        baseURL: ApiBaseUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    });

    protected static getAxiosConfiguration(culture: string, jwt?: string): AxiosRequestConfig | undefined {
        if (!jwt) return undefined;
        const config: AxiosRequestConfig = {
            params:{culture: culture},
            headers: {
                Authorization: 'Bearer ' + jwt
            }
        };  
        return config;     
    }

    static async getAll<TEntity>(apiEndpoint: string, culture:string, jwt?: string): Promise<IFetchResponse<TEntity[]>> {
        try {
            let response = await this.axios.get<TEntity[]>(apiEndpoint, BaseService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as TEntity[]
            };    
        }
        catch (err) {
            let error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 500,
                messages: (error.response?.data as IMessages).messages,
            }
        }
    }

    static async get<TEntity>(apiEndpoint: string, id: string, culture:string, jwt?: string): Promise<IFetchResponse<TEntity>> {
        try {
            let response = await this.axios.get<TEntity>(apiEndpoint + "/" + id, BaseService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as TEntity
            };    
        }
        catch (err) {
            let error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 500,
                messages: (error.response?.data as IMessages).messages,
            }
        }
    }

    static async update<TEntity>(apiEndpoint: string, id: string, culture:string, queryParams?: IQueryParams, jwt?: string): Promise<IFetchResponse<TEntity>> {
        try {
            const body = queryParams;
            const bodyStr = JSON.stringify(body);
            let response = await this.axios.put<TEntity>(apiEndpoint + "/" + id, bodyStr, BaseService.getAxiosConfiguration(culture, jwt));

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as TEntity
            };    
        }
        catch (err) {
            let error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 500,
                messages: (error.response?.data as IMessages).messages,
            }
        }
    }

    static async create<TEntity>(apiEndpoint: string, culture:string, queryParams?: IQueryParams, jwt?: string): Promise<IFetchResponse<TEntity>> {
        try {
            const body = queryParams;
            const bodyStr = JSON.stringify(body);
            let response = await this.axios.post<TEntity>(apiEndpoint + "/", bodyStr, BaseService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as TEntity
            };    
        }
        catch (err) {
            let error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 500,
                messages: (error.response?.data as IMessages).messages,
            }
        }
    }

    static async delete<TEntity>(apiEndpoint: string, id: string, culture:string, jwt?: string): Promise<IFetchResponse<TEntity>> {
        try {

            let response = await this.axios.delete<TEntity>(apiEndpoint + "/" + id, BaseService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as TEntity
            };    
        }
        catch (err) {
            let error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 500,
                messages: (error.response?.data as IMessages).messages,
            }
        }
    }

}