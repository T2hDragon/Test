import { useParams } from "react-router-dom";
import { IRouteId } from "../../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Link } from "react-router-dom";
import { IDrivingLesson } from "../../../dto/IDrivingLesson";
import { IStudentCourseReport } from "../../../dto/IStudentCourseReport";
import { IRequirementProgress } from "../../../dto/IRequirementProgress";
import { IDrivingRequirementProgress } from "../../../dto/IDrivingRequirementProgress";
import Loader from "../../../components/Loader";
import { DrivingLessonService } from "../../../services/driving-lessons-service";
import { EPageStatus } from "../../../types/EPageStatus";
import { AppContext } from "../../../context/AppContext";
import { StudentService } from "../../../services/student-service";

const initialStudentCourseReportValues: IStudentCourseReport = {
    id: '',
    courseName: '',
    drivingRequirementProgress: null,
    checkmarkProgress: [] as IRequirementProgress[]
}
const initialNextLessonValue: any = null;

const CourseSchedule = () => {
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.student.course.courseSchedule;
    const { id } = useParams() as IRouteId;
    const [drivingLessons, setDrivingLessons] = useState([] as IDrivingLesson[]);
    const [drivingLessonProgress, setdrivingLessonProgress] = useState(0);
    const [report, setReport] = useState(initialStudentCourseReportValues);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const [nextLesson, setNextLesson] = useState(initialNextLessonValue)
    const LoadReportData = async () => {
        let result = await StudentService.getReport(id, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setReport(result.data);
            const progress = result.data.drivingRequirementProgress as IDrivingRequirementProgress | null;
            if (progress != null) {
                var progressPrecent = ((progress.completed / progress.needed) * 100);
                if (progressPrecent > 100) {
                    progressPrecent = 100;
                }
                setdrivingLessonProgress(progressPrecent);
            }
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    const LoadDrivingScheduleData = async () => {
        let result = await DrivingLessonService.getAllByContractCourse(id, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setDrivingLessons(result.data);
            for (var i = 0; i < result.data.length; i++) {
                if (result.data[i].start >= new Date()) {
                    setNextLesson(result.data[i]);
                    break;

                }
            }
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        LoadReportData(); // eslint-disable-next-line react-hooks/exhaustive-deps
        LoadDrivingScheduleData(); // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <h1>{report.courseName}</h1>
            <div>
                <Link
                    to={"/student/course/" + report.id}
                >{resource.backToOverview}</Link>
            </div >
            <Loader {...pageStatus} />
            <br /><br />
            {report.drivingRequirementProgress === null ? null :
                <div className="table-entry">
                    <h3 className="text-center">{resource.drivingLessons}</h3>
                    <br /><br />
                    <div className='progress'>
                        <div className='progress-bar bg-success'
                            role='progressbar'
                            aria-valuenow={Number(drivingLessonProgress)}
                            aria-valuemin={0}
                            aria-valuemax={100}
                            style={{ width: drivingLessonProgress + '%' }}>
                            <span>{drivingLessonProgress.toFixed(2)}% {resource.complete}</span>
                        </div>
                    </div>
                    <br></br>
                    <div>
                        {resource.progress}: {report.drivingRequirementProgress.completed}/{report.drivingRequirementProgress.needed}
                    </div>
                    {(nextLesson === null) ? null :
                        <div>
                            <div>
                                Next Lesson: {
                                    nextLesson.start.getFullYear() + " " +
                                    nextLesson.start.toLocaleString('default', { month: 'long' }) + " " +
                                    nextLesson.start.getDate() + " " +
                                    ("0" + nextLesson.start.getHours()).slice(-2) +
                                    ":" +
                                    ("0" + nextLesson.start.getMinutes()).slice(-2) +
                                    "-" +
                                    ("0" + nextLesson.end.getHours()).slice(-2) +
                                    ":" +
                                    ("0" + nextLesson.end.getMinutes()).slice(-2)
                                }
                            </div>
                            <div>{nextLesson.teachers}</div>
                        </div>
                    }
                </div>
            }
            <br /><br />
            <table className="table table-hover">
                <thead>
                    <tr className="row">
                        <th className="col">{resource.year}</th>
                        <th className="col">{resource.month}</th>
                        <th className="col">{resource.day}</th>
                        <th className="col">{resource.time}</th>
                        <th className="col">{resource.teacher}</th>
                    </tr>
                </thead>
                <tbody>
                    {drivingLessons.map(drivingLesson =>
                        <tr className="row" key={drivingLesson.id}>
                            <td className="col">{drivingLesson.start.getFullYear()}</td>
                            <td className="col">{drivingLesson.start.toLocaleString(appState.currentLanguage.name, { month: 'long' })}</td>
                            <td className="col">{drivingLesson.start.getDate()}</td>
                            <td className="col">
                                {
                                    ("0" + drivingLesson.start.getHours()).slice(-2) +
                                    ":" +
                                    ("0" + drivingLesson.start.getMinutes()).slice(-2) +
                                    "-" +
                                    ("0" + drivingLesson.end.getHours()).slice(-2) +
                                    ":" +
                                    ("0" + drivingLesson.end.getMinutes()).slice(-2)
                                }
                            </td>
                            <td className="col">{drivingLesson.teachers}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </>
    );
}

export { CourseSchedule as Schedule };