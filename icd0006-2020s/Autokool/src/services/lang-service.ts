import { ISupportedLanguage } from './../dto/ISupportedLanguage';
import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IFetchResponse } from '../types/IFetchResponse';
import { ILangResources } from '../dto/ILangResources';
import { IMessages } from '../types/IMessages';

export abstract class LangService {
    protected static axios = Axios.create({
        baseURL: ApiBaseUrl,
        headers: {
            'Content-Type': 'application/json'
        }
    });

    protected static getAxiosConfiguration(jwt?: string): AxiosRequestConfig | undefined {
        if (!jwt) return undefined;
        const config: AxiosRequestConfig = {
            headers: {
                Authorization: 'Bearer ' + jwt
            }
        };  
        return config;      
    }


    static async getSupportedLanguages(): Promise<IFetchResponse<ISupportedLanguage[]>> {
        try {
            let response = await this.axios.get<ISupportedLanguage[]>("/Lang/GetSupportedLanguages");
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as ISupportedLanguage[]
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

    static async getLangResources(language: string): Promise<IFetchResponse<ILangResources>> {
        try {
            let response = await this.axios.get<ILangResources>("/Lang/GetLangResources?culture=" + language);
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as ILangResources
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