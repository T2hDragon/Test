import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IStudentCourse } from '../dto/IStudentCourse';
import { IStudentCourseReport } from '../dto/IStudentCourseReport';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessages } from '../types/IMessages';


export abstract class StudentService {
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

    static async getAll(contractId: string, culture: string, jwt: string): Promise<IFetchResponse<IStudentCourse[]>> {
        try {
            let response = await this.axios.get<IStudentCourse[]>("Students/Courses/" + contractId, StudentService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IStudentCourse[]
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

    static async AddCourse(studentCourse: IStudentCourse, culture: string, jwt: string): Promise<IFetchResponse<IStudentCourse>> {
        let bodyJson = JSON.stringify({
            name: studentCourse.name,
            description: studentCourse.description,
            category: studentCourse.category,
            courseId: studentCourse.courseId,
            contractId: studentCourse.contractId,

        });
        try {
            console.log(bodyJson);
            let response = await this.axios.post<IStudentCourse>("Students/Course", bodyJson, StudentService.getAxiosConfiguration(culture, jwt));
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

    static async getReport(studentCourseId: string, culture: string, jwt: string): Promise<IFetchResponse<IStudentCourseReport>> {
        try {
            
            let response = await this.axios.get<IStudentCourseReport>("Students/CourseReport/" + studentCourseId, StudentService.getAxiosConfiguration(culture, jwt));
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IStudentCourseReport
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
    static async updateReport(report: IStudentCourseReport, culture:string, jwt?: string): Promise<IFetchResponse<IStudentCourseReport>> {
        try {
            const bodyStr = JSON.stringify(report);
            let response = await this.axios.put<IStudentCourseReport>("Students/CourseReport", bodyStr, StudentService.getAxiosConfiguration(culture, jwt));

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IStudentCourseReport
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

    static async deleteStudentContract(studentContractCourseId: string, culture:string, jwt?: string): Promise<IFetchResponse<IStudentCourse>> {
        try {
            let response = await this.axios.delete<IStudentCourse>("Students/CourseDelete/" + studentContractCourseId, StudentService.getAxiosConfiguration(culture, jwt));

            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: response.data as IStudentCourse
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