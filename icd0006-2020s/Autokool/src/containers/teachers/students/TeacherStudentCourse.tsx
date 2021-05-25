import { useEffect, useState, useContext } from "react";
import Loader from "../../../components/Loader";
import { Link, useHistory, useParams } from "react-router-dom";
import { IStudentCourseReport } from "../../../dto/IStudentCourseReport";
import { IDrivingRequirementProgress } from "../../../dto/IDrivingRequirementProgress";
import { IRequirementProgress } from "../../../dto/IRequirementProgress";
import { StudentService } from "../../../services/student-service";
import { EPageStatus } from "../../../types/EPageStatus";
import { AppContext } from "../../../context/AppContext";
import { IRouteId } from "../../../types/IRouteId";
import { IContract } from "../../../dto/IContract";
import { ContractService } from "../../../services/contract-service";
import Alert, { EAlertClass } from "../../../components/Alert";

const initialStudentValues: IContract = {
    contractId: '',
    name: '',
    drivingSchoolId: '',
    drivingSchoolName: '',
    title: '',
    status: ''
}


const initialStudentCourseReportValues: IStudentCourseReport = {
    id: '',
    courseName: '',
    drivingRequirementProgress: null,
    checkmarkProgress: [] as IRequirementProgress[]
}

const TeacherStudentCourse = () => {
    const { teacherId, studentId, studentCourseId } = useParams() as IRouteId;
    const appState = useContext(AppContext);
    const [drivingLessonProgress, setdrivingLessonProgress] = useState(0);
    const [report, setReport] = useState(initialStudentCourseReportValues);
    const [student, setStudent] = useState(initialStudentValues);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const [alertMessage, setAlertMessage] = useState('');
    const history = useHistory();
    const resource = appState.langResources.frontEnd.containers.teacher.students.teacherStudentCourse;

    const updateCourseReport = async (e: Event) => {
        e.preventDefault();
        let result = await StudentService.updateReport(report, appState.currentLanguage.name, appState.jwt!)
        if (result.ok && result.data) {
            setAlertMessage(resource.reportUpdated);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    const deleteContractCourse = async (e: Event) => {
        e.preventDefault();
        let result = await StudentService.deleteStudentContract(studentCourseId, appState.currentLanguage.name, appState.jwt!)
        if (result.ok && result.data) {
            history.push("/teacher/" + teacherId + "/student/" + studentId)
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    const LoadStudentInfo = async () => {
        if (appState.jwt === null) return;
        let result = await ContractService.get(studentId, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStudent(result.data!);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }
    const LoadReport = async () => {
        let result = await StudentService.getReport(studentCourseId, appState.currentLanguage.name, appState.jwt!);
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
        LoadReport();
        LoadStudentInfo();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1>{report.courseName}</h1>
            <h2>{student.name}</h2>
            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Success} />
            <Loader {...pageStatus} />
            <br /><br />
            {report.drivingRequirementProgress === null ? null :
                <Link to={{ pathname: "/teacher/" + teacherId + "/student/" + studentId + "/course/" + studentCourseId + "/schedule" }} className="table-entry btn">
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
                        <tr key={checkmark.id} >
                            <td>
                                <label className="container" htmlFor={"checkbox-" + checkmark.id}>
                                    {checkmark.requirementName}
                                </label>

                            </td>
                            <td>
                                <input checked={checkmark.isCompleted} onChange={
                                    (e) => {
                                        let reportResult: IStudentCourseReport = JSON.parse(JSON.stringify(report));
                                        for (let i = 0; i < reportResult.checkmarkProgress.length; i++) {
                                            const reportCheckmark = reportResult.checkmarkProgress[i];
                                            if (reportCheckmark.id === checkmark.id) {
                                                reportResult.checkmarkProgress[i].isCompleted = (e.target as HTMLInputElement).checked;
                                                setReport(reportResult);
                                                return;
                                            }
                                        };
                                    }} type="checkbox" className="checkmark-input" id={"checkbox-" + checkmark.id} />
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
            <div className="container">
                <div className="btn btn-primary col-2" onClick={(e) => updateCourseReport(e.nativeEvent)}>{resource.update}</div>
                <div className="btn btn-danger col-2 float-right" onClick={(e) => deleteContractCourse(e.nativeEvent)}>{resource.delete}</div>
            </div>
        </>
    );
}
export { TeacherStudentCourse };