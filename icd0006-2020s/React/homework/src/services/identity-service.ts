import { ILoginResponse } from './../types/ILoginResponse';
import { IRegisterResponse } from './../types/IRegisterResponse';
import Axios, { AxiosError } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessages } from '../types/IMessages';

export abstract class IdentityService {
    protected static axios = Axios.create({
        baseURL: ApiBaseUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    });

    static async Login(apiEndpoint: string, loginData: {userName: string, password:string}): Promise<IFetchResponse<ILoginResponse>> {
        let loginDataJson = JSON.stringify(loginData);
        try {
            let response = await this.axios.post<ILoginResponse>(apiEndpoint, loginDataJson);
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

    static async Register(apiEndpoint: string, registerData: {email:string, firstName:string, lastName:string, userName: string, password:string}): Promise<IFetchResponse<IRegisterResponse>> {
        let registerDataJson = JSON.stringify(registerData);
        try {
            let response = await this.axios.post<IRegisterResponse>(apiEndpoint, registerDataJson);
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