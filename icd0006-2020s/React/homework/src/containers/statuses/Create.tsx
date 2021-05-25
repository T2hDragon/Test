import { useState, useContext } from "react";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import { IStatus } from "../../dto/IStatus";
import { BaseService } from "../../services/base-service";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";

const initialStatusValues: IStatus = {
    id: '',
    name: '',
    nameId: ''
};

const Create = () => {
    const appState = useContext(AppContext);
    const [statusData, setStatusData] = useState(initialStatusValues);
    const [pass, setPass] = useState(false);
    const [alertMessage, setAlertMessage] = useState('');


    const UpdateClicked = async (e: Event) => {
        e.preventDefault();
        if (statusData.name === '') {
            setAlertMessage('Empty name!');
        };
        setAlertMessage('');
        let response = await BaseService.create<IStatus>('/statuses', { name: statusData.name }, appState.jwt!);
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
            <h1>Create</h1>
            <form onSubmit={(e) => e.preventDefault()}>
                <div className="row">
                    <div className="col-md-6">
                        <section>
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
                                <button onClick={(e) => UpdateClicked(e.nativeEvent)} type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </section>
                    </div>
                </div>
            </form>
            <Link to={'/statuses'}>Back to list</Link>
        </>
    );
}

export { Create }
