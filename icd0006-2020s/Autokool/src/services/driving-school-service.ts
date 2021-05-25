import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IDrivingSchool } from '../dto/IDrivingSchool';
import { IStudent } from '../dto/IStudent';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessages } from '../types/IMessages';


export abstract class DrivingSchoolService {
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


    
    static async InviteUser(inviteData: {username: string, schoolId: string}, culture: string, jwt: string): Promise<IFetchResponse<IStudent[]>> {
        let inviteDataJson = JSON.stringify(inviteData);

        try {
            let response = await this.axios.post<IStudent[]>(
                "DrivingSchools/InviteStudent", 
                inviteDataJson,
                DrivingSchoolService.getAxiosConfiguration(culture, jwt)
            );
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IStudent[]
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

    static async GetStudents(schoolId: string, fullName: string, username: string, culture: string, jwt: string): Promise<IFetchResponse<IStudent[]>> {
        try {
            let response = await this.axios.get<IStudent[]>(
                "DrivingSchools/Students/" + schoolId + "/" + fullName + "/" + username, 
                DrivingSchoolService.getAxiosConfiguration(culture, jwt)
            );
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IStudent[]
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

    static async GetContractDrivingSchool(contractId: string, culture: string, jwt: string): Promise<IFetchResponse<IDrivingSchool>> {
        try {
            let response = await this.axios.get<IDrivingSchool>(
                "DrivingSchools/DrivingSchoolByContract/" + contractId, 
                DrivingSchoolService.getAxiosConfiguration(culture, jwt)
            );
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IDrivingSchool
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