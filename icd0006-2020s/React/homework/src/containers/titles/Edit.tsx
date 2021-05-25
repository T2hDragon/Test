import { useParams } from "react-router-dom";
import { IRouteId } from "../../types/IRouteId";
import { useEffect, useState, useContext } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { ITitle } from "../../dto/ITitle";
import Loader from "../../components/Loader";
import { BaseService } from "../../services/base-service";
import { EPageStatus } from "../../types/EPageStatus";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const initialTitleValues: ITitle = {
    id: '',
    name: '',
    nameId: ''
};

const Edit = () => {
    const appState = useContext(AppContext);
    const { id } = useParams() as IRouteId;
    const [titleData, setTitleData] = useState(initialTitleValues);
    const [pass, setPass] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');
    const [pageStatus, setPageStatus] = useState({ pageStatus: EPageStatus.Loading, statusCode: -1 });

    const loadData = async () => {
        let result = await BaseService.get<ITitle>('/titles', id, appState.jwt!);

        if (result.ok && result.data) {
            setPageStatus({ pageStatus: EPageStatus.OK, statusCode: 0 });
            setTitleData(result.data);
        } else {
            setPageStatus({ pageStatus: EPageStatus.Error, statusCode: result.statusCode });
        }
    }

    useEffect(() => {
        loadData();// eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const UpdateClicked = async (e: Event) => {
        e.preventDefault();
        if (titleData.name === '') {
            setAlertMessage('Empty name!');
        };
        setAlertMessage('');
        let response = await BaseService.update<ITitle>('/titles', id, titleData, appState.jwt!);
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
        let response = await BaseService.delete<ITitle>('/titles', id, appState.jwt!);
        if (!response.ok) {
            setAlertMessage(response.messages![0]);
        } else {
            setAlertMessage('');
            setPass(true);
        }

    }

    return (
        <>
            { pass ? <Redirect to="/titles" /> : null}
            <h1>Edit</h1>
            <form onSubmit={(e) => e.preventDefault()}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
                            <Loader {...pageStatus} />
                            <hr />
                            <Alert show={alertMessage !== ''} message={alertMessage} alertClass={EAlertClass.Danger} />

                            <div className="form-group">
                                <label htmlFor="inputTitleName">Title</label>
                                <input
                                    className="form-control"
                                    type="text"
                                    id="inputTitleName"
                                    name="inputTitleName"
                                    value={titleData.name} onChange={(e) => setTitleData({ ...titleData, name: e.target.value })}
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
            <Link to={'/titles'}>Back to list</Link>
        </>
    );
}

export { Edit }
