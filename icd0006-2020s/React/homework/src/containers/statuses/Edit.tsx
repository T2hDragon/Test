import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { IStatus } from "../../dto/IStatus";
import Loader from "../../components/Loader";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const initialStatusValues: IStatus = {
    id: '',
    name: '',
    nameId: ''
};

const Edit = () => {
    const appState = useContext(AppContext);
    const { id } = useParams() as IRouteId;
    const [statusData, setStatusData] = useState(initialStatusValues);
    const [pass, setPass] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.get<IStatus>('/statuses', id, appState.jwt!);

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setStatusData(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData();// eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const UpdateClicked = async (e: Event) => {
        e.preventDefault();
        if (statusData.name === '') {
            setAlertMessage('Empty name!');
        };
        setAlertMessage('');
        let response = await BaseService.update<IStatus>('/statuses', id, statusData, appState.jwt!);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            setPass(true);
        }
    }

    const DeleteClicked = async (e: Event) => {
        e.preventDefault();
        setAlertMessage('');
        let response = await BaseService.delete<IStatus>('/statuses', id, appState.jwt!);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            setPass(true);
        }

    }

    return (
        <>
            { pass ? <Redirect to="/statuses" /> : null}
            <h1>Edit</h1>
            <form onSubmit={(e) => e.preventDefault()}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <Loader {...pageStatus} />
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />

                            <div className="form-group">
                                <label htmlFor="inputStatusName">Status</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    id="inputStatusName"
                                    name="inputStatusName"
                                    value={statusData.name} onChange={(e) => setStatusData({ ...statusData, name: e.target.value })}
                                />
                            </div>
                            <div className="form-group">
                                <button onClick={(e) => UpdateClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Update</button>
                                <button onClick={(e) => DeleteClicked(e.nativeEvent)} type="submit" className="btn btn-danger float-right">Delete</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
            <Link to={'/statuses'}>Back to list</Link>
        </>
    );
}

export { Edit }
