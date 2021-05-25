import Axios, { AxiosError, AxiosRequestConfig } from 'axios';
import { ApiBaseUrl } from '../configuration';
import { IDrivingLesson } from '../dto/IDrivingLesson';
import { IFetchResponse } from '../types/IFetchResponse';
import { IMessages } from '../types/IMessages';


export abstract class DrivingLessonService {
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


    static async delete(lessonId: string, culture: string, jwt: string): Promise<IFetchResponse<IDrivingLesson>> {
        try {
            let response = await this.axios.delete<IDrivingLesson>("DrivingLessons/Delete/" + lessonId, DrivingLessonService.getAxiosConfiguration(culture, jwt));
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

    static async create(lesson: {teacherId: string, studentCourseId: string, start: Date, length: number}, culture: string, jwt: string): Promise<IFetchResponse<IDrivingLesson>> {
        try {
            lesson.start = new Date(lesson.start.setHours(lesson.start.getHours() + 3));

            let bodyStr = JSON.stringify(lesson);
            let response = await this.axios.post<IDrivingLesson>("DrivingLessons/Create", bodyStr, DrivingLessonService.getAxiosConfiguration(culture, jwt));
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

    static async getTimeFramedByContract(contractId: string, start: Date, end: Date, culture: string, jwt: string): Promise<IFetchResponse<IDrivingLesson[]>> {
        const startStr = 
        ("0" + start.getDate()).slice(-2) + "-" + 
        ("0" + (start.getMonth() + 1)).slice(-2) + "-" + 
        start.getFullYear() + " " + 
        ("0" + (start.getHours())).slice(-2) + ':' + 
        ("0" + start.getMinutes()).slice(-2) + ':' + 
        ("0" + start.getSeconds()).slice(-2);
        const endStr = 
        ("0" + end.getDate()).slice(-2) + "-" + 
        ("0" + (end.getMonth() + 1)).slice(-2) + "-" + 
        start.getFullYear() + " " + 
        ("0" + end.getHours()).slice(-2) + ':' + 
        ("0" + end.getMinutes()).slice(-2) + ':' + 
        ("0" + end.getSeconds()).slice(-2);
        try {
            let response = await this.axios.get<IDrivingLesson[]>(
                "DrivingLessons/ContractDrivingLessons/" + contractId + "/" + startStr + "/" + endStr, 
            DrivingLessonService.getAxiosConfiguration(culture, jwt)
            );
            var result = [] as IDrivingLesson[];
            response.data!.forEach(drivingLessong => {
                const entry: IDrivingLesson = {
                    id: drivingLessong.id,
                    teachers: drivingLessong.teachers,
                    students: drivingLessong.students,
                    courseName: drivingLessong.courseName,
                    start: new Date(drivingLessong.start),
                    end: new Date(drivingLessong.end)
                }
                result.push(entry);
            });
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: result
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

    static async getAllByContractCourse(contractCoureId: string, culture: string, jwt: string): Promise<IFetchResponse<IDrivingLesson[]>> {
        try {
            let response = await this.axios.get("DrivingLessons/ContractCourseDrivingLessons/" + contractCoureId, DrivingLessonService.getAxiosConfiguration(culture, jwt));
            var tempData = response.data as { 
                id: string;
                teachers: string;
                students: string;
                courseName: string;
                start: string;
                end: string;}[]
            var result = [] as IDrivingLesson[]
            tempData.forEach(element => {
                const entry: IDrivingLesson = {
                    id: element.id,
                    teachers: element.teachers,
                    students: element.students,
                    courseName: element.courseName,
                    start: new Date(element.start),
                    end: new Date(element.end)
                }
                result.push(entry);
            });
            return {
                ok: response.status <= 299,
                statusCode: response.status,
                data: result
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