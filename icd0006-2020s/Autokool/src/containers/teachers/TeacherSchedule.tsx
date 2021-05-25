import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { IDrivingLesson } from "../../dto/IDrivingLesson";
import Loader from "../../components/Loader";
import { DrivingLessonService } from "../../services/driving-lessons-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const date = new Date()

const TeacherSchedule = () => {
    const { teacherId } = useParams() as IRouteId;
    const [drivingLessons, setDrivingLessons] = useState([] as IDrivingLesson[]);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const [alertMessage, setAlertMessage] = useState('');
    const [range, setRange] = useState({ start: new Date(date.getFullYear(), date.getMonth(), 1), end: new Date(date.getFullYear(), date.getMonth() + 1, 0) });
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.teacher.teacherSchedule;


    const changeRange = async (e: Event, change: number) => {
        e.preventDefault();
        setRange(
            {
                start: new Date(range.start.setMonth(range.start.getMonth() + change)),
                end: new Date(range.end.setMonth(range.end.getMonth() + change))
            })
        LoadDrivingLessons();
    }

    const removeLesson = async (e: Event, lessonId: string) => {
        e.preventDefault();
        let result = await DrivingLessonService.delete(lessonId, appState.currentLanguage.name, appState.jwt!)
        if (result.ok && result.data) {
            setAlertMessage(resource.lessonRemoved);
            LoadDrivingLessons();
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    const LoadDrivingLessons = async () => {
        if (appState.jwt === null) return;
        let result = await DrivingLessonService.getTimeFramedByContract(teacherId, range.start, range.end, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setDrivingLessons(result.data!);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        LoadDrivingLessons();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1 className="text-center">{resource.lessons}</h1>
            <Loader {...pageStatus} />
            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Success} />
            <div className="text-center">
                <i className="fa fa-arrow-left cursor-pointer" onClick={(e) => changeRange(e.nativeEvent, -1)}></i>
                <span>
                    {" " + range.start.getFullYear() + " " + range.start.toLocaleString(appState.currentLanguage.name, { month: 'long' }) + " "}
                </span>
                <i className="fa fa-arrow-right cursor-pointer" onClick={(e) => changeRange(e.nativeEvent, +1)}></i>
            </div>
            <br /><br />
            <div className="table-entry">
                <div>
                    <table className="table text-center">
                        <thead>
                            <th>{resource.time}</th>
                            <th>{resource.student}</th>
                            <th></th>
                        </thead>
                        <tbody>

                            {drivingLessons.map(drivingLesson =>
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
                                        <span className="btn btn-danger" onClick={(e) => removeLesson(e.nativeEvent, drivingLesson.id)}>{resource.remove}</span>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </>
    );
}


export { TeacherSchedule };