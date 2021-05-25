import { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import Loader from "../../components/Loader";
import { IDrivingLesson } from "../../dto/IDrivingLesson";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";
import { IRouteId } from "../../types/IRouteId";
import { DrivingLessonService } from "../../services/driving-lessons-service";

const currentDate = new Date()
const initialDrivingLessonValues: IDrivingLesson = {
    id: '',
    teachers: '',
    students: '',
    courseName: '',
    start: currentDate,
    end: currentDate
}


const TeacherOverview = () => {
    const { teacherId } = useParams() as IRouteId;
    const [drivingLessons, setDrivingLessons] = useState([] as IDrivingLesson[]);
    const [nextDrivingLesson, setNextDrivingLesson] = useState(initialDrivingLessonValues);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.teacher.teacherOverview;

    const LoadContractCourses = async () => {
        if (appState.jwt === null) return;
        const currentDate = new Date();
        const currentDayEnd = new Date();
        currentDayEnd.setHours(23, 59, 59, 999)
        let result = await DrivingLessonService.getTimeFramedByContract(teacherId, currentDate, currentDayEnd, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setDrivingLessons(result.data!);
            if (result.data.length > 0) setNextDrivingLesson(result.data![0]);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        LoadContractCourses();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1 className="text-center">{resource.course}</h1>
            <Loader {...pageStatus} />
            <br /><br />
            <div className="table-entry">
                <div>
                    <h2 className="text-center">{resource.nextLesson}</h2>
                    <table >
                        <thead>
                            <tr></tr>
                            <tr></tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{resource.time}</td>
                                <td>
                                    {(drivingLessons.length === 0) ? "" :
                                        ("0" + nextDrivingLesson.start.getHours()).slice(-2) +
                                        ":" +
                                        ("0" + nextDrivingLesson.start.getMinutes()).slice(-2) +
                                        "-" +
                                        ("0" + nextDrivingLesson.end.getHours()).slice(-2) +
                                        ":" +
                                        ("0" + nextDrivingLesson.end.getMinutes()).slice(-2)
                                    }
                                </td>
                            </tr>
                            <tr>
                                <td>{resource.student}</td>
                                <td>{nextDrivingLesson.students}</td>
                            </tr>
                            <tr>
                                <td>{resource.course}</td>
                                <td>{nextDrivingLesson.courseName}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div>
                    <h4 className="text-center">{resource.restOfTheDay}</h4>
                    {drivingLessons.map(drivingLesson =>
                        <table key={drivingLesson.id} className="inline-table-overview-students table-entry">
                            <thead>
                                <tr></tr>
                                <tr></tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>{resource.time}</td>
                                    <td>
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
                                </tr>
                                <tr>
                                    <td>{resource.student}</td>
                                    <td>{drivingLesson.students}</td>
                                </tr>
                                <tr>
                                    <td>{resource.course}</td>
                                    <td>{drivingLesson.courseName}</td>
                                </tr>
                            </tbody>
                        </table>
                    )}
                </div>
            </div>
        </>
    );
}

export { TeacherOverview };