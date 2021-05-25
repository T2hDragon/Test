import { useEffect, useState, useContext } from "react";
import Loader from "../../../components/Loader";
import { Link, useParams } from "react-router-dom";
import { IStudentCourseReport } from "../../../dto/IStudentCourseReport";
import { IDrivingRequirementProgress } from "../../../dto/IDrivingRequirementProgress";
import { IRequirementProgress } from "../../../dto/IRequirementProgress";
import { StudentService } from "../../../services/student-service";
import { EPageStatus } from "../../../types/EPageStatus";
import { AppContext } from "../../../context/AppContext";
import { IRouteId } from "../../../types/IRouteId";

const initialStudentCourseReportValues: IStudentCourseReport = {
    id: '',
    courseName: '',
    drivingRequirementProgress: null,
    checkmarkProgress: [] as IRequirementProgress[]
}

const CourseIndex = () => {
    const { id } = useParams() as IRouteId;
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.student.course.courseIndex;
    const [drivingLessonProgress, setdrivingLessonProgress] = useState(0);
    const [report, setReport] = useState(initialStudentCourseReportValues);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const LoadData = async () => {
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

    useEffect(() => {
        LoadData();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1>{report.courseName}</h1>
            <Loader {...pageStatus} />
            <br /><br />
            {report.drivingRequirementProgress === null ? null :
                <Link to={{ pathname: "/student/courseSchedule/" + report.id }} className="table-entry btn">
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
                </Link>
            }
            <br /><br /><br /><br />
            <table>
                <tbody>
                    {report.checkmarkProgress.map(checkmark =>
                        <tr key={checkmark.id}>
                            <td>{checkmark.requirementName}</td>
                            <td> < i className={"fa fa-" + ((checkmark.isCompleted) ? "check text-success" : "times text-danger")} ></i></td>
                        </tr>
                    )}
                </tbody>
            </table>

        </>
    );
}
export { CourseIndex as Index };