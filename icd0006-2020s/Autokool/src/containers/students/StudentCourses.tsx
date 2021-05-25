import { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import Loader from "../../components/Loader";
import { IStudentCourse } from "../../dto/IStudentCourse";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";
import { IRouteId } from "../../types/IRouteId";
import { useHistory } from "react-router-dom";
import { StudentService } from "../../services/student-service";



const StudentCourses = () => {
    const { id } = useParams() as IRouteId;
    const [studentCourses, setStudentCourses] = useState([] as IStudentCourse[]);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.student.studentCourses;
    const history = useHistory();

    const CourseClicked = async (e: Event, courseId: string) => {
        const pushUrl = "/student/course/";
        history.push(pushUrl + courseId);
    }

    const LoadContractCourses = async () => {
        if (appState.jwt === null) return;
        let result = await StudentService.getAll(id, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStudentCourses(result.data!);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        LoadContractCourses();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1 className="text-center">{resource.courses}</h1>
            <Loader {...pageStatus} />
            <br /><br />
            {studentCourses.map(studentCourse =>
                <div key={studentCourse.id} className="table-entry btn" onClick={(e) => CourseClicked(e.nativeEvent, studentCourse.id)} >
                    <h3>{studentCourse.name}</h3>
                    <h5>{studentCourse.description}</h5>
                    <h5>{resource.category}: {studentCourse.category}</h5>
                </div>
            )}
        </>
    );
}

export { StudentCourses as Courses };