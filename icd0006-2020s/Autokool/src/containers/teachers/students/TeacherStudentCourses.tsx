import { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import Loader from "../../../components/Loader";
import { IStudentCourse } from "../../../dto/IStudentCourse";
import { EPageStatus } from "../../../types/EPageStatus";
import { AppContext } from "../../../context/AppContext";
import { IRouteId } from "../../../types/IRouteId";
import { useHistory } from "react-router-dom";
import { StudentService } from "../../../services/student-service";
import { ICourse } from "../../../dto/ICourse";
import { ContractService } from "../../../services/contract-service";
import { IContract } from "../../../dto/IContract";

const initialStudentValues: IContract = {
    contractId: '',
    name: '',
    drivingSchoolId: '',
    drivingSchoolName: '',
    title: '',
    status: ''
}


const TeacherStudentCourses = () => {
    const { teacherId, studentId } = useParams() as IRouteId;
    const [studentCourses, setStudentCourses] = useState([] as IStudentCourse[]);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const [missingCourses, setMissingCourses] = useState([] as ICourse[]);
    const [student, setStudent] = useState(initialStudentValues);
    const [selectedMissingCourse, setSelectedMissingCourse] = useState('');
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.teacher.students.teacherStudentCourses;
    const history = useHistory();
    const CourseClicked = async (e: Event, courseId: string) => {
        const pushUrl = "/teacher/" + teacherId + "/student/" + studentId + "/course/" + courseId;
        history.push(pushUrl);
    }
    const kickStudentClicked = async (e: Event, kickStudentId: string) => {
        e.preventDefault();

        let studentsResult = await ContractService.delete(kickStudentId, appState.currentLanguage.name, appState.jwt!);
        if (studentsResult.ok && studentsResult.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            const pushUrl = "/teacher/" + teacherId + "/students";
            history.push(pushUrl);

        }
    }

    const addCourseClicked = async (e: Event, courseId: string) => {
        e.preventDefault();
        var studentCourse: IStudentCourse = {
            id: '',
            name: '',
            description: '',
            category: '',
            contractId: studentId,
            courseId: courseId
        }
        let studentsResult = await StudentService.AddCourse(
            studentCourse
            , appState.currentLanguage.name, appState.jwt!);
        if (studentsResult.ok && studentsResult.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            LoadContractCourses();
            LoadMissingCourses();
        };
    };



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
    const LoadContractCourses = async () => {
        if (appState.jwt === null) return;
        let result = await StudentService.getAll(studentId, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStudentCourses(result.data!);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }
    const LoadMissingCourses = async () => {
        if (appState.jwt === null) return;
        let result = await ContractService.getMissing(studentId, appState.currentLanguage.name, appState.jwt!);
        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setMissingCourses(result.data!);
            if (result.data!.length > 0) {
                setSelectedMissingCourse(result.data[0].id)
            }
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        LoadContractCourses();
        LoadMissingCourses();
        LoadStudentInfo();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1 className="text-center">{student.name} {resource.courses}</h1>
            <Loader {...pageStatus} />
            <br /><br />
            {missingCourses.length === 0 ? null :
                <div className="row justify-content">
                    <div className="col-10"></div>
                    <form className="col-2" onSubmit={(e) => e.preventDefault}>
                        <section>
                            <hr />
                            <div className="form-group text-center">
                                <label htmlFor="course-select">{resource.courses}</label>
                                <select onChange={(e) => setSelectedMissingCourse(e.target.value)} className="form-control" id="course-select">
                                    {missingCourses.map(course =>
                                        <option key={course.id} value={course.id}>{course.name}</option>
                                    )}
                                </select>

                            </div>
                            <div className="form-group text-center">
                                <button onClick={(e) => addCourseClicked(e.nativeEvent, selectedMissingCourse)} type="submit" className="btn btn-primary">{resource.addCourse}</button>
                            </div>
                        </section>
                    </form>
                </div>
            }

            {studentCourses.map(studentCourse =>
                <div key={studentCourse.id} onClick={(e) => CourseClicked(e.nativeEvent, studentCourse.id)} className="table-entry btn" >
                    <h3>{studentCourse.name}</h3>
                    <h5>{studentCourse.description}</h5>
                    <h5>{resource.category}: {studentCourse.category}</h5>
                </div>
            )}
            <form className="float-right" onSubmit={(e) => e.preventDefault}>
                <section>
                    <hr />
                    <div className="form-group">
                        <button onClick={(e) => kickStudentClicked(e.nativeEvent, studentId)} type="submit" className="btn float-right btn-danger">{resource.kick}</button>
                    </div>
                </section>
            </form>
        </>
    );
}

export { TeacherStudentCourses };