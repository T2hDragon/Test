import { IDrivingRequirementProgress } from "./IDrivingRequirementProgress";
import { IRequirementProgress } from "./IRequirementProgress";

export interface IStudentCourseReport {
    id: string;
    courseName: string;
    drivingRequirementProgress: IDrivingRequirementProgress | null;
    checkmarkProgress: IRequirementProgress[];
}