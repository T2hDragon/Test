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
import Alert, { EAlertClass } from "../../../components/Alert";
import { IContract } from "../../../dto/IContract";
import { ContractService } from "../../../services/contract-service";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker, { registerLocale } from "react-datepicker";
import et from "date-fns/locale/et"; // the locale you want
registerLocale("et", et);

const initialStudentCourseReportValues: IStudentCourseReport = {
    id: '',
    courseName: '',
    drivingRequirementProgress: null,
    checkmarkProgress: [] as IRequirementProgress[]
}

const initialStudentValues: IContract = {
    contractId: '',
    name: '',
    drivingSchoolId: '',
    drivingSchoolName: '',
    title: '',
    status: ''
}

const initialInviteValue: IInvite = {
    lessonDate: new Date(),
    length: 2
}

interface IInvite {
    lessonDate: Date,
    length: number
}
const date = new Date()

const initialNextLessonValue: any = null;


const TeacherStudentCourseSchedule = () => {
    const appState = useContext(AppContext);
    const { teacherId, studentId, studentCourseId } = useParams() as IRouteId;
    const [studentDrivingLessons, setStudentDrivingLessons] = useState([] as IDrivingLesson[]);
    const [studentDrivingLessonProgress, setStudentDrivingLessonProgress] = useState(0);
    const [report, setReport] = useState(initialStudentCourseReportValues);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const [nextLesson, setNextLesson] = useState(initialNextLessonValue)
    const [alertMessage, setAlertMessage] = useState('');
    const [alertLessonAddedMessage, setLessonAddedAlertMessage] = useState('');
    const [dangerAlertMessage, setDangerAlertMessage] = useState('');
    const [teacherScheduleRange, setTeacherScheduleRange] = useState({ start: new Date(date.getFullYear(), date.getMonth(), 1), end: new Date(date.getFullYear(), date.getMonth() + 1, 0) });
    const [teacherDrivingLessons, setTeacherDrivingLessons] = useState([] as IDrivingLesson[]);
    const [student, setStudent] = useState(initialStudentValues);
    const [newLesson, setInvite] = useState(initialInviteValue);
    const resource = appState.langResources.frontEnd.containers.teacher.students.teacherStudentCourseSchedule;



    const changeTeacherScheduleRange = async (e: Event, change: number) => {
        e.preventDefault();
        setTeacherScheduleRange(
            {
                start: new Date(teacherScheduleRange.start.setMonth(teacherScheduleRange.start.getMonth() + change)),
                end: new Date(teacherScheduleRange.end.setMonth(teacherScheduleRange.end.getMonth() + change))
            })
        LoadTeacherDrivingLessons();
    }

    const removeTeacherLesson = async (e: Event, lessonId: string) => {
        e.preventDefault();
        let result = await DrivingLessonService.delete(lessonId, appState.currentLanguage.name, appState.jwt!)
        if (result.ok && result.data) {
            setAlertMessage(resource.lessonRemoved);
            LoadTeacherDrivingLessons();
            LoadStudentReportData();
            LoadStudentDrivingScheduleData();
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    const addLesson = async (e: Event) => {
        e.preventDefault();
        let response = await DrivingLessonService.create(
            {
                teacherId: teacherId,
                studentCourseId: studentCourseId,
                start: newLesson.lessonDate,
                length: newLesson.length
            }, appState.currentLanguage.name, appState.jwt!)
        if (response.ok && response.data) {
            setDangerAlertMessage('');
            setLessonAddedAlertMessage(resource.lessonAdded);
            LoadTeacherDrivingLessons();
            LoadStudentReportData();
            LoadStudentDrivingScheduleData();
        } else {
            setDangerAlertMessage(response.messages![0]);
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


    const LoadTeacherDrivingLessons = async () => {
        if (appState.jwt === null) return;
        let result = await DrivingLessonService.getTimeFramedByContract(teacherId, teacherScheduleRange.start, teacherScheduleRange.end, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setTeacherDrivingLessons(result.data!);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }


    const LoadStudentReportData = async () => {
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
                setStudentDrivingLessonProgress(progressPrecent);
            }
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    const LoadStudentDrivingScheduleData = async () => {
        let result = await DrivingLessonService.getAllByContractCourse(studentCourseId, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStudentDrivingLessons(result.data);
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
        LoadTeacherDrivingLessons(); // eslint-disable-next-line react-hooks/exhaustive-deps
        LoadStudentInfo(); // eslint-disable-next-line react-hooks/exhaustive-deps
        LoadStudentReportData(); // eslint-disable-next-line react-hooks/exhaustive-deps
        LoadStudentDrivingScheduleData(); // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <>
            <h1>{report.courseName}</h1>
            <h3>{student.name}</h3>
            <div>
                <Link
                    to={"/teacher/" + teacherId + "/student/" + studentId + "/course/" + studentCourseId}
                >{resource.backToStudentCourse}</Link>
            </div >
            <Loader {...pageStatus} />
            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Success} />
            <br /><br />
            {report.drivingRequirementProgress === null ? null :
                <div className="table-entry">
                    <h3 className="text-center">{resource.drivingLessons}</h3>
                    <br /><br />
                    <div className='progress'>
                        <div className='progress-bar bg-success'
                            role='progressbar'
                            aria-valuenow={Number(studentDrivingLessonProgress)}
                            aria-valuemin={0}
                            aria-valuemax={100}
                            style={{ width: studentDrivingLessonProgress + '%' }}>
                            <span>{studentDrivingLessonProgress.toFixed(2)}% {resource.complete}</span>
                        </div>
                    </div>
                    <br></br>
                    <div>
                        {resource.progress}: {report.drivingRequirementProgress.completed}/{report.drivingRequirementProgress.needed}
                    </div>
                    {(nextLesson === null) ? null :
                        <div>
                            <div>
                                {resource.nextLesson}: {
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
            <h2>{resource.teacherLessons}</h2>
            <br /><br />
            <div className="table-entry">
                <div className="text-center h4">
                    <i className="fa fa-arrow-left cursor-pointer" onClick={(e) => changeTeacherScheduleRange(e.nativeEvent, -1)}></i>
                    <span>
                        {" " + teacherScheduleRange.start.getFullYear() + " " + teacherScheduleRange.start.toLocaleString(appState.currentLanguage.name, { month: 'long' }) + " "}
                    </span>
                    <i className="fa fa-arrow-right cursor-pointer" onClick={(e) => changeTeacherScheduleRange(e.nativeEvent, +1)}></i>
                </div>
                <br></br>
                <div>
                    <table className="table text-center">
                        <thead>
                            <tr>
                                <th>{resource.time}</th>
                                <th>{resource.student}</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>

                            {teacherDrivingLessons.map(drivingLesson =>
                                <tr key={drivingLesson.id}>
                                    <td>
                                        {
                                            ("0" + drivingLesson.start.getDate()).slice(-2) +
                                            " " +
                                            ("0" + drivingLesson.start.getHours()).slice(-2) +
                                            ":" +
                                            ("0" + drivingLesson.start.getMinutes()).slice(-2) +
                                            "-" +
                                            ("0" + drivingLesson.end.getHours()).slice(-2) +
                                            ":" +
                                            ("0" + drivingLesson.end.getMinutes()).slice(-2)
                                        }
                                    </td>
                                    <td>
                                        {drivingLesson.students === "" ? "Removed Student" : drivingLesson.students}
                                    </td>
                                    <td>
                                        <span className="btn btn-danger" onClick={(e) => removeTeacherLesson(e.nativeEvent, drivingLesson.id)}>{resource.remove}</span>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
            <br />
            <h4>{resource.addLesson}</h4>
            <br />
            <div className="container">
                <div className="row">
                    <div className="col-auto">
                        <DatePicker
                            showTimeSelect
                            dateFormat="MMMM d, yyyy hh:mm"
                            timeIntervals={15}
                            selected={newLesson.lessonDate}
                            locale={appState.currentLanguage.name}
                            onChange={(date) => {
                                setInvite({ lessonDate: new Date(date!.toString()), length: newLesson.length });
                            }}
                            inline

                        />
                    </div>
                    <div className="col-3">
                        <Alert show={dangerAlertMessage !== ''} message={dangerAlertMessage} alertClass={EAlertClass.Danger} />
                        <Alert show={alertLessonAddedMessage !== ''} message={alertLessonAddedMessage} alertClass={EAlertClass.Success} />
                        <label htmlFor="lessonLength">{resource.lessonLength}</label>
                        <br></br>
                        <input className="col-6" id="lessonLength" type="number" onChange={(e) => {
                            if (Number(e.target.value) <= 0) {
                                setDangerAlertMessage(resource.lessonLengthError + e.target.value + "!");
                            } else {
                                setDangerAlertMessage('');
                                setInvite({ lessonDate: newLesson.lessonDate, length: Number(e.target.value) })
                            }
                        }} value={newLesson.length}></input>
                    </div>
                </div>
            </div>
            <br />
            <div className="btn btn-primary" onClick={(e) => addLesson(e.nativeEvent)}>{resource.add}</div>
            <br /><br /><br /><br />
            <h2>{resource.studentLessons}</h2>
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
                    {studentDrivingLessons.map(drivingLesson =>
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

export { TeacherStudentCourseSchedule };