import { useEffect, useState, useContext } from "react";
import { useHistory, useParams } from "react-router-dom";
import Loader from "../../../components/Loader";
import { IStudent } from "../../../dto/IStudent";
import { EPageStatus } from "../../../types/EPageStatus";
import { AppContext } from "../../../context/AppContext";
import { IRouteId } from "../../../types/IRouteId";
import { DrivingSchoolService } from "../../../services/driving-school-service";
import Alert, { EAlertClass } from "../../../components/Alert";
import { IDrivingSchool } from "../../../dto/IDrivingSchool";

const initialDrivingSchoolValues: IDrivingSchool = {
    id: '',
    name: '',
    description: ''
}

const StudentsIndex = () => {
    const { teacherId } = useParams() as IRouteId;
    const [students, setStudents] = useState([] as IStudent[]);
    const [drivingSchool, setDrivingSchool] = useState(initialDrivingSchoolValues);
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });
    const [alertMessage, setAlertMessage] = useState('');
    const [filterData, setSetFilterData] = useState({ userName: '', fullName: '' });
    const [inviteUsername, setInviteUsername] = useState('');
    const history = useHistory();
    const appState = useContext(AppContext);
    const resource = appState.langResources.frontEnd.containers.teacher.students.studentsIndex;

    const filterClicked = async (e: Event) => {
        e.preventDefault();
        if (filterData.userName === '') {
            filterData.userName = " ";
        }
        if (filterData.fullName === '') {
            filterData.fullName = " ";
        };
        let studentsResult = await DrivingSchoolService.GetStudents(drivingSchool.id, filterData.fullName, filterData.userName, appState.currentLanguage.name, appState.jwt!);
        if (studentsResult.ok && studentsResult.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStudents(studentsResult.data!);
        } else {
            setAlertMessage(studentsResult.messages![0]);
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: studentsResult.statusCode });
        }
    }

    const inviteClicked = async (e: Event) => {
        e.preventDefault();

        let studentsResult = await DrivingSchoolService.InviteUser(
            {
                schoolId: drivingSchool.id,
                username: inviteUsername
            }
            , appState.currentLanguage.name, appState.jwt!);
        if (studentsResult.ok && studentsResult.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            filterClicked(e);
        } else {
            setAlertMessage(studentsResult.messages![0]);
        }
    }

    const studentClicked = async (e: Event, studentId: string) => {
        e.preventDefault();
        history.push("/teacher/" + teacherId + "/student/" + studentId);

    }

    const LoadContractCourses = async () => {
        if (appState.jwt === null) return;
        let drivingschoolResult = await DrivingSchoolService.GetContractDrivingSchool(teacherId, appState.currentLanguage.name, appState.jwt!);
        if (drivingschoolResult.ok && drivingschoolResult.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setDrivingSchool(drivingschoolResult.data!);
        } else {
            setAlertMessage(drivingschoolResult.messages![0]);
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: drivingschoolResult.statusCode });
            return;
        }
        let studentsResult = await DrivingSchoolService.GetStudents(drivingschoolResult.data.id, " ", " ", appState.currentLanguage.name, appState.jwt!);
        if (studentsResult.ok && studentsResult.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStudents(studentsResult.data!);
        } else {
            setAlertMessage(studentsResult.messages![0]);
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: studentsResult.statusCode });
        }
    }

    useEffect(() => {
        LoadContractCourses();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    return (
        <>
            <h1 className="text-center">{resource.schoolStudents}</h1>
            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />
            <Loader {...pageStatus} />
            <br /><br />

            <div className="row justify-content">
                <form className="col-2" onSubmit={(e) => e.preventDefault}>
                    <section>
                        <hr />
                        <div className="form-group text-center">
                            <label htmlFor="Input_inviteUsername">{resource.username}</label>
                            <input
                                value={inviteUsername}
                                onChange={e => setInviteUsername(e.target.value)}
                                className="form-control"
                                type="text"
                                id="Input_inviteUsername"
                                name="Input.InviteUsername"
                            />
                        </div>
                        <div className="form-group">
                            <button onClick={(e) => inviteClicked(e.nativeEvent)} type="submit" className="btn btn-primary">{resource.invite}</button>
                        </div>
                    </section>
                </form>
                <div className="col-7"></div>
                <form className="col-3 float-right" onSubmit={(e) => e.preventDefault}>
                    <section>
                        <hr />
                        <div className="form-group text-center">
                            <label htmlFor="Input_username">{resource.username}</label>
                            <input
                                value={filterData.userName}
                                onChange={e => setSetFilterData({ ...filterData, userName: e.target.value })}
                                className="form-control"
                                type="text"
                                id="Input_Username"
                                name="Input.Username"
                            />
                        </div>
                        <div className="form-group text-center">
                            <label htmlFor="Input_Password">{resource.name}</label>
                            <input
                                value={filterData.fullName}
                                onChange={e => setSetFilterData({ ...filterData, fullName: e.target.value })}
                                className="form-control"
                                type="fullName"
                                id="Input_FullName"
                                name="Input.FullName"
                            />
                        </div>
                        <div className="form-group">
                            <button onClick={(e) => filterClicked(e.nativeEvent)} type="submit" className="btn float-right btn-primary">Filter</button>
                        </div>
                    </section>
                </form>
            </div>
            {students.map(student =>
                <div key={student.id} onClick={(e) => studentClicked(e.nativeEvent, student.id)} className="btn inline-table-overview-students table-entry">
                    <h3>{student.fullName}</h3>
                    <br></br>
                    <table>
                        <thead>
                            <tr>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{resource.username}</td>
                                <td>{student.username}</td>
                            </tr>
                            <tr>
                                <td>{resource.email}</td>
                                <td>{student.email}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            )}
        </>
    );
}

export { StudentsIndex };