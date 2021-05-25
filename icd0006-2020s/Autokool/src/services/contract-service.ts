import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IContract } from '../dto/IContract';
import { ICourse } from '../dto/ICourse';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessages } from '../types/IMessages';


export abstract class ContractService {
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

    static async getAll(culture: string, jwt: string): Promise<IFetchResponse<IContract[]>> {
        try {
            let response = await this.axios.get<IContract[]>("Contracts/GetAll"  , ContractService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IContract[]
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

    static async get(id: string, culture: string, jwt: string): Promise<IFetchResponse<IContract>> {
        try {
            let response = await this.axios.get<IContract>("Contracts/Get/" + id, ContractService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IContract
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


    static async getMissing(contractrId: string, culture: string, jwt: string): Promise<IFetchResponse<ICourse[]>> {
        try {
            let response = await this.axios.get<ICourse[]>("Contracts/MissingCourses/" + contractrId, ContractService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as ICourse[]
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

    static async InviteHandle( id: string, accept: boolean, culture: string, jwt: string): Promise<IFetchResponse<IContract>> {
        const url = accept? "Contracts/AcceptInvite/" :"Contracts/DeclineInvite/";
        try {
            let response = await this.axios.put<IContract>(url + id, '', ContractService.getAxiosConfiguration(culture, jwt));

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data
            };    
        }
        catch (err) {
            let error = err as AxiosError;
            return {
                ok: false,
                statusCode: error.response?.status ?? 500,
                messages: [error.response?.statusText!],
            }
        }
    }

    static async delete(id: string, culture: string, jwt: string): Promise<IFetchResponse<IContract>> {
        try {
            let response = await this.axios.delete<IContract>("Contracts/Delete/" + id, ContractService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data
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